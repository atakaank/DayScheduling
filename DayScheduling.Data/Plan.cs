namespace DayScheduling.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Plan
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Plan()
        {
            Activities = new HashSet<Activity>();
            Accounts = new HashSet<Account>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PlanID { get; set; }

        [Required]
        [StringLength(50)]
        public string PlanName { get; set; }

        public DateTime PlanDate { get; set; }

        [Required]
        [StringLength(50)]
        public string PlanType { get; set; }

        public int PlanPopularity { get; set; }

        public bool PlanComplete { get; set; }

        public int PlanRate { get; set; }

        public int ProvinceID { get; set; }

        public int AccountID { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Activity> Activities { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Account> Accounts { get; set; }
    }
}
