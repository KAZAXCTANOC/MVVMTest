using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMTest.Structur.Commands
{
    internal class CloseApp : BaseCommand
    {
        public override bool CanExecute(object parameter)
        {
            return true;
        }

        public override void Execute(object parameter)
        {
            App.Current.Shutdown();
        }
    }
}
