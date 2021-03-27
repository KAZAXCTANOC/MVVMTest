using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MVVMTest.Structur.Commands
{
    internal abstract class BaseCommand : ICommand
    {
        //Событие генерируеться в тот момент, когда CanExecute начинает возращять другое значение(смена состояния)
        public event EventHandler CanExecuteChanged 
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
            //WPF автоматически генеририует это свойство когда происходит выполнение команд 
        }

        //Возразщает либо истину либо ложь в зависимости можно ли выполнить команду и элемент к которому он привязан отлючается 
        public abstract bool CanExecute(object parameter);
        // То что должно быть выполнено командой, ее основная функция
        public abstract void Execute(object parameter);
    }
}
