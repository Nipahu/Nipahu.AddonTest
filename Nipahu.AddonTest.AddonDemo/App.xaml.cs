using System;
using System.Collections;
using System.Windows;
using Nipahu.AddonTest.AddonDemo.Addon;
using Nipahu.AddonTest.AddonDemo.Mvvm;
using Nipahu.AddonTest.AddonDemo.ViewModels;

namespace Nipahu.AddonTest.AddonDemo
{
    public partial class App
    {
        private void App_OnStartup(object sender, StartupEventArgs e)
        {
            var viewProvider = new ViewProviderAddon();
            Core.Startup.RegisterViews(viewProvider);
            RegisterAddons(viewProvider);

            viewProvider.Show(new MainViewModelAddon());
        }

        private void RegisterAddons(ViewProviderAddon viewProvider)
        {
            foreach (object resource in Current.Resources)
            {
                if (!(resource is DictionaryEntry entry))
                    continue;

                if (!(entry.Value is WindowAddon windowAddon))
                    continue;

                if (!(windowAddon.WindowType is Type windowType))
                    continue;

                viewProvider.RegisterAddon(windowType, windowAddon);
            }
        }
    }
}