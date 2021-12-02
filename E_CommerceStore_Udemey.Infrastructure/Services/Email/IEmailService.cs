using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceStore_Udemey.Infrastructure.Services
{
    public interface IEmailService
    {
        Task Send(string to, string subject, string body);
    }
}
