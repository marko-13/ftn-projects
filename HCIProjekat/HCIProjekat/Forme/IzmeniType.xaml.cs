using HCIProjekat.Model;
using System;
using System.Collections.Generic;
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

namespace HCIProjekat.Forme
{
    /// <summary>
    /// Interaction logic for IzmeniType.xaml
    /// </summary>
    public partial class IzmeniType : Window, INotifyPropertyChanged
    {

        private void Window_SourceInitialized(object sender, EventArgs ea)
        {
            WindowAspectRatio.Register((Window)sender);


        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));

            }
        }

        private string _oznakaTextBox;
        private string _imeTextBox;
        private string _ikonica;
        private string _opis;

        public double sirina { get; set; }
        public double visina { get; set; }

        public string OznakaTextBox
        {
            get { return _oznakaTextBox; }
            set
            {
                if (value != _oznakaTextBox)
                {
                    _oznakaTextBox = value;
                    OnPropertyChanged("OznakaTextBox");
                }
            }
        }

        public string ImeTextBox
        {
            get { return _imeTextBox; }
            set
            {
                if (value != _imeTextBox)
                {
                    _imeTextBox = value;
                    OnPropertyChanged("ImeTextBox");
                }
            }
        }

        public string IkonicaTextBox
        {
            get
            {
                return _ikonica;
            }
            set
            {
                if (_ikonica != value)
                {
                    _ikonica = value;
                    OnPropertyChanged("IkonicaTextBox");
                }
            }
        }

        public string OpisTextBox
        {
            get
            {
                return _opis;
            }
            set
            {
                if (_opis != value)
                {
                    _opis = value;
                    OnPropertyChanged("OpisTextBox");
                }
            }
        }


        public IzmeniType()
        {
            InitializeComponent();
        }
        public CPodaci podaci { get; set; }

        public IzmeniType(CPodaci pod, Tip t, double s, double v)
        {
            InitializeComponent();
            this.DataContext = this;
            podaci = pod;

            sirina = s;
            visina = v;

            this.OznakaTextBox = t._oznaka;
            this.ImeTextBox = t._ime;
            this.OpisTextBox = t._opis;
            this.IkonicaTextBox = t._ikonica;
            imgIcon.Source = t._image;

            #region IKONICA PRIKAZ
            //Create openfile dialog
            // Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            //postavi filter za tipove fajla
            // dlg.DefaultExt = ".ico";
            //dlg.Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif";

            //prikazi openfiledijalog pozivom showdialog metode

            //preuzmi ime fajla i prikazi ga u textboxu


            //otvori fajl
            string filename = t._ikonica;
            // tbIcon.Text = filename;

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
            #endregion

        }

        int index = -1;
        private void ButtonAddType(object sender, RoutedEventArgs e)
        {
            ImageSource img;
            string errorMsg = "";
            int errorFlag = 0;

            for (int i=0; i<podaci.listaTipova.Count; i++)
            {
                if (podaci.listaTipova[i]._oznaka == this.txtOzn.Text)
                {
                    index = i;
                }
            }

            if (!String.IsNullOrEmpty(_ikonica))
            {
                Uri path = new Uri(_ikonica);
                img = new BitmapImage(path);

            }
            else
            {
                img = null;
            }
            if (String.IsNullOrEmpty(txtIme.Text))
            {
                
                if (errorFlag == 0)
                {
                    errorMsg += "Greska:" + Environment.NewLine+ "    Ime tipa mora biti popunjeno";
                    errorFlag = 1;
                }
                else
                {
                    errorMsg += Environment.NewLine+"    Ime tipa mora biti popunjeno";
                }
            }
            if (!Regex.Match(txtIme.Text, "^[^0-9]*$").Success)
            {
                if (errorFlag == 0)
                {
                    errorMsg += "Greska:" + Environment.NewLine + "    Ime sme sadrzati samo slova";
                    errorFlag = 1;
                }
                else
                {
                    errorMsg += Environment.NewLine + "    Ime nije validno popunjeno";
                }
            }
            if (String.IsNullOrEmpty(tbIcon.Text))
            {
                if (errorFlag == 0)
                {
                    errorMsg += "Greska:" +Environment.NewLine+ "    Morate uneti sliku tipa";
                    errorFlag = 1;
                }
                else
                {
                    errorMsg +=Environment.NewLine+ "    Morate uneti sliku tipa";
                }
               

            }


            if (errorFlag == 1)
            {
                Xceed.Wpf.Toolkit.MessageBox.Show(
                    errorMsg,
                     "Greska",
                     MessageBoxButton.OK,
                     MessageBoxImage.None,
                     MessageBoxResult.Cancel,
                     (Style)Resources["MessageBoxStyle1"]
                 );
                // MessageBox.Show(errorMsg);
                return;
            }
           

            #region DODAVANJE POLJA U MODEL
            if (_imeTextBox != null)
            {
                podaci.listaTipova[index]._ime = txtIme.Text;
            }

            if (_oznakaTextBox != null)
                podaci.listaTipova[index]._oznaka = _oznakaTextBox;

            if (_opis != null)
                podaci.listaTipova[index]._opis = _opis;

            if (_ikonica != null)
                podaci.listaTipova[index]._ikonica = _ikonica;
            #endregion

            var noviMW = new HCIProjekat.MainWindow(podaci,visina,sirina);
            noviMW.Show();
            this.Close();

        }

        //POVRATAK DUGME
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var s = new HCIProjekat.MainWindow(podaci,visina,sirina);
            s.Show();
            this.Close();
        }




        #region DODAVANJE IKONICE DUGME
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            //Create openfile dialog
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            //postavi filter za tipove fajla
            dlg.DefaultExt = ".ico";
            dlg.Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif";

            //prikazi openfiledijalog pozivom showdialog metode
            Nullable<bool> result = dlg.ShowDialog();

            //preuzmi ime fajla i prikazi ga u textboxu
            if (result == true)
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

        //BOJE ZA IZGLED DUGMETA
        //BOJE ZA DEFAULTNI IZGLED DUGMETA
        #region IZGLED BUTTONA
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
    }
}
