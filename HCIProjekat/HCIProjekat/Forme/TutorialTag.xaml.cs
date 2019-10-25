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
    /// Interaction logic for AddTag.xaml
    /// </summary>
    public partial class TutorialTag : Window, INotifyPropertyChanged
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


        private Color _boja;
        private string _oznaka;
        private string _opis;
        int dobriKoraci =0;


        public Color Boja
        {
            get
            {
                return _boja;
            }
            set
            {
                _boja = value;
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

        CPodaci pod;
        public double sirina { get; set; }
        public double visina { get; set; }
        //Etiketa _tag = new Etiketa();
        public TutorialTag(CPodaci p, double s, double v)
        {
            pod = p;
            sirina = s;
            visina = v;
            InitializeComponent();
            this.DataContext = this;
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

        #region ZA PROZOR DA SE MOZE SAMO DIJAGONALNO SIRITI
        private void Window_SourceInitialized(object sender, EventArgs ea)
        {
            WindowAspectRatio.Register((Window)sender);
        }
        #endregion

        private void ButtonAddTag(object sender, RoutedEventArgs e)
        {

            string errorMsg = "";
            int errorFlag = 0;

            foreach (Etiketa etiketa in pod.listaEtiketa)
            {
                if (etiketa._oznaka == this.Oznaka)
                {
                    errorMsg += "Greska:" + Environment.NewLine + "    Etiketa sa oznakom " + this.Oznaka + " vec postoji";
                    errorFlag = 1;

                }
            }

            if (String.IsNullOrEmpty(txtOzn.Text))
            {
                if (errorFlag == 0)
                {
                    errorMsg += "Greska:" + Environment.NewLine + "    Oznaka mora biti popunjena";
                    errorFlag = 1;
                }
                else
                    errorMsg += Environment.NewLine + "    Oznaka mora biti popunjena";

            }
            if (!Regex.Match(txtOzn.Text, "^[A-Za-z]*$").Success)
            {
                if (errorFlag == 0)
                {
                    errorMsg += "Greska:" + Environment.NewLine + "    Oznaka mora biti od slova bez razmaka";
                    errorFlag = 1;
                }
                else
                    errorMsg += Environment.NewLine + "    Oznaka mora biti od slova bez razmaka";

            }
            if (txtOzn.Text.Length > 12)
            {
                if (errorFlag == 0)
                {
                    errorMsg += "Greska:" + Environment.NewLine + "    Oznaka ne sme biti duza od 12 karaktera";
                    errorFlag = 1;
                }
                else
                    errorMsg += Environment.NewLine + "    Oznaka ne sme biti duza od 12 karaktera";

            }

            if (String.IsNullOrEmpty(txtOpis.Text))
            {
                if (errorFlag == 0)
                {
                    errorMsg += "Greska:" + Environment.NewLine + "    Opis mora biti popunjen";
                    errorFlag = 1;
                }
                else
                    errorMsg += Environment.NewLine + "    Opis mora biti popunjen";
            }

            if (ClrPcker_Background.SelectedColorText.Equals("#00000000"))
            {
                if (errorFlag == 0)
                {
                    errorMsg += "Greska:" + Environment.NewLine + "    Boja mora biti odabrana";
                    errorFlag = 1;
                }
                else
                    errorMsg += Environment.NewLine + "    Boja mora biti odabrana";
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
                //System.Windows.MessageBox.Show(errorMsg);
                return;
            }


            Etiketa _tag = new Etiketa(txtOzn.Text, _boja, _opis);


            pod.listaEtiketa.Add(_tag);

            var noviMW = new HCIProjekat.MainWindow(pod, sirina, visina);
            noviMW.Show();
            this.Close();
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
            if (ClrPcker_Background.SelectedColor.HasValue)
            {
                Boja = ClrPcker_Background.SelectedColor.Value;

            }
            else
            {
                Boja = new Color();
            }
        }

        #endregion

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var s = new HCIProjekat.MainWindow(pod, sirina, visina);
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

            dobriKoraci = 0;

            txtOzn.Focus();
            ClrPcker_Background.IsEnabled = false;
            txtOpis.IsEnabled = false;
            dodajteEtiketu.IsEnabled = false;

            ToolTip hintOznaka = new ToolTip();
            hintOznaka.Content = "Unesite oznaku etikete koja sme sadrzati samo slova i mora biti jedinstvena";
            txtOzn.ToolTip = hintOznaka;

        }

        private void Button_Click_NextTutorialStep(object sender, RoutedEventArgs e)
        {
            //*****************************
            //prvi korak provera da li je oznaka ok
            //*****************************
            if (dobriKoraci == 0)
            {
                string errorMsg = "";
                int greskaFlag = 0;
                for (int i = 0; i < pod.listaEtiketa.Count; i++)
                {
                    if (txtOzn.Text.Equals(pod.listaEtiketa[i]._oznaka))
                    {
                        errorMsg += "Greska:" + Environment.NewLine + "    Etiketa sa takvom oznakom vec postoji";
                        greskaFlag = 1;
                    }
                }
                if (!String.IsNullOrEmpty(txtOzn.Text))
                {

                    if (!Regex.Match(txtOzn.Text, "^[^0-9]+$").Success)
                    {
                        if (greskaFlag == 0)
                        {
                            errorMsg += "Greska:" + Environment.NewLine + "    Polje oznaka sme sadrzati samo slova";
                            greskaFlag = 1;
                        }
                        else
                        {
                            errorMsg += Environment.NewLine + "    Oznaka nije dobro popunjena";
                        }
                    }

                }
                if (String.IsNullOrEmpty(txtOzn.Text))
                {
                    if (greskaFlag == 0)
                    {
                        errorMsg += "Greska:" + Environment.NewLine + "    Polje oznaka mora biti popunjeno";
                        greskaFlag = 1;
                    }
                    else
                    {
                        errorMsg += Environment.NewLine + "    Oznaka nije dobro popunjena";
                    }
                }
                //gotove sve provere za oznaku
                if (greskaFlag == 0)
                {
                    dobriKoraci++;
                    txtOzn.IsEnabled = false;
                    ClrPcker_Background.IsEnabled = true;
                    

                    ToolTip hintIme = new ToolTip();
                    hintIme.Content = "Odaberite boju iz padajuceg menija";
                    ClrPcker_Background.ToolTip = hintIme;

                    var res = Xceed.Wpf.Toolkit.MessageBox.Show(
                           "Uspesno ste popunili polje oznaka. Naredno polje je boja",
                           "Cestitamo",
                           MessageBoxButton.OK,
                           MessageBoxImage.None,
                           MessageBoxResult.Cancel,
                           (Style)Resources["MessageBoxStyle1"]
                       );

                    // Xceed.Wpf.Toolkit.MessageBox.Show("Test");
                    //System.Windows.MessageBox.Show("Cestitamo, uspesno ste popunili polje oznaka. Naredno polje je ime");
                }
                else
                {
                    //System.Windows.MessageBox.Show(errorMsg + Environment.NewLine + "Pokusajte ponovo");
                    Xceed.Wpf.Toolkit.MessageBox.Show(
                         errorMsg + Environment.NewLine + Environment.NewLine + "Pokusajte ponovo",
                         "Greska",
                         MessageBoxButton.OK,
                         MessageBoxImage.None,
                         MessageBoxResult.Cancel,
                         (Style)Resources["MessageBoxStyle1"]
                     );
                }
            }
            //********************
            //drugi korak provera da li je boja ok
            //*******************
            else if (dobriKoraci == 1)
            {
                string errorMsg = "";
                int greskaFlag = 0;
                if (ClrPcker_Background.SelectedColorText.Equals("#00000000"))
                {
                    
                        errorMsg += "Greska:" + Environment.NewLine + "    Boja mora biti odabrana";
                        greskaFlag = 1;
                    
                }
                //gotove sve provere za boju
                if (greskaFlag == 0)
                {
                    dobriKoraci++;
                    ClrPcker_Background.IsEnabled = false;
                    txtOpis.IsEnabled = true;
                    txtOpis.Focus();


                    ToolTip hintIme = new ToolTip();
                    hintIme.Content = "Unesite opis etikete";
                    txtOpis.ToolTip = hintIme;

                    var res = Xceed.Wpf.Toolkit.MessageBox.Show(
                           "Uspesno ste odabrali boju. Naredno polje je opis",
                           "Cestitamo",
                           MessageBoxButton.OK,
                           MessageBoxImage.None,
                           MessageBoxResult.Cancel,
                           (Style)Resources["MessageBoxStyle1"]
                       );

                    // Xceed.Wpf.Toolkit.MessageBox.Show("Test");
                    //System.Windows.MessageBox.Show("Cestitamo, uspesno ste popunili polje oznaka. Naredno polje je ime");
                }
                else
                {
                    //System.Windows.MessageBox.Show(errorMsg + Environment.NewLine + "Pokusajte ponovo");
                    Xceed.Wpf.Toolkit.MessageBox.Show(
                         errorMsg + Environment.NewLine + Environment.NewLine + "Pokusajte ponovo",
                         "Greska",
                         MessageBoxButton.OK,
                         MessageBoxImage.None,
                         MessageBoxResult.Cancel,
                         (Style)Resources["MessageBoxStyle1"]
                     );
                }
            }
            //*********************
            //treci korak provera da li je opis ok
            //*********************
            else if (dobriKoraci == 2)
            {
                string errorMsg = "";
                int greskaFlag = 0;
                if (String.IsNullOrEmpty(txtOpis.Text))
                {
                    errorMsg += "Greska:" + Environment.NewLine + "    Morate uneti opis etikete";
                    greskaFlag = 1;
                }
                //gotove sve provere za opis
                if (greskaFlag == 0)
                {
                    dobriKoraci++;
                    txtOpis.IsEnabled = false;
                    dodajteEtiketu.IsEnabled = true;

                    

                    var res = Xceed.Wpf.Toolkit.MessageBox.Show(
                           "Uspesno ste popunili polje opis i tako zavrsili unos podataka za novu etiketu" +
                           ". Ukoliko zelite da je dodate u podatke kliknite dugme Dodajte etiketu." +
                           " Ukoliko zelite da se vratite na glavni prozor kliknite Nazad na pocetnu.",
                           "Cestitamo",
                           MessageBoxButton.OK,
                           MessageBoxImage.None,
                           MessageBoxResult.Cancel,
                           (Style)Resources["MessageBoxStyle1"]
                       );

                    // Xceed.Wpf.Toolkit.MessageBox.Show("Test");
                    //System.Windows.MessageBox.Show("Cestitamo, uspesno ste popunili polje oznaka. Naredno polje je ime");
                }
                else
                {
                    //System.Windows.MessageBox.Show(errorMsg + Environment.NewLine + "Pokusajte ponovo");
                    Xceed.Wpf.Toolkit.MessageBox.Show(
                         errorMsg + Environment.NewLine + Environment.NewLine + "Pokusajte ponovo",
                         "Greska",
                         MessageBoxButton.OK,
                         MessageBoxImage.None,
                         MessageBoxResult.Cancel,
                         (Style)Resources["MessageBoxStyle1"]
                     );
                }
            }
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
