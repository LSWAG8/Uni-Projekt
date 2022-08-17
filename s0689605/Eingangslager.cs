using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Beleg2019
{
    class Eingangslager : Lager
    {
        
        /// <summary>
        /// Konstruktor des Eingangslagers
        /// </summary>
        /// <param name="name">Name des Eingangslagers</param>
        /// <param name="position">Standort des Eingangslagers</param>        
        public Eingangslager(string name, string position) : base(name, position)
        {
            //Zum vermeiden bei try catch
            if (name != null)
            {
                // Teile nach Eingangslagerkonfiguration erzeugen          
                InitialisiereBestand(@"..\..\Eingangslager.csv");
            }
        }

        /// <summary>
        /// Erstellt die Teile basierend auf der Datei Eingangslager.csv
        /// </summary>
        /// <param name="pfad">Dateipfad der Einlagerungskonfigurationsdatei</param>
        public void InitialisiereBestand(string pfad)
        {
            _Internal.Ausgabe("Initialisiere Bestand");

            //Reader zum Lesen der Datei
            var reader = new StreamReader(pfad);
            //Schleife um Datei durchzugehen
            while (!reader.EndOfStream)
            
            {

                // einzelne Zeile einlesen 
                String line = reader.ReadLine();
                // Teile der Zeile werden getrennt (Semikoleon ist Trennzeichen)
                String[] _values = line.Split(';');
                // die erste Spalte gibt die ID des Teils an
                String teil_id = _values[0];
                // die zweite Spalte gibt die erforderlichen Arbeitsschritte an
                // Teile des Arbeitsschritt-Strings werden getrennt
                String[] erforderlichearbeit = _values[1].Split(',');
                // Liste der erforderlichen Arbeitsschritte wird angelegt
                List<Verarbeitungsschritt> schritte = new List<Verarbeitungsschritt>();
                // alle getrennten Prozessschritte werden einzeln bearbeitet
                foreach (string prozessschritt in erforderlichearbeit)
                {

                    //Die gefundenen Prozessschritte werden dem Rezept hinzugefügt, wenn Sie vom Typ Prozessschritt sind
                    schritte.Add((Verarbeitungsschritt)Enum.Parse(typeof(Verarbeitungsschritt), prozessschritt));
                }
               
                if (teil_id != null)
                {
                    Teil teil;
                    teil = new Teil(teil_id, schritte);
                    _Bestand.Enqueue(teil);
                    _Internal.Ausgabe(teil.GetID() + " wird dem Bestand hinzugefuegt");
                }



                //Setzt den Status des Eingangslagers auf INTERAKTIONSBEREIT
                SetStatus(Status.INTERAKTIONSBEREIT);
                
            }
            
            

        }
    }
}
