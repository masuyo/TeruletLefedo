using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TeruletLefedo
{
    class Lefedo
    {
        public delegate void MegrendelesElkeszultEventHandler(LancoltLista<ZoldTerulet> T);

        public event MegrendelesElkeszultEventHandler MegrendelesElkeszult;

        public Lefedo(List<Megrendelo> megrendelok, LancoltLista<IGyepElem> elemek, LancoltLista<ZoldTerulet> teruletek)
        {
            this.Megrendelok = megrendelok;
            this.Elemek = elemek;
            this.Teruletek = teruletek;
        }

        List<Megrendelo> Megrendelok;
        LancoltLista<IGyepElem> Elemek;
        LancoltLista<ZoldTerulet> Teruletek;
        LancoltLista<ZoldTerulet> ElkeszultTeruletek;
        int _kiszolgaltMegrendelokSzama;

        public int KiszolgaltMegrendelokSzama
        {
            get
            {
                return _kiszolgaltMegrendelokSzama;
            }

            set
            {
                _kiszolgaltMegrendelokSzama = value;
            }
        }

        public void MegrendelesekElkeszitese()
        {
            _kiszolgaltMegrendelokSzama = 0;

            foreach (Megrendelo m in Megrendelok)
            {
                if (AktMegrendeloKiszolgalasa(m))
                {
                    _kiszolgaltMegrendelokSzama++;
                }
            }
        }

        // egy adott megrendelőt szolgál ki; ha az összes telkét sikerült lefedni, igazzal tér vissza
        public bool AktMegrendeloKiszolgalasa(Megrendelo M)
        {
            int i = 0;
            int j = 1;
            ZoldTerulet z;
            ElkeszultTeruletek = new LancoltLista<ZoldTerulet>();
            LancoltLista<IGyepElem> kivettGyepElemek = new LancoltLista<IGyepElem>();

            while (i < M.FeluletekSzama)
            {
                z = Teruletek.GetAktElem(j);
                // a korábban sorbarendezett Megrendelők ID-i alapján keresi ki a felületek láncolt listájából a lefedendő felületet
                if (z.MegrendeloID == M.ID && AktTeruletLefedo(z, kivettGyepElemek))
                {
                    Teruletek.Torles(z);
                    ElkeszultTeruletek.BeszurCsokkenoSorrendben(z.Terulet,z);
                    i++;
                }
                else
                {
                    if (z.MegrendeloID == M.ID)
                    {
                        return false;
                    }
                    j++;
                }
            }
            if (i >= M.FeluletekSzama)
            {
                OnMegrendelesElkeszult(ElkeszultTeruletek);
                return true;
            }
            ListaElemeketAtrak(kivettGyepElemek,Elemek);
            return false;
        }

        // egy telket fed le, mohó szeretne lenni
        public bool AktTeruletLefedo(ZoldTerulet Z, LancoltLista<IGyepElem> kivettGyepElemek)
        {
            int t = 1;
            double osszLefedettTerulet = 0;
            double lefedettsegMerteke = 0;
            int futasokSzama = 0;
            IGyepElem aktGyepElem;

            LancoltLista<IGyepElem> AktTeruletElemei = new LancoltLista<IGyepElem>();
            bool tulszamolt = false;

            while (osszLefedettTerulet < Z.Terulet && !Elemek.UresLista() && lefedettsegMerteke < Z.LefedettsegMin && !tulszamolt)
            {
                aktGyepElem = Elemek.GetAktElem(t);

                tulszamolt = (aktGyepElem == null);
                
                if (aktGyepElem != null && (osszLefedettTerulet + aktGyepElem.Terulet) < Z.Terulet)
                {
                    if (Z is MaganKert && (aktGyepElem is KertiGyep || aktGyepElem is SportGyep))
                    {
                        osszLefedettTerulet += aktGyepElem.Terulet;
                        AktTeruletElemei.BeszurCsokkenoSorrendben(aktGyepElem.Terulet, aktGyepElem);
                        Elemek.Torles(aktGyepElem);
                    }
                    else if (Z is KozPark && (aktGyepElem is ParkGyep || aktGyepElem is SportGyep))
                    {
                        osszLefedettTerulet += aktGyepElem.Terulet;
                        AktTeruletElemei.BeszurCsokkenoSorrendben(aktGyepElem.Terulet, aktGyepElem);
                        Elemek.Torles(aktGyepElem);
                    }
                    else if (Z is FutballStadion && aktGyepElem is SportGyep)
                    {
                        osszLefedettTerulet += aktGyepElem.Terulet;
                        AktTeruletElemei.BeszurCsokkenoSorrendben(aktGyepElem.Terulet, aktGyepElem);
                        Elemek.Torles(aktGyepElem);
                    }
                    else
                    {
                        t++;
                    }
                }
                else
                {
                    t++;
                }
                futasokSzama++;

                lefedettsegMerteke = 100 * osszLefedettTerulet / Z.Terulet;
            }

            if (lefedettsegMerteke >= Z.LefedettsegMin)
            {
                // ha sikerült lefedni a területet - megrendelő nem kerül kiszolgálásra, ha nem sikerül minden területét lefedni
                ListaElemeketAtrak(AktTeruletElemei, kivettGyepElemek);
                return true;
            }
            // ha nem sikerült lefedni a területet
            ListaElemeketAtrak(AktTeruletElemei, Elemek);
            return false;
        }

        public void OnMegrendelesElkeszult(LancoltLista<ZoldTerulet> T)
        {
            if (MegrendelesElkeszult != null)
            {
                MegrendelesElkeszult(T);
            }
        }

        public void ListaElemeketAtrak(LancoltLista<IGyepElem> Honnan, LancoltLista<IGyepElem> Hova)
        {
            IGyepElem aktGyepElem;
            while (!Honnan.UresLista())
            {
                aktGyepElem = Honnan.GetAktElem(1);
                Hova.BeszurCsokkenoSorrendben(aktGyepElem.Terulet, aktGyepElem);
                Honnan.Torles(aktGyepElem);
            }
        }
    }

    class MegrendelesErtesito
    {
        public void OnMegrendelesElkeszult(LancoltLista<ZoldTerulet> T)
        {
            int i = 1;
            Console.WriteLine("\nMegrendelés elkészült, telkek: \n");
            while (T.GetAktElem(i) != null)
            {
                Console.WriteLine(T.GetAktElem(i).ToString());
                Console.WriteLine();
                i++;
            }
        }
    }
}
