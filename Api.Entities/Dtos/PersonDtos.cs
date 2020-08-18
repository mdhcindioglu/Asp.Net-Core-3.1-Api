using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Entities.Dtos
{
    public class PersonDto
    {
        public Guid Id { get; set; }

        public string FullName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string Job { get; set; }

        public IEnumerable<AddressDto> Addresses { get; set; }
    }

    public class PersonForCreationDto
    {
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
    }

    public class PersonForUpdateDto
    {
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
    }

    public class PersonIsAddedDto
    {
        public Guid? Id { get; set; }

        [Display(Name = "Full Name")]
        [Required(ErrorMessage = "{0} is required")]
        [StringLength(50, ErrorMessage = "{0} can't be longer than 50 characters")]
        public string FullName { get; set; }
    }
}
