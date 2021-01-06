using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace BlogSoft.Data.Entity
{
    public class ApplicationUser : IdentityUser
    {

        public string fullName { get; set; }

        public bool isVerified { get; set; }

        public bool isActive { get; set; }

        public bool isDeleted { get; set; }

        public DateTime? createdAt { get; set; }

        [MaxLength(120)]
        public string createdBy { get; set; }

        public DateTime? updatedAt { get; set; }

        [MaxLength(120)]
        public string updatedBy { get; set; }

    }
}
