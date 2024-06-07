using System;

namespace WpfApp5.Models
{
    public class OpgaveModel
    {
        public int OpgaveID { get; set; }
        public string CPR { get; set; }
        public string Stue { get; set; }
        public bool Isolation { get; set; }
        public string Prøver { get; set; }
        public string SærligeForhold { get; set; }
        public bool Inaktiv { get; set; }
        public string Prioritet { get; set; }
        public DateTime Dato { get; set; }
        public string Kommentar { get; set; }
        public string Patientnavn { get; set; }
        public int AfdelingID { get; set; }
    }
}



