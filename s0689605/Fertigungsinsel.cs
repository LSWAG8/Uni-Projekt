using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beleg2019
{
    class Fertigungsinsel : Produktionseinrichtung          
    {
        /// <summary>
        /// Eigenschaften der Fertigungsinseln
        /// </summary>
        private DateTime _Belegtbis;

        /// <summary>
        /// Beziehungen der Fi
        /// </summary>
        private Teil _AktuellesTeil;


        /// <summary>
        /// Konstruktor der Fertigungsinseln
        /// </summary>
        /// <param name="name">Name der Fertigungsinsel</param>
        /// <param name="position">Standort der Fertigungsinsel</param>

        public Fertigungsinsel(string name, string position) : base(name, position)
        {

        }

        /// <summary>
        /// Ermittelt Status/Fortschritt der Fertigungsinsel und startet falls möglich nächsten Arbeitsschritt
        /// </summary>
        /// <returns>Status der Fertigungsinsel</returns>
        public override Status ErmittleStatus()
        {
            if (this._AktuellesTeil != null)
            {
                
                
                //Überprüft ob Fertigungsinsel Belegt ist
                if (this._AktuellerStatus == Status.BELEGT)
                {
                    //Überprüft ob BearbeiteTeil abgeschlossen ist
                    if (this._Belegtbis <= DateTime.Now)
                    {
                        _Internal.Ausgabe(this.GetName() + " ist mit dem " +this._AktuellesTeil.GibNaechstenSchritt() + " von " + this._AktuellesTeil.GetID() + " fertig");
                        _Internal.Ausgabe("Abgeschlossener Schritt wird aus dem Rezept entfernt");
                        //Entfernt nächsten Schritt
                        this._AktuellesTeil.EntferneFertigenSchritt();
                        //Überprüft ob nächster schritt des Teils hier möglich ist
                        foreach(Verarbeitungsschritt vs in this._HatFaehigkeiten)
                        if (vs == this._AktuellesTeil.GibNaechstenSchritt())
                        {
                            _Internal.Ausgabe("Weitere Bearbeitung möglich - Zustand weiterhin BELEGT");
                            //Bearbeitet Teil und ändert Status
                            BearbeiteTeil();
                            this._AktuellerStatus = Status.BELEGT;
                            return this._AktuellerStatus;
                        }
                        //falls nicht Status abholbereit
                        else
                        {

                        }  
                        
                        _Internal.Ausgabe("weitere Bearbeitung nicht möglich - Neuer Zustand ABHOLBEREIT");
                        this._AktuellerStatus = Status.ABHOLBEREIT;
                        return this._AktuellerStatus;

                    }
                    //Falls nicht gib Status aus
                    else
                    {
                        
                        return this._AktuellerStatus;
                    }

                }
                //Falls nicht gib Status aus
                else
                {
                    
                    return this._AktuellerStatus;
                }
            }
            else
            {
                
                this._AktuellerStatus = Status.EMPFANGSBEREIT;
                return this._AktuellerStatus;
                
            }
        }

        /// <summary>
        /// Übernahme eines Teils und anschließende Bearbeitung
        /// </summary>
        /// <param name="teil">zu übergebendes Teil</param>
        /// <returns>Ob Annahme möglich ist</returns>
        public bool EmpfangeTeil(Teil teil)
        {
            //Teil wird zugewiesen
            this._AktuellesTeil = teil;
            
            //Überprüfung auf Null-Objekt und Bereitschaft der Fertigungsinsel
            if (this._AktuellerStatus == Status.EMPFANGSBEREIT)
            {
                foreach (Verarbeitungsschritt vs in _HatFaehigkeiten)
                {
                    //Überprüfung ob Fertigungsinsel die erforderlichen Fähigkeiten hat
                    if (vs == teil.GibNaechstenSchritt())
                    {
                        //Neuer Status, BEarbeitung und Annahme des TEils
                        this._AktuellerStatus = Status.BELEGT;
                        _Internal.Ausgabe("Teil übernommen - Neuer Status BELEGT");
                        BearbeiteTeil();
                        return true;
                    }
                    //andernfalls ablehnen
                    else { }
                   
                }
                _Internal.Ausgabe("Teil kann nicht übernommen werden - Fertigungsinsel besitzt nicht die benötigten Fähigkeiten");
                return false;
            }
            //andernfalls ablehnen
            else
            {
                _Internal.Ausgabe("Teil kann nicht übernomen werden - Fertigungsinsel oder Teil nicht Betriebsfähig");
                return false;
            }
            
        }

        /// <summary>
        /// Teil an Transportfahrzeug übergeben
        /// </summary>
        /// <returns>das zu übergebende Teil</returns>
        public Teil GibTeil()
        {
            if (_AktuellesTeil != null)
            {
                //Status auf Empfangsbereit gesetzt
                this._AktuellerStatus = Status.EMPFANGSBEREIT;
                
                return this._AktuellesTeil;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Simuliert Bearbeitungszeit durch erstellen eines 10 Sekunden Zeitfensters
        /// </summary>
        public void BearbeiteTeil()
        {
            //Setzt Zeitvariable auf jetzigen Zeitpunkt
            this._Belegtbis = DateTime.Now;
            //Erhöht die Zeitvariable um 10 Sekunden
            this._Belegtbis = this._Belegtbis.AddSeconds(10);
            _Internal.Ausgabe("Teil wird bearbeitet bis: " + this._Belegtbis.ToString());
            
        }


      
        
    }
}
