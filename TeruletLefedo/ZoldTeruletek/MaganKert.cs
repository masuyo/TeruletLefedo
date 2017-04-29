using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TeruletLefedo
{
    class MaganKert : ZoldTerulet, IComparable
    {
        public MaganKert(FeluletTipusok feluletTipus, double szelesseg, double magassag, int megrendeloID, int lefedettsegMin)
        {
            this._feluletTipus = feluletTipus;
            this._szelesseg = szelesseg;
            this._magassag = magassag;
            this._megrendeloID = megrendeloID;
            this._lefedettsegMin = lefedettsegMin;
        }

        #region Vars

        private FeluletTipusok _feluletTipus;
        private double _magassag;
        private double _szelesseg;
        private int _megrendeloID;
        private int _lefedettsegMin;

        #endregion

        #region Props

        public override double Terulet
        {
            get { return _szelesseg * _magassag; }
        }
        public override FeluletTipusok FeluletTipus
        {
            get
            {
                return _feluletTipus;
            }

            set
            {
                _feluletTipus = value;
            }
        }

        public override double Magassag
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

        public override double Szelesseg
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

        public override int MegrendeloID
        {
            get
            {
                return _megrendeloID;
            }

            set
            {
                _megrendeloID = value;
            }
        }

        public override int LefedettsegMin
        {
            get
            {
                return _lefedettsegMin;
            }

            set
            {
                _lefedettsegMin = value;
            }
        }

        #endregion

        #region Methods

        public int CompareTo(object obj)
        {
            if (obj == null)
            {
                return 1;
            }
            ZoldTerulet zt = obj as ZoldTerulet;
            if (zt != null)
            {
                return (this.Terulet).CompareTo(zt.Terulet);
            }
            else
            {
                // TODO: catch, cw
                throw new ArgumentException("A megadott objektum nem ZoldTerulet tipusu.");
            }
        }

        public override string ToString()
        {
            string mkStr = "A MaganKert adatai:\n Tipus: " + FeluletTipus + "\n Szelesseg: " + Szelesseg + "\n Magassag: " + Magassag + "\n Terulet: " + Terulet;
            return mkStr;
        }

        #endregion
    }
}
