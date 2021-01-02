using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http;
using System.Reflection;
using CefGlue.Lib.Browser;
using Type = System.Type;

namespace CefGlue.Lib.Framework
{
    /// <summary>
    /// 数据模型驱动
    /// </summary>
    public class DataModelProvider
    {
        /// <summary>
        /// 数据模型后缀
        /// </summary>
        public const string MODEL_SUFFIX = "Model";
 
        /// <summary>
        /// 将数据集导入数据模型
        /// </summary>
        /// <param name="assembly">需要导入数据模型的程序集</param>
        /// <exception cref="ArgumentNullException">参数为空错误</exception>
        /// <returns>List{RouteConfig}</returns> 
        public List<RouteConfig> ImportDataModelAssembly(Assembly assembly)
        {
            var routes = new List<RouteConfig>();
            
            if (assembly == null)
                throw new ArgumentNullException($"{nameof(assembly)}");

            var models = assembly.GetTypes().Where(x => x.IsSubclassOf(typeof(ViewModelBase)));
            foreach (var model in models) 
                routes.AddRange(ImportDataModel(model));

            return routes;
        }

        /// <summary>
        /// 导入数据模型
        /// </summary>
        /// <param name="type">Type</param>
        /// <exception cref="TypeLoadException">Type 加载 错误</exception>
        /// <returns>List{RouteConfig}</returns> 
        private List<RouteConfig> ImportDataModel(Type type)
        {
            if (!type.IsSubclassOf(typeof(ViewModelBase)))
                throw new TypeLoadException();

            var instance = (ViewModelBase) Activator.CreateInstance(type);
            return ImportDataModel(instance);
        }

        /// <summary>
        /// 导入数据模型
        /// </summary>
        /// <param name="instance">数据模型实例</param>
        /// <typeparam name="T">ViewModelBase</typeparam>
        /// <returns>List{RouteConfig}</returns>
        /// <exception cref="ArgumentNullException">参数为空错误</exception> 
        private List<RouteConfig> ImportDataModel<T>(T instance) where T : ViewModelBase
        {
            if (instance == null)
                throw new ArgumentNullException();

            var routes = new List<RouteConfig>();
            var routePaths = new List<string>();
            var type = instance.GetType();
            if (!type.Name.EndsWith(MODEL_SUFFIX)) return routes;

            var modelName = type.Name.Substring(0, type.Name.LastIndexOf(MODEL_SUFFIX));
            var routeAttributes = type.GetCustomAttributes(typeof(RouteAttribute), false);
            if (routeAttributes != null && routeAttributes.Any())
            {
                foreach (RouteAttribute dataRouteAttr in routeAttributes)
                {
                    modelName = dataRouteAttr.RoutePath.Replace(@"\", @"/").Trim('/');
                    routePaths.Add(modelName);
                }
            }
            else
            {
                routePaths.Add(modelName);
            }

            routePaths = routePaths.Distinct().ToList();
            var methods = type.GetMethods()
                .Where(x => x.ReturnType.IsAssignableFrom(typeof(WebResponse)));

            foreach (var methodInfo in methods)
            {
                var parameterExpressions = methodInfo.GetParameters()
                    .Select(x => Expression.Parameter(x.ParameterType, x.Name)).ToArray();
                var methodCallExpression =
                    Expression.Call(Expression.Constant(instance), methodInfo, parameterExpressions);
                var func =
                    Expression.Lambda(methodCallExpression, parameterExpressions).Compile() as
                        Func<WebRequest, WebResponse>;
                if (func == null)
                    continue;

                var routeMethodAttributes = methodInfo.GetCustomAttributes(typeof(RouteMethodAttribute), true);
                if (routeMethodAttributes != null && routeMethodAttributes.Any())
                {
                    foreach (RouteMethodAttribute routeMehtodAttr in routeMethodAttributes)
                    {
                        var method = routeMehtodAttr?.RequestMethod;
                        var methodPath = string.IsNullOrWhiteSpace(routeMehtodAttr?.RoutePath)
                            ? methodInfo.Name
                            : routeMehtodAttr.RoutePath.Replace(@"\", @"/").TrimEnd('/');

                        if (methodPath.StartsWith("~") || methodPath.StartsWith("/"))
                            methodPath = methodPath.Length > 1 ? methodPath.Substring(1).Trim('/') : string.Empty;

                        foreach (var routePath in routePaths) 
                            CreateRouteConfig(routes,routePath, methodPath, method, func); 
                    }
                }
                else
                { 
                    foreach (var routePath in routePaths)
                        CreateRouteConfig(routes,routePath, methodInfo.Name, HttpMethod.Trace, func); 
                }
            }

            return routes;
        }

        /// <summary>
        /// 创建 RouteConfig
        /// </summary>
        /// <param name="routes">路由配置集合</param>
        /// <param name="routePath">路由路径</param>
        /// <param name="methodPath">函数路径</param>
        /// <param name="method">请求方式 GET POST PUT DELETE</param>
        /// <param name="handler">委托事件</param>
        /// <returns>RouteConfig</returns>
        private RouteConfig CreateRouteConfig(List<RouteConfig> routes, string routePath, string methodPath, HttpMethod method,Func<WebRequest, WebResponse> handler)
        { 
            var path = $"{routePath}/{methodPath}".Trim('/').ToLower();
            if (routes.Any(x => x.RoutePath == path && x.RouteMethod == method)) 
                return default;

            var routeConfig = new RouteConfig(method, path, handler);
            routes.Add(routeConfig);
            return routeConfig;
        }
    }
}