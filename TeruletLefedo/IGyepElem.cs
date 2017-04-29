using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TeruletLefedo
{
    interface IGyepElem
    {
        string TermekNev { get; set; }
        double Szelesseg { get; set; }
        double Magassag { get; set; }

        // minden gyepelemet téglalap alakúnak tekintünk
        double Terulet { get; /*set;*/ }
    }
}
