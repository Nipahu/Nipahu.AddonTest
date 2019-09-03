using System.Windows;
using Nipahu.AddonTest.Core.Mvvm;
using Nipahu.AddonTest.Core.ViewModels;
using Nipahu.AddonTest.Core.Views;

namespace Nipahu.AddonTest.Demo
{
    public partial class App
    {
        private void App_OnStartup(object sender, StartupEventArgs e)
        {
            var viewProvider = new ViewProvider();
            Core.Startup.RegisterViews(viewProvider);

            viewProvider.Show(new MainViewModel());
        }
    }
}