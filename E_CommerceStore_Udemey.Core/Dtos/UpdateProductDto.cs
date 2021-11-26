using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceStore_Udemey.Core.Dtos
{
    public class UpdateProductDto
    {
        public int Id { get; set; }
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
        public double ListPrice { get; set; }
        [Required]
        [Range(1, 10000)]
        public double Price { get; set; }
        [Required]
        [Range(1, 10000)]
        public double Price100 { get; set; }

        public IFormFile ImageUrl { get; set; }

        public int CategoryId { get; set; }

        public int CoverTypeId { get; set; }
    }
}
