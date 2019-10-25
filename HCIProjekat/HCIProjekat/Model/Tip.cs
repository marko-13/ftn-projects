using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace HCIProjekat.Model
{
    //DA LI JE OVO OK URADITI DA CLASSA BUDE PUBLIC
    public class Tip
    {
        public string _oznaka { get; set; }
       

        public string _ime { get; set; }
        

        public string _ikonica { get; set; }
       

        public ImageSource _image { get; set; }
       

        public string _opis { get; set; }
        

        public List<CZivotinja> _zivotinje { get; set; }

        public Tip()
        {
        }
        public Tip(string oznaka, string ime, string ikonica, string opis)
        {
            this._ikonica = ikonica;
            this._ime = ime;
            this._opis = opis;
            this._oznaka = oznaka;
            this._zivotinje = new List<CZivotinja>();
        }

        public Tip(string oznaka, string ime, ImageSource img, string opis, string imgpath)
        {
            this._oznaka = oznaka;
            this._ime = ime;
            this._image = img;
            this._opis = opis;
            this._ikonica = imgpath;
        }


        public string Ispis()
        {
            return "Oznaka = " + this._oznaka + "\nIme = " + this._ime + "\nImgPath = " + this._ikonica + "\nOpis = " + this._opis;
        }

        public override string ToString()
        {
            return this._ime;
        }
        public  string ToString1()
        {
            return this._oznaka;
        }
    }
}
