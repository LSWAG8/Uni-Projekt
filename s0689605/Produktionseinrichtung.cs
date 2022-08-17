using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beleg2019
{
    abstract class Produktionseinrichtung
    {
        /// <summary>
        /// Eigenschaften der Produktionseinrichtungen
        /// </summary>
        private string _Position;
        private string _Name;

        /// <summary>
        /// Beziehungen der Produktionseinrichtungen zu anderen Objekten
        /// </summary>
        private Abteilungssteuerung _Abteilungsteuerung;
        protected Status _AktuellerStatus;
        protected List<Verarbeitungsschritt> _HatFaehigkeiten = new List<Verarbeitungsschritt>();

        /// <summary>
        /// Konstruktor der Produktionseinrichtungen
        /// </summary>
        /// <param name="name">Name der Einrichtung</param>
        /// <param name="position">Standort der Einrichtung</param>
        
        public Produktionseinrichtung(string name, string position)
        {
            this._Name = name;
            this._Position = position;
            
        }

        /// <summary>
        /// Setzt Status der Pe
        /// </summary>
        /// <param name="value">WElcher STatus</param>
        protected void SetStatus(Status value)     
        {
            this._AktuellerStatus = value;

        }

        /// <summary>
        /// Setter für Fähigkeiten der Pe
        /// </summary>
        /// <param name="value">Fähigkeitenliste</param>
        public void SetFaehigkeiten(List<Verarbeitungsschritt> value)          
        {
            
            this._HatFaehigkeiten = value;
           
        }

        /// <summary>
        /// Getter für Pe Name
        /// </summary>
        /// <returns>Name</returns>
        public string GetName()                        
        {
            return this._Name;
        }

        /// <summary>
        /// Getter für Pe Position
        /// </summary>
        /// <returns>Position</returns>
        public string GetPosition()
        {
            return this._Position;
        }

        /// <summary>
        /// Überprüfung ob Bestimmte Fähigkeiten in der Pe vorhanden ist
        /// </summary>
        /// <param name="schritt">Bestimmte Fähigkeit</param>
        /// <returns>Ja/Nein</returns>
        public bool HatFaehigkeiten(Verarbeitungsschritt schritt)
        {
            foreach (Verarbeitungsschritt vs in this._HatFaehigkeiten)
            {
                
                if (vs == schritt)
                {
                    
                    return true;
                }
                else
                {

                    
                }
            }
            return false;
        }
        /// <summary>
        /// Abstrakte Funktion zur Statusermittlung
        /// </summary>
        /// <returns>Status der Produktionseinrichtungen</returns>
        abstract public Status ErmittleStatus();         
        


    }
}
