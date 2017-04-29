using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TeruletLefedo
{
    class ParkGyep : IGyepElem, IComparable
    {
        public ParkGyep(double szelesseg, double magassag)
        {
            this._termekNev = "ParkGyep";
            this._szelesseg = szelesseg;
            this._magassag = magassag;
        }

        #region Vars

        private double _magassag;
        private double _szelesseg;
        private string _termekNev;

        #endregion

        #region Props

        public double Magassag
        {
            get
            {
                return _magassag;
            }

            set
            {
                _magassag = value;
            }
        }

        public double Szelesseg
        {
            get
            {
                return _szelesseg;
            }

            set
            {
                _szelesseg = value;
            }
        }

        public string TermekNev
        {
            get
            {
                return _termekNev;
            }

            set
            {
                _termekNev = value;
            }
        }

        public double Terulet
        {
            get
            {
                return _magassag * _szelesseg;
            }
        }

        #endregion

        #region Methods

        // 0 > current instance precedes the object specified by the CompareTo method in the sort order
        // 0: current instance occurs in the same position as the object specified by the method
        // 0 < current instance follows the object specified bye the method
        public int CompareTo(object obj)
        {
            if (obj == null)
            {
                return 1;
            }
            IGyepElem elem = obj as IGyepElem;
            if (elem != null)
            {
                return this.Terulet.CompareTo(elem.Terulet);
            }
            else
            {
                // TODO: catch, cw
                throw new ArgumentException("A megadott objektum nem GyepElem típusú.");
            }
        }

        public override string ToString()
        {
            string gyepStr = "Termek adatai: \nNev: " + TermekNev + "\nSzelesseg: " + Szelesseg + "\nMagassag: " + Magassag + "\n------------------------";
            return gyepStr;
        }

        #endregion
    }
}
