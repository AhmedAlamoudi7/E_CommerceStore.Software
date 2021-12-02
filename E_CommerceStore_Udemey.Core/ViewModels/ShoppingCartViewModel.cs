using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceStore_Udemey.Core.ViewModels
{
    public class ShoppingCartViewModel
    {

        public int Id { get; set; }

        public ProductViewModel ProductVm { get; set; }

        public int Count { get; set; }

        public UserViewModel UserVm { get; set; }

        public double Price { get; set; }
    }
}
