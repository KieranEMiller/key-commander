//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace KeyCdr.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class AnalysisType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public AnalysisType()
        {
            this.AnalysisAccuracy = new HashSet<AnalysisAccuracy>();
            this.AnalysisSpeed = new HashSet<AnalysisSpeed>();
            this.KeySequenceAnalysis = new HashSet<KeySequenceAnalysis>();
        }
    
        public int AnalysisTypeId { get; set; }
        public string Name { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AnalysisAccuracy> AnalysisAccuracy { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AnalysisSpeed> AnalysisSpeed { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<KeySequenceAnalysis> KeySequenceAnalysis { get; set; }
    }
}
