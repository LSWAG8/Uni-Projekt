using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beleg2019
{
    class Lager : Produktionseinrichtung
    {
        /// <summary>
        /// Eigenschaften der Lager
        /// </summary>
        protected int _Kapazitaet = 20;
        

        /// <summary>
        /// Beziehungen der Lager zu anderen Objekten
        /// </summary>
        public Queue<Teil> _Bestand = new Queue<Teil>();

        /// <summary>
        /// Konstruktor der Lager
        /// </summary>
        /// <param name="name">Name des Lagers</param>
        /// <param name="position">Standort des Lagers</param>

        public Lager(string name, string position) : base(name, position)
        {
            
        }

        /// <summary>
        /// Funktion zum Ermitteln des Status für des Transportfahrzeug
        /// </summary>
        /// <returns>Status des Lagers</returns>
        public override Status ErmittleStatus()
        {
            return this._AktuellerStatus;
        }

        /// <summary>
        /// Prüft mögliche Übernahme vom Teil und nimmt es falls möglich in den Bestand auf
        /// </summary>
        /// <param name="teil">zu übergebendes Teil</param>
        /// <returns>Möglichkeitswert der Übergabe</returns>
        public bool EmpfangeTeil(Teil teil)
        {
            //Prüft ob Lager bereits voll ist oder ob es sich um ein Nullobjekt handelt
            if(this._Bestand.LongCount() == 20 || teil == null )
            {
                _Internal.Ausgabe("Teilaufnahme abgelehnt");
                //Lehnt übernahme ab
                return false;
            }
            

            else
            {
                //Aufnahme des Teils in den Bestand
                this._Bestand.Enqueue(teil);
                
                //Prüft ob Lager jetzt voll ist
                if (this._Bestand.LongCount() == 20)
                {
                    //Falls Lager voll wird der Status aktuallisiert
                    this._AktuellerStatus = Status.ABHOLBEREIT;
                    _Internal.Ausgabe("Lager voll - Neuer Status: ABHOLBEREIT");
                }
                
                //Nimmt Aufnahme des Teils an
                return true;
            }
        }

        /// <summary>
        /// Übergibt Teil an Transportfahrzeug
        /// </summary>
        /// <returns>das Teil</returns>
        public Teil GibTeil()
        {
            //Prüfen ob noch ein Objekt im Lager ist
            if (this._Bestand.LongCount() != 0)
            {
                //Prüfen ob es das letzte Objekt ist
                if(this._Bestand.LongCount() == 1)
                {
                    //Status auf Empfangsbereit setzen falls das lager jetzt leer ist
                    this._AktuellerStatus = Status.EMPFANGSBEREIT;
                    _Internal.Ausgabe("Lager leer - Neuer Status EMPFANGSBEREIT");
                }
                
                    
                    //gib das Teil aus
                    return this._Bestand.Dequeue();
               
                
            }
            //Ansonsten Rückgabe null
            else
            {
                _Internal.Ausgabe("Kein Teil zur übergabe gefunden");
                return null;
            }

            
           
        }
    }
}
