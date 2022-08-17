using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beleg2019
{
    abstract class Transportsystem
    {
        /// <summary>
        /// Eigenschaften der Transportsysteme
        /// </summary>
        protected string _Position;
        protected string _Name;

        /// <summary>
        /// Beziehungen der Transportsysteme zu anderen Objekten
        /// </summary>
        private Abteilungssteuerung _Abteilungssteuerung;

        /// <summary>
        /// Konstruktor Transportsystem
        /// </summary>
        /// <param name="position">Name des Fahrzeugs</param>
        /// <param name="name">Positions des Fahrzeugs</param>
        public Transportsystem(string position, string name)
        {
            this._Name = name;
            this._Position = position;
        }

        /// <summary>
        /// Fahranweisung für des Transportfahrzeug
        /// </summary>
        /// <param name="ziel">Ziel der Fahrt</param>
        public void FahreZu(Produktionseinrichtung ziel)
        {
            this._Position = ziel.GetPosition();
        }
        /// <summary>
        /// Durchläuft immer wieder den Hauptprozess in einer Endlosschleife
        /// </summary>
        /// <param name="pe">Gibt Liste der Pe weiter</param>
        public void StarteHauptprozess(List<Produktionseinrichtung> pe)        
        {
            for(int i=5; i<=10; i--)
            {
                Hauptprozess(pe);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pe">Liste der Pe</param>
        public abstract void Hauptprozess(List<Produktionseinrichtung> pe);     
        




    }
}
