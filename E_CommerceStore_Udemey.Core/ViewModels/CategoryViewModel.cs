﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceStore_Udemey.Core.ViewModels
{
    public class CategoryViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CratedDateTime { get; set; }
        public int DisplayOrder { get; set; }

    }
}
