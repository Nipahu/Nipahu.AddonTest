using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace Nipahu.AddonTest.AddonDemo.Utils
{
    public static class DeepCopy
    {
        public static object Create(object obj, object dataContext)
        {
            if (!(obj is UIElement uiElement))
                throw new NotSupportedException($"Currently the type {obj.GetType()} is not supported for add-ons.");

            var copy = (UIElement) Activator.CreateInstance(uiElement.GetType());

            var enumerator = uiElement.GetLocalValueEnumerator();
            while (enumerator.MoveNext())
            {
                var entry = enumerator.Current;
                if (entry.Property.ReadOnly)
                    continue;

                if (entry.Property.Name == nameof(ICommandSource.Command))
                {
                    if (copy is FrameworkElement frameworkElement && entry.Value is BindingExpression expression)
                    {
                        var clone = CloneBinding(expression.ParentBinding, dataContext);
                        frameworkElement.SetBinding(entry.Property, clone);
                    }
                }
                else
                {
                    copy.SetValue(entry.Property, entry.Value);
                }
            }

            if (uiElement is ItemsControl itemsControl)
            {
                var itemControlCopy = (ItemsControl) copy;
                foreach (var item in itemsControl.Items)
                {
                    var itemCopy = Create(item, dataContext);
                    itemControlCopy.Items.Add(itemCopy);
                }
            }

            return copy;
        }

        private static BindingBase CloneBinding(BindingBase bindingBase, object source)
        {
            var binding = bindingBase as Binding;
            if (binding == null)
                throw new NotSupportedException("Failed to clone binding");

            var result = new Binding
            {
                Source = source,
                AsyncState = binding.AsyncState,
                BindingGroupName = binding.BindingGroupName,
                BindsDirectlyToSource = binding.BindsDirectlyToSource,
                Converter = binding.Converter,
                ConverterCulture = binding.ConverterCulture,
                ConverterParameter = binding.ConverterParameter,
                FallbackValue = binding.FallbackValue,
                IsAsync = binding.IsAsync,
                Mode = binding.Mode,
                NotifyOnSourceUpdated = binding.NotifyOnSourceUpdated,
                NotifyOnTargetUpdated = binding.NotifyOnTargetUpdated,
                NotifyOnValidationError = binding.NotifyOnValidationError,
                Path = binding.Path,
                StringFormat = binding.StringFormat,
                TargetNullValue = binding.TargetNullValue,
                UpdateSourceExceptionFilter = binding.UpdateSourceExceptionFilter,
                UpdateSourceTrigger = binding.UpdateSourceTrigger,
                ValidatesOnDataErrors = binding.ValidatesOnDataErrors,
                ValidatesOnExceptions = binding.ValidatesOnExceptions,
                XPath = binding.XPath
            };

            foreach (var validationRule in binding.ValidationRules)
                result.ValidationRules.Add(validationRule);

            return result;
        }
    }
}