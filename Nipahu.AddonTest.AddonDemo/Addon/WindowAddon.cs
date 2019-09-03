using System;
using System.Collections.Generic;
using System.Windows;

namespace Nipahu.AddonTest.AddonDemo.Addon
{
    public class WindowAddon : ResourceDictionary
    {
        private Type _windowType;

        public object WindowType
        {
            get => _windowType;
            set
            {
                if (value is Type type)
                    _windowType = type;
                else if (value is string typeName)
                    _windowType = Type.GetType(typeName);
                else
                    throw new ArgumentException("WindowType not set", nameof(WindowType));
            }
        }
    }
}