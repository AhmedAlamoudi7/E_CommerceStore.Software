﻿//using E_CommerceStore_Udemey.Core.Constants;
//using E_CommerceStore_Udemey.Core.Dtos;
//using E_CommerceStore_Udemey.Core.ViewModels;
//using E_CommerceStore_Udemey.DATA;
//using E_CommerceStore_Udemey.DATA.Data;
//using E_CommerceStore_Udemey.DATA.Models;
//using E_CommerceStore_Udemey.Infrastructure.Services.CategoryServices;
//using E_CommerceStore_Udemey.Infrastructure.Services.CoverTypeServices;
//using E_CommerceStore_Udemey.Infrastructure.Services.ProductService;
//using E_CommerceStore_Udemey.Infrastructure.Services.Repository.IRepository;
//using E_CommerceStore_Udemey.Infrastructure.Services.ShoppingCartServices;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Identity.UI.Services;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Rendering;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Logging;
//using Stripe.Checkout;
//using System;
//using System.Collections.Generic;
//using System.Diagnostics;
//using System.Linq;
//using System.Security.Claims;
//using System.Threading.Tasks;

//namespace E_CommerceStore_Udemey.WEB.Controllers
//{
//    [Area("Customer")]
//    public class CartController : Controller
//    {

//        private readonly IUnitOfWork _unitOfWork;
//        private readonly IEmailSender _emailSender;
//        [BindProperty]
//        public ShoppingCartVM ShoppingCartVM { get; set; }
//        public CartController(IUnitOfWork unitOfWork, IEmailSender emailSender)
//        {
//            _unitOfWork = unitOfWork;
//            _emailSender = emailSender;
//        }
//        public IActionResult Index()
//        {
//            var claimsIdentity = (ClaimsIdentity)User.Identity;
//            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

//            ShoppingCartVM = new ShoppingCartVM()
//            {
//                ListCart = _unitOfWork.ShoppingCart.GetAll(u => u.UserId == claim.Value,
//                includeProperties: "Product"),
//                OrderHeader = new()

//            };
//            foreach (var cart in ShoppingCartVM.ListCart)
//            {
//                cart.Price = GetPriceBasedOnQuantity(cart.Count, cart.Product.Price,
//                    cart.Product.Price50, cart.Product.Price100);
//                   ShoppingCartVM.OrderHeader.OrderTotal += (cart.Price * cart.Count);
//            }
//            return View(ShoppingCartVM);
//        }


//        public IActionResult Plus(int cartId)
//        {
//            var cart = _unitOfWork.ShoppingCart.GetFirstOrDefault(u => u.Id == cartId);
//            _unitOfWork.ShoppingCart.IncrementCount(cart, 1);
//            _unitOfWork.Save();
//            return RedirectToAction(nameof(Index));
//        }

//        public IActionResult Minus(int cartId)
//        {
//            var cart = _unitOfWork.ShoppingCart.GetFirstOrDefault(u => u.Id == cartId);
//            if (cart.Count <= 1)
//            {
//                _unitOfWork.ShoppingCart.Remove(cart);
//                var count = _unitOfWork.ShoppingCart.GetAll(u => u.UserId == cart.UserId).ToList().Count - 1;
//                //HttpContext.Session.SetInt32(SD.SessionCart, count);
//            }
//            else
//            {
//                _unitOfWork.ShoppingCart.DecrementCount(cart, 1);
//            }
//            _unitOfWork.Save();
//            return RedirectToAction(nameof(Index));
//        }

//        public IActionResult Remove(int cartId)
//        {
//            var cart = _unitOfWork.ShoppingCart.GetFirstOrDefault(u => u.Id == cartId);
//            _unitOfWork.ShoppingCart.Remove(cart);
//            _unitOfWork.Save();
//            var count = _unitOfWork.ShoppingCart.GetAll(u => u.UserId == cart.UserId).ToList().Count;
//            //HttpContext.Session.SetInt32(SD.SessionCart, count);
//            return RedirectToAction(nameof(Index));
//        }



//        public IActionResult Summary()
//        {
//            var claimsIdentity = (ClaimsIdentity)User.Identity;
//            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

//            ShoppingCartVM = new ShoppingCartVM()
//            {
//                ListCart = _unitOfWork.ShoppingCart.GetAll(u => u.UserId == claim.Value,
//                includeProperties: "Product"),
//                OrderHeader = new()
//            };
//            ShoppingCartVM.OrderHeader.User = _unitOfWork.ApplicationUser.GetFirstOrDefault(
//                u => u.Id == claim.Value);

//            ShoppingCartVM.OrderHeader.Name = ShoppingCartVM.OrderHeader.User.FullName;
//            ShoppingCartVM.OrderHeader.PhoneNumber = ShoppingCartVM.OrderHeader.User.PhoneNumber;
//            ShoppingCartVM.OrderHeader.StreetAddress = ShoppingCartVM.OrderHeader.User.StreetAddress;
//            ShoppingCartVM.OrderHeader.City = ShoppingCartVM.OrderHeader.User.City;
//            ShoppingCartVM.OrderHeader.State = ShoppingCartVM.OrderHeader.User.State;
//            ShoppingCartVM.OrderHeader.PostalCode = ShoppingCartVM.OrderHeader.User.PostalCode;



//            foreach (var cart in ShoppingCartVM.ListCart)
//            {
//                cart.Price = GetPriceBasedOnQuantity(cart.Count, cart.Product.Price,
//                    cart.Product.Price50, cart.Product.Price100);
//                ShoppingCartVM.OrderHeader.OrderTotal += (cart.Price * cart.Count);
//            }
//            return View(ShoppingCartVM);
//        }

//        [HttpPost]
//        [ActionName("Summary")]
//        [ValidateAntiForgeryToken]
//        public IActionResult SummaryPOST()
//        {
//            var claimsIdentity = (ClaimsIdentity)User.Identity;
//            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

//            ShoppingCartVM.ListCart = _unitOfWork.ShoppingCart.GetAll(u => u.UserId == claim.Value,
//                includeProperties: "Product");


//            ShoppingCartVM.OrderHeader.OrderDate = System.DateTime.Now;
//            ShoppingCartVM.OrderHeader.UserId = claim.Value;


//            foreach (var cart in ShoppingCartVM.ListCart)
//            {
//                cart.Price = GetPriceBasedOnQuantity(cart.Count, cart.Product.Price,
//                    cart.Product.Price50, cart.Product.Price100);
//                ShoppingCartVM.OrderHeader.OrderTotal += (cart.Price * cart.Count);
//            }
//           User applicationUser = _unitOfWork.ApplicationUser.GetFirstOrDefault(u => u.Id == claim.Value);

//            if (applicationUser.CompanyId.GetValueOrDefault() == 0)
//            {
//                ShoppingCartVM.OrderHeader.PaymentStatus = RolesConstant.PaymentStatusPending;
//                ShoppingCartVM.OrderHeader.OrderStatus = RolesConstant.StatusPending;
//            }
//            else
//            {
//                ShoppingCartVM.OrderHeader.PaymentStatus = RolesConstant.PaymentStatusDelayedPayment;
//                ShoppingCartVM.OrderHeader.OrderStatus = RolesConstant.StatusApproved;
//            }

//            _unitOfWork.OrderHeader.Add(ShoppingCartVM.OrderHeader);
//            _unitOfWork.Save();
//            foreach (var cart in ShoppingCartVM.ListCart)
//            {
//                OrderDetail orderDetail = new()
//                {
//                    ProductId = cart.ProductId,
//                    OrderId = ShoppingCartVM.OrderHeader.Id,
//                    Price = cart.Price,
//                    Count = cart.Count
//                };
//                _unitOfWork.OrderDetail.Add(orderDetail);
//                _unitOfWork.Save();
//            }


//            if (applicationUser.CompanyId.GetValueOrDefault() == 0)
//            {
//                //stripe settings 
//                var domain = "https://localhost:5001/";
//                var options = new SessionCreateOptions
//                {
//                    PaymentMethodTypes = new List<string>
//                {
//                  "card",
//                },
//                    LineItems = new List<SessionLineItemOptions>(),
//                    Mode = "payment",
//                    SuccessUrl = domain + $"customer/cart/OrderConfirmation?id={ShoppingCartVM.OrderHeader.Id}",
//                    CancelUrl = domain + $"customer/cart/index",
//                };

//                foreach (var item in ShoppingCartVM.ListCart)
//                {

//                    var sessionLineItem = new SessionLineItemOptions
//                    {
//                        PriceData = new SessionLineItemPriceDataOptions
//                        {
//                            UnitAmount = (long)(item.Price * 100),//20.00 -> 2000
//                            Currency = "usd",
//                            ProductData = new SessionLineItemPriceDataProductDataOptions
//                            {
//                                Name = item.Product.Title
//                            },

//                        },
//                        Quantity = item.Count,
//                    };
//                    options.LineItems.Add(sessionLineItem);

//                }

//                var service = new SessionService();
//                Session session = service.Create(options);
//                _unitOfWork.OrderHeader.UpdateStripePaymentID(ShoppingCartVM.OrderHeader.Id, session.Id, session.PaymentIntentId);
//                _unitOfWork.Save();
//                Response.Headers.Add("Location", session.Url);
//                return new StatusCodeResult(303);
//            }

//            else
//            {
//                return RedirectToAction("OrderConfirmation", "Cart", new { id = ShoppingCartVM.OrderHeader.Id });
//            }

//        }


//        public IActionResult OrderConfirmation(int id)
//        {
//            OrderHeader orderHeader = _unitOfWork.OrderHeader.GetFirstOrDefault(u => u.Id == id, includeProperties: "ApplicationUser");
//            if (orderHeader.PaymentStatus != RolesConstant.PaymentStatusDelayedPayment)
//            {
//                var service = new SessionService();
//                Session session = service.Get(orderHeader.SessionId);
//                //check the stripe status
//                if (session.PaymentStatus.ToLower() == "paid")
//                {
//                    _unitOfWork.OrderHeader.UpdateStatus(id, RolesConstant.StatusApproved, RolesConstant.PaymentStatusApproved);
//                    _unitOfWork.Save();
//                }
//            }
//            //_emailSender.SendEmailAsync(orderHeader.User.Email, "New Order - Bulky Book", "<p>New Order Created</p>");
//            List<ShoppingCart> shoppingCarts = _unitOfWork.ShoppingCart.GetAll(u => u.UserId ==
//            orderHeader.UserId).ToList();
//            _unitOfWork.ShoppingCart.RemoveRange(shoppingCarts);
//            _unitOfWork.Save();
//            return View(id);
//        }


//        private double GetPriceBasedOnQuantity(double quantity, double price, double price50, double price100)
//        {
//            if (quantity <= 50)
//            {
//                return price;
//            }
//            else
//            {
//                if (quantity <= 100)
//                {
//                    return price50;
//                }
//                return price100;
//            }
//        }
//    }
//}



using E_CommerceStore_Udemey.Core.Constants;
using E_CommerceStore_Udemey.DATA;
using E_CommerceStore_Udemey.DATA.Models;
using E_CommerceStore_Udemey.Infrastructure.Services.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Stripe.Checkout;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace BulkyBookWeb.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize]
    public class CartController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmailSender _emailSender;

        [BindProperty]
        public ShoppingCartVM ShoppingCartVM { get; set; }
        public CartController(IUnitOfWork unitOfWork, IEmailSender emailSender)
        {
            _unitOfWork = unitOfWork;
            _emailSender = emailSender;

        }
        public IActionResult Index()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            ShoppingCartVM = new ShoppingCartVM()
            {
                ListCart = _unitOfWork.ShoppingCart.GetAll(u => u.UserId == claim.Value,
                includeProperties: "Product"),
                OrderHeader = new()
            };
            foreach (var cart in ShoppingCartVM.ListCart)
            {
                cart.Price = GetPriceBasedOnQuantity(cart.Count, cart.Product.Price,
                    cart.Product.Price50, cart.Product.Price100);
                ShoppingCartVM.OrderHeader.OrderTotal += (cart.Price * cart.Count);
            }
            return View(ShoppingCartVM);
        }

        public IActionResult Summary()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            ShoppingCartVM = new ShoppingCartVM()
            {
                ListCart = _unitOfWork.ShoppingCart.GetAll(u => u.UserId == claim.Value,
                includeProperties: "Product"),
                OrderHeader = new()
            };
            ShoppingCartVM.OrderHeader.User = _unitOfWork.ApplicationUser.GetFirstOrDefault(
                u => u.Id == claim.Value);

            ShoppingCartVM.OrderHeader.Name = ShoppingCartVM.OrderHeader.User.FullName;
            ShoppingCartVM.OrderHeader.PhoneNumber = ShoppingCartVM.OrderHeader.User.PhoneNumber;
            ShoppingCartVM.OrderHeader.StreetAddress = ShoppingCartVM.OrderHeader.User.StreetAddress;
            ShoppingCartVM.OrderHeader.City = ShoppingCartVM.OrderHeader.User.City;
            ShoppingCartVM.OrderHeader.State = ShoppingCartVM.OrderHeader.User.State;
            ShoppingCartVM.OrderHeader.PostalCode = ShoppingCartVM.OrderHeader.User.PostalCode;



            foreach (var cart in ShoppingCartVM.ListCart)
            {
                cart.Price = GetPriceBasedOnQuantity(cart.Count, cart.Product.Price,
                    cart.Product.Price50, cart.Product.Price100);
                ShoppingCartVM.OrderHeader.OrderTotal += (cart.Price * cart.Count);
            }
            return View(ShoppingCartVM);
        }

        [HttpPost]
        [ActionName("Summary")]
        [ValidateAntiForgeryToken]
        public IActionResult SummaryPOST()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            ShoppingCartVM.ListCart = _unitOfWork.ShoppingCart.GetAll(u => u.UserId == claim.Value,
                includeProperties: "Product");


            ShoppingCartVM.OrderHeader.OrderDate = System.DateTime.Now;
            ShoppingCartVM.OrderHeader.UserId = claim.Value;


            foreach (var cart in ShoppingCartVM.ListCart)
            {
                cart.Price = GetPriceBasedOnQuantity(cart.Count, cart.Product.Price,
                    cart.Product.Price50, cart.Product.Price100);
                ShoppingCartVM.OrderHeader.OrderTotal += (cart.Price * cart.Count);
            }
              User applicationUser = _unitOfWork.ApplicationUser.GetFirstOrDefault(u => u.Id == claim.Value);

            if (applicationUser.CompanyId.GetValueOrDefault() == 0)
            {
                ShoppingCartVM.OrderHeader.PaymentStatus = RolesConstant.PaymentStatusPending;
                ShoppingCartVM.OrderHeader.OrderStatus = RolesConstant.StatusPending;
            }
            else
            {
                ShoppingCartVM.OrderHeader.PaymentStatus = RolesConstant.PaymentStatusDelayedPayment;
                ShoppingCartVM.OrderHeader.OrderStatus = RolesConstant.StatusApproved;
            }

            _unitOfWork.OrderHeader.Add(ShoppingCartVM.OrderHeader);
            _unitOfWork.Save();
            foreach (var cart in ShoppingCartVM.ListCart)
            {
                OrderDetail orderDetail = new()
                {
                    ProductId = cart.ProductId,
                    OrderId = ShoppingCartVM.OrderHeader.Id,
                    Price = cart.Price,
                    Count = cart.Count
                };
                _unitOfWork.OrderDetail.Add(orderDetail);
                _unitOfWork.Save();
            }


            if (applicationUser.CompanyId.GetValueOrDefault() == 0)
            {
                //stripe settings 
                var domain = "https://localhost:5001/";
                var options = new SessionCreateOptions
                {
                    PaymentMethodTypes = new List<string>
                {
                  "card",
                },
                    LineItems = new List<SessionLineItemOptions>(),
                    Mode = "payment",
                    SuccessUrl = domain + $"customer/cart/OrderConfirmation?id={ShoppingCartVM.OrderHeader.Id}",
                    CancelUrl = domain + $"customer/cart/index",
                };

                foreach (var item in ShoppingCartVM.ListCart)
                {

                    var sessionLineItem = new SessionLineItemOptions
                    {
                        PriceData = new SessionLineItemPriceDataOptions
                        {
                            UnitAmount = (long)(item.Price * 100),//20.00 -> 2000
                            Currency = "usd",
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Name = item.Product.Title
                            },

                        },
                        Quantity = item.Count,
                    };
                    options.LineItems.Add(sessionLineItem);

                }

                var service = new SessionService();
                Session session = service.Create(options);
                _unitOfWork.OrderHeader.UpdateStripePaymentID(ShoppingCartVM.OrderHeader.Id, session.Id, session.PaymentIntentId);
                _unitOfWork.Save();
                Response.Headers.Add("Location", session.Url);
                return new StatusCodeResult(303);
            }

            else
            {
                return RedirectToAction("OrderConfirmation", "Cart", new { id = ShoppingCartVM.OrderHeader.Id });
            }
        }

        public IActionResult OrderConfirmation(int id)
        {
            OrderHeader orderHeader = _unitOfWork.OrderHeader.GetFirstOrDefault(u => u.Id == id, includeProperties: "User");
            if (orderHeader.PaymentStatus != RolesConstant.PaymentStatusDelayedPayment)
            {
                var service = new SessionService();
                Session session = service.Get(orderHeader.SessionId);
                //check the stripe status
                if (session.PaymentStatus.ToLower() == "paid")
                {
                    _unitOfWork.OrderHeader.UpdateStatus(id, RolesConstant.StatusApproved, RolesConstant.PaymentStatusApproved);
                    _unitOfWork.Save();
                }
            }
            //_emailSender.SendEmailAsync(orderHeader.User.Email, "New Order - Bulky Book", "<p>New Order Created</p>");
            List<ShoppingCart> shoppingCarts = _unitOfWork.ShoppingCart.GetAll(u => u.UserId ==
            orderHeader.UserId).ToList();
            _unitOfWork.ShoppingCart.RemoveRange(shoppingCarts);
            HttpContext.Session.Clear();

            _unitOfWork.Save();
            return View(id);
        }

        public IActionResult Plus(int cartId)
        {
            var cart = _unitOfWork.ShoppingCart.GetFirstOrDefault(u => u.Id == cartId);
            _unitOfWork.ShoppingCart.IncrementCount(cart, 1);
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Minus(int cartId)
        {
            var cart = _unitOfWork.ShoppingCart.GetFirstOrDefault(u => u.Id == cartId);
            if (cart.Count <= 1)
            {
                _unitOfWork.ShoppingCart.Remove(cart);
                var count = _unitOfWork.ShoppingCart.GetAll(u => u.UserId == cart.UserId).ToList().Count - 1;
                HttpContext.Session.SetInt32(RolesConstant.SessionCart, count);
            }
            else
            {
                _unitOfWork.ShoppingCart.DecrementCount(cart, 1);
            }
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Remove(int cartId)
        {
            var cart = _unitOfWork.ShoppingCart.GetFirstOrDefault(u => u.Id == cartId);
            _unitOfWork.ShoppingCart.Remove(cart);
            _unitOfWork.Save();
            var count = _unitOfWork.ShoppingCart.GetAll(u => u.UserId == cart.UserId).ToList().Count;
            HttpContext.Session.SetInt32(RolesConstant.SessionCart, count);
            return RedirectToAction(nameof(Index));
        }





        private double GetPriceBasedOnQuantity(double quantity, double price, double price50, double price100)
        {
            if (quantity <= 50)
            {
                return price;
            }
            else
            {
                if (quantity <= 100)
                {
                    return price50;
                }
                return price100;
            }
        }
    }
}
