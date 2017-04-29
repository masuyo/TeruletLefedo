using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TeruletLefedo
{
    // maximum 8 felület
    class Megrendelo : IComparable
    {
        public Megrendelo(int feluletekSzama, int ID)
        {
            this._feluletekSzama = feluletekSzama;
            this._osszTerulet = 0;
            this._id = ID;

            Feluletek = new ZoldTerulet[8];

            for (int i = 0; i < feluletekSzama; i++)
            {
                Feluletek[i] = RandomTerulet(ID);
                _osszTerulet += Feluletek[i].Terulet;
            }
        }

        private ZoldTerulet[] feluletek;
        internal ZoldTerulet[] Feluletek
        {
            get
            {
                return feluletek;
            }

            set
            {
                feluletek = value;
            }
        }

        private int _feluletekSzama;
        public int FeluletekSzama
        {
            get
            {
                return _feluletekSzama;
            }

            set
            {
                _feluletekSzama = value;
            }
        }

        private double _osszTerulet;
        public double OsszTerulet
        {
            get { return _osszTerulet; }
            set { _osszTerulet = value; }
        }

        private int _id;
        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }


        static Random R = new Random();
        public ZoldTerulet RandomTerulet(int megrendeloiD)
        {
            int random = R.Next(3);
            int kozRandom = R.Next(4);

            if (random == 0)
            {
                if (kozRandom == 0)
                {
                    return new KozPark(FeluletTipusok.Park, R.Next(10, 300), R.Next(150, 350), megrendeloiD, R.Next(85,100));
                }
                else if (kozRandom == 1)
                {
                    return new KozPark(FeluletTipusok.Kutyafuttato, R.Next(10, 300), R.Next(150, 350), megrendeloiD, R.Next(85, 100));
                }
                else if (kozRandom == 2)
                {
                    return new KozPark(FeluletTipusok.Strand, R.Next(10, 300), R.Next(150, 350), megrendeloiD, R.Next(85, 100));
                }
                else if (kozRandom == 3)
                {
                    return new KozPark(FeluletTipusok.JatszoTer, R.Next(10, 300), R.Next(150, 350), megrendeloiD, R.Next(85, 100));
                }
            }
            else if (random == 1)
            {
                return new MaganKert(FeluletTipusok.BelsoKert,R.Next(10,100),R.Next(15,150), megrendeloiD, R.Next(85, 100));
            }
            return new FutballStadion(FeluletTipusok.SportPalya,R.Next(50,100),R.Next(100,130), megrendeloiD, R.Next(85, 100));
        }

        public int CompareTo(object obj)
        {
            if (obj == null)
            {
                return 1;
            }
            Megrendelo m = obj as Megrendelo;
            if (m != null)
            {
                return (this.OsszTerulet).CompareTo(m.OsszTerulet);
            }
            else
            {
                throw new ArgumentException("A megadott objektum nem Megrendelo tipusu.");
            }
        }
    }
}
