using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCIProjekat.Model
{
    public class CPodaci
    {
        public ObservableCollection <CZivotinja> listaZivotinja { get; set; }
        public ObservableCollection<Etiketa> listaEtiketa { get; set; }
        public ObservableCollection<Tip> listaTipova { get; set; }
        public ObservableCollection<KolekcijaTipova> listaKolekcijaTipova { get; set; }

        public CPodaci()
        {
            listaZivotinja = new ObservableCollection<CZivotinja>();
            listaEtiketa = new ObservableCollection<Etiketa>();
            listaTipova = new ObservableCollection<Tip>();
            listaKolekcijaTipova = new ObservableCollection<KolekcijaTipova>();
        }
    }
}
