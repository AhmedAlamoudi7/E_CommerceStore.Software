using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceStore_Udemey.Core.ViewModels
{
    public class ProductViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string ISBN { get; set; }

        public string Author { get; set; }

        public double ListPrice { get; set; }

        public double Price { get; set; }

        public double Price100 { get; set; }
        public string ImageUrl { get; set; }

        public string CategoryVMName { get; set; }
        public string CoverTypeVMName { get; set; }
    }
}
