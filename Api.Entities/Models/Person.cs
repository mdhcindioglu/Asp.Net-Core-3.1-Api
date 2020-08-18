using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    [Table("Persons")]
    public class Person
    {
        [Column("PersonId")]
        public Guid Id { get; set; }

        [Display(Name = "Full Name")]
        [Required(ErrorMessage = "{0} is required")]
        [StringLength(50, ErrorMessage = "{0} can't be longer than 50 characters")]
        public string FullName { get; set; }

        [Display(Name = "Date Of Birth")]
        [Required(ErrorMessage = "{0} is required")]
        public DateTime DateOfBirth { get; set; }

        [Display(Name = "Job")]
        [Required(ErrorMessage = "{0} is required")]
        [StringLength(50, ErrorMessage = "{0} can't be longer than 50 characters")]
        public string Job { get; set; }

        public ICollection<Address> Addresses { get; set; }
    }
}
