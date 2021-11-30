using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceStore_Udemey.Core.Dtos
{
   public class CreateProductDto
    {

    
        [Required]
        [MaxLength(70)]
        public string Title { get; set; }
        [MaxLength(70)]
        public string Description { get; set; }
        [Required]
        public string ISBN { get; set; }
        [Required]
        public string Author { get; set; }
        [Required]
        [Range(1, 10000)]
        [Display(Name = "List Price")]
        public double ListPrice { get; set; }
        [Required]
        [Range(1, 10000)]
        [Display(Name ="Price For 1-50")]
        public double Price { get; set; }
        [Required]
        [Range(1, 10000)]
        [Display(Name = "Price For 51-100")]
        public double Price100 { get; set; }
        public IFormFile ImageUrl { get; set; }

        [Display(Name = "Category")]
        public int CategoryId { get; set; }
        [Display(Name = "Cover Type")]
        public int CoverTypeId { get; set; }
    }
}
