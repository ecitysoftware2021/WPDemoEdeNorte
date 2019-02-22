using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPEDENorte.Classes
{
    public class Facturas
    {
        public string Factura { get; set; }
        public string Ref_Pago { get; set; }
        public string Contrato { get; set; }
        public string Pague_Antes_De { get; set; }
        public string Valida_Hasta { get; set; }
        public string Fecha_Emision { get; set; }
        public string Direccion_Suministro { get; set; }
        public string Titular_De_Pago { get; set; }
        public string RNC_Cliente { get; set; }
        public string Valor { get; set; }
    }
}
