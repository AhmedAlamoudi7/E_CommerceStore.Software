﻿using E_CommerceStore_Udemey.DATA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceStore_Udemey.DATA
{
    public class ShopCartVm
    {
        public int Count { get; set; }
   
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
