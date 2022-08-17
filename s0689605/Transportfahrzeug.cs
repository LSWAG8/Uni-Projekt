using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beleg2019
{
    class Transportfahrzeug : Transportsystem
    {
        /// <summary>
        /// Eigenschaft die zum überprüfen des Erfolgs einer Suche dient
        /// </summary>
        private bool erfolg;

        /// <summary>
        /// Beziehungen des Fahrzeugs zu anderen Objekten
        /// </summary>
        private Teil _AktuellesTeil = new Teil(null,null);
        private Produktionseinrichtung _AktuellesZiel;
        private Lager _InteragierendesLager = new Lager(null,null);
        private Fertigungsinsel _InteragierendeFertigungsinsel = new Fertigungsinsel(null,null);



        /// <summary>
        /// Konstruktor des Fahrzeugs
        /// </summary>
        /// <param name="position">Name des Fahrzeugs</param>
        /// <param name="name">Positions des Fahrzeugs</param>

        public Transportfahrzeug(string name, string position) : base(name, position)
        {

        }

        /// <summary>
        /// Betriebsfunkution für das Transportfahrzeug
        /// </summary>
        /// <param name="pe">Liste der Pe als mögliche Ziele</param>
        override public void Hauptprozess(List<Produktionseinrichtung> pe)
        {
            
            //Suche Ziel bis in der Variable _AktuellesZiel eine Abholbereite oder Interaktionsbereite Pe festgelegt ist

            do
            {
                
                _Internal.Ausgabe("Suche Ziel...");
                SucheZielAbholen(pe);
            }

            while (this._AktuellesZiel.ErmittleStatus() != Status.ABHOLBEREIT && this._AktuellesZiel.ErmittleStatus() != Status.INTERAKTIONSBEREIT );

            _Internal.Ausgabe("Ziel gefunden: " + this._AktuellesZiel.GetName() + ". Beginne Fahrt zum Ziel");
            //Fährt zu aktuellem Ziel
            FahreZu(this._AktuellesZiel);
            _Internal.Ausgabe("Angekommen bei :" + this._AktuellesZiel.GetName());

            Boolean lager = false;
            //Übernahme falls Lager
            try
            {
                Lager test = new Lager(null, null);
                test = (Lager)_AktuellesZiel; lager = true;
                EmpfangeTeil(this._InteragierendesLager.GibTeil());
                _Internal.Ausgabe(this._AktuellesTeil.GetID() + " von " + this._InteragierendesLager.GetName() + " übernommen");
            }
            //Übernahme falls Fi
            catch (InvalidCastException e)
            {
                Fertigungsinsel fertigung = new Fertigungsinsel(null, null);
                fertigung = (Fertigungsinsel)_AktuellesZiel; lager = false;
                EmpfangeTeil(this._InteragierendeFertigungsinsel.GibTeil());
                _Internal.Ausgabe("Teil von " + this._InteragierendeFertigungsinsel.GetName() + " übernommen");
            };

            //Sucht geeignete Pe
            _Internal.Ausgabe("Suche Produktionseinrichtung mit Fähigkeit: " + this._AktuellesTeil.GibNaechstenSchritt().ToString() + " für: " + this._AktuellesTeil.GetID());
            SucheZielWeiterverarbeitung(pe, this._AktuellesTeil.GibNaechstenSchritt());
            

            //Test ob geeignete Pe gefunden wurde
            if (erfolg == false)
            {
                foreach (Produktionseinrichtung p in pe)
                {
                    
                    try
                    {
                        Eingangslager al = new Eingangslager(null, null);
                        al = (Eingangslager)p;
                        this._InteragierendesLager = al;
                        this._AktuellesZiel = p;
                        
                        _Internal.Ausgabe("Keine geeignete Produktionseinrichtung gefunden - Neues Ziel zum Zwischenlagern: " + this._AktuellesZiel.GetName().ToString());
                    }
                    catch (InvalidCastException e)
                    {
                        
                    };
                   
                    

                }
            }
            else
            {
                _Internal.Ausgabe("Geeignetes Ziel gefunden: " + this._AktuellesZiel.GetName() + ". Beginne Fahrt zur Fertigungsinsel");
            }

            FahreZu(this._AktuellesZiel);
            _Internal.Ausgabe("Angekommen bei: " + this._AktuellesZiel.GetName());

            //Übergabe des Teils falls Lager
            lager = false;
            try { Lager test = (Lager)_AktuellesZiel; lager = true; }
            catch (InvalidCastException e) { };
            if (lager == true)

            {
                this._InteragierendesLager.EmpfangeTeil(this.GibTeil());
                _Internal.Ausgabe("Teil an Lager übergeben");
            }
            //Übergabe des Teils falls Fi
            else
            {
                this._InteragierendeFertigungsinsel.EmpfangeTeil(this.GibTeil());
                
            }




        }
        /// <summary>
        /// Nimmt Teil von Pe an
        /// </summary>
        /// <param name="teil">Empfangenes Teil</param>
        public void EmpfangeTeil(Teil teil)
        {
            this._AktuellesTeil = teil;
            
        }
        /// <summary>
        /// Gibt Teil and Produktionseinrichtung
        /// </summary>
        /// <returns>Aktuelles Teil</returns>
        public Teil GibTeil()
        {
            return this._AktuellesTeil;
        }
        /// <summary>
        /// Suche Abholbereite Pe und aktualiesiert deren Statuus
        /// </summary>
        /// <param name="pe">Liste der Pe</param>
        private void SucheZielAbholen(List<Produktionseinrichtung> pe)
        {
            int nonull = 0;
            //Suche abholbereite Pe. Prio auf Abholbereites um steckenbleiben zu vermeiden.
            foreach (Produktionseinrichtung p in pe)
            {
                
                if (p.ErmittleStatus() == Status.ABHOLBEREIT)
                {
                    Boolean type = false;
                    try
                    {
                        Lager lager = new Lager(null, null);
                        lager = (Lager)p; type = true;
                        this._InteragierendesLager = lager;
                        this._AktuellesZiel = p;
                        nonull = 1;
                        

                    }
                    catch (InvalidCastException e)
                    {
                        Fertigungsinsel fi = new Fertigungsinsel(null, null);
                        fi = (Fertigungsinsel)p; type = false;
                        this._InteragierendeFertigungsinsel = fi;
                        this._AktuellesZiel = p;
                        
                        nonull = 1;
                        
                    };

                }

            }
            if(nonull == 0)
            {
                foreach (Produktionseinrichtung p in pe)
                {
                    if (p.ErmittleStatus() == Status.INTERAKTIONSBEREIT)
                    {
                        Boolean type = false;
                        try
                        {
                            Lager lager = new Lager(null, null);
                            lager = (Lager)p; type = true;
                            this._InteragierendesLager = lager;
                            this._AktuellesZiel = p;


                        }
                        catch (InvalidCastException e)
                        {
                            Fertigungsinsel fi = new Fertigungsinsel(null, null);
                            fi = (Fertigungsinsel)p; type = false;
                            this._InteragierendeFertigungsinsel = fi;
                            this._AktuellesZiel = p;

                        };

                    }
                }
            }

            

            
        }
        /// <summary>
        /// Suche Pe zur Weiterverarbeitung oder Einlagerung des Aktuellen Teils
        /// </summary>
        /// <param name="pe">Liste der Pe</param>
        /// <param name="vs">Nächster erforderlicher Schritt des teils</param>
        private void SucheZielWeiterverarbeitung(List<Produktionseinrichtung> pe, Verarbeitungsschritt vs)
        {
            erfolg = false;

            foreach (Produktionseinrichtung p in pe)
            {
                if (p.ErmittleStatus() == Status.EMPFANGSBEREIT || p.ErmittleStatus() == Status.INTERAKTIONSBEREIT)
                {
                    
                    if (p.HatFaehigkeiten(vs) == true)
                    {
                        
                        Boolean type = false;
                        try
                        {
                            Lager lager = new Lager(null, null);
                            lager = (Lager)p; type = true;
                            this._InteragierendesLager = lager;
                            this._AktuellesZiel = p;
                            erfolg = true;
                        }
                        catch (InvalidCastException e)
                        {
                            Fertigungsinsel fi = new Fertigungsinsel(null, null);
                            fi = (Fertigungsinsel)p; type = false;
                            this._InteragierendeFertigungsinsel = fi;
                            this._AktuellesZiel = p;
                            erfolg = true;

                        }



                    }

                }  
            }

            
           
        }
    }
}
