using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace E_CommerceStore_Udemey.DATA.Models
{
    public class Category
    {
        
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Display(Name="Display Order")]
        [Range(1,100,ErrorMessage ="Display Order must be between 1 and 100 only !!")]
        public int DisplayOrder { get; set; } 
        public DateTime CratedDateTime { get; set; } = DateTime.Now;
    }
}
