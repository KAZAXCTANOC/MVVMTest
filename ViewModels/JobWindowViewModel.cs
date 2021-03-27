using MVVMTest.Date;
using MVVMTest.Structur.Commands;
using MVVMTest.ViewModels;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Windows;
using System.Windows.Input;

//private void On???Executed(object p)
//{
//}
//private bool Can???Execute(object p) => true;

namespace MVVMTest.ViewModels 
{
    class JobWindowViewModel : BaseViewModel
    {
        protected string sign { get; set; }
        public ICommand PlusMinusCount { get; }
        protected int count { get; set; }
        public ICommand Plus100Command { get; }
        public ICommand Plus10Command { get; }
        public ICommand Plus1Command { get; }
        public ICommand Minus100Command { get; }
        public ICommand Minus10Command { get; }
        public ICommand Minus1Command { get; }

        protected Product selecterdProduct;
        SkladEntities sklad = new SkladEntities();
        public ObservableCollection<Product> allProducts { get; set;}

        public ObservableCollection<Product> _AllProducts()
        {
            ObservableCollection<Product> AllProductss = new ObservableCollection<Product>();
            List<Product> Allp = sklad.Products.ToList();
            foreach (Product product in Allp)
            {
                AllProductss.Add(product);
            }
            return AllProductss;
        }

        public ObservableCollection<Product> AllProducts
        {
            get
            {
                return allProducts;
            }
            set
            {
                allProducts = _AllProducts();
                OnPropertyChanged("allProducts");
            }
        }

        #region Конпки управлеия кол-вом 

        private void OnPlusMinusExecuted(object p)
        {
            var Product = (p as Product);
            switch (sign)
            {
                case "+":
                    Product.Qty += count;
                    sklad.SaveChanges();
                    AllProducts = _AllProducts();
                    break;
                case "-":
                    Product.Qty -= count;
                    if (Product.Qty > 0)
                    {
                        sklad.SaveChanges();
                        AllProducts = _AllProducts();
                    }
                    else
                    {
                        MessageBox.Show("Невозможно провести операцию");
                        Product.Qty += count;
                    }
                    break;
                    default:
                    MessageBox.Show("Невозможно провести операцию");
                    break;
            }

        }

        public string Sign
        {
            get { return sign; }
            set 
            { 
                sign = value.Substring(value.Length - 1, 1); 
                OnPropertyChanged();
            }
        }

        public int Count
        {

            get { return count; }
            set
            {
                count = value;
                OnPropertyChanged();
            }
        }


        private void OnPlus100CommandExecuted(object p)
        {
            var Product = (p as Product);
            var MyProduct = sklad.Products.Where(P => P.Id == Product.Id).FirstOrDefault();
            Product.Qty += 100;
            sklad.SaveChanges();
            AllProducts = _AllProducts();
        }

        private void OnPlus10CommandExecuted(object p)
        {
            var Product = (p as Product);
            var MyProduct = sklad.Products.Where(P => P.Id == Product.Id).FirstOrDefault();
            Product.Qty += 10;
            sklad.SaveChanges();
            AllProducts = _AllProducts();
        }

        private void OnPlus1CommandExecuted(object p)
        {
            var Product = (p as Product);
            var MyProduct = sklad.Products.Where(P => P.Id == Product.Id).FirstOrDefault();
            Product.Qty += 1;
            sklad.SaveChanges();
            AllProducts = _AllProducts();
        }

        private void OnMinus100CommandExecuted(object p)
        {
            var Product = (p as Product);
            var MyProduct = sklad.Products.Where(P => P.Id == Product.Id).FirstOrDefault();
            Product.Qty -= 100;
            if (Product.Qty > 0)
            {
                sklad.SaveChanges();
                AllProducts = _AllProducts();
            }
            else MessageBox.Show("Значение не может быть ментшу нуля");
            
        }

        private void OnMinus10CommandExecuted(object p)
        {
            var Product = (p as Product);
            var MyProduct = sklad.Products.Where(P => P.Id == Product.Id).FirstOrDefault();
            Product.Qty -= 10;
            if (Product.Qty > 0)
            {
                sklad.SaveChanges();
                AllProducts = _AllProducts();
            }
            else MessageBox.Show("Значение не может быть ментшу нуля");
        }

        private void OnMinus1CommandExecuted(object p)
        {
            var Product = (p as Product);
            var MyProduct = sklad.Products.Where(P => P.Id == Product.Id).FirstOrDefault();
            Product.Qty -= 1;
            if (Product.Qty > 0)
            {
                sklad.SaveChanges();
                AllProducts = _AllProducts();
            }
            else MessageBox.Show("Значение не может быть ментшу нуля");
        }

        private bool CanPlusMinusCommandExecute(object p)
        {
            if (selecterdProduct != null) return true;
            else return false;
        }

        public Product SelecterdProduct
        {
            get { return selecterdProduct; }
            set 
            {
                selecterdProduct = value;
                OnPropertyChanged();
            }
        }

        
        #endregion

        public JobWindowViewModel()
        {
            AllProducts = _AllProducts();
            PlusMinusCount = new LambdaCommand(OnPlusMinusExecuted, CanPlusMinusCommandExecute);
            Plus100Command = new LambdaCommand(OnPlus100CommandExecuted, CanPlusMinusCommandExecute);
            Plus10Command = new LambdaCommand(OnPlus10CommandExecuted, CanPlusMinusCommandExecute);
            Plus1Command = new LambdaCommand(OnPlus1CommandExecuted, CanPlusMinusCommandExecute);

            Minus100Command = new LambdaCommand(OnMinus100CommandExecuted, CanPlusMinusCommandExecute);
            Minus10Command = new LambdaCommand(OnMinus10CommandExecuted, CanPlusMinusCommandExecute);
            Minus1Command = new LambdaCommand(OnMinus1CommandExecuted, CanPlusMinusCommandExecute);
        }

    }
}
