using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMTest.Structur.Commands
{
    //"Рабочая лошадка" чтобы это не значило 9)
    internal class LambdaCommand : BaseCommand
    {
        private Action<object> _Execute { get; }
        private Func<object, bool> _CanExecute { get; }

        public LambdaCommand(Action<object> Execute, Func<object, bool> CanExecute = null)
        {
            _Execute = Execute ?? throw new ArgumentException(nameof(Execute)); ;
            _CanExecute = CanExecute;
        }



        public override bool CanExecute(object parameter) => _CanExecute?.Invoke(parameter) ?? true;

        public override void Execute(object parameter) => _Execute(parameter);
    }
}
