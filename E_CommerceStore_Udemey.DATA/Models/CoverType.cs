﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceStore_Udemey.DATA.Models
{
   public class CoverType
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        [Display(Name="Cover Name")]
        public string CoverName { get; set; }
    }
}
