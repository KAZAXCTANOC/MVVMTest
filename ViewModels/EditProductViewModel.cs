using MVVMTest.Date;
using MVVMTest.Structur.Commands;
using MVVMTest.ViewModels;
using MVVMTest.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace MVVMTest.ViewModels
{
    class EditProductViewModel : BaseViewModel
    {
        public ICommand CancelCommand { get; }
        public ICommand SaveChangesCommand { get; }
        private SkladEntities sklad = new SkladEntities();
        private Product product = new Product();

        #region Кнопка отмены
        private void OnCancelCommandExecuted(object p)
        {
            (p as Window).Close();
        }

        private bool CanCancelCommandExecuted(object p) => true;
        #endregion

        #region Кнопка сохранения изменения
        private void OnSaveChangesCommandExecuted(object p)
        {
            var Product = sklad.Products.Where(P => P.Id == product.Id).FirstOrDefault();
            Product.Name = product.Name;
            Product.Cost = product.Cost;
            Product.Qty = product.Qty;
            Product.DateDelivery = product.DateDelivery;
            Product.DateExpiration = product.DateExpiration;
            Product.Units = product.Units;
            Product.Type = product.Type;
            Product.MarksProduct = product.MarksProduct;
            Product.Descriotion = product.Descriotion;
            sklad.SaveChanges();
        }

        private bool CanSaveChangesCommandExecuted(object p) => true;
        #endregion

        #region Данные для comdobox'сов

        #region Еденицы
        public CollectionView UnitsEntries { get; }
        private Unit _unitEntry;
        #endregion

        #region Типы
        public CollectionView TypenEtries { get; }
        public TypeProduct _typeEntry;
        #endregion

        #endregion

        #region Геттеры и сетрреры продукта

        public string PoductName
        {
            get { return product.Name; }
            set
            {
                product.Name = value;
                OnPropertyChanged();
            }
        }

        public decimal? PoductCost
        {
            get { return product.Cost; }
            set
            {
                product.Cost = value;
                OnPropertyChanged();
            }
        }

        public decimal? ProductQty
        {
            get { return product.Qty; }
            set
            {
                product.Qty = value;
                OnPropertyChanged();
            }
        }

        public DateTime? ProductDateDelivery
        {
            get { return product.DateDelivery; }
            set
            {
                product.DateDelivery = value;
                OnPropertyChanged();
            }
        }

        public int? ProductDateExpiration
        {
            get { return product.DateExpiration; }
            set
            {
                product.DateExpiration = value;
                OnPropertyChanged();
            }
        }

        public Unit UnitEntry
        {
            get { return _unitEntry; }
            set
            {
                _unitEntry = value;
                product.Units = _unitEntry.Id;
                OnPropertyChanged();
            }
        }

        public TypeProduct TypeEntry
        {
            get { return _typeEntry; }
            set
            {
                _typeEntry = value;
                product.Type = _typeEntry.Id;
                OnPropertyChanged();
            }
        }

        public string ProductMarksProduct
        {
            get { return product.MarksProduct; }
            set
            {
                product.MarksProduct = value;
                OnPropertyChanged();
            }
        }

        public string ProductDescripton
        {
            get { return product.Descriotion; }
            set
            {
                product.Descriotion = value;
                OnPropertyChanged();
            }
        }
        #endregion

        public EditProductViewModel()
        {
            SaveChangesCommand = new LambdaCommand(OnSaveChangesCommandExecuted, CanSaveChangesCommandExecuted);
            CancelCommand = new LambdaCommand(OnCancelCommandExecuted, CanCancelCommandExecuted);

            #region Для прогрузки 
            var frms = Application.Current.Windows;
            for(int i = 0; i < frms.Count; i++)
            {
                var bb = frms[i].DataContext;
                if(bb != null)
                { 
                    var b = frms[i].DataContext.ToString();
                    if(b == "MVVMTest.ViewModels.JobWindowViewModel")
                    {
                        product = (bb as JobWindowViewModel).selecterdProduct;
                    }
                }
            }


            List<Unit> Units = sklad.Units.ToList();
            UnitsEntries = new CollectionView(Units);

            List<TypeProduct> typeProducts = sklad.TypeProducts.ToList();
            TypenEtries = new CollectionView(typeProducts);

            _typeEntry = product.TypeProduct;
            _unitEntry = product.Unit;
            #endregion
        }
    }
}
