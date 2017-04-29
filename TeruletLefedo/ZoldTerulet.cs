using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TeruletLefedo
{
    abstract class ZoldTerulet
    {
        public abstract FeluletTipusok FeluletTipus { get; set; }
        public abstract double Szelesseg { get; set; }
        public abstract double Magassag { get; set; }

        // MegrendeloID: listába rendezés megfosztotta a ZöldTerületeket a Megrendelőktől, így generálásnál kapnak egy ID-t
        public abstract int MegrendeloID { get; set; }
        public abstract double Terulet { get; }
        public abstract int LefedettsegMin { get; set; }
    }

    enum FeluletTipusok
    {
        Park, SportPalya, Kutyafuttato, Strand, BelsoKert, JatszoTer
    }
}
