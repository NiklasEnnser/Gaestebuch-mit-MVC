//------------------------------------------------------------------------------
// <auto-generated>
//     Der Code wurde von einer Vorlage generiert.
//
//     Manuelle Änderungen an dieser Datei führen möglicherweise zu unerwartetem Verhalten der Anwendung.
//     Manuelle Änderungen an dieser Datei werden überschrieben, wenn der Code neu generiert wird.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Gästebuch.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class tbl_Admin
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_Admin()
        {
            this.tbl_Eintrag = new HashSet<tbl_Eintrag>();
        }
    
        public System.Guid ID { get; set; }
        [Required(ErrorMessage = "Input Required")]
        public string Benutzername { get; set; }
        [Required(ErrorMessage = "Input Required")]
        public string Passwort { get; set; }
        [Required(ErrorMessage = "Input Required")]
        public string twoStep { get; set; }
        public string ErrorMsg { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Eintrag> tbl_Eintrag { get; set; }
    }
}
