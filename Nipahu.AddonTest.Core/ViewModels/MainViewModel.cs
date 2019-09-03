using System.Windows;
using System.Windows.Input;
using Nipahu.AddonTest.Core.Mvvm;
using Nipahu.AddonTest.Core.Utils;

namespace Nipahu.AddonTest.Core.ViewModels
{
    public class MainViewModel
    {
        public ICommand HelloCommand { get; }

        public MainViewModel()
        {
            HelloCommand = new Command(Hello, CanHello);
        }

        private bool CanHello(object arg)
        {
            return true;
        }

        private void Hello(object obj)
        {
            MessageBox.Show("Hello!");
        }
    }
}