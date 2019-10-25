using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xaml;
using System.Collections;
using System.Windows.Media;
using System.ComponentModel;

namespace HCIProjekat.Model
{
    public class Etiketa : INotifyPropertyChanged
    {
        #region PropertyChanged
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        public string _oznaka { get; set; }


        private Color _boja;
        public Color Boja
        {
            get { return _boja; }
            set
            {
                if (_boja != value)
                {
                    _boja = value;
                    OnPropertyChanged("Boja");
                }
            }
        }
        

        public string _opis { get; set; }

        public Etiketa(string oznaka, Color boja, string opis)
        {
            this._oznaka = oznaka;
            this._boja = boja;
            this._opis = opis;
        }

        public override string ToString()
        {
            return this._oznaka;
        }

        public string Ispis()
        {
            return "Oznaka = " + this._oznaka + "\nBoja = " + this._boja.ToString() + "\nOpis = " + this._opis;
        }

    }

    

    

    
}
