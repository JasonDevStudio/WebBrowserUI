using Xilium.CefGlue;

namespace CefGlue.Lib.Framework
{
    public class FormiumApp: CefApp
    {
        private readonly CefRenderProcessHandler _renderProcessHandler;

        private readonly CefBrowserProcessHandler _browserProcessHandler;

        public FormiumApp()
        {
        }
 
        protected override CefRenderProcessHandler GetRenderProcessHandler()
        {
            return _renderProcessHandler;
        }

        protected override CefBrowserProcessHandler GetBrowserProcessHandler()
        {
            return _browserProcessHandler;
        }
 
        protected override void OnBeforeCommandLineProcessing(string processType, CefCommandLine commandLine)
        {
        }
    }
}