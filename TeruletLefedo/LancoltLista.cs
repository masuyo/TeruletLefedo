using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TeruletLefedo
{
    class LancoltLista<TContent>
    {
        class ListaElem
        {
            public TContent tartalom;
            public ListaElem kovetkezo;
            public double kulcs;
        }

        ListaElem fej;

        // desc
        public void BeszurCsokkenoSorrendben(double kulcs, TContent tartalom)
        {
            ListaElem ujElem = new ListaElem();
            ujElem.kulcs = kulcs;
            ujElem.tartalom = tartalom;
            ListaElem p = fej;
            ListaElem e = null;

            while (p != null && p.kulcs >= kulcs)
            {
                e = p;
                p = p.kovetkezo;
            }
            if (e == null)
            {
                ujElem.kovetkezo = fej;
                fej = ujElem;
            }
            else
            {
                ujElem.kovetkezo = p;
                e.kovetkezo = ujElem;
            }
        }

        public TContent GetUtolsoElem()
        {
            TContent utolso;

            if (fej != null)
            {
                ListaElem elemBejaro = fej;
                while (elemBejaro.kovetkezo != null)
                {
                    elemBejaro = elemBejaro.kovetkezo;
                }
                utolso = elemBejaro.tartalom;
            }
            else
            {
                utolso = default(TContent);
                Console.WriteLine("Hiba: A lista üres.");
            }

            return utolso;
        }

        public TContent GetAktElem(int elemSorszam)
        {
            if (fej != null)
            {
                ListaElem elemBejaro = fej;
                int i = 1;
                while (elemBejaro != null)
                {
                    if (i == elemSorszam)
                    {
                        return elemBejaro.tartalom;
                    }
                    i++;
                    elemBejaro = elemBejaro.kovetkezo;
                }
            }
            //Console.WriteLine("A lista üres.");
            return default(TContent);
        }

        public void Torles(TContent tartalom)
        {
            if (fej != null)
            {
                ListaElem elemBejaro = fej;
                ListaElem elemBejaroSzulo = null;
                while (elemBejaro != null && !elemBejaro.tartalom.Equals(tartalom))
                {
                    elemBejaroSzulo = elemBejaro;
                    elemBejaro = elemBejaro.kovetkezo;
                }
                if (elemBejaro != null)
                {
                    if (elemBejaroSzulo == null)
                    {
                        fej = elemBejaro.kovetkezo;
                    }
                    else
                    {
                        elemBejaroSzulo.kovetkezo = elemBejaro.kovetkezo;
                    }
                }
                else
                {
                    Console.WriteLine("Nem található listaelem.");
                }
            }
            else
            {
                Console.WriteLine("A lista üres.");
            }
        }

        public void Kereses(TContent tartalom)
        {
            if (fej != null)
            {
                ListaElem elemBejaro = fej;
                while (elemBejaro != null && !elemBejaro.tartalom.Equals(tartalom))
                {
                    elemBejaro = elemBejaro.kovetkezo;
                }
                if (elemBejaro != null)
                {
                    Console.WriteLine("Megtalált listaelem, tartalma: {0}", elemBejaro.tartalom);
                }
                else
                {
                    Console.WriteLine("Nem található listaelem.");
                }
            }
            else
            {
                Console.WriteLine("A lista üres.");
            }
        }

        public bool UresLista()
        {
            if (fej != null)
            {
                return false;
            }
            return true;
        }
    }
}
