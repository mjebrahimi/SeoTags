using Microsoft.AspNetCore.Mvc;

namespace SeoTags.Sample.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            //HttpContext.SetSeoInfo(seoInfo =>
            //{
            //    //Set SEO info
            //});
            return View();
        }

        public IActionResult JsonLd1()
        {
            return View();
        }

        public IActionResult JsonLd2()
        {
            return View();
        }
    }
}
