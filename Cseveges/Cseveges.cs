using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace Cseveges
{
    class Beszelgetes
    {
        public DateTime kezdet { get; private set; }
        public DateTime veg { get; private set; }
        public string kezdemenyezo { get; private set; }
        public string fogado { get; private set; }
        public TimeSpan eltelt => veg - kezdet;
        public Beszelgetes(string sor)
        {
            string[] sordarabok = sor.Split(';');
            kezdet = DateTime.ParseExact(sordarabok[0], "yy.MM.dd-HH:mm:ss", CultureInfo.InvariantCulture);
            veg = DateTime.ParseExact(sordarabok[1], "yy.MM.dd-HH:mm:ss", CultureInfo.InvariantCulture);
            kezdemenyezo = sordarabok[2];
            fogado = sordarabok[3];
        }
    }
    class Cseveges
    {
        static void Main()
        {
            List<Beszelgetes> beszelgetesek = new List<Beszelgetes>();
            foreach (var sor in File.ReadAllLines("csevegesek.txt").Skip(1))
            {
                beszelgetesek.Add(new Beszelgetes(sor));
            }
            HashSet<string> tagok = new HashSet<string>();
            foreach (var tag in File.ReadAllLines("tagok.txt")) tagok.Add(tag);

            Console.WriteLine($"4. feladat: Tagok száma: {tagok.Count}fő - Beszélgetések: {beszelgetesek.Count}db");

            Console.WriteLine($"5. feladat: Leghosszabb beszélgetés adatai");
            Beszelgetes maxHosszBeszelgetes = beszelgetesek[0];
            foreach (var e in beszelgetesek)
            {
                if (e.eltelt > maxHosszBeszelgetes.eltelt)
                {
                    maxHosszBeszelgetes = e;
                }
            }
            Console.WriteLine($"\tKezdeményező: {maxHosszBeszelgetes.kezdemenyezo}");
            Console.WriteLine($"\tFogadó:       {maxHosszBeszelgetes.fogado}");
            Console.WriteLine($"\tKezdete:      {maxHosszBeszelgetes.kezdet.ToString("yy.MM.dd-HH:mm:ss")}");
            Console.WriteLine($"\tVége:         {maxHosszBeszelgetes.veg.ToString("yy.MM.dd-HH:mm:ss")}");
            Console.WriteLine($"\tHossz:        {maxHosszBeszelgetes.eltelt.TotalSeconds}mp");

            Console.Write("6. feladat: Adja meg egy tag nevét: ");
            string? inputTag = Console.ReadLine();
            TimeSpan szumIdo = new TimeSpan();
            foreach (var e in beszelgetesek)
            {
                if (e.kezdemenyezo == inputTag || e.fogado == inputTag)
                {
                    szumIdo += e.eltelt;
                }
            }
            Console.WriteLine($"\tA beszélgetések összes ideje: {szumIdo}");

            Console.WriteLine("7. feladat: Nem beszélgettek senkivel");
            HashSet<string> beszelgetok = new HashSet<string>();
            foreach (var e in beszelgetesek)
            {
                beszelgetok.Add(e.kezdemenyezo);
                beszelgetok.Add(e.fogado);
            }
            foreach (var e in tagok.Except(beszelgetok))  Console.WriteLine($"\t{e}");

            Console.WriteLine("8. feladat: Leghosszabb csendes időszak 15h-tól");
            DateTime maxCsendKezdete = new DateTime(2021, 9, 27, 15, 0, 0);
            DateTime maxCsendVege = beszelgetesek[0].kezdet;
            TimeSpan maxCsendHossz = maxCsendVege - maxCsendKezdete;
            DateTime aktCsendVege = beszelgetesek[0].veg;

            foreach (var b in beszelgetesek.Skip(1))
            {
                if (b.kezdet > aktCsendVege)
                {
                    TimeSpan aktCsendHossz = b.kezdet - aktCsendVege;
                    if (aktCsendHossz > maxCsendHossz)
                    {
                        maxCsendHossz = aktCsendHossz;
                        maxCsendKezdete = aktCsendVege;
                        maxCsendVege = b.kezdet;
                    }
                }
                if (b.veg > aktCsendVege) aktCsendVege = b.veg;
            }
            Console.WriteLine($"\tKezdete: {maxCsendKezdete.ToString("yy.MM.dd-HH:mm:ss")}");
            Console.WriteLine($"\tVége:    {maxCsendVege.ToString("yy.MM.dd-HH:mm:ss")}");
            Console.WriteLine($"\tHossza:  {maxCsendHossz}");            
        }
    }
}