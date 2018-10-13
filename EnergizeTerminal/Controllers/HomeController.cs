using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace EnergizeTerminal.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            ViewData["Welcome"] = $@"
 ______ _   _ ______ _____   _____ _____ ____________
|  ____| \ | |  ____|  __ \ / ____|_   _|___  /  ____|
| |__  |  \| | |__  | |__) | |  __  | |    / /| |__
|  __| | . ` |  __| |  _  /| | |_ | | |   / / |  __|
| |____| |\  | |____| | \ \| |__| |_| |_ / /__| |____
|______|_| \_|______|_|  \_\\_____|_____/_____|______|

--------------------------------------------------------
WELCOME TO THE ENERGIZE ADMINISTRATION SUB-SYSTEM.
PLEASE DO IDENTIFY USING THE {"\"login\""} COMMAND.
--------------------------------------------------------";
            return View();
        }
    }
}
