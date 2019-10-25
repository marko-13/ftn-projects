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
    /// Interaction logic for AddType.xaml
    /// </summary>
    public partial class AddType : Window, INotifyPropertyChanged
    {
        #region PROPERTY CHANGED
        public event PropertyChangedEventHandler PropertyChanged;

        private void Window_SourceInitialized(object sender, EventArgs ea)
        {
            WindowAspectRatio.Register((Window)sender);
        }

        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
        #endregion

        private string _oznakaTextBox;
        private string _imeTextBox;
        private string _ikonica;
        private string _opis;

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

        CPodaci pod;
        public double sirina { get; set; }
        public double visina { get; set; }
        //Tip _type = new Tip();
        public AddType(CPodaci p,double s, double v)
        {
            pod = p;
            sirina = s;
            visina = v;
            InitializeComponent();
            this.DataContext = this;

            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var s = new HCIProjekat.MainWindow(pod,sirina,visina);
            s.Show();
            this.Close();
        }


        private void ButtonAddType(object sender, RoutedEventArgs e)
        {
            ImageSource img;

            string errorMsg="";
            int errorFlag = 0;

            if (!String.IsNullOrEmpty(_ikonica))
            {
                Uri path = new Uri(_ikonica);
                img = new BitmapImage(path);

            }
            else
            {
                img = null;
            }
            if (!String.IsNullOrEmpty(txtOzn.Text))
            {
                foreach (Tip tp in pod.listaTipova)
                {
                    if (tp._oznaka == this.txtOzn.Text)
                    {
                        errorMsg += "Greska:" + Environment.NewLine + "    Tip sa oznakom" + this.txtOzn.Text + "vec postoji";
                        errorFlag = 1;
                    }
                }
            }
            if (String.IsNullOrEmpty(txtOzn.Text))
            {
                if (errorFlag == 0)
                {
                    errorMsg += "Greska:"+Environment.NewLine +"    Oznaka tipa ne sme biti prazna";
                    errorFlag = 1;
                }
                else
                    errorMsg +=Environment.NewLine+ "    Oznaka tipa ne sme biti prazna";
                //txtOzn.Focus();
               
            }
            if (!Regex.Match(txtOzn.Text, "^[A-Za-z]*$").Success)
            {
                if (errorFlag == 0)
                {
                    errorMsg += "Greska:"+Environment.NewLine+ "    Oznaka mora biti od slova bez razmaka";
                    errorFlag = 1;
                }
                else
                    errorMsg += Environment.NewLine+"    Oznaka mora biti od slova bez razmaka";
                    //txtOzn.Focus();
                    
            }
            if (txtOzn.Text.Length > 12)
            {
                if (errorFlag == 0)
                {
                    errorMsg += "Greska:"+Environment.NewLine +"    Oznaka ne sme biti duza od 12 karaktera";
                    errorFlag = 1;
                }
                else
                    errorMsg += Environment.NewLine+"    Oznaka ne sme biti duza od 12 karaktera";

            }
            if (String.IsNullOrEmpty(txtIme.Text))
            {
                if (errorFlag == 0)
                {
                    errorMsg += "Greska:"+Environment.NewLine+ "    Ime tipa mora biti popunjeno";
                    errorFlag = 1;
                }
                else
                {
                    errorMsg += Environment.NewLine+"    Ime tipa mora biti popunjeno";
                }
            }
            if(!Regex.Match(txtIme.Text, "^[^0-9]*$").Success)
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
                    errorMsg += "Greska:"+Environment.NewLine+ "    Morate uneti sliku tipa";
                    errorFlag = 1;
                }
                else
                {
                    errorMsg += Environment.NewLine+"    Morate uneti sliku tipa";
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
                //MessageBox.Show(errorMsg);
                return;
            }
            
            
                
            
                Tip _type = new Tip();
            _type._oznaka = this.txtOzn.Text;
            _type._ime = this.txtIme.Text;
            _type._ikonica = this.tbIcon.Text;
            _type._opis = this.OpisTextBox;

                pod.listaTipova.Add(_type);

            var noviMW = new HCIProjekat.MainWindow(pod,sirina,visina);
            noviMW.Show();
            this.Close();
        }









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
