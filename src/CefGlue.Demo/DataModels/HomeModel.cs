using CefGlue.Lib;
using CefGlue.Lib.Browser;
using CefGlue.Lib.Framework;

namespace CefGlue.Demo.DataModels
{
    [Route("home")]
    public class HomeModel : ViewModelBase
    {
        [Get("get")]
        public WebResponse Get(WebRequest request)
        {
            var data = new {Name = "avbc", Code = "200"};
            return Json(data);
        }
        
        [Post("post")]
        public WebResponse Post(WebRequest request)
        {
            var formData = request.JsonData;
            var data = new {Name = "post_abcd", Code = "200"};
            return Json(formData);
        }
    }
}