using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MVVMTest.ViewModels
{
    //Базовый класс модели представления
    internal abstract class BaseViewModel : INotifyPropertyChanged, IDisposable
    {
        //Евент при изменении свойства
        public event PropertyChangedEventHandler PropertyChanged;

        public void Dispose()
        {
            Disposee(true);
        }

        private bool _Disposed;
        protected virtual void Disposee(bool Disposing)
        { 
            if (!Disposing || _Disposed) return;
            _Disposed = true;
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string PropertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }

        protected virtual bool Set<T>(ref T field, T value, [CallerMemberNameAttribute] string PropertyName = null)
        {
            if (Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(PropertyName);
            return true;
        }
    }
}
