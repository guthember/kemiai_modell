using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace modell
{
    class Modell
    {
        private char[,] tomb = new char[10, 10];
        private bool elso;

        private int cDarab;

        public int cDB
        {
            get { return cDarab; }
        }


        private int aDarab;

        public int aDB
        {
            get { return aDarab; }
        }

        private int bDarab;

        public int bDB
        {
            get { return bDarab; }
        }


        public Modell()
        {
            aDarab = 0;
            bDarab = 0;
            cDarab = 0;
            elso = true;
        }


        public void Kiir()
        {
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    Console.Write(tomb[i, j]);
                    Console.Write(' ');
                }
                Console.WriteLine();
            }
            Console.WriteLine("A: {0}", aDB);
            Console.WriteLine("B: {0}", bDB);
            Console.WriteLine("C: {0}", cDB);
        }

        private void KezdVeg(int mit, out int k, out int v) {
            if (mit == 0)
            {
                k = 0;
            }
            else
            {
                k = mit - 1;
            }

            if (mit == 9)
            {
                v = 9;
            }
            else
            {
                v = mit + 1;
            }
        }

        private void BomlasFeltolt()
        {
            Random vel = new Random(Guid.NewGuid().GetHashCode());
            double szam;
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    szam = vel.NextDouble();
                    if (szam < 0.5)
                    {
                        tomb[i, j] = 'C';
                        cDarab++;
                    }
                    else
                    {
                        tomb[i, j] = ' ';
                    }
                }
            }
        }


        public void Bomlas() 
        {
            if (elso)
            {
                BomlasFeltolt();
                elso = false;
            }

            Random vel = new Random(Guid.NewGuid().GetHashCode());
            
            int k, l;
            int i, j;
            int kezd, veg;

            i = vel.Next(0, 10);
            j = vel.Next(0, 10);

            do
            {
                KezdVeg(i, out kezd, out veg);
                k = vel.Next(kezd, veg + 1);
                KezdVeg(j, out kezd, out veg);
                l = vel.Next(kezd, veg + 1);
            } while (i == k && j == l);

            Console.WriteLine(i.ToString() + "," + j.ToString() + "-->" + k.ToString() + "," + l.ToString());
            Console.WriteLine(tomb[i, j].ToString());
            Console.WriteLine(tomb[k, l].ToString());


            if (tomb[i, j] == 'C' && tomb[k, l] == ' ')
            {
                if (vel.NextDouble() < 0.2)
                {
                    tomb[i, j] = 'A';
                    tomb[k, l] = 'B';
                    aDarab++;
                    bDarab++;
                    cDarab--;
                }
            }

            char tmp;
            tmp = tomb[i, j];
            tomb[i, j] = tomb[k, j];
            tomb[k, j] = tmp;
        }

        private void EgyesulesFeltolt() 
        {
            Random vel = new Random(Guid.NewGuid().GetHashCode());
            double szam;
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    szam = vel.NextDouble();
                    if (szam < 0.5)
                    {
                        tomb[i, j] = 'A';
                        aDarab++;
                    }
                    else if (szam >= 0.5)
                    {
                        tomb[i, j] = 'B';
                        bDarab++;
                    }
                    else
                    {
                        tomb[i, j] = ' ';
                    }
                }
            }
        }

        public void Egyesules()
        {
            if (elso)
            {
                EgyesulesFeltolt();
                elso = false;
            }
            Random vel = new Random(Guid.NewGuid().GetHashCode());
            int k, l;
            int i, j;
            int kezd, veg;

            i = vel.Next(0, 10);
            j = vel.Next(0, 10);

            do
            {
                KezdVeg(i, out kezd, out veg);
                k = vel.Next(kezd, veg + 1);
                KezdVeg(j, out kezd, out veg);
                l = vel.Next(kezd, veg + 1); 
            } while (i == k && j == l );



            //Console.WriteLine(i.ToString() + "," + j.ToString() + "-->" + k.ToString() + "," + l.ToString());
            //Console.WriteLine(tomb[i, j].ToString());
            //Console.WriteLine(tomb[k, l].ToString());
            
            if ((tomb[i, j] == 'A' && tomb[k, l] == 'B') || (tomb[i, j] == 'B' && tomb[k, l] == 'A'))
            {
                if (vel.NextDouble() < 0.5)
                {
                    aDarab--;
                    bDarab--;
                    tomb[i, j] = 'C';
                    tomb[k, l] = ' ';
                    cDarab++;
                }
            }
            char tmp;
            tmp = tomb[i, j];
            tomb[i, j] = tomb[k, j];
            tomb[k, j] = tmp;
        }
    }
    class Program
    {

        static void Main(string[] args)
        {
            //Random vel = new Random();
            //double szam;
            //int db = 0;
            //for (int i = 0; i < 100; i++)
            //{
            //    szam = vel.NextDouble();
            //    Console.WriteLine(szam);
            //    if (szam < 0.1)
            //    {
            //        db++;
            //    }
            //}
            //Console.WriteLine("Darab "+db);
            //Console.ReadKey();
            FileStream file = new FileStream("adatok.txt", FileMode.Create);
            StreamWriter fileKi = new StreamWriter(file);
            Modell m = new Modell();

            for (int i = 0; i < 10000; i++)
            {
                //m.Kiir();
                m.Bomlas();
                //m.Egyesules();
                //Console.ReadKey();
                //System.Threading.Thread.Sleep(100);
                Console.Clear();
                
                fileKi.WriteLine(m.aDB.ToString() + ";" + m.bDB.ToString() + ";" + m.cDB.ToString());
            }

            fileKi.Close();
            file.Close();

            Console.ReadKey();
        }


    }
}
