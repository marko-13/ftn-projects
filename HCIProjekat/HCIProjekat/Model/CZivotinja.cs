using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace HCIProjekat.Model
{
    public class CZivotinja
    {
        //Enumeracija za status ugrozenosti
        public enum StatusUgrozenosti
        {
            kriticnoUgorzena,
            ugrozena,
            ranjiva,
            zavisnaOdOcuvanjaStanista,
            blizuRizika,
            najmanjegRizika

        };
        
        //Funkcija za lep ispis enumeracije
        public string GetUIFriendlyEndangeredEnum(StatusUgrozenosti statusUgorzenosti)
        {
            switch(statusUgorzenosti)
            {
                case StatusUgrozenosti.kriticnoUgorzena: return "Kriticno ugrozena";
                    
                case StatusUgrozenosti.ugrozena: return "Ugorzena";

                case StatusUgrozenosti.ranjiva: return "Ranjiva";

                case StatusUgrozenosti.zavisnaOdOcuvanjaStanista: return "Zavisna od ocuvanja stansita";

                case StatusUgrozenosti.blizuRizika: return "Blizu rizika";

                case StatusUgrozenosti.najmanjegRizika: return "Najmanje rizika";

                default: return "Nije specificirano";
            }
        }

       
        //Enumeracija za turisticki status
        public enum TuristickiStatus
        {
            izolovana, delimicnoHabituirana, habituirana
        };

        //Funkcija za lep ispis enumeracije
        public string GetUIFriendlyTouristEnum(TuristickiStatus turistickiStatus)
        {
            switch(turistickiStatus)
            {
                case TuristickiStatus.izolovana: return "Izolvoana";

                case TuristickiStatus.delimicnoHabituirana: return "Delimicno habituirana";

                case TuristickiStatus.habituirana: return "Habituirana";

                default: return "Nije specificirano";
            }
        }
        public bool naMapi { get; set; }

        public string _turisticki { get; set; }
        
        public string _status { get; set; }
        private string Oznaka;
        public string _oznaka { get; set; }
        public string _ime { get; set; }
        public string _opis { get; set; }
        public Tip _tip { get; set; }

        public string _statusUgorzenosti { get; set; }
       

        public ImageSource _ikonica { get; set; }
       

        public string _ikonicaString { get; set; }
        

        public bool _opasnaZaLjude { get; set; }
        

        public bool _naCrvenojListi { get; set; }
      

        public bool _ziviUNaseljenom { get; set; }
        

        public string _turistickiStatus { get; set; }
        

        public double _prihodOdTurizma { get; set; }
       

        public DateTime _datumOtkrica { get; set; }
       

        public ObservableCollection<Etiketa> _tag { get; set; }


        //**********************************
        private double x;
        public double X
        {
            get { return x; }
            set { x = value; }
        }

        private double y;
        public double Y
        {
            get { return y; }
            set { y = value; }
        }

        private double xstaro;
        public double Xstaro
        {
            get { return xstaro; }
            set { xstaro = value; }
        }

        private double ystaro;
        public double Ystaro
        {
            get { return ystaro; }
            set { ystaro = value; }
        }
        //******************************

        public CZivotinja() {
            _ime = "";
            _oznaka = "";
            _opis = "";
            _prihodOdTurizma = 0;
            _datumOtkrica = DateTime.Today;
            _tag = new ObservableCollection<Etiketa>();
            _ikonicaString = null;
            _naCrvenojListi = false;
            _opasnaZaLjude = false;
            _status = "";
            _turisticki = "";
            naMapi = false;
        }
    }
}
