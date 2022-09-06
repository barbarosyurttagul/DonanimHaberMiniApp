using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using DH.MvcUI.Models;
using DH.Business.Abstract;
using X.PagedList;
using DH.Core.CrossCuttingConcerns.Caching;
using DH.Entities.Concrete;

namespace DH.MvcUI.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IPostService _postService;
    private readonly ICacheManager _cacheManager;

    public HomeController(ILogger<HomeController> logger, IPostService postService, ICacheManager cacheManager)
    {
        _logger = logger;
        _postService = postService;
        _cacheManager = cacheManager;
    }

    public IActionResult Index(int? page)
    {
        var pageNumber = page ?? 1;
            var pageSize = 3;
            var posts = GetPostsFromCache().ToPagedList(pageNumber, pageSize);
            var model = new PostIndexViewModel
            {
                posts = posts
            };
        return View(model);
    }

    private List<Post> GetPostsFromCache()
    {
        return _cacheManager.GetOrAdd("allposts", () => { return _postService.GetAll(); });
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
