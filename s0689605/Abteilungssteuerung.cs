using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using System;
using Beleg2019;

namespace Beleg2019
{
    /// <summary>
    /// Klasse der Abteilungssteuerung
    /// Diese Klasse wird vom Framework vorgegeben.
    /// Hier bitte nichts verändern
    /// </summary>
    public class Abteilungssteuerung
    {
        /// <summary>
        /// Datenstruktur für das Transportfahrzeug
        /// </summary>
        private Transportsystem _Transportroboter = null;

        /// <summary>
        /// Diese Datenstruktur soll alle Produktionseinrichtungen enthalten, nachdem diese initialisiert wurden!
        /// Die entsprechende Datei (Konfiguration.csv) besteht aus zwei Spalten. Die erste/linke Spalte enthaelt dabei
        /// den Namen der Produktionseinrichtung. Die zweite/rechte Spalte der Datei 
        /// enthaelt die ID der jeweiligen Produktionseinrichtung, diese wird beim Erzeugen benoetigt.  
        /// </summary>
        private List<Produktionseinrichtung> _Produktionseinrichtungen = new List<Produktionseinrichtung>();

        /// <summary>
        /// Konstruktor der Abteilungssteuerung
        /// </summary>
        public Abteilungssteuerung()
        {
            // Produktionseinrichtungen nach Konfigurationsdatei erzeugen
            InitialisiereAbteilung(@"..\..\Konfiguration.csv");
            // Erzeugen eines Transferfahrzeuges (Unterklasse von Transportsystem)
            _Transportroboter = (Transportsystem)Beleg2019._Internal.Anlegen(typeof(Transportsystem), "Transportfahrzeug", "Transportfahrzeug_X1", "A1");
            // Simulationsfunktion starten
            if (_Transportroboter != null) _Transportroboter.StarteHauptprozess(_Produktionseinrichtungen);
            
        }


        /// <summary>
        /// Funktion zum Einlesen der Produktionseinrichtungen
        /// Die Produktionseinrichtungen werden erzeugt und in der Liste der Produktionseinrichtungen abgelegt
        /// </summary>
        /// <param name="pfad">Pfad der Konfigurationsdatei</param>
        public void InitialisiereAbteilung(String pfad)
        {
            _Internal.Ausgabe("Abteilungs-Konfiguration wird eingelesen");
           
            // Objekt zum Einlesen von Dateien wird angelegt
            var reader = new StreamReader(pfad);
            // Schleife bis zum Ende der Datei
            while (!reader.EndOfStream)
            {
                // einzelne Zeile einlesen 
                String line = reader.ReadLine();
                // Teile der Zeile werden getrennt (Semikoleon ist Trennzeichen)
                String[] _values = line.Split(';');
                // die erste Spalte gibt die klasse an
                String objekt_klasse = _values[0];
                // die zweite Spalte gibt den Namen an
                String objekt_name = _values[1];
                // die dritte Spalte gibt die Position an
                String objekt_pos = _values[2];
                // die vierte Spalte gibt die Verarbeitungsfähigkeiten an - (Komma ist hier das Trennzeichen)
                // Teile des Fähigkeiten-Strings werden getrennt (Komma ist Trennzeichen)
                String[] faheigkeitenArray = _values[3].Split(',');
                // Liste von Verarbeitungsfähigkeiten wird angelegt
                List<Verarbeitungsschritt> faehigkeitenliste = new List<Verarbeitungsschritt>();
                // alle getrennten Fähigkeiten bzw. Prozessschritte werden einzeln bearbeitet
                foreach (string prozessschritt in faheigkeitenArray)
                {
                    //Die gefundenen Verarbeitungsschritte werden der Fähigkeitenliste hinzugefügt, wenn Sie vom Typ Verarbeitungsschritt sind
                    faehigkeitenliste.Add((Verarbeitungsschritt)Enum.Parse(typeof(Verarbeitungsschritt), prozessschritt));
                }

                // Das Objekt der speziellen Produktionseinrichtungen wird angelegt mit Name und Position
                Object Abteilungsobjekt = _Internal.Anlegen(typeof(Produktionseinrichtung), objekt_klasse, objekt_name, objekt_pos);
                if (Abteilungsobjekt != null)
                {
                    // die Produktionseinrichtungen werden der Liste der Produktionseinrichtungen hinzugefügt
                    _Produktionseinrichtungen.Add((Produktionseinrichtung)Abteilungsobjekt);
                    // die Verarbeitungsfähigkeiten werden der Produktionseinrichtung hinzugefügt
                    _Produktionseinrichtungen.ElementAt(_Produktionseinrichtungen.Count - 1).SetFaehigkeiten(faehigkeitenliste);
                    _Internal.Ausgabe(((Produktionseinrichtung)Abteilungsobjekt).GetName() + " wird hinzugefuegt");
                   
                }
            }
            reader.Close();
        }
    }
}

