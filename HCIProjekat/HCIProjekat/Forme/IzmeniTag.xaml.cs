using HCIProjekat.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Interaction logic for IzmeniTag.xaml
    /// </summary>
    public partial class IzmeniTag : Window, INotifyPropertyChanged
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

        public CPodaci podaci { get; set; }
        public double sirina { get; set; }
        public double visina { get; set; }

        private Color _boja;
        private string _oznaka;
        private string _opis;


        public Color Boja
        {
            get
            {
                return _boja;
            }
            set
            {
                _boja = value;
                OnPropertyChanged("Boja");
            }
        }

        public string Oznaka
        {
            get
            {
                return _oznaka;
            }
            set
            {
                if (_oznaka != value)
                {
                    _oznaka = value;
                    OnPropertyChanged("Oznaka");
                }
            }
        }

        public string Opis
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
                    OnPropertyChanged("Opis");
                }
            }
        }


        public IzmeniTag()
        {
            InitializeComponent();
        }

        public IzmeniTag(CPodaci pod, Etiketa eti, double s, double v)
        {
            InitializeComponent();
            this.DataContext = this;


            podaci = new CPodaci();
            podaci = pod;
            sirina = s;
            visina = v;

            this.Oznaka = eti._oznaka;
            this.Boja = eti.Boja;
            this.Opis = eti._opis;
            
        }

        int index = -1;
        private void ButtonAddTag(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < podaci.listaEtiketa.Count; i++)
            {
                if (podaci.listaEtiketa[i]._oznaka == (String)Oznaka)
                {
                    index = i;
                }
            }

            /* if (!Regex.Match(txtOzn.Text, "^[A-Za-z0-9]*$").Success)
             {
                 System.Windows.MessageBox.Show("Oznaka mora biti od slova i brojeva bez razmaka!");
                 txtOzn.Focus();
                 return;
             }
             else if (txtOzn.Text.Length > 12)
             {
                 System.Windows.MessageBox.Show("Oznaka ne sme biti duza od 12 karaktera!");
                 txtOzn.Focus();
                 return;
             }*/
            string errorMsg = "";
            int errorFlag = 0;

       

            if (String.IsNullOrEmpty(txtOzn.Text))
            {
                //System.Windows.MessageBox.Show("Oznaka etikete ne sme biti prazna!");
                if (errorFlag == 0)
                {
                    errorMsg += "Greska:"+Environment.NewLine +"    Oznaka mora biti popunjena";
                    errorFlag = 1;
                }
                else
                    errorMsg += Environment.NewLine+"    Oznaka mora biti popunjena";

            }
            if (!Regex.Match(txtOzn.Text, "^[A-Za-z]*$").Success)
            {
                //System.Windows.MessageBox.Show("Oznaka mora biti od slova bez razmaka!");
                if (errorFlag == 0)
                {
                    errorMsg += "Greska:"+Environment.NewLine+ "    Oznaka mora biti od slova bez razmaka";
                    errorFlag = 1;
                }
                else
                    errorMsg += Environment.NewLine+"    Oznaka mora biti od slova bez razmaka";

            }
            if (txtOzn.Text.Length > 12)
            {
                //System.Windows.MessageBox.Show("Oznaka ne sme biti duza od 12 karaktera!");
                if (errorFlag == 0)
                {
                    errorMsg += "Greska:"+Environment.NewLine+ "    Oznaka ne sme biti duza od 12 karaktera";
                    errorFlag = 1;
                }
                else
                    errorMsg +=Environment.NewLine +"    Oznaka ne sme biti duza od 12 karaktera";
                //txtOzn.Focus();

            }

            if (String.IsNullOrEmpty(txtOpis.Text))
            {
                if (errorFlag == 0)
                {
                    errorMsg += "Greska:"+Environment.NewLine +"    Opis mora biti popunjnen";
                    errorFlag = 1;
                }
                else
                    errorMsg +=Environment.NewLine+"    Opis mora biti popunjen";
            }

            if (ClrPcker_Background.SelectedColorText.Equals("#00000000"))
            {
                if (errorFlag == 0)
                {
                    errorMsg += "Greska:"+Environment.NewLine+ "    Boja mora biti odabrana";
                    errorFlag = 1;
                }
                else
                    errorMsg += Environment.NewLine+"    Boja mora biti odabrana";
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
                // System.Windows.MessageBox.Show(errorMsg);
                return;
            }

            podaci.listaEtiketa[index]._oznaka = Oznaka;
            podaci.listaEtiketa[index]._opis = Opis;
            podaci.listaEtiketa[index].Boja = Boja;


            var noviMW = new HCIProjekat.MainWindow(podaci,visina,sirina);
            noviMW.Show();
            this.Close();
        }




        private void Cp_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            if (ClrPcker_Background.SelectedColor.HasValue)
            {
                Boja = ClrPcker_Background.SelectedColor.Value;
                byte Red = Boja.R;
                byte Green = Boja.G;
                byte Blue = Boja.B;
                //long colorVal = Convert.ToInt64(Blue * (Math.Pow(256, 0)) + Green * (Math.Pow(256, 1)) + Red * (Math.Pow(256, 2)));
            }
            else
            {
                Boja = new Color();
            }

        }

        

        #region IZGLEDI DUGMICA
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

        private void ClrPcker_Background_MouseEnter(object sender, MouseEventArgs e)
        {

            ColorPicker cp = (ColorPicker)sender;
            Color colorBorder = (Color)ColorConverter.ConvertFromString("#007ACC");



            cp.BorderThickness = new Thickness(1, 1, 1, 1);
            cp.BorderBrush = new SolidColorBrush(colorBorder);
        }

        private void ClrPcker_Background_MouseLeave(object sender, MouseEventArgs e)
        {
            ColorPicker cp = (ColorPicker)sender;
            Color colorBorder = (Color)ColorConverter.ConvertFromString("#555555");



            cp.BorderThickness = new Thickness(1, 1, 1, 1);
            cp.BorderBrush = new SolidColorBrush(colorBorder);
        }

        private void ClrPcker_Background_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color> e)
        {
            // System.Windows.Media.Color mojaBoja;

            //var boja=new 
        }

        #endregion

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var s = new HCIProjekat.MainWindow(podaci,visina,sirina);
            s.Show();
            this.Close();
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
