using MVVMTest.Date;
using MVVMTest.Structur.Commands;
using MVVMTest.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace MVVMTest.ViewModels
{
    class NewProductViewModel : BaseViewModel
    {
        private static SkladEntities sklad = new SkladEntities();
        public ICommand MessegeBoxCommand { get; }
        private Product newProduct = new Product();

        public ICommand CancelCommand { get; }

        #region Данные для comdobox'сов

        #region Еденицы
        private CollectionView _unitsEntries;
        public CollectionView UnitsEntries
        {
            get { return _unitsEntries; }
        }

        private Unit _unitEntry;
        #endregion

        #region Еденицы
        private CollectionView _typeEntries;
        public CollectionView TypenEtries
        {
            get { return _typeEntries; }
        }

        private TypeProduct _typeEntry;
        #endregion

        #endregion

        #region Геттеры и сетрреры продукта

        public string NewPoductName
        {
            get { return newProduct.Name; }
            set 
            {
                newProduct.Name = value;
                OnPropertyChanged();
            }
        }

        public decimal? NewPoductCost
        {
            get { return newProduct.Cost; }
            set
            {
                newProduct.Cost = value;
                OnPropertyChanged();
            }
        }

        public decimal? NewProductQty
        {
            get { return newProduct.Qty; }
            set 
            {
                newProduct.Qty = value;
                OnPropertyChanged();
            }
        }

        public DateTime? NewProductDateDelivery
        {
            get { return newProduct.DateDelivery; }
            set 
            {
                newProduct.DateDelivery = value;
                OnPropertyChanged();
            }
        }

        public int? NewProductDateExpiration
        {
            get { return newProduct.DateExpiration; }
            set
            {
                newProduct.DateExpiration = value;
                OnPropertyChanged();
            }
        }

        public Unit UnitEntry
        {
            get { return _unitEntry; }
            set
            {
                _unitEntry = value;
                newProduct.Units = _unitEntry.Id;
                OnPropertyChanged();
            }
        }

        public TypeProduct TypeEntry
        {
            get { return _typeEntry; }
            set
            {
                _typeEntry = value;
                newProduct.Type = _typeEntry.Id;
                OnPropertyChanged();
            }
        }

        public string NewProductMarksProduct
        {
            get { return newProduct.MarksProduct; }
            set
            {
                newProduct.MarksProduct = value;
                OnPropertyChanged();
            }
        }

        public string NewProductDescripton
        {
            get { return newProduct.Descriotion; }
            set
            {
                newProduct.Descriotion = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Команда выхода с формы
        private void OnCancelCommandExecuted(object p)
        {
            (p as Window).Close();
        }
        private bool CanCancelCommandExecuted(object p) => true;
        #endregion

        #region Проверка и сохранение результата
        private void OnMessegeBoxCommandExecuted(object p)
        {
            sklad.Products.Add(newProduct);
            sklad.SaveChanges();
            MessageBox.Show("Продукт успешно добавлен");
        }
        private bool CanMessegeBoxCommandExecuted(object p) => true;
        #endregion


        public NewProductViewModel()
        {
            List<Unit> Units = sklad.Units.ToList();
            _unitsEntries = new CollectionView(Units);

            List <TypeProduct> typeProducts = sklad.TypeProducts.ToList();
            _typeEntries = new CollectionView(typeProducts);

            MessegeBoxCommand = new LambdaCommand(OnMessegeBoxCommandExecuted, CanMessegeBoxCommandExecuted);
            CancelCommand = new LambdaCommand(OnCancelCommandExecuted, CanCancelCommandExecuted);
        }
    }
}
