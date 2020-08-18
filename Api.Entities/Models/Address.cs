using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    [Table("Addresses")]
    public class Address
    {
        [Column("AddressId")]
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

        [ForeignKey(nameof(Person))]
        public Guid PersonId { get; set; }
        public Person Person { get; set; }
    }
}
