using MVVMTest.Date;
using MVVMTest.Structur.Commands;
using MVVMTest.ViewModels;
using MVVMTest.Views;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Excel = Microsoft.Office.Interop.Excel;

//private void On???Executed(object p)
//{
//}
//private bool Can???Execute(object p) => true;

namespace MVVMTest.ViewModels 
{
    class JobWindowViewModel : BaseViewModel
    {
        public ICommand GoToExcel { get; }
        public ICommand EditCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand UpdateCommand { get; }
        public ICommand NewProductCommand { get; }
        public ICommand PlusMinusCount { get; }
        public ICommand Plus100Command { get; }
        public ICommand Plus10Command { get; }
        public ICommand Plus1Command { get; }
        public ICommand Minus100Command { get; }
        public ICommand Minus10Command { get; }
        public ICommand Minus1Command { get; }

        protected int count { get; set; }
        protected string sign { get; set; }

        #region Таблица в excel
        private void OnGoToExcelExecuted(object p)
        {
            SkladEntities sklad = new SkladEntities();

            var excelApp = new Excel.Application();
            excelApp.Workbooks.Add();
            Excel.Worksheet worksheet = excelApp.ActiveSheet;

            Product[] products = sklad.Products.ToArray();
            worksheet.Cells[1][1] = "№";
            worksheet.Cells[2][1] = "Описание";
            worksheet.Cells[3][1] = "Кол-во";
            worksheet.Cells[4][1] = "Дата доставки";
            worksheet.Cells[5][1] = "Срок хранения";
            worksheet.Cells[6][1] = "Марка";
            worksheet.Cells[7][1] = "Тип продукта";
            worksheet.Cells[8][1] = "Цена";
            worksheet.Cells[9][1] = "Ед.из";
            worksheet.Cells[10][1] = "Наименовние";

            for (int x = 2; x < products.Length + 2; x++)
            {
                worksheet.Cells[1][x] = products[x - 2].Id;
                worksheet.Cells[2][x] = products[x - 2].Descriotion;
                worksheet.Cells[3][x] = products[x - 2].Qty;
                worksheet.Cells[4][x] = products[x - 2].DateDelivery;
                worksheet.Cells[5][x] = products[x - 2].DateExpiration;
                worksheet.Cells[6][x] = products[x - 2].MarksProduct;
                worksheet.Cells[7][x] = products[x - 2].TypeProduct.TypeName;
                worksheet.Cells[8][x] = products[x - 2].Cost;
                worksheet.Cells[9][x] = products[x - 2].Unit.UnitsName;
                worksheet.Cells[10][x] = products[x - 2].Name;
            }

            excelApp.Visible = true;
        }

        private bool CanGoToExcelExecuted(object p) => true;
        #endregion

        #region Конпка изменения продукта 
        private void OnEditCommandExecuted(object p)
        {
            EditProductWindow newProduct = new EditProductWindow();
            newProduct.Show();
        }
        #endregion

        #region Кнопка удаления продукта 
        private void OnDeleteCommandExecuted(object p)
        {
            sklad.Products.Remove(selecterdProduct);
            sklad.SaveChanges();
            OnPropertyChanged();
            AllProducts = _AllProducts();
        }
        #endregion

        #region Кнопка довавления нового продукта
        private void OnNewProductCommandExecuted(object p)
        {
            NewProduct newProduct = new NewProduct();
            newProduct.Show();
        }

        private bool CanNewProductCommandExecuted(object p) => true;
        #endregion

        #region Кнопка обновления
        private void OnUpdateExecuted(object p)
        {
            AllProducts = null;
            AllProducts = _AllProducts();
        }

        private bool CanUpdateExecuted(object p) => true;

        #endregion

        #region Список продуктов
        public Product selecterdProduct;
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

        #endregion

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
            else MessageBox.Show("Значение не может быть меньше нуля");
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

            GoToExcel = new LambdaCommand(OnGoToExcelExecuted, CanGoToExcelExecuted);

            EditCommand = new LambdaCommand(OnEditCommandExecuted, CanPlusMinusCommandExecute);

            DeleteCommand = new LambdaCommand(OnDeleteCommandExecuted, CanPlusMinusCommandExecute);

            UpdateCommand = new LambdaCommand(OnUpdateExecuted, CanUpdateExecuted);

            NewProductCommand = new LambdaCommand(OnNewProductCommandExecuted, CanNewProductCommandExecuted);

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
