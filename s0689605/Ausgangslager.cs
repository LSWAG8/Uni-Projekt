using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beleg2019
{
    class Ausgangslager : Lager
    {

       
        /// <summary>
        /// Konstruktor des Ausgangslagers
        /// </summary>
        /// <param name="name">Name des Ausgangslagers</param>
        /// <param name="position">Standort des Ausgangslagers</param>
        

        public Ausgangslager(string name, string position) : base(name, position)
        {
            this._AktuellerStatus = Status.EMPFANGSBEREIT;
        }
    }
}
