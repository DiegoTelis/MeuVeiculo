using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MeuVeiculo.Model
{
    public class BaseObservavel : INotifyPropertyChanged
    {
        public BaseObservavel()
        {
                
        }

        public event PropertyChangedEventHandler PropertyChanged = delegate
        {

        };

        protected void OnProPertyChanged([CallerMemberName] string name = "")
        {
            // if (PropertyChanged == null)
            //    return;

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        //exemplo para uso futuro metodo para setar na property das classes que pegarem de herança esta
        protected bool SetProperty<T>(ref T storege, T value, [CallerMemberName] string name = null)
        {
            if (EqualityComparer<T>.Default.Equals(storege, value))
            {
                return false;
            }
            storege = value;
            OnProPertyChanged(name);
            return true;
        }

    }
}
