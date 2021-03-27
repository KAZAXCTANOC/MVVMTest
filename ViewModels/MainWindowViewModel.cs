using MVVMTest.Date;
using MVVMTest.Structur.Commands;
using MVVMTest.ViewModels;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace MVVMTest.Views
{
    internal class MainWindowViewModel : BaseViewModel
    {
        #region CMD
        //Сама команда
        public ICommand CommandMessegeForUser { get; }
        public ICommand AutorizeUser { get; }
        public ICommand RegisterButton { get; }
        private User _MyUser = new User();
        internal SkladEntities sklad = new SkladEntities();

        #region Кнопка авторизации

        //Методы, который выполняется в момент выполнения команды 
        private void OnCommandMessegeForUserExecuted(object p)
        {
            MessageBox.Show("YES");
        }

        private bool CanCommandMessegeForUserExecute(object p) => true;

        private void OnAutorizeUserExecuted(object p)
        {
            List<User> AllUsers = sklad.Users.ToList();
            if(_MyUser != null)
            {
                foreach (User user in AllUsers) 
                {
                    if(user.Login == _MyUser.Login && user.Password == _MyUser.Password)
                    {
                        JobWindow window = new JobWindow();
                        window.Show();
                    }
                }
            }
        }
        private bool CanAutorizeUserExecute(object p) => true;

        public string MyUserLogin
        {
            get { return _MyUser.Login; }
            set 
            {
                _MyUser.Login = value;
                OnPropertyChanged("MyUserLogin");
            }
        }

        public string MyUserPassword
        {
            get { return _MyUser.Password; }
            set
            {
                _MyUser.Password = value;
                OnPropertyChanged("MyUserPassword");
            }
        }
        #endregion

        #region Кнопка перехода к регистрации
        private bool CanRegisterButtonExecute(object p) => true;

        private void OnRegisterButtonExecuted(object p)
        {
            RegisterForm registerForm = new RegisterForm();
            registerForm.Show();
            (p as Window).Close();
        }
        #endregion

        #endregion

        public MainWindowViewModel()
        {
            CommandMessegeForUser = new LambdaCommand(OnCommandMessegeForUserExecuted, CanCommandMessegeForUserExecute);
            AutorizeUser = new LambdaCommand(OnAutorizeUserExecuted, CanAutorizeUserExecute);
            RegisterButton = new LambdaCommand(OnRegisterButtonExecuted, CanRegisterButtonExecute);
        }
    }
}
