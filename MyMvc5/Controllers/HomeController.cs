using EF.Business.IService.ICombinationService;
using EF.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace MyMvc5.Controllers
{
    /// <summary>
    /// NuGet包引用：Unity.Mvc5/Unity.Abstractions/Unity.Container
    /// </summary>
    public class HomeController : Controller
    {
        //注意：需要考虑DBContext释放问题，通过依赖注入构造太多DBContext导致性能问题
        private readonly IMqLogService _iMqLogService = null;
        public HomeController(IMqLogService iMqLogService)
        {
            _iMqLogService = iMqLogService;
        }
        public ActionResult Index()
        {
            //1 using释放DBContext
            using (IMqLogService iMqLogService = _iMqLogService)
            {
                tb_log lg = _iMqLogService.Find<tb_log>(1);
            }
            //2 手动释放DBContext
            //_iMqLogService.Dispose();

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}