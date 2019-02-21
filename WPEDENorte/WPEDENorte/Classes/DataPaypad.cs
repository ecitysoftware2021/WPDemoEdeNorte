using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPEDENorte.Classes
{
    public class DataPaypad
    {
        public bool State { get; set; }
        public bool StateAceptance { get; set; }
        public bool StateDispenser { get; set; }
        public string Message { get; set; }
        public object ListImages { get; set; }
    }
}
