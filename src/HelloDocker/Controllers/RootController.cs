﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;

namespace HelloDocker.Controllers
{
  public class RootController : Controller
  {
    [HttpGet("")]
    public IActionResult Index()
    {
      return View();
    }
  }
}
