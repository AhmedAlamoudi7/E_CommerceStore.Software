using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceStore_Udemey.Core.ViewModels
{
    public class UserViewModel
    {
      
        public string FullName { get; set; }

        public string? DOB { get; set; }
        public string ImageUrl { get; set; }
        public string PhoneNumber { get; set; }
        public CompanyViewModel CompanyVm { get; set; }
    }
}
