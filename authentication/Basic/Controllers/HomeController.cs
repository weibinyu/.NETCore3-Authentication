using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Basic.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public IActionResult Secret()
        {
            return View();
        }

        //works just like microsoft identity
        public IActionResult Authenticate()
        {
            var gClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name,"Bob"),
                new Claim("G.Says","Ok")
            };

            var licenseClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name,"Bobby"),
                new Claim("License","A")
            };

            var gIdentity = new ClaimsIdentity(gClaims, "G Identity");
            var licenseIdentity = new ClaimsIdentity(licenseClaims, "license Identity");
            var userPrincipal = new ClaimsPrincipal(new[] { gIdentity, licenseIdentity});

            HttpContext.SignInAsync(userPrincipal);

            return RedirectToAction("Index");
        }
    }
}
