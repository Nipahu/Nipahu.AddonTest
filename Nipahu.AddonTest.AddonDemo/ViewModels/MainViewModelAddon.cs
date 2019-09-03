using System.Windows;
using System.Windows.Input;
using Nipahu.AddonTest.Core.Mvvm;
using Nipahu.AddonTest.Core.Utils;
using Nipahu.AddonTest.Core.ViewModels;

namespace Nipahu.AddonTest.AddonDemo.ViewModels
{
    public class MainViewModelAddon : MainViewModel
    {
        public ICommand WorldCommand { get; }
        public ICommand FoobarCommand { get; }

        public MainViewModelAddon()
        {
            WorldCommand = new Command(World, CanWorld);
            FoobarCommand = new Command(Foobar);
        }

        private bool CanWorld(object arg)
        {
            return true;
        }

        private void World(object obj)
        {
            MessageBox.Show("World!");
        }

        private void Foobar(object obj)
        {
            MessageBox.Show("Foobar!");
        }
    }
}