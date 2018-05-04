namespace DayScheduling.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Activity")]
    public partial class Activity
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Activity()
        {
            Plans = new HashSet<Plan>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ActivityID { get; set; }

        [Required]
        [StringLength(255)]
        public string ActivityName { get; set; }

        [Required]
        [StringLength(255)]
        public string ActivityType { get; set; }

        public DateTime ActivityStartTime { get; set; }

        public DateTime ActivityFinishTime { get; set; }

        public bool ActivityComplete { get; set; }

        [Required]
        public int ActivityPlaceID { get; set; }
        [Required]
        public int PlanID { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Plan> Plans { get; set; }
    }
}
