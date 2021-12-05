using BulkyBook.DataAccess.Repository.IRepository;
using E_CommerceStore_Udemey.Core.Constants;
using E_CommerceStore_Udemey.Infrastructure.Services.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace E_CommerceStore_Udemey.ViewComponents
{
    public class ShoppingCartViewComponent : ViewComponent
    {
        private readonly IUnitOfWork _unitOfWork;
        public ShoppingCartViewComponent(IUnitOfWork unitOfWork)
        {
            _unitOfWork= unitOfWork;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            if (claim != null)
            {
                if (HttpContext.Session.GetInt32(RolesConstant.SessionCart) != null)
                {
                    return View(HttpContext.Session.GetInt32(RolesConstant.SessionCart));
                }
                else
                {
                    HttpContext.Session.SetInt32(RolesConstant.SessionCart,
                        _unitOfWork.ShoppingCart.GetAll(u => u.UserId == claim.Value).ToList().Count);
                    return View(HttpContext.Session.GetInt32(RolesConstant.SessionCart));
                }
            }
            else
            {
                HttpContext.Session.Clear();
                return View(0);
            }
        }
    }
}
