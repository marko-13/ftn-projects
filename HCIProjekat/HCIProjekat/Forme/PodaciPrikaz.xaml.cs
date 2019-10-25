using HCIProjekat.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
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
    /// Interaction logic for PodaciPrikaz.xaml
    /// </summary>
    public partial class PodaciPrikaz : Window, INotifyPropertyChanged
    {
        private CPodaci pod = new CPodaci();

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        public CPodaci Pod
        {
            get
            {
                return this.pod;
            }
            set
            {
                if (value != pod)
                {
                    this.pod = value;
                    OnPropertyChanged("Pod");
                }
            }
        }

        public double sirina { get; set; }
        public double visina { get; set; }
        
        public PodaciPrikaz(CPodaci pod)
        {
            Pod = pod;
           
           // MessageBox.Show(Pod.listaZivotinja[0]._oznaka);
           

            InitializeComponent();
            this.DataContext = this;

           
        }

        public PodaciPrikaz(CPodaci pod, double w, double h)
        {
            Pod = pod;

            // MessageBox.Show(Pod.listaZivotinja[0]._oznaka);
            sirina = w;
            visina = h;

            InitializeComponent();
            this.DataContext = this;


        }

        /* private string _oznakaZTextBox;
         public string OznakaZKolona
         {
             get { return _oznakaZTextBox; }
             set
             {
                 if (value != _oznakaZTextBox)
                 {
                     _oznakaZTextBox = value;
                     OnPropertyChanged("OznakaZKolona");
                 }
             }
         }*/

        #region BRISANJE IZ MODELA CODE
        public bool omoguciBrisanje = false;
        public string id;
        private void TabelaZivotinja_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid gd = (DataGrid)sender;
            DataRowView row_selected = gd.SelectedItem as DataRowView;

            if (row_selected != null)
            {
                id=row_selected["oznakaZKolona"].ToString();
                Console.WriteLine(row_selected[0].ToString());

            }
        }

        private void BtnIzbrisiZivotinju_Click(object sender, RoutedEventArgs e)
        {
            foreach(CZivotinja z in Pod.listaZivotinja.ToList())
            {
                if (z._oznaka == (String)OznakaSelektovaneZTextBox.Text)
                {
                    Pod.listaZivotinja.Remove(z);
                    TabelaZivotinja.SelectedIndex = -1;
                    TabelaZivotinja.Items.Refresh();
                }
            }
        }

        private void BtnIzbrisiTip_Click(object sender, RoutedEventArgs e)
        {
            int jedanIspis = 0;
            Tip mojTip = new Tip();
            //foreach (Tip z in Pod.listaTipova.ToList())
            //{
            foreach (Tip z in Pod.listaTipova.ToList()) {
                if (z._oznaka.Equals((String)OznakaSelektovanogTTextBox.Text))
                {
                    mojTip = z;
                }
            }
               // Console.WriteLine("Ime tipa: "+z._ime);
                bool okBrisanje = true;
                foreach(CZivotinja zivotinja in Pod.listaZivotinja.ToList())
                {
                    //Console.WriteLine(zivotinja._tip);
                    //Console.WriteLine(zivotinja._ime);
                    if (zivotinja._tip._oznaka.Equals((String)OznakaSelektovanogTTextBox.Text) && jedanIspis==0)
                    {
                        okBrisanje = false;
                        Xceed.Wpf.Toolkit.MessageBox.Show(
                    "Nije moguce obrisati tip koji je dodeljen nekoj zivotinji",
                     "Greska",
                     MessageBoxButton.OK,
                     MessageBoxImage.None,
                     MessageBoxResult.Cancel,
                     (Style)Resources["MessageBoxStyle1"]
                 );
                        jedanIspis++;

                        
                        // MessageBox.Show("Nije moguce obrisati tip koji je dodeljen nekoj zivotinji");
                    }
                }

                if (mojTip._oznaka.Equals((String)OznakaSelektovanogTTextBox.Text) && okBrisanje)
                {
                    Pod.listaTipova.Remove(mojTip);
                    TabelaTipova.SelectedIndex = -1;
                    TabelaTipova.Items.Refresh();
                }
                
            //}
        }


        private void BtnIzbrisiEtiketu_Click(object sender, RoutedEventArgs e)
        {
            int jedanIspis = 0;
            Etiketa mojaEtiketa = null;

            foreach (Etiketa etk in Pod.listaEtiketa.ToList())
            {
                if (etk._oznaka.Equals((String)OznakaSelektovaneETextBox.Text))
                {
                    mojaEtiketa = etk;
                }
            }
            bool okBrisanje = true;
            foreach (CZivotinja zivotinja in Pod.listaZivotinja.ToList())
            {
                if (zivotinja._tag.Contains(mojaEtiketa) && jedanIspis == 0)
                {
                    okBrisanje = false;
                    Xceed.Wpf.Toolkit.MessageBox.Show(
                   "Nije moguce obrisati etiketu koja je dodeljena nekoj zivotinji",
                    "Greska",
                    MessageBoxButton.OK,
                    MessageBoxImage.None,
                    MessageBoxResult.Cancel,
                    (Style)Resources["MessageBoxStyle1"]
                );
                    jedanIspis++;

                    // MessageBox.Show("Nije moguce obrisati etiketu koja je dodeljena nekoj zivotinji");
                }
            }

            if (mojaEtiketa._oznaka == (String)OznakaSelektovaneETextBox.Text && okBrisanje)
            {
                Pod.listaEtiketa.Remove(mojaEtiketa);
                TabelaEtiketa.SelectedIndex = -1;
                TabelaEtiketa.Items.Refresh();
            }
        }

       

        #endregion

        private void TabelaZivotinja_GotFocus(object sender, RoutedEventArgs e)
        {

        }

        private void MenuItem_ClickIzmeniZ(object sender, RoutedEventArgs e)
        {
            CZivotinja zaPrenos=null;
            foreach (CZivotinja z in Pod.listaZivotinja.ToList())
            {
                if (z._oznaka == (String)OznakaSelektovaneZTextBox.Text)
                {
                    zaPrenos = z;
                }
            }


            var noviProzor = new HCIProjekat.Forme.IzmeniOne(Pod, zaPrenos, sirina,visina);
            noviProzor.Show();
            this.Close();
        }

        private void MenuItem_ClickIzmeniT(object sender, RoutedEventArgs e)
        {
            Tip zaPrenos = null;
            foreach (Tip t in Pod.listaTipova.ToList())
            {
                if (t._oznaka == (String)OznakaSelektovanogTTextBox.Text)
                {
                    zaPrenos = t;
                }
            }

            var noviProzor = new HCIProjekat.Forme.IzmeniType(Pod, zaPrenos, sirina,visina);
            noviProzor.Show();
            this.Close();
        }

        private void MenuItem_ClickIzmeniE(object sender, RoutedEventArgs e)
        {
            Etiketa zaPrenos = null;
            foreach (Etiketa eti in Pod.listaEtiketa.ToList())
            {
                if (eti._oznaka == (String)OznakaSelektovaneETextBox.Text)
                {
                    zaPrenos = eti;
                }
            }

            var noviProzor = new HCIProjekat.Forme.IzmeniTag(Pod, zaPrenos, sirina,visina);
            noviProzor.Show();
            this.Close();
        }

        #region IZGLED BUTTONA
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
        #endregion

        private void Button_ClickPovratak(object sender, RoutedEventArgs e)
        {
            var noviMW = new HCIProjekat.MainWindow(Pod, visina,sirina);
            noviMW.Show();
            this.Close();
        }


        #region MENU ITEM I FUNKCIJE ZA DESNI KLIK NA ZIVOTINJU
        private void Row_RightClick(object sender, MouseButtonEventArgs e)
        {
           // MessageBox.Show("Radi akcija");
            DataGridRow row = sender as DataGridRow;
            TabelaZivotinja.SelectedIndex = row.GetIndex();


            int rowIndex = row.GetIndex();

            object item = TabelaZivotinja.SelectedItem;
            string id = (TabelaZivotinja.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text;
           // MessageBox.Show(id);

            ContextMenu cm = new ContextMenu();

            row.ContextMenu = cm;

            MenuItem miObrisiZivotinju = new MenuItem();
            miObrisiZivotinju.Header = "Obrisite";
            
            miObrisiZivotinju.Icon = new Image {
                Source= new BitmapImage(new Uri("../Images/rubbish-bin.png", UriKind.RelativeOrAbsolute))
            };
            miObrisiZivotinju.Click += new RoutedEventHandler(obrisiZ_Click);

            MenuItem miIzmeniZivotinju = new MenuItem();
            miIzmeniZivotinju.Header = "Izmenite";
            miIzmeniZivotinju.Icon = new Image
            {
                Source = new BitmapImage(new Uri("../Images/shuffle.png", UriKind.Relative))
            };
            miIzmeniZivotinju.Click += new RoutedEventHandler(izmeniZ_Click);

            cm.Items.Add(miObrisiZivotinju);
            cm.Items.Add(miIzmeniZivotinju);


        }

        private void izmeniZ_Click(object sender, RoutedEventArgs e)
        {
            CZivotinja zaPrenos = null;
            foreach (CZivotinja z in Pod.listaZivotinja.ToList())
            {
                if (z._oznaka == (String)OznakaSelektovaneZTextBox.Text)
                {
                    zaPrenos = z;
                }
            }


            var noviProzor = new HCIProjekat.Forme.IzmeniOne(Pod, zaPrenos, sirina, visina);
            noviProzor.Show();
            this.Close();
        }

        private void obrisiZ_Click(object sender, EventArgs e)
        {
            foreach (CZivotinja z in Pod.listaZivotinja.ToList())
            {
                if (z._oznaka == (String)OznakaSelektovaneZTextBox.Text)
                {
                    Pod.listaZivotinja.Remove(z);
                    TabelaZivotinja.SelectedIndex = -1;
                    TabelaZivotinja.Items.Refresh();
                }
            }
        }
#endregion

        #region MENUITEM I FUNKCIJE ZA DESNI KLIK NA TIP
        private void Row_RightClickTip(object sender, MouseButtonEventArgs e)
        {
            // MessageBox.Show("Radi akcija");
            DataGridRow row = sender as DataGridRow;
            TabelaTipova.SelectedIndex = row.GetIndex();


            int rowIndex = row.GetIndex();

            object item = TabelaTipova.SelectedItem;
            string id = (TabelaTipova.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text;
            // MessageBox.Show(id);

            ContextMenu cm = new ContextMenu();

            row.ContextMenu = cm;

            MenuItem miObrisiTip = new MenuItem();
            miObrisiTip.Header = "Obrisite";
            miObrisiTip.Icon = new Image
            {
                Source = new BitmapImage(new Uri("../Images/rubbish-bin.png", UriKind.RelativeOrAbsolute))
            };
            miObrisiTip.Click += new RoutedEventHandler(obrisiT_Click);

            MenuItem miIzmeniTip = new MenuItem();
            miIzmeniTip.Header = "Izmenite";
            miIzmeniTip.Icon = new Image
            {
                Source = new BitmapImage(new Uri("../Images/shuffle.png", UriKind.Relative))
            };
            miIzmeniTip.Click += new RoutedEventHandler(izmeniT_Click);

            cm.Items.Add(miObrisiTip);
            cm.Items.Add(miIzmeniTip);


        }

        private void izmeniT_Click(object sender, RoutedEventArgs e)
        {
            Tip zaPrenos = null;
            foreach (Tip t in Pod.listaTipova.ToList())
            {
                if (t._oznaka == (String)OznakaSelektovanogTTextBox.Text)
                {
                    zaPrenos = t;
                }
            }

            var noviProzor = new HCIProjekat.Forme.IzmeniType(Pod, zaPrenos, sirina, visina);
            noviProzor.Show();
            this.Close();
        }

        private void obrisiT_Click(object sender, RoutedEventArgs e)
        {
            int jedanIspis = 0;
            Tip mojTip = new Tip();
            
            foreach (Tip z in Pod.listaTipova.ToList())
            {
                if (z._oznaka.Equals((String)OznakaSelektovanogTTextBox.Text))
                {
                    mojTip = z;
                }
            }
            // Console.WriteLine("Ime tipa: "+z._ime);
            bool okBrisanje = true;
            foreach (CZivotinja zivotinja in Pod.listaZivotinja.ToList())
            {
                if (zivotinja._tip._oznaka.Equals((String)OznakaSelektovanogTTextBox.Text) && jedanIspis == 0)
                {
                    okBrisanje = false;
                    Xceed.Wpf.Toolkit.MessageBox.Show(
                "Nije moguce obrisati tip koji je dodeljen nekoj zivotinji",
                 "Greska",
                 MessageBoxButton.OK,
                 MessageBoxImage.None,
                 MessageBoxResult.Cancel,
                 (Style)Resources["MessageBoxStyle1"]
             );
                    jedanIspis++;


                    // MessageBox.Show("Nije moguce obrisati tip koji je dodeljen nekoj zivotinji");
                }
            }

            if (mojTip._oznaka.Equals((String)OznakaSelektovanogTTextBox.Text) && okBrisanje)
            {
                Pod.listaTipova.Remove(mojTip);
                TabelaTipova.SelectedIndex = -1;
                TabelaTipova.Items.Refresh();
            }
        }
        #endregion

        #region MENUITEM I FUNKCIJE ZA DESNI KLIK NA ETIKETU
        private void Row_RightClickE(object sender, MouseButtonEventArgs e)
        {
            // MessageBox.Show("Radi akcija");
            DataGridRow row = sender as DataGridRow;
            TabelaEtiketa.SelectedIndex = row.GetIndex();


            int rowIndex = row.GetIndex();

            object item = TabelaEtiketa.SelectedItem;
            string id = (TabelaEtiketa.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text;
            // MessageBox.Show(id);

            ContextMenu cm = new ContextMenu();

            row.ContextMenu = cm;

            MenuItem miObrisiEtiketu = new MenuItem();
            miObrisiEtiketu.Header = "Obrisite";
            miObrisiEtiketu.Icon = new Image
            {
                Source = new BitmapImage(new Uri("../Images/rubbish-bin.png", UriKind.RelativeOrAbsolute))
            };
            miObrisiEtiketu.Click += new RoutedEventHandler(obrisiE_Click);

            MenuItem miIzmeniEtiketu = new MenuItem();
            miIzmeniEtiketu.Header = "Izmenite";
            miIzmeniEtiketu.Icon = new Image
            {
                Source = new BitmapImage(new Uri("../Images/shuffle.png", UriKind.Relative))
            };
            miIzmeniEtiketu.Click += new RoutedEventHandler(izmeniE_Click);

            cm.Items.Add(miObrisiEtiketu);
            cm.Items.Add(miIzmeniEtiketu);


        }

        private void obrisiE_Click(object sender, RoutedEventArgs e)
        {
            int jedanIspis = 0;
            Etiketa mojaEtiketa = null;

            foreach(Etiketa etk in Pod.listaEtiketa.ToList())
            {
                if (etk._oznaka.Equals((String)OznakaSelektovaneETextBox.Text))
                {
                    mojaEtiketa = etk;
                }
            }
                bool okBrisanje = true;
                foreach (CZivotinja zivotinja in Pod.listaZivotinja.ToList())
                {
                    if (zivotinja._tag.Contains(mojaEtiketa) && jedanIspis==0)
                    {
                        okBrisanje = false;
                        Xceed.Wpf.Toolkit.MessageBox.Show(
                       "Nije moguce obrisati etiketu koja je dodeljena nekoj zivotinji",
                        "Greska",
                        MessageBoxButton.OK,
                        MessageBoxImage.None,
                        MessageBoxResult.Cancel,
                        (Style)Resources["MessageBoxStyle1"]
                    );
                    jedanIspis++;
                        
                       // MessageBox.Show("Nije moguce obrisati etiketu koja je dodeljena nekoj zivotinji");
                    }
                }

                if (mojaEtiketa._oznaka == (String)OznakaSelektovaneETextBox.Text && okBrisanje)
                {
                    Pod.listaEtiketa.Remove(mojaEtiketa);
                    TabelaEtiketa.SelectedIndex = -1;
                    TabelaEtiketa.Items.Refresh();
                }
        }

        private void izmeniE_Click(object sender, RoutedEventArgs e)
        {
            Etiketa zaPrenos = null;
            foreach (Etiketa eti in Pod.listaEtiketa.ToList())
            {
                if (eti._oznaka == (String)OznakaSelektovaneETextBox.Text)
                {
                    zaPrenos = eti;
                }
            }

            var noviProzor = new HCIProjekat.Forme.IzmeniTag(Pod, zaPrenos, sirina, visina);
            noviProzor.Show();
            this.Close();
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
