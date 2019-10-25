using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCIProjekat.Model
{
    public class KolekcijaTipova
    {
        public string _ime
        {
            get;
            set;
        }
        public List<Tip> _tipovi
        {
            get;
            set;
        }

        public KolekcijaTipova(string ime)
        {
            this._ime = ime;
            this._tipovi = new List<Tip>();
        }

         
    }
}
