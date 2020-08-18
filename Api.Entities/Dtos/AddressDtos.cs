using System;
using System.ComponentModel.DataAnnotations;

namespace Entities.Dtos
{
    public class AddressDto
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Country { get; set; }

        public string City { get; set; }

        public string Details { get; set; }

        public Guid PersonId { get; set; }
    }

    public class AddressForCreationDto
    {
        [Display(Name = "Title")]
        [Required(ErrorMessage = "{0} is reuired")]
        [StringLength(50, ErrorMessage = "{0} can't be longer than 50 characters")]
        public string Title { get; set; }

        [Display(Name = "Country")]
        [Required(ErrorMessage = "{0} is reuired")]
        [StringLength(50, ErrorMessage = "{0} can't be longer than 50 characters")]
        public string Country { get; set; }

        [Display(Name = "City")]
        [Required(ErrorMessage = "{0} is reuired")]
        [StringLength(50, ErrorMessage = "{0} can't be longer than 50 characters")]
        public string City { get; set; }

        [Display(Name = "Details")]
        [Required(ErrorMessage = "{0} is reuired")]
        [StringLength(250, ErrorMessage = "{0} can't be longer than 250 characters")]
        public string Details { get; set; }

        public Guid PersonId { get; set; }
    }

    public class AddressForUpdateDto
    {
        public Guid Id { get; set; }

        [Display(Name = "Title")]
        [Required(ErrorMessage = "{0} is reuired")]
        [StringLength(50, ErrorMessage = "{0} can't be longer than 50 characters")]
        public string Title { get; set; }

        [Display(Name = "Country")]
        [Required(ErrorMessage = "{0} is reuired")]
        [StringLength(50, ErrorMessage = "{0} can't be longer than 50 characters")]
        public string Country { get; set; }

        [Display(Name = "City")]
        [Required(ErrorMessage = "{0} is reuired")]
        [StringLength(50, ErrorMessage = "{0} can't be longer than 50 characters")]
        public string City { get; set; }

        [Display(Name = "Details")]
        [Required(ErrorMessage = "{0} is reuired")]
        [StringLength(250, ErrorMessage = "{0} can't be longer than 250 characters")]
        public string Details { get; set; }

        public Guid PersonId { get; set; }
    }

    public class AddressIsAddedDto
    {
        public Guid? Id { get; set; }

        [Display(Name = "Title")]
        [Required(ErrorMessage = "{0} is required")]
        [StringLength(50, ErrorMessage = "{0} can't be longer than 50 characters")]
        public string Title { get; set; }
    
        [Display(Name = "Person Id")]
        [Required(ErrorMessage = "{0} is required")]
        public Guid PersonId { get; set; }
    }
}
