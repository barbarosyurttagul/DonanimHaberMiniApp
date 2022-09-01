using DH.Business.Abstract;
using Microsoft.AspNetCore.Mvc;
using DH.Entities.Concrete;

namespace DH.MvcUI.Controllers
{
    public class PostController : Controller
    {        
        private readonly IPostService _postService;
        public PostController(IPostService postService)
        {
            _postService = postService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreatePost(Post post)
        {
            _postService.Insert(post);
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Detail(int id)
        {
            Post post = _postService.GetById(id);
            List<Post> replies = _postService.GetReplies(id);
            var model = new PostDetailViewModel
            {
                Post = post,
                Replies = replies
            };
            return View(model);
        }

        public IActionResult Reply(int id)
        {
            Post post = new Post();            
            ViewBag.RootId = id;
            ViewBag.PostTitle = _postService.GetById(id).PostTitle;
            var model = new PostSaveViewModel
            {
                Post = post
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult ReplyPost(int rootId, string postTitle, Post post)
        {
            post.RootId = rootId;
            post.PostTitle = postTitle;
            _postService.InsertReply(post);
            return RedirectToAction("Detail", "Post", new {id = rootId});
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}