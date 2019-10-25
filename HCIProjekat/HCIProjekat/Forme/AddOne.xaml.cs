﻿using HCIProjekat.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Xceed.Wpf.Toolkit;

namespace HCIProjekat.Forme
{
    /// <summary>
    /// Interaction logic for AddOne.xaml
    /// </summary>
    public partial class AddOne : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public bool oznakaOK { get; set; } = false;
       
        public bool imeOK { get; set; } = false;
        public bool opisOK { get; set; } = false;
        public bool prihodOK { get; set; } = false;

        

        private void Window_SourceInitialized(object sender, EventArgs ea)
        {
            WindowAspectRatio.Register((Window)sender);

            
        }

        

        protected virtual void OnPropertyChanged(string name)
        {
            if(PropertyChanged!=null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));

            }
        }


        private CZivotinja _zivotinja = new CZivotinja();
        public double sirina { get; set; }
        public double visina { get; set; }

        private string _oznakaTextBox;
        private string _imeTextBox;
        private string _opisTextBox;
        private string _prihodTextBox;
        private string _ikonicaTextBox;
        private Tip _tipComboBox;
        private string _statusComboBox;
        private string _opasna;
        private string _IUCNLista;
        private string _ziviNaselje;
        private string _turistickiStatus;
        private DateTime _datum=DateTime.Now;
        //private ObservableCollection<Etiketa> _etikete;

        #region SELECTED ON PROPERTY CHANGED
        public Tip TipComboBoxSelected
        {
            get { return _tipComboBox; }
            set
            {
                if (value != _tipComboBox)
                {
                    _tipComboBox = value;
                    OnPropertyChanged("TipComboBoxSelected");
                }
            }
        }

        //public ObservableCollection<Etiketa> EtiketaComboBoxSelected
        //{
          //  get { return _etikete; }
            //set
            //{
              //  if (value != _etikete)
                //{
                  //  _etikete = value;
                    //OnPropertyChanged("EtiketaComboBoxSelected");
                //}
            //}
        //}

        public DateTime DatumSelected
        {
            get { return _datum; }
            set
            {
                if (value != _datum)
                {
                    _datum = value;
                    OnPropertyChanged("DatumSelected");
                }
            }
        }

        public string TuristickiComboBoxSelected
        {
            get { return _turistickiStatus; }
            set
            {
                if (value != _turistickiStatus)
                {
                    _turistickiStatus = value;
                    OnPropertyChanged("TuristickiComboBoxSelected");
                }
            }
        }

        public string NaseljeComboBoxSelected
        {
            get { return _ziviNaselje; }
            set
            {
                if (value != _ziviNaselje)
                {
                    _ziviNaselje = value;
                    OnPropertyChanged("NaseljeComboBoxSelected");
                }
            }
        }

        public string IUCNComboBoxSelected
        {
            get { return _IUCNLista; }
            set
            {
                if (value != _IUCNLista)
                {
                    _IUCNLista = value;
                    OnPropertyChanged("IUCNComboBoxSelected");
                }
            }
        }

        public string StatusComboBoxSelected
        {
            get { return _statusComboBox; }
            set
            {
                if(value!= _statusComboBox)
                {
                    _statusComboBox = value;
                    OnPropertyChanged("StatusComboBoxSelected");
                }
            }
        }

        public string OpasnaComboBoxSelected
        {
            get { return _opasna; }
            set
            {
                if (value != _opasna)
                {
                    _opasna = value;
                    OnPropertyChanged("OpasnaComboBoxSelected");
                }
            }
        }


        public string IkonicaTextBox
        {
            get { return _ikonicaTextBox; }
            set
            {
                if (value != _ikonicaTextBox)
                {
                    _ikonicaTextBox = value;
                    OnPropertyChanged("IkonicaTextBox");
                }
            }
        }
        public string OznakaTextBox
        {
            get { return _oznakaTextBox; }
            set
            {
                if(value!= _oznakaTextBox)
                {
                    _oznakaTextBox = value;
                    OnPropertyChanged("OznakaTextBox");
                }
            }
        }

        

        public string PrihodTextBox
        {
            get { return _prihodTextBox; }
            set
            {
                if(value!= _prihodTextBox)
                {
                    _prihodTextBox = value;
                    OnPropertyChanged("PrihodTextBox");
                }
            }
        }

        public string ImeTextBox
        {
            get { return _imeTextBox; }
            set
            {
                if(value!= _imeTextBox)
                {
                    
                    _imeTextBox = value;
                    
                    OnPropertyChanged("ImeTextBox");
                    
                }
            }
        }

        public string OpisTextBox
        {
            get { return _opisTextBox; }
            set
            {
                if (value != _opisTextBox)
                {
                    _opisTextBox = value;
                    OnPropertyChanged("OpisTextBox");
                }
            }
        }
        #endregion


        #region PROVERA IME I PROVERA PRIHOD
       /* public bool proveraIme(String ime)
        {
            if (ime != null) { 
            
                foreach (char c in ime)
                {
                    if (Char.IsNumber(c))
                    {
                        return false;
                    }
                }
                return true;
            }
            else
            {
                return false;
            }
        }*/
        public bool proveraPrihod(String prihod)
        {
            if (prihod != null)
            {
                try
                {
                    double n;
                    if (!(double.TryParse(prihod, out n)))
                    {
                        return false;
                    }
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            else
                return false;
        }
        #endregion


        #region PROPERTYJI ZA OBSERVABLE COLLECTIONE
        public ObservableCollection<Tip> TipComboBox
        {
            get;
            set;
        }

        

        public ObservableCollection<string> StatusComboBox
        {
            get;
            set;
        }

        public ObservableCollection<string> OpasnaComboBox
        {
            get;
            set;
        }

        public ObservableCollection<string> IUCNListaComboBox
        {
            get;
            set;
        }

        public ObservableCollection<string> ZiviUNaseljuComboBox
        {
            get;
            set;
        }

        public ObservableCollection<string> TuristickiStatusComboBox
        {
            get;
            set;
        }

        public ObservableCollection<Etiketa> EtiketaComboBox
        {
            get;
            set;
        }

        #endregion

        //--------------------------------------------------------------------------
        CPodaci pod;

        public AddOne(CPodaci p, double s, double v)
        {

            pod = p;
            sirina = s;
            visina = v;
            InitializeComponent();
            this.DataContext = this;

            #region POPUNJAVANJE COMBOBOXA
            //OVDE CE SE PROMENITI U <TIP> KAD NAPRAVIM MODEL, OVDE SU NEKI BASIC TIPOVI KORISNIK CE MOCI DA DODA JOS

            TipComboBox = new ObservableCollection<Tip>();
            //TipComboBox.Add(pod.listaTipova[0]);
            //TipComboBox.Add(pod.listaTipova[1]);
            //TipComboBox.Add(pod.listaTipova[2]);

            foreach(Tip t in pod.listaTipova.ToList())
            {
                TipComboBox.Add(t);
            }

            //OVDE CE SE PROMENITI U <ETIKETA> KAD NAPRAVIM MODEL
            EtiketaComboBox = new ObservableCollection<Etiketa>();

            foreach(Etiketa e in pod.listaEtiketa.ToList())
            {
                EtiketaComboBox.Add(e);
                
            }
            //MORAS OVERRIDE TOSTRING U ETIKETI DA BI IH ISPISIVAO
            //EtiketaComboBox.Add("Etiketa 1");
            //EtiketaComboBox.Add("Etiketa 2");


            StatusComboBox = new ObservableCollection<string>();
            StatusComboBox.Add("Kriticno ugrozena");
            StatusComboBox.Add("Ugrozena");
            StatusComboBox.Add("Ranjiva");
            StatusComboBox.Add("Zavisna od ocuvanja stanista");
            StatusComboBox.Add("Blizu rizika");
            StatusComboBox.Add("Najmanjeg rizika");


            OpasnaComboBox = new ObservableCollection<string>();
            OpasnaComboBox.Add("Da");
            OpasnaComboBox.Add("Ne");

            ZiviUNaseljuComboBox = new ObservableCollection<string>();
            ZiviUNaseljuComboBox.Add("Da");
            ZiviUNaseljuComboBox.Add("Ne");

            IUCNListaComboBox = new ObservableCollection<string>();
            IUCNListaComboBox.Add("Da");
            IUCNListaComboBox.Add("Ne");

            ZiviUNaseljuComboBox = new ObservableCollection<string>();
            ZiviUNaseljuComboBox.Add("Da");
            ZiviUNaseljuComboBox.Add("Ne");

            TuristickiStatusComboBox = new ObservableCollection<string>();
            TuristickiStatusComboBox.Add("Izolovana");
            TuristickiStatusComboBox.Add("Delimicno habituirana");
            TuristickiStatusComboBox.Add("Habituirana");
            #endregion
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var s = new HCIProjekat.MainWindow(pod,sirina,visina);
            s.Show();
            this.Close();
        }

        private void ButtonAddAnimal(object sender, RoutedEventArgs e)
        {
            //PROVERA DA LI JE OK DODATI SVE PROVERE
            #region PROVERE DA LI SU SVA POLJA POPUNJENA

            string errorMsg="";
            int greskaFlag = 0;
            if (!String.IsNullOrEmpty(oznTB.Text))
            {
                for (int i = 0; i < pod.listaZivotinja.Count; i++)
                {
                    if (_oznakaTextBox.Equals(pod.listaZivotinja[i]._oznaka))
                    {
                        errorMsg += "Greska:" + Environment.NewLine + "    Zivotinja sa takvom oznakom vec postoji";
                        greskaFlag = 1;
                    }
                }
            }
            int izadji = 0;
            Console.WriteLine(_oznakaTextBox);
            Console.WriteLine(oznTB.Text);
            Console.WriteLine(cbTip.Text);
            if (String.IsNullOrEmpty(cbTip.Text))
            {
                if (greskaFlag == 0)
                {
                    errorMsg += "Greska:" + Environment.NewLine + "    Polje tip mora biti popunjeno";
                    greskaFlag = 1;
                }
                else
                {
                    errorMsg += Environment.NewLine + "    Tip nije dobro popunjen";
                }
            }

            if (!String.IsNullOrEmpty(oznTB.Text) )
            {
                
               if(!Regex.Match(oznTB.Text, "^[^0-9]+$").Success)
                {
                    if (greskaFlag == 0)
                    {
                        errorMsg += "Greska:" + Environment.NewLine+ "    Polje oznaka sme sadrzati samo slova";
                        greskaFlag = 1;
                    }
                    else
                    {
                        errorMsg += Environment.NewLine+"    Oznaka nije dobro popunjena";
                    }
                }      
                
            }
            if (String.IsNullOrEmpty(oznTB.Text))
            {
                if (greskaFlag==0)
                {
                    errorMsg += "Greska:" + Environment.NewLine+ "    Polje oznaka mora biti popunjeno";
                    greskaFlag = 1;
                }
                else
                {
                    errorMsg += Environment.NewLine + "    Oznaka nije dobro popunjena";
                }
            }
            if (!String.IsNullOrEmpty(imeTB.Text))
            {
               
                if (!Regex.Match(imeTB.Text, "^[^0-9]+$").Success)
                {
                    if (greskaFlag == 0)
                    {
                        errorMsg += "Greska:" + Environment.NewLine+ "    Polje ime sme sadrzati samo slova";
                        greskaFlag = 1;
                    }
                    else
                    {
                        errorMsg += Environment.NewLine + "    Ime nije dobro popunjeno";
                    }
                }
            }
            if (String.IsNullOrEmpty(imeTB.Text))
            {
                if (greskaFlag == 0)
                {
                    errorMsg += "Greska:" + Environment.NewLine+"    Polje ime mora biti popunjeno";
                    greskaFlag = 1;
                }
                else
                {
                    errorMsg += Environment.NewLine + "    Ime nije dobro popunjeno";
                }
            }
           
            if (!proveraPrihod(_prihodTextBox))
            {
                if (greskaFlag==0)
                {
                    errorMsg += "Greska:" + Environment.NewLine+ "    Polje prihod nije dobro popunjeno";
                    greskaFlag = 1;
                }
                else
                {
                    errorMsg += Environment.NewLine + "    Prihod nije dobro popunjen";
                }
            }
            
            if (_statusComboBox == null)
            {
                if (greskaFlag==0)
                {
                    errorMsg += "Greska:" + Environment.NewLine+ "    Status mora biti odabran";
                    greskaFlag = 1;
                }
                else
                {
                    errorMsg += Environment.NewLine + "    Status nije dobro popunjen";
                }
            }
            if (_opisTextBox == null)
            {
                if (greskaFlag==0)
                {
                    errorMsg += "Greska:" + Environment.NewLine+ "    Polje opis mora biti popunjeno";
                    greskaFlag = 1;
                }
                else
                {
                    errorMsg += Environment.NewLine + "    Opis nije dobro popunjen";
                }
            }
            if (_opasna == null)
            {
                if (greskaFlag==0)
                {
                    errorMsg += "Greska:" + Environment.NewLine+ "    Nivo opasnosti mora biti odabran";
                    greskaFlag = 1;
                }
                else
                {
                    errorMsg += Environment.NewLine + "    Nivo opasnosti nije dobro popunjen";
                }
            }
            if (_IUCNLista == null)
            {
                if (greskaFlag==0)
                {
                    errorMsg += "Greska:" + Environment.NewLine+ "    Status na IUCN listi mora biti odabran";
                    greskaFlag = 1;
                }
                else
                {
                    errorMsg += Environment.NewLine + "    Status na IUCN listi nije dobro popunjen";
                }
            }
            if (_ziviNaselje == null)
            {
                if (greskaFlag==0)
                {
                    errorMsg += "Greska:" + Environment.NewLine+ "    Polje da li zivi u naselju mora biti popunjeno";
                    greskaFlag = 1;
                }
                else
                {
                    errorMsg += Environment.NewLine + "    Polje da li zivi u naselju nije dobro popunjen";
                }
            }
            if (_turistickiStatus == null)
            {
                if (greskaFlag==0)
                {
                    errorMsg += "Greska:" + Environment.NewLine+ "    Turisticki status mora biti odabran";
                    greskaFlag = 1;
                }
                else
                {
                    errorMsg += Environment.NewLine + "    Turisticki status nije dobro popunjen";
                }
            }

            int izadji2 = 0;
            if (_prihodTextBox == null)
            {
                if (greskaFlag==0)
                {
                    errorMsg += "Greska:" + Environment.NewLine+ "    Polje prihod mora biti popunjeno";
                    greskaFlag = 1;
                    izadji2 = 1;
                }
                else
                {
                    errorMsg += Environment.NewLine + "    Prihod nije dobro popunjen";
                    izadji2 = 1;
                }
            }
            if (_datum == null)
            {
                System.Windows.MessageBox.Show("Greska: Polje datum mora biti popunjeno");
                return;
            }
            double brojNeki = 0;

            if (!String.IsNullOrEmpty(txtPrihod.Text))
            {
                if (!Regex.Match(txtPrihod.Text, "^[0-9]*$").Success)
                {
                    if (greskaFlag == 0 && izadji2 != 1)
                    {
                        errorMsg += "Greska:" + Environment.NewLine + "    Prihod mora biti ceo broj";
                        greskaFlag = 1;
                    }
                    else
                    {
                        errorMsg += Environment.NewLine + "    Prihod mora biti ceo broj";
                    }
                }
            }
            double.TryParse(txtPrihod.Text,out brojNeki);

            if (brojNeki <= 0)
            {
                if (greskaFlag==0 && izadji2!=1)
                {
                    errorMsg += "Greska:" + Environment.NewLine+ "    Prihod ne sme biti negativan i ne sme sadrzati slova";
                    greskaFlag = 1;
                }
                else if(izadji2!=1)
                {
                    errorMsg += Environment.NewLine + "    Prihod ne sme biti negativan i ne sme sadrzati slova";
                }
            }
           

            if (String.IsNullOrEmpty(ccbEtiketa.Text))
            {
                if (greskaFlag == 0)
                {
                    errorMsg += "Greska:" + Environment.NewLine + "    Barem jedna etiketa mora biti odabrana";
                    greskaFlag = 1;
                }
                else
                {
                    errorMsg += Environment.NewLine + "    Barem jedna etiketa mora biti odabrana";
                }
            }

            if (greskaFlag==1)
            {
                Xceed.Wpf.Toolkit.MessageBox.Show(
                    errorMsg,
                     "Greska",
                     MessageBoxButton.OK,
                     MessageBoxImage.None,
                     MessageBoxResult.Cancel,
                     (Style)Resources["MessageBoxStyle1"]
                 );
                //System.Windows.MessageBox.Show(errorMsg);
                return;
            }
            //  if (_etikete == null)
            //{
              //System.Windows.MessageBox.Show("Greska: Polje etiketa mora biti popunjeno");
            //return;
            //}
            #endregion

            //------------------DODAVANJE U MODEL SVIH POLJA-------------------------------------
            #region DODAVANJE POLJA U MODEL

            var listaEtiketa = ccbEtiketa.SelectedItems;
            ObservableCollection<Etiketa> _etikete = new ObservableCollection<Etiketa>();
            for(int i=0; i<ccbEtiketa.SelectedItems.Count; i++)
          {
                Etiketa eti = (Etiketa)listaEtiketa[i];
                _etikete.Add(eti);
                Console.WriteLine(eti.ToString());
               // _zivotinja._tag = _etikete;
            }
            _zivotinja._tag = _etikete;

            if (_imeTextBox != null)
            {
                _zivotinja._ime = imeTB.Text;
            }
           
            if (_oznakaTextBox != null)
                _zivotinja._oznaka = oznTB.Text;

            if (_opisTextBox != null)
                _zivotinja._opis = _opisTextBox;

            if (_prihodTextBox != null)
            {
                Double izlaz;
                Double.TryParse(_prihodTextBox,out izlaz);
                _zivotinja._prihodOdTurizma = izlaz;
            }

            if (_opasna.Equals("Da"))
            {
                _zivotinja._opasnaZaLjude = true;
            }
            else if (_opasna.Equals("Ne"))
            {
                _zivotinja._opasnaZaLjude = false;
            }

            if (_ikonicaTextBox != null)
                _zivotinja._ikonicaString = _ikonicaTextBox;
            else
            {
                _zivotinja._ikonicaString = _tipComboBox._ikonica;
            }

            if (_tipComboBox != null)
                _zivotinja._tip = _tipComboBox;

            if (_statusComboBox != null)
                _zivotinja._status = _statusComboBox;

            if (_IUCNLista.Equals("Da"))
                _zivotinja._naCrvenojListi = true;
            else
                _zivotinja._naCrvenojListi = false;

            if (_ziviNaselje.Equals("Da"))
                _zivotinja._ziviUNaseljenom = true;
            else
                _zivotinja._ziviUNaseljenom = false;

            if (_turistickiStatus != null)
                _zivotinja._turisticki = _turistickiStatus;

            if (_datum != null)
                _zivotinja._datumOtkrica = _datum;

            if (_etikete != null)
                _zivotinja._tag = _etikete;

            
#endregion

            pod.listaZivotinja.Add(_zivotinja);

            var noviMW = new HCIProjekat.MainWindow(pod,sirina,visina);
            noviMW.Show();

            //Console.WriteLine(pod.listaZivotinja[0]._oznaka);
            //Console.WriteLine(pod.listaZivotinja[0]._ime);
            //Console.WriteLine(pod.listaZivotinja[0]._ikonicaString);
            //Console.WriteLine(pod.listaZivotinja[0]._tip);
            //Console.WriteLine(pod.listaZivotinja[0]._status);
            //Console.WriteLine(pod.listaZivotinja[0]._opis);

            this.Close();
        }

        //DUGME ZA DODAVANJE IKONICE
        #region DODAVANJE IKONICE
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            //Create openfile dialog
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            //postavi filter za tipove fajla
            dlg.DefaultExt = ".ico";
            dlg.Filter= "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif";

            //prikazi openfiledijalog pozivom showdialog metode
            Nullable<bool> result = dlg.ShowDialog();

            //preuzmi ime fajla i prikazi ga u textboxu
            if(result==true)
            {
                //otvori fajl
                string filename = dlg.FileName;
                tbIcon.Text = filename;

                FileInfo fi = new FileInfo(filename);
                string extn = fi.Extension;
                Uri myUri = new Uri(filename, UriKind.RelativeOrAbsolute);

                if (extn.Equals(".jpg"))
                {
                    JpegBitmapDecoder decoder2 = new JpegBitmapDecoder(myUri, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.Default);
                    BitmapSource bitmapSource2 = decoder2.Frames[0];
                    imgIcon.Source = bitmapSource2;
                }
                else if (extn.Equals(".png"))
                {
                    PngBitmapDecoder decoder2 = new PngBitmapDecoder(myUri, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.Default);
                    BitmapSource bitmapSource2 = decoder2.Frames[0];
                    imgIcon.Source = bitmapSource2;
                }
                else
                {
                    imgIcon.Source = null;
                }
            }
        }
        #endregion 


        #region IZGLEDI BUTTONA TEXTBOXOVA,...
        private void TxtKap_GotFocus(object sender, RoutedEventArgs e)
        {
            Color colorForeground=(Color)ColorConverter.ConvertFromString("#FFF1F1F1");
            Color colorBackground = (Color)ColorConverter.ConvertFromString("#FF252526");
            Color colorForegroundHover = (Color)ColorConverter.ConvertFromString("#FF007ACC");
            Color colorBackgroundHover = (Color)ColorConverter.ConvertFromString("#FF3E3E40");
            Color colorBorder = (Color)ColorConverter.ConvertFromString("#FF252526");
            Color colorBorderHover = (Color)ColorConverter.ConvertFromString("#FF3E3E40");

            TextBox t = (TextBox)sender;
            t.BorderBrush =new SolidColorBrush (colorBorderHover);
            t.Background = new SolidColorBrush(colorBackgroundHover);
            // lbKap.Foreground = new SolidColorBrush(Color.FromRgb(212, 212, 212));
            t.BorderBrush.Opacity = 0.5;
            t.BorderThickness = new Thickness(0, 0, 0, 1);
        }

        private void TxtKap_LostFocus(object sender, RoutedEventArgs e)
        {
            Color colorForeground = (Color)ColorConverter.ConvertFromString("#FFF1F1F1");
            Color colorBackground = (Color)ColorConverter.ConvertFromString("#FF252526");
            Color colorForegroundHover = (Color)ColorConverter.ConvertFromString("#FF007ACC");
            Color colorBackgroundHover = (Color)ColorConverter.ConvertFromString("#FF3E3E40");
            Color colorBorder = (Color)ColorConverter.ConvertFromString("#FF252526");
            Color colorBorderHover = (Color)ColorConverter.ConvertFromString("#FF3E3E40");

            TextBox t = (TextBox)sender;
            t.BorderBrush = new SolidColorBrush(colorBorder);
            // lbKap.Foreground = new SolidColorBrush(Color.FromRgb(212, 212, 212));
            t.BorderBrush.Opacity = 1;
            t.BorderThickness = new Thickness(1, 1, 1, 1);
        }

    //BOJE ZA IZGLED DUGMETA
        //BOJE ZA DEFAULTNI IZGLED DUGMETA
        private void ButtonDefault(object sender, RoutedEventArgs e)
        {
            Color colorForeground = (Color)ColorConverter.ConvertFromString("#F1F1F1");
            Color colorBackground = (Color)ColorConverter.ConvertFromString("#3F3F46");
            Color colorBorder = (Color)ColorConverter.ConvertFromString("#555555");

            Button b = (Button)sender;
            b.BorderThickness = new Thickness(1, 1, 1, 1);
            b.BorderBrush = new SolidColorBrush(colorBorder);
            b.Background = new SolidColorBrush(colorBackground);
            b.Foreground = new SolidColorBrush(colorForeground);
        }
        //BOJE ZA IZGLED LEFT  DUGMETA
        private void ButtonMouseLeft(object sender, RoutedEventArgs e)
        {
            Color colorForeground = (Color)ColorConverter.ConvertFromString("#F1F1F1");
            Color colorBackground = (Color)ColorConverter.ConvertFromString("#3F3F46");
            Color colorBorder = (Color)ColorConverter.ConvertFromString("#555555");

            Button b = (Button)sender;
            b.FontSize = b.FontSize - 2;
            b.BorderBrush = new SolidColorBrush(colorBorder);
            b.Background = new SolidColorBrush(colorBackground);
            b.Foreground = new SolidColorBrush(colorForeground);
        }
        //BOJE ZA IZGLED KAD SE HOVERUJE PREKO DUGMETA
        private void ButtonMouseOver(object sender, RoutedEventArgs e)
        {
            Color colorForeground = (Color)ColorConverter.ConvertFromString("#F1F1F1");
            Color colorBackground = (Color)ColorConverter.ConvertFromString("#3F3F46");
            Color colorBorder = (Color)ColorConverter.ConvertFromString("#007ACC");

            Button b = (Button)sender;
            
            b.BorderThickness = new Thickness(1.5, 1.5, 1.5, 1.5);
            b.BorderBrush = new SolidColorBrush(colorBorder);
            b.Background = new SolidColorBrush(colorBackground);
            b.Foreground = new SolidColorBrush(colorForeground);
            
            b.FontSize = b.FontSize + 2;
        }

        private void ComboBoxDefault(object sender, RoutedEventArgs e)
        {
            ComboBox cb = (ComboBox)sender;

            Color colorText = (Color)ColorConverter.ConvertFromString("#F1F1F1");
            Color colorBackground = (Color)ColorConverter.ConvertFromString("#3F3F46");
            Color colorBorder = (Color)ColorConverter.ConvertFromString("#434346");
            Color colorSeparator = (Color)ColorConverter.ConvertFromString("#333337");
            Color colorGlyph = (Color)ColorConverter.ConvertFromString("#999999");
            Color colorGlyphBackground = (Color)ColorConverter.ConvertFromString("#333337");

            cb.Foreground = new SolidColorBrush(colorText);
            cb.Background = new SolidColorBrush(colorBackground);
            cb.BorderBrush = new SolidColorBrush(colorBorder);
            
        }

        private void SmanjiFont(object sender, MouseEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            tb.FontSize = tb.FontSize - 2;
        }

        private void PovecajFont(object sender, MouseEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            tb.FontSize = tb.FontSize + 2;
        }

        private void TbIcon_GotFocus(object sender, RoutedEventArgs e)
        {
            Color colorForeground = (Color)ColorConverter.ConvertFromString("#F1F1F1");
            Color colorBackground = (Color)ColorConverter.ConvertFromString("#3F3F46");
            Color colorBorder = (Color)ColorConverter.ConvertFromString("#555555");

            TextBox b = (TextBox)sender;
            b.BorderThickness = new Thickness(1, 1, 1, 1);
            b.BorderBrush = new SolidColorBrush(colorBorder);
            b.Background = new SolidColorBrush(Color.FromRgb(105, 105, 105));
            b.Foreground = new SolidColorBrush(colorForeground);
        }


        private void dp_loaded(object sender, RoutedEventArgs e)
        {
            DatePicker datePicker = sender as DatePicker;
            if (datePicker != null)
            {
                System.Windows.Controls.Primitives.DatePickerTextBox datePickerTextBox = FindVisualChild<System.Windows.Controls.Primitives.DatePickerTextBox>(datePicker);
                if (datePickerTextBox != null)
                {

                    ContentControl watermark = datePickerTextBox.Template.FindName("PART_Watermark", datePickerTextBox) as ContentControl;
                    if (watermark != null)
                    {
                        watermark.Content = "Izaberite datum*";
                        //or set it some value here...
                    }
                }
            }


            Color dpFore = (Color)ColorConverter.ConvertFromString("#FFD3D3D3");

            datePicker.Foreground = new SolidColorBrush(dpFore);
        }
#endregion


        private T FindVisualChild<T>(DependencyObject depencencyObject) where T : DependencyObject
        {
            if (depencencyObject != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depencencyObject); ++i)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(depencencyObject, i);
                    T result = (child as T) ?? FindVisualChild<T>(child);
                    if (result != null)
                        return result;
                }
            }

            return null;
        }

       
        #region COLORPICKER IZGLED
        //------------------------------------------------
        private void ClrPcker_Background_MouseEnter(object sender, MouseEventArgs e)
        {

            CheckComboBox cp = (CheckComboBox)sender;
            Color colorBorder = (Color)ColorConverter.ConvertFromString("#007ACC");



            cp.BorderThickness = new Thickness(2, 2, 2, 2);
            cp.BorderBrush = new SolidColorBrush(colorBorder);
        }

        private void ClrPcker_Background_MouseLeave(object sender, MouseEventArgs e)
        {
            CheckComboBox cp = (CheckComboBox)sender;
            Color colorBorder = (Color)ColorConverter.ConvertFromString("#555555");



            cp.BorderThickness = new Thickness(1, 1, 1, 1);
            cp.BorderBrush = new SolidColorBrush(colorBorder);
        }
        #endregion
        
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            //System.Windows.Forms.Help.ShowHelp(this, "HCIHelp.chm");
            System.Diagnostics.Process.Start(System.IO.Path.GetFullPath(@"..\..\HCIHelp.chm"));

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.KeyDown += new KeyEventHandler(F1_KeyDown);
        }
        void F1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key.ToString() == "F1")
            {
                System.Diagnostics.Process.Start(System.IO.Path.GetFullPath(@"..\..\HCIHelp.chm"));

            }
        }



        /* private int NoOfErrors = 0;
         private void TextBox_Error(object sender, ValidationErrorEventArgs e)
         {
             if (e.Action == ValidationErrorEventAction.Added)
                 NoOfErrors++;

             else
                 NoOfErrors--;
             Console.WriteLine("Broj gresaka: " + NoOfErrors);
             dugmeZaDodavanje.IsEnabled = NoOfErrors > 0 ? false : true;
         }*/
    }
}
