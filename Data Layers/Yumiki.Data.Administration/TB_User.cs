namespace Yumiki.Data.Administration
{
    using Interface.Administration;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class TB_User : IUser
    {
        public Guid ID { get; set; }

        [Required]
        [StringLength(20)]
        public string UserLoginName { get; set; }

        [Required]
        [StringLength(50)]
        public string CurrentPassword { get; set; }

        [StringLength(15)]
        public string FirstName { get; set; }

        [StringLength(15)]
        public string LastName { get; set; }

        [StringLength(255)]
        public string Descriptions { get; set; }

        [Required]
        [StringLength(10)]
        public string IsActive { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime? LastUpdateDate { get; set; }
    }
}
