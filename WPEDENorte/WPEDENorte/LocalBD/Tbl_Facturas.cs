//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WPEDENorte.LocalBD
{
    using System;
    using System.Collections.Generic;
    
    public partial class Tbl_Facturas
    {
        public int Id { get; set; }
        public string Factura { get; set; }
        public string Ref_Pago { get; set; }
        public string Pague_Antes_De { get; set; }
        public string Valida_Hasta { get; set; }
        public string Fecha_Emision { get; set; }
        public string Direccion_Suministro { get; set; }
        public string Titular_De_Pago { get; set; }
        public string RNC_Cliente { get; set; }
        public string Total_Pagar { get; set; }
        public string Contrato { get; set; }
    }
}