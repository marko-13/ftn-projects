using HCIProjekat.Model;
using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Drawing;

namespace HCIProjekat
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window,  INotifyPropertyChanged
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

        public CPodaci podaci { get; set; }

        public CPodaci pomocniPodaci { get; set; }

        private CPodaci podd = new CPodaci();
        public CPodaci Podd
        {
            get
            {
                return this.podd;
            }
            set
            {
                if (value != podd)
                {
                    this.podd = value;
                    OnPropertyChanged("Podd");
                }

            }
        }

        private string _filterComboBox;
        public string FilterComboBoxSelected
        {
            get { return _filterComboBox; }
            set
            {
                if (value != _filterComboBox)
                {
                    _filterComboBox = value;
                    OnPropertyChanged("StatusComboBoxSelected");
                }
            }
        }

        public ObservableCollection<string> FilterComboBox
        {
            get;
            set;
        }

        Point startPoint = new Point();
        public ObservableCollection<double> Xpolozaji
        {
            get;
            set;
        }
        public ObservableCollection<double> Ypolozaji
        {
            get;
            set;
        }
        public ObservableCollection<CZivotinja> Zivotinje_mapa
        {
            get;
            set;
        }

        public MainWindow()
        {
            InitializeComponent();
            podaci = new CPodaci();
            this.DataContext = this;

            Zivotinje_mapa = new ObservableCollection<CZivotinja>();
            Xpolozaji = new ObservableCollection<double>();
            Ypolozaji = new ObservableCollection<double>();


            #region DODAVANJE DEFAULTNIH TIPOVA U LISTU TIPOVA
           /* string oznakaTip ="kz";
            string imeTip="kopnena zivotinja";
            string ikonicaTip="D:\\Desktop\\DaftPunk.jpg";
            string opisTip="zivotinje ovog tipa zive na kopnu";

            Tip primer1 = new Tip() ;
            primer1._oznaka = oznakaTip;
            primer1._ime = imeTip;
            primer1._opis = opisTip;
            primer1._ikonica = ikonicaTip;

            podaci.listaTipova.Add(primer1);

            oznakaTip = "vz";
            imeTip = "vazdusna zivotinja";
            opisTip = "zivotinje ovog tipa mogu da lete";

            Tip primer2 = new Tip();
            primer2._oznaka = oznakaTip;
            primer2._ime = imeTip;
            primer2._opis = opisTip;
            primer2._ikonica = ikonicaTip;


            podaci.listaTipova.Add(primer2);

            oznakaTip = "mz";
            imeTip = "morska zivotinja";
            opisTip = "zivotinje ovog tipa zive u morima";

            Tip primer3 = new Tip();
            primer3._oznaka = oznakaTip;
            primer3._ime = imeTip;
            primer3._opis = opisTip;
            primer3._ikonica = ikonicaTip;


            podaci.listaTipova.Add(primer3);
            */
            #endregion

            FilterComboBox = new ObservableCollection<string>();
            FilterComboBox.Add("Po imenu A-Z");
            FilterComboBox.Add("Po imenu Z-A");
            FilterComboBox.Add("Po tipu A-Z");
            FilterComboBox.Add("Po tipu Z-A");

            


            //FilterComboBox.Add("Blizu rizika");
            //FilterComboBox.Add("Najmanjeg rizika");

        }

        public void ispis()
        {
            for(int i=0; i<podaci.listaZivotinja.Count; i++)
            {
                Console.WriteLine(podaci.listaZivotinja[i]._oznaka);
                Console.WriteLine(podaci.listaZivotinja[i]._ime);
                Console.WriteLine(podaci.listaZivotinja[i]._ikonicaString);
                Console.WriteLine(podaci.listaZivotinja[i]._tip);
                Console.WriteLine(podaci.listaZivotinja[i]._status);
                Console.WriteLine(podaci.listaZivotinja[i]._opis);

                Console.WriteLine("*******************************");
            }
        }

        public  MainWindow(CPodaci pod, double h, double w)
        {
            InitializeComponent();
            this.DataContext = this;
            this.Width = h;
            this.Height = w;
            podaci = new CPodaci();
            podaci = pod;

            pomocniPodaci = new CPodaci();
            pomocniPodaci = pod;

            Podd = pod;
            Zivotinje_mapa = new ObservableCollection<CZivotinja>();

            FilterComboBox = new ObservableCollection<string>();
            FilterComboBox.Add("Po imenu A-Z");
            FilterComboBox.Add("Po imenu Z-A");
            FilterComboBox.Add("Po tipu A-Z");
            FilterComboBox.Add("Po tipu Z-A");

            


            foreach (CZivotinja z in podaci.listaZivotinja)
            {
                if (z.naMapi)
                {
                    Image i = new Image();
                    i.Source = new BitmapImage(new Uri(z._ikonicaString, UriKind.RelativeOrAbsolute));

                    
                        i.Height = 45;
                        i.Width = 45;
                   

                    //zivotinja.Xstaro = p.X * 723.0 / canvasMap.Width;
                    //zivotinja.Ystaro = p.Y * 424.0 / canvasMap.Height;
                    //OVDE JE BILO SAMO X ne XSTARO ali da kad opet ucita bude ok
                    //VISE NE VRATIO SAM
                    i.SetValue(Canvas.LeftProperty, z.X - i.Width / 2);
                    i.SetValue(Canvas.TopProperty, z.Y - i.Height / 2);

                    Console.WriteLine("............................" + i.Source + "    " + z.Xstaro);
                    canvasMap.Children.Add(i);


                    z.naMapi = true;
                    Zivotinje_mapa.Add(z);

                    WrapPanel wp = new WrapPanel();
                    wp.Orientation = Orientation.Vertical;

                    TextBlock id_ziv = new TextBlock();
                    id_ziv.IsEnabled = false;
                    id_ziv.Text = "Id zivotinje: " + z._oznaka;
                    wp.Children.Add(id_ziv);

                    TextBlock ime_ziv = new TextBlock();
                    ime_ziv.IsEnabled = false;
                    ime_ziv.Text = "Ime zivotinje: " + z._ime;
                    wp.Children.Add(ime_ziv);

                    ToolTip hint = new ToolTip();
                    hint.Content = wp;
                    i.ToolTip = hint;

                    ContextMenu cm = new ContextMenu();
                    i.ContextMenu = cm;
                    MenuItem mi = new MenuItem();
                    mi.Header = "Izbrisi";

                    mi.Icon = new Image
                    {
                        Source = new BitmapImage(new Uri("Images/pawprint.ico", UriKind.Relative))
                    };


                    cm.Items.Add(mi);

                    mi.Click += delegate (object s, RoutedEventArgs ev) { mi_Click(s, ev, i, z); };
                }
            }
            //***************************************
            //***************************************
            //ovde uradi crtanje na mapu proslih pri svakom ucitavanju
            //***************************************
            //***************************************


            ispis();
        }

        public MainWindow(CPodaci pod)
        {
            InitializeComponent();
            this.DataContext = this;

            podaci = new CPodaci();
            podaci = pod;
            pomocniPodaci = new CPodaci();
            pomocniPodaci = pod;
            Podd = pod;

            FilterComboBox = new ObservableCollection<string>();
            FilterComboBox.Add("Po imenu A-Z");
            FilterComboBox.Add("Po imenu Z-A");
            FilterComboBox.Add("Po tipu A-Z");
            FilterComboBox.Add("Po tipu Z-A");

            


            Zivotinje_mapa = new ObservableCollection<CZivotinja>();

            foreach(CZivotinja z in podaci.listaZivotinja)
            {
                if (z.naMapi)
                {
                    Image i = new Image();
                    i.Source = new BitmapImage(new Uri(z._ikonicaString, UriKind.RelativeOrAbsolute));

                    i.Height = 45;
                    i.Width = 45;
                    //OVDE JE BILO SAMO X ne XSTARO ali da kad opet ucita bude ok
                    //VISE NE VRATIO SAM
                    i.SetValue(Canvas.LeftProperty, z.X - i.Width / 2);
                    i.SetValue(Canvas.TopProperty, z.Y - i.Height / 2);

                    Console.WriteLine("............................" + i.Source + "    "+z.Xstaro);
                    canvasMap.Children.Add(i);

                    
                    z.naMapi = true;
                    Zivotinje_mapa.Add(z);

                    WrapPanel wp = new WrapPanel();
                    wp.Orientation = Orientation.Vertical;

                    TextBlock id_ziv = new TextBlock();
                    id_ziv.IsEnabled = false;
                    id_ziv.Text = "Id zivotinje: " + z._oznaka;
                    wp.Children.Add(id_ziv);

                    TextBlock ime_ziv = new TextBlock();
                    ime_ziv.IsEnabled = false;
                    ime_ziv.Text = "Ime zivotinje: " + z._ime;
                    wp.Children.Add(ime_ziv);

                    ToolTip hint = new ToolTip();
                    hint.Content = wp;
                    i.ToolTip = hint;

                    ContextMenu cm = new ContextMenu();
                    i.ContextMenu = cm;
                    MenuItem mi = new MenuItem();
                    mi.Header = "Izbrisi";

                    mi.Icon = new Image
                    {
                        Source = new BitmapImage(new Uri("Images/pawprint.ico", UriKind.Relative))
                    };


                    cm.Items.Add(mi);

                    mi.Click += delegate (object s, RoutedEventArgs ev) { mi_Click(s, ev, i, z); };
                }
            }

            ispis();
        }

        private void Window_SourceInitialized(object sender, EventArgs ea) {
            WindowAspectRatio.Register((Window)sender);
        }

        private void OpenAddAnimalForm(object sender, RoutedEventArgs e)
        {
            var s = new HCIProjekat.Forme.AddOne(podaci, this.Width, this.Height);
            s.Show();
            this.Close();
        }

        private void OpenAddTypeForm(object sender, RoutedEventArgs e)
        {
            var s = new HCIProjekat.Forme.AddType(podaci, this.Width, this.Height);
            s.Show();
            this.Close();
        }

        private void OpenAddTagForm(object sender, RoutedEventArgs e)
        {
            var s = new HCIProjekat.Forme.AddTag(podaci, this.Width, this.Height);
            s.Show();
            this.Close();
        }



        #region izgledDugme
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

        private void MenuAddAnimalTutorial(object sender, RoutedEventArgs e)
        {
            double w = this.Width;
            double h = this.Height;
            var s = new HCIProjekat.Forme.TutorialOne(podaci, w, h);
            s.Show();
            this.Close();
        }
        private void MenuAddTypeTutorial(object sender, RoutedEventArgs e)
        {
            double w = this.Width;
            double h = this.Height;
            var s = new HCIProjekat.Forme.TutorialType(podaci, w, h);
            s.Show();
            this.Close();
        }

        private void MenuAddTagTutorial(object sender, RoutedEventArgs e)
        {
            double w = this.Width;
            double h = this.Height;
            var s = new HCIProjekat.Forme.TutorialTag(podaci, w, h);
            s.Show();
            this.Close();
        }

        private void MenuAddAnimalnForm(object sender, RoutedEventArgs e)
        {
            double w = this.Width;
            double h = this.Height;
            var s = new HCIProjekat.Forme.AddOne(podaci,w,h);
            s.Show();
            this.Close();
        }

        private void MenuAddTypeForm(object sender, RoutedEventArgs e)
        {
            double w = this.Width;
            double h = this.Height;
            var s = new HCIProjekat.Forme.AddType(podaci,w,h);
            s.Show();
            this.Close();
        }

        private void MenuAddTagForm(object sender, RoutedEventArgs e)
        {
            double w = this.Width;
            double h = this.Height;
            var s = new HCIProjekat.Forme.AddTag(podaci,w,h);
            s.Show();
            this.Close();
        }

        private void DataTableDisplay(object sender, RoutedEventArgs e)
        {
            double w = this.Width;
            double h = this.Height;
            var s = new HCIProjekat.Forme.PodaciPrikaz(podaci, h, w);
            

            s.Show();
            this.Close();
        }

        private void MenuItem_ClickSacuvaj(object sender, RoutedEventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Json file (*.json)|*.json";
            sfd.Title = "Sacuvajte kao...";

            if (sfd.ShowDialog() == true)
            {
                using (StreamWriter file = File.CreateText(sfd.FileName))
                {
                    JsonSerializer serializer = new JsonSerializer();

                    serializer.Serialize(file, podaci);
                }
            }
        }
        private CPodaci NoviPodaci { get; set; }
        private void MenuItem_ClickUcitaj(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Odaberite fajl";
            op.Filter = "JSON file (*.json)|*.json";
            try
            {
                if (op.ShowDialog() == true)
                {
                    StreamReader file = File.OpenText(op.FileName);

                    using (file)
                    {
                        JsonSerializer serializer = new JsonSerializer();
                        NoviPodaci = (CPodaci)serializer.Deserialize(file, typeof(CPodaci));
                    }
                }

                for (int i = 0; i < NoviPodaci.listaZivotinja.Count; i++)
                {
                    foreach (Tip t in NoviPodaci.listaTipova)
                    {
                        if (NoviPodaci.listaZivotinja[i]._tip.ToString1().Equals(t._oznaka))
                        {
                            NoviPodaci.listaZivotinja[i]._tip = t;
                        }
                    }
                    //Console.WriteLine(NoviPodaci.listaZivotinja[i]._tag);


                    Console.WriteLine(NoviPodaci.listaZivotinja[i]._tag.Count);
                    Console.WriteLine(NoviPodaci.listaZivotinja[i]._tag[0]);

                    //ima 3 etikete u listi
                    //Console.WriteLine(NoviPodaci.listaEtiketa.Count + " IMA OVOLIKO ETIKETA U LISTI SVIH ETIKETA");

                    ObservableCollection<Etiketa> pomocnaListaEtiketa = new ObservableCollection<Etiketa>();
                    foreach (Etiketa eettkk in NoviPodaci.listaZivotinja[i]._tag)
                    {
                        pomocnaListaEtiketa.Add(eettkk);
                    }

                    ObservableCollection<Etiketa> pomocnaListaEtiketa2 = new ObservableCollection<Etiketa>();
                    NoviPodaci.listaZivotinja[i]._tag.Clear();

                    //Console.WriteLine(pomocnaListaEtiketa.Count + " IMA OVOLIKO ETIKETA U POMOCNOJ LISTI");

                    for (int j = 0; j < pomocnaListaEtiketa.Count; j++)
                    {
                        //Console.WriteLine("USAO OVDE 1");
                        foreach (Etiketa etk in NoviPodaci.listaEtiketa)
                        {
                            //Console.WriteLine("USAO OVE 2");
                            if (pomocnaListaEtiketa[j].ToString().Equals(etk.ToString()))
                            {
                                //Console.WriteLine("USAO OVDE 3");
                                pomocnaListaEtiketa2.Add(etk);
                                NoviPodaci.listaZivotinja[i]._tag.Add(etk);
                            }
                        }
                    }
                }

                var s = new HCIProjekat.MainWindow(NoviPodaci);
                s.Show();
                this.Close();
            }
            catch
            {
                return;
            }
        }





        //*****************************
        //DRAG AND DROP
        private void ListView_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            startPoint = e.GetPosition(null);
        }

        private void ListView_MouseMove(object sender, MouseEventArgs e)
        {
            Point mousePos = e.GetPosition(null);
            Vector diff = startPoint - mousePos;

            if (e.LeftButton == MouseButtonState.Pressed &&
               (Math.Abs(diff.X) > SystemParameters.MinimumHorizontalDragDistance ||
               Math.Abs(diff.Y) > SystemParameters.MinimumVerticalDragDistance))
            {

                ListBox listBox = sender as ListBox;
                ListBoxItem listBoxItem =
                    FindAncestor<ListBoxItem>((DependencyObject)e.OriginalSource);

                if (null != listBoxItem)
                {
                    CZivotinja zivotinja = (CZivotinja)listBox.ItemContainerGenerator.
                    ItemFromContainer(listBoxItem);
                

                    DataObject dragData = new DataObject("myFormat", zivotinja);
                    DragDrop.DoDragDrop(listBoxItem, dragData, DragDropEffects.Move);
                }
            }
        }

        private static T FindAncestor<T>(DependencyObject current) where T : DependencyObject
        {
            do
            {
                if (current is T)
                {
                    return (T)current;
                }
                current = VisualTreeHelper.GetParent(current);
            }
            while (current != null);
            return null;
        }

        private void ListView_DragEnter(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent("myFormat") || sender == e.Source)
            {
                e.Effects = DragDropEffects.None;
            }
        }

        private void ListView_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent("myFormat"))
            {
                CZivotinja zivotinja = e.Data.GetData("myFormat") as CZivotinja;

                Point p = e.GetPosition(canvasMap);
                bool zauzeto = false;

                //Image img = new Image();
                //img.Height = 45;
                //img.Width = 45;
                //img.Source = zivotinja._ikonica;
                Console.WriteLine(zivotinja._ikonicaString);

                foreach(CZivotinja zivotinjaZaProveru in Zivotinje_mapa)
                {
                    if (zivotinjaZaProveru._oznaka == zivotinja._oznaka)
                    {
                        MessageBox.Show("Ta zivotinja vec postoji na mapi!");
                        return;
                    }
                }
            

                Image i = new Image();
                i.Source = new BitmapImage(new Uri(zivotinja._ikonicaString, UriKind.RelativeOrAbsolute));

                i.Height = 45;
                i.Width = 45;

                foreach (CZivotinja z in Zivotinje_mapa)
                {
                    if (z.X != -1 && z.Y != -1)
                    {
                        if (Math.Abs(z.X - p.X) <= 45 && Math.Abs(z.Y - p.Y) <= 45)
                        {
                            zauzeto = true;
                            Xceed.Wpf.Toolkit.MessageBox.Show(
                 "Ikonica ne moze da se preklapa sa drugom ikonicom",
                  "Greska",
                  MessageBoxButton.OK,
                  MessageBoxImage.None,
                  MessageBoxResult.Cancel,
                  (Style)Resources["MessageBoxStyle1"]
              );
                            break;
                        }
                    }
                }
                //******************
                //JpegBitmapDecoder decoder2 = new JpegBitmapDecoder(new Uri(zivotinja._ikonicaString, UriKind.RelativeOrAbsolute)
                //            , BitmapCreateOptions.PreservePixelFormat,
                //              BitmapCacheOption.Default);
                //BitmapSource bitmapSource2 = decoder2.Frames[0];
                //i.Source = bitmapSource2;
                //****************

                if (zauzeto != true)
                {
                    if (p.X + i.Width / 2 >= canvasMap.Width)
                    {
                        Xceed.Wpf.Toolkit.MessageBox.Show(
                  "Ikonica ne moze da stane na mapu",
                   "Greska",
                   MessageBoxButton.OK,
                   MessageBoxImage.None,
                   MessageBoxResult.Cancel,
                   (Style)Resources["MessageBoxStyle1"]
               );
                       // MessageBox.Show("Slika ne moze da ne stane na ekran");
                        return;
                    }
                    if (p.X - i.Width / 2 <= 0)
                    {
                        Xceed.Wpf.Toolkit.MessageBox.Show(
                 "Ikonica ne moze da stane na mapu",
                  "Greska",
                  MessageBoxButton.OK,
                  MessageBoxImage.None,
                  MessageBoxResult.Cancel,
                  (Style)Resources["MessageBoxStyle1"]
              );
                        //MessageBox.Show("Slika ne moze da ne stane na ekran");
                        return;
                    }
                    if (p.Y - i.Height / 2 <= 0)
                    {
                        Xceed.Wpf.Toolkit.MessageBox.Show(
                 "Ikonica ne moze da stane na mapu",
                  "Greska",
                  MessageBoxButton.OK,
                  MessageBoxImage.None,
                  MessageBoxResult.Cancel,
                  (Style)Resources["MessageBoxStyle1"]
              );
                        //MessageBox.Show("Slika ne moze da ne stane na ekran");
                        return;
                    }
                    if (p.Y + i.Height / 2 >= canvasMap.Height)
                    {
                        Xceed.Wpf.Toolkit.MessageBox.Show(
                 "Ikonica ne moze da stane na mapu",
                  "Greska",
                  MessageBoxButton.OK,
                  MessageBoxImage.None,
                  MessageBoxResult.Cancel,
                  (Style)Resources["MessageBoxStyle1"]
              );
                        //MessageBox.Show("Slika ne moze da ne stane na ekran");
                        return;
                    }

                    i.SetValue(Canvas.LeftProperty, p.X - i.Width / 2);
                    i.SetValue(Canvas.TopProperty, p.Y - i.Height / 2);
                    
                    Console.WriteLine("............................"+i.Source);
                    canvasMap.Children.Add(i);

                    zivotinja.X = p.X;
                    zivotinja.Y = p.Y;
                    //zivotinja.Xstaro = p.X * 723.0 / canvasMap.Width;
                    //zivotinja.Ystaro = p.Y * 424.0 / canvasMap.Height;
                        
                    zivotinja.naMapi = true;
                    Zivotinje_mapa.Add(zivotinja);

                    WrapPanel wp = new WrapPanel();
                    wp.Orientation = Orientation.Vertical;

                    TextBlock id_ziv = new TextBlock();
                    id_ziv.IsEnabled = false;
                    id_ziv.Text = "Id zivotinje: " + zivotinja._oznaka;
                    wp.Children.Add(id_ziv);

                    TextBlock ime_ziv = new TextBlock();
                    ime_ziv.IsEnabled = false;
                    ime_ziv.Text = "Ime zivotinje: " + zivotinja._ime;
                    wp.Children.Add(ime_ziv);

                    ToolTip hint = new ToolTip();
                    hint.Content = wp;
                    i.ToolTip = hint;

                    ContextMenu cm = new ContextMenu();
                    i.ContextMenu = cm;
                    MenuItem mi = new MenuItem();
                    mi.Header = "Izbrisi";

                    mi.Icon = new Image
                    {
                        Source = new BitmapImage(new Uri("Images/rubbish-bin.png", UriKind.Relative))
                    };


                    cm.Items.Add(mi);

                    mi.Click += delegate (object s, RoutedEventArgs ev) { mi_Click(s, ev, i, zivotinja); };

                }

            }
        }

        private void mi_Click(object sender, RoutedEventArgs e, Image img, CZivotinja ziv)
        {
            canvasMap.Children.Remove(img);
            foreach(CZivotinja z in podaci.listaZivotinja)
            {
                if (ziv._oznaka.Equals(z._oznaka))
                {
                    z.naMapi = false;
                }
            }
            Zivotinje_mapa.Remove(ziv);
        }

        private void CanvasMap_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            List<double> posX = new List<double>();
            List<double> posY = new List<double>();

            foreach (CZivotinja z in Zivotinje_mapa)
            {
                posX.Add(z.X);
                posY.Add(z.Y);
            }
            int brojac = 0;

            Size staraVelicina = e.PreviousSize;
            Size novaVelicina = e.NewSize;


            foreach (Image i in canvasMap.Children)
            {
                if (novaVelicina.Height / staraVelicina.Height != double.PositiveInfinity)
                {
                    //skaliranje ikonice ipak necu to da radim
                    //i.Height = i.Height * novaVelicina.Height / staraVelicina.Height;
                    //i.Width = i.Width * novaVelicina.Width / staraVelicina.Width;

                    i.SetValue(Canvas.LeftProperty, ((posX[brojac] - i.Width / 2) * (novaVelicina.Width / staraVelicina.Width)));
                    i.SetValue(Canvas.TopProperty, ((posY[brojac] - i.Height / 2) * (novaVelicina.Height / staraVelicina.Height)));

                    Zivotinje_mapa[brojac].X = (posX[brojac] * (novaVelicina.Width / staraVelicina.Width));
                    Zivotinje_mapa[brojac].Y = (posY[brojac] * (novaVelicina.Height / staraVelicina.Height));

                }
                brojac++;
            }

            posX.Clear();
            posY.Clear();
        }









        //******************************************
        //obrisi sve sa mape
        private void BtnClearMap_Click(object sender, RoutedEventArgs e)
        {
           var res= Xceed.Wpf.Toolkit.MessageBox.Show(
               "Da li ste sigurni da zelite da sklonite sve zivotinje sa mape",
                "Obavestenje",
                MessageBoxButton.YesNo,
                MessageBoxImage.None,
                MessageBoxResult.Cancel,
                (Style)Resources["MessageBoxStyle1"]
            );
            if ("No" == res.ToString())
            {
                return;
            }
            if ("Yes" == res.ToString())
            {
                canvasMap.Children.Clear();
                Zivotinje_mapa.Clear();
            }

        }

        private void CbFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           
           /* Console.WriteLine("ISPIS");
            CZivotinja temp = new CZivotinja();
            if(cbFilter.Text.Equals("Po imenu A-Z"))
            {
                for(int i=0; i<pomocniPodaci.listaZivotinja.Count-1; i++)
                {
                    for(int j=i+1; j<pomocniPodaci.listaZivotinja.Count; j++)
                    {
                        if(String.Compare(pomocniPodaci.listaZivotinja[i]._ime, pomocniPodaci.listaZivotinja[j]._ime) == -1)
                        {
                            temp = pomocniPodaci.listaZivotinja[i];
                            pomocniPodaci.listaZivotinja[i] = pomocniPodaci.listaZivotinja[j];
                            pomocniPodaci.listaZivotinja[j] = temp;
                        }
                    }
                }

               // ListBox lb = new ListBox();
                myListBox.Items.Refresh();
                
                
            }
            if (cbFilter.Text.Equals("Po imenu Z-A"))
            {
                for (int i = 0; i < pomocniPodaci.listaZivotinja.Count - 1; i++)
                {
                    for (int j = i + 1; j < pomocniPodaci.listaZivotinja.Count; j++)
                    {
                        if (String.Compare(pomocniPodaci.listaZivotinja[i]._ime, pomocniPodaci.listaZivotinja[j]._ime) == 1)
                        {
                            temp = pomocniPodaci.listaZivotinja[i];
                            pomocniPodaci.listaZivotinja[i] = pomocniPodaci.listaZivotinja[j];
                            pomocniPodaci.listaZivotinja[j] = temp;
                        }
                    }
                }
                // ListBox lb = new ListBox();
                myListBox.Items.Refresh();
            }
            if (cbFilter.Text.Equals("Po tipu A-Z"))
            {
                for (int i = 0; i < pomocniPodaci.listaZivotinja.Count - 1; i++)
                {
                    for (int j = i + 1; j < pomocniPodaci.listaZivotinja.Count; j++)
                    {
                        if (String.Compare(pomocniPodaci.listaZivotinja[i]._tip.ToString(), pomocniPodaci.listaZivotinja[j]._tip.ToString()) == -1)
                        {
                            temp = pomocniPodaci.listaZivotinja[i];
                            pomocniPodaci.listaZivotinja[i] = pomocniPodaci.listaZivotinja[j];
                            pomocniPodaci.listaZivotinja[j] = temp;
                        }
                    }
                }
                // ListBox lb = new ListBox();
                myListBox.Items.Refresh();
            }
            if (cbFilter.Text.Equals("Po tipu Z-A"))
            {
                for (int i = 0; i < pomocniPodaci.listaZivotinja.Count - 1; i++)
                {
                    for (int j = i + 1; j < pomocniPodaci.listaZivotinja.Count; j++)
                    {
                        if (String.Compare(pomocniPodaci.listaZivotinja[i]._tip.ToString(), pomocniPodaci.listaZivotinja[j]._tip.ToString()) == 1)
                        {
                            temp = pomocniPodaci.listaZivotinja[i];
                            pomocniPodaci.listaZivotinja[i] = pomocniPodaci.listaZivotinja[j];
                            pomocniPodaci.listaZivotinja[j] = temp;
                        }
                    }
                }
                // ListBox lb = new ListBox();
                myListBox.Items.Refresh();
            }*/
        }

        private void CbFilter_GotFocus(object sender, RoutedEventArgs e)
        {
            int moze = 0;
            if (pomocniPodaci != null)
            {
                moze = 1;
            }
            //Console.WriteLine("ISPIS");
            CZivotinja temp = new CZivotinja();
            if (cbFilter.Text.Equals("Po imenu A-Z") && moze==1)
            {
                for (int i = 0; i < pomocniPodaci.listaZivotinja.Count - 1; i++)
                {
                    for (int j = i + 1; j < pomocniPodaci.listaZivotinja.Count; j++)
                    {
                        if (String.Compare(pomocniPodaci.listaZivotinja[i]._ime, pomocniPodaci.listaZivotinja[j]._ime) == 1)
                        {
                            temp = pomocniPodaci.listaZivotinja[i];
                            pomocniPodaci.listaZivotinja[i] = pomocniPodaci.listaZivotinja[j];
                            pomocniPodaci.listaZivotinja[j] = temp;
                        }
                    }
                }

                // ListBox lb = new ListBox();
                myListBox.Items.Refresh();


            }
            if (cbFilter.Text.Equals("Po imenu Z-A") && moze == 1)
            {
                for (int i = 0; i < pomocniPodaci.listaZivotinja.Count - 1; i++)
                {
                    for (int j = i + 1; j < pomocniPodaci.listaZivotinja.Count; j++)
                    {
                        if (String.Compare(pomocniPodaci.listaZivotinja[i]._ime, pomocniPodaci.listaZivotinja[j]._ime) == -1)
                        {
                            temp = pomocniPodaci.listaZivotinja[i];
                            pomocniPodaci.listaZivotinja[i] = pomocniPodaci.listaZivotinja[j];
                            pomocniPodaci.listaZivotinja[j] = temp;
                        }
                    }
                }
                // ListBox lb = new ListBox();
                myListBox.Items.Refresh();
            }
            if (cbFilter.Text.Equals("Po tipu A-Z") && moze == 1)
            {
                for (int i = 0; i < pomocniPodaci.listaZivotinja.Count - 1; i++)
                {
                    for (int j = i + 1; j < pomocniPodaci.listaZivotinja.Count; j++)
                    {
                        if (String.Compare(pomocniPodaci.listaZivotinja[i]._tip.ToString(), pomocniPodaci.listaZivotinja[j]._tip.ToString()) == 1)
                        {
                            temp = pomocniPodaci.listaZivotinja[i];
                            pomocniPodaci.listaZivotinja[i] = pomocniPodaci.listaZivotinja[j];
                            pomocniPodaci.listaZivotinja[j] = temp;
                        }
                    }
                }
                // ListBox lb = new ListBox();
                myListBox.Items.Refresh();
            }
            if (cbFilter.Text.Equals("Po tipu Z-A") && moze == 1)
            {
                for (int i = 0; i < pomocniPodaci.listaZivotinja.Count - 1; i++)
                {
                    for (int j = i + 1; j < pomocniPodaci.listaZivotinja.Count; j++)
                    {
                        if (String.Compare(pomocniPodaci.listaZivotinja[i]._tip.ToString(), pomocniPodaci.listaZivotinja[j]._tip.ToString()) == -1)
                        {
                            temp = pomocniPodaci.listaZivotinja[i];
                            pomocniPodaci.listaZivotinja[i] = pomocniPodaci.listaZivotinja[j];
                            pomocniPodaci.listaZivotinja[j] = temp;
                        }
                    }
                }
                // ListBox lb = new ListBox();
                myListBox.Items.Refresh();
            }
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
