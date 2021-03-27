using MVVMTest.Date;
using MVVMTest.Structur.Commands;
using MVVMTest.ViewModels;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace MVVMTest.ViewModels
{
    internal class RegisterFormViewModel : BaseViewModel
    {
        public ICommand RegisterUser { get; }
        public ICommand GoToMain { get; }
        SkladEntities sklad = new SkladEntities();

        #region Регистрация пользователя

        User NewUser = new User();
        public string UserName
        {
            get { return NewUser.Name; }
            set
            {
                NewUser.Name = value;
                OnPropertyChanged("UserName");
            }
        }
        public string UserSurname 
        {
            get { return NewUser.Surname; }
            set 
            {
                NewUser.Surname = value;
                OnPropertyChanged("UserSurname");
            }
        }
        public string UserLogin
        {
            get { return NewUser.Login; }
            set 
            {
                NewUser.Login = value;
                OnPropertyChanged("UserLogin");
            }
        }
        public string UserPassword 
        {
            get { return NewUser.Password; }
            set 
            {
                NewUser.Password = value;
                OnPropertyChanged("UserPassword");
            }
        }

        private void OnRegisterUserExecuted(object p)
        {
            if (NewUser != null)
            {
                var results = new List<ValidationResult>();
                var context = new ValidationContext(NewUser);
                if (!Validator.TryValidateObject(NewUser, context, results, true))
                {
                    string er = "";
                    foreach (var error in results)
                    {
                        er += error.ToString();
                    }
                    MessageBox.Show(er + "\n");
                }
                else 
                {
                    sklad.Users.Add(NewUser);
                    sklad.SaveChanges();
                }
            }
        }

        private bool CanRegisterUserExecute(object p) => true;
        #endregion

        #region Кнопка перехода к авторизации

        private void OnGoToMainExecuted(object p)
        {
            MainWindow main = new MainWindow();
            main.Show();
            (p as Window).Close();
        }
        private bool CanGoToMainExecute(object p) => true;


        #endregion

        public RegisterFormViewModel()
        {
            RegisterUser = new LambdaCommand(OnRegisterUserExecuted, CanRegisterUserExecute);
            GoToMain = new LambdaCommand(OnGoToMainExecuted, CanGoToMainExecute);
        }
    }
}
