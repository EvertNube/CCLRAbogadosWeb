//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CCLRAbogadosWeb.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class TipoDynamicContent
    {
        public TipoDynamicContent()
        {
            this.DynamicContent = new HashSet<DynamicContent>();
        }
    
        public int IdTipoDynamicContent { get; set; }
        public string Nombre { get; set; }
    
        public virtual ICollection<DynamicContent> DynamicContent { get; set; }
    }
}
