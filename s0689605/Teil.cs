using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Beleg2019
{
    class Teil
    {   
        /// <summary>
        /// Eigenschaften Teil
        /// </summary>
        private string _TeilID;

        /// <summary>
        /// Beziehungen des Teils zu anderen Objekten
        /// </summary>        
        private Transportfahrzeug _Transportfahrzeug;
        private Fertigungsinsel _Fertigungsinsel;
        private Lager _Lager;
        private List<Verarbeitungsschritt> _Rezept = new List<Verarbeitungsschritt>();


        /// <summary>
        /// Konstruiere Teil
        /// </summary>
        /// <param name="id">ID des neuen Teils</param>
        /// <param name="rezept">Bearbeitungsfolge des Teils</param>
        public Teil(string id, List<Verarbeitungsschritt> rezept)

        {                                    
            this._TeilID = id;
            this._Rezept = rezept;

            
        }

        /// <summary>
        /// Gibt nächsten erforderlichen Arbeitsschritt des Teils aus
        /// </summary>
        /// <returns>nächster erforderlicher Schritt</returns>
        public Verarbeitungsschritt GibNaechstenSchritt()
        {
            
           
                

            return this._Rezept.First();
            
        }

        /// <summary>
        /// Entfernt abgeschlossenen Schritt aus dem Rezept
        /// </summary>
        public void EntferneFertigenSchritt()
        {
            

            this._Rezept.RemoveAt(0);
        }
        
        /// <summary>
        /// Abfrage der Teil ID
        /// </summary>
        /// <returns>Übergabe der Teil ID</returns>
        public string GetID()
        {
            return this._TeilID;
        }

        /// <summary>
        /// Setter fürs Rezept des Teils
        /// </summary>
        /// <param name="re">Rezept</param>
        public void SetRezept(List<Verarbeitungsschritt> re)
        {
            this._Rezept = re;
        }
    }
}
