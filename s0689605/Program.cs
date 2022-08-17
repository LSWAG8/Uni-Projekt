using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;
using System.Reflection;

using Beleg2019;

namespace Beleg2019
{
    /// <summary>
    /// Dies ist der Einstiegspunkt für das Progamm.
    /// Bitte ändern Sie hier nichts.
    /// Durch Schließen des Fensters beenden Sie das Programm.
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Beleg2019.Abteilungssteuerung verwaltung = new Abteilungssteuerung();    
            }
            finally { Console.ReadKey(); }
            

#if DEBUG
            Console.WriteLine("\nProgram exits. Push any key to continue ...");
            Console.ReadKey();
#endif
        }
    }
}
