﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PeterGardinerAssignment5.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using PeterGardinerAssignment5.Models;
using PeterGardinerAssignment5.Models.ViewModels;

namespace PeterGardinerAssignment5.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private IProductRepository _repository;

        public int PageSize = 5;

        public HomeController(ILogger<HomeController> logger, IProductRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public IActionResult Index(int page=1)
        {
            return View(new ProjectListViewModel
            {
                Products = _repository.Projects.OrderBy(p => p.BookId).Skip((page - 1) * PageSize).Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalNumItems = _repository.Projects.Count()
                }
            });
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
