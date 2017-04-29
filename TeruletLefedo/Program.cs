using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TeruletLefedo
{
    class Program
    {
        public static LancoltLista<ZoldTerulet> feluletek;
        static void Main(string[] args)
        {
            int MEGRENDELOK_SZAMA = 10;
            int GYEPELEMEK_SZAMA = 10000;

            Console.WriteLine("\n***START***");

            List<Megrendelo> megrendeloLista = MegrendeloGenerator(MEGRENDELOK_SZAMA);
            megrendeloLista.Sort(); // minél több megrendelő kiszolgálása érdekében sorbarendezés növekvő sorrendben

            LancoltLista<IGyepElem> gyepElemek;
            feluletek = FeluleteketLancoltListabaTesz(megrendeloLista);

            try
            {
                gyepElemek = GyepRaktar(GYEPELEMEK_SZAMA);
                Lefedo kivitelezes = new Lefedo(megrendeloLista, gyepElemek, feluletek);
                MegrendelesErtesito ertesito = new MegrendelesErtesito();
                kivitelezes.MegrendelesElkeszult += ertesito.OnMegrendelesElkeszult;
                kivitelezes.MegrendelesekElkeszitese();

                Console.WriteLine("A kiszolgált megrendelők száma: " + kivitelezes.KiszolgaltMegrendelokSzama);
            }
            catch (GyepElemTulNagyKivetel gy)
            {
                Console.WriteLine("A bekerült gyeptégla területe ({0}) nagyobb, mint a legkisebb zöldterületé ({1})!", gy.GyepElem.Terulet,gy.ZoldTerulet.Terulet);

            }

            Console.WriteLine("\n***STOP***\n");

            Console.ReadLine();
        }

        public static LancoltLista<ZoldTerulet> FeluleteketLancoltListabaTesz(List<Megrendelo> mLista)
        {
            LancoltLista<ZoldTerulet> feluletek = new LancoltLista<ZoldTerulet>();

            foreach (Megrendelo m in mLista)
            {
                for (int i = 0; i < m.FeluletekSzama; i++)
                {
                    feluletek.BeszurCsokkenoSorrendben(m.Feluletek[i].Terulet, m.Feluletek[i]);
                }
            }

            return feluletek;
        }

        public static List<Megrendelo> MegrendeloGenerator(int megrendelokSzama)
        {
            List<Megrendelo> megrendeloLista = new List<Megrendelo>();

            for (int i = 0; i < megrendelokSzama; i++)
            {
                megrendeloLista.Add(new Megrendelo(R.Next(1, 8), i + 1));
            }

            return megrendeloLista;
        }

        static Random R = new Random();
        public static LancoltLista<IGyepElem> GyepRaktar(int gyepekSzama)
        {
            LancoltLista<IGyepElem> gyepRaktar = new LancoltLista<IGyepElem>();
            IGyepElem gyepElem;
            int random;
            ZoldTerulet legkisebbTerulet = feluletek.GetUtolsoElem();

            for (int i = 0; i < gyepekSzama; i++)
            {
                random = R.Next(0,4);
                if (random == 0)
                {
                    gyepElem = new KertiGyep(R.Next(1, 10), R.Next(1, 10));
                }
                else if (random == 1)
                {
                    gyepElem = new MediterranGyep(R.Next(1, 10), R.Next(1, 10));
                }
                else if (random == 2)
                {
                    gyepElem = new ParkGyep(R.Next(1, 10), R.Next(1, 10));
                }
                else
                {
                    gyepElem = new SportGyep(R.Next(1, 10), R.Next(1, 10));
                }

                if (gyepElem.Terulet > legkisebbTerulet.Terulet)
                {
                    // A rendszer váltson ki kivételt, ha olyan méretű gyeptégla kerül be, amely meghaladja a legkisebb zöldterület méretét.
                    throw new GyepElemTulNagyKivetel(gyepElem, legkisebbTerulet);
                }
                gyepRaktar.BeszurCsokkenoSorrendben(gyepElem.Terulet, gyepElem);
            }

            return gyepRaktar;
        }
    }
}
