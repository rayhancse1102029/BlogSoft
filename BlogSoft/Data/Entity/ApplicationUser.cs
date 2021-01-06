using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using FBAI.Models.MasterData;
using Microsoft.AspNetCore.Identity;

namespace FBAI.Data.Entity
{
    public class ApplicationUser : IdentityUser
    {

        [Required]
        [MaxLength(20)]
        public string firstName { get; set; } 
        
        [Required]
        [MaxLength(20)]
        public string lastName { get; set; }

        public string fullName { get; set; }

        [Required]
        public string imgUrl { get; set; }
        
        public string employeeCode { get; set; }

        public int genderId { get; set; }

        public Gender Gender { get; set; }

        public decimal? salary { get; set; }

        public bool isVerified { get; set; }

        public bool isActive { get; set; }

        public bool isDeleted { get; set; }


        public DateTime? createdAt { get; set; }

        [MaxLength(120)]
        public string createdBy { get; set; }

        public DateTime? updatedAt { get; set; }

        [MaxLength(120)]
        public string updatedBy { get; set; }

        [NotMapped]
        public string roleName { get; set; }
    }
}
