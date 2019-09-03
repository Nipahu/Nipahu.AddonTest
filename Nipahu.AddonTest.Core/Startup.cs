using Nipahu.AddonTest.Core.Mvvm;
using Nipahu.AddonTest.Core.ViewModels;
using Nipahu.AddonTest.Core.Views;

namespace Nipahu.AddonTest.Core
{
    public static class Startup
    {
        public static void RegisterViews(ViewProvider viewProvider)
        {
            viewProvider.Register<MainViewModel, MainWindow>();
        }
    }
}