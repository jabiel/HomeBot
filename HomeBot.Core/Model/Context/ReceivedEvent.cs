//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HomeBot.Core.Model.Context
{
    using System;
    using System.Collections.Generic;
    
    public partial class ReceivedEvent
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ReceivedEvent()
        {
            this.SentEvents = new HashSet<SentEvent>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public System.DateTime EventDate { get; set; }
        public string SenderId { get; set; }
        public System.DateTime SendDate { get; set; }
        public System.DateTime CreateDate { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SentEvent> SentEvents { get; set; }
    }
}
