//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace InnnovixHRG1.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class RoomTypeTBL
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public RoomTypeTBL()
        {
            this.RoomPriceTBL = new HashSet<RoomPriceTBL>();
        }
    
        public int RoomTypeID { get; set; }
        public string RoomTypeName { get; set; }
        public bool IsStopped { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RoomPriceTBL> RoomPriceTBL { get; set; }
    }
}
