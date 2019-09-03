using System;
using System.Collections.Generic;
using System.Windows;

namespace Nipahu.AddonTest.Core.Mvvm
{
    public class ViewProvider
    {
        private readonly Dictionary<Type, Type> _registeredViews = new Dictionary<Type, Type>();

        public void Register<TViewModel, TView>() where TView : Window
        {
            _registeredViews.Add(typeof(TViewModel), typeof(TView));
        }

        public void Show(object viewModel)
        {
            var viewModelType = viewModel.GetType();
            if (!_registeredViews.TryGetValue(viewModelType, out var windowType))
                foreach (var registeredType in _registeredViews.Keys)
                    if (registeredType.IsAssignableFrom(viewModelType))
                        _registeredViews.TryGetValue(registeredType, out windowType);

            if (windowType == null)
                throw new ArgumentException("No window registered for this type.", nameof(viewModel));

            var window = (Window) Activator.CreateInstance(windowType);

            window.DataContext = viewModel;
            window.Loaded += OnWindowLoaded;
            window.Show();
        }

        protected virtual void OnWindowLoaded(object sender, RoutedEventArgs e)
        {
        }
    }
}