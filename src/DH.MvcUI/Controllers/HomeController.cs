﻿using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using DH.MvcUI.Models;
using DH.Business.Abstract;
using X.PagedList;

namespace DH.MvcUI.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IPostService _postService;

    public HomeController(ILogger<HomeController> logger, IPostService postService)
    {
        _logger = logger;
        _postService = postService;
    }

    public IActionResult Index(int? page)
    {
        var pageNumber = page ?? 1;
            var pageSize = 3;
            var posts = _postService.GetAll().ToPagedList(pageNumber, pageSize);
            var model = new PostIndexViewModel
            {
                posts = posts
            };
            return View(model);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
