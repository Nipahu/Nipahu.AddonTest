using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Nipahu.AddonTest.AddonDemo.Addon;
using Nipahu.AddonTest.AddonDemo.Utils;
using Nipahu.AddonTest.Core.Mvvm;
using Nipahu.AddonTest.Core.Utils;

namespace Nipahu.AddonTest.AddonDemo.Mvvm
{
    public class ViewProviderAddon : ViewProvider
    {
        private readonly Dictionary<Type, WindowAddon> _windowAddons = new Dictionary<Type, WindowAddon>();

        public void RegisterAddon(Type target, WindowAddon windowAddon)
        {
            _windowAddons.Add(target, windowAddon);
        }

        protected override void OnWindowLoaded(object sender, RoutedEventArgs e)
        {
            base.OnWindowLoaded(sender, e);

            if (!(sender is Window window))
                return;

            if (!_windowAddons.TryGetValue(window.GetType(), out var windowAddon))
                return;

            foreach (var obj in windowAddon)
            {
                if (!(obj is DictionaryEntry entry))
                    continue;

                if (!(entry.Value is WindowAddonItem windowAddonItem))
                    continue;

                var target = window.FindChild<ItemsControl>(windowAddonItem.Target);
                if(target == null)
                    continue;

                foreach (var element in windowAddonItem)
                {
                    // Create copy, otherwise we get a NotSupportedException
                    // because the MenuItem already has a parent set.
                    // I don't like this solution, but it makes it work
                    // Looking for better solution because of this
                    var copy = DeepCopy.Create(element, window.DataContext);
                    target.Items.Add(copy);
                }
            }
        }
    }
}