using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Subproject_2;        //Our own dataservice as a library
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.Language.CodeGeneration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;

namespace WebService.Controllers
{
    [Route("/api/posts")]
    public class PostController : Controller
    {
        private const int standardPageSize = 15;
        private IDataServicePost _dataService;

        public PostController(IDataServicePost dataService)
        {
            _dataService = dataService;
        }

        [HttpGet(Name = nameof(GetPost))]
        public IActionResult GetPost(int page = 0, int pageSize = standardPageSize)
        {
            var totalPosts = _dataService.amountPost();
            var totalPages = GetTotalPages(pageSize, totalPosts);
            if (page > totalPages-1)
            {
                page = 0;
            }
            var posts = _dataService.getPost(page, pageSize)
            .Select(x => new {
                 Link = Url.Link(nameof(GetSpecificPost), new { id = x.id }),
                 Body = x.text,
                 x.title
             });
            if (posts == null) return NotFound();
            var result = new
            {
                Total = totalPosts,
                Pages = totalPages,
                Page = page,
                Prev = Link(nameof(GetPost), page, pageSize, -1, () => page > 0),
                Next = Link(nameof(GetPost), page, pageSize, 1, () => page < totalPages - 1),
                Url = Link(nameof(GetPost), page, pageSize),
                Data = posts
            };
            return Ok(result);
        }

        [HttpGet("{id}",Name = nameof(GetSpecificPost))]
        public IActionResult GetSpecificPost(int id)
        {
            var post = _dataService.getPostById(id)
                .Select(x => new {
                    Link = Url.Link("GetCommentsByPost", new { postId = id }),
                    x.title,
                    Body = x.text
                });

            return Ok(post);
        }

        [HttpGet("tag/{tagName}", Name = nameof(GetPostByTag))]
        public IActionResult GetPostByTag(string tagName, int page = 0, int pageSize = standardPageSize)
        {
            var totalPosts = _dataService.amountPostByTag(tagName);
            var totalPages = GetTotalPages(pageSize, totalPosts);
            if (page > totalPages - 1)
            {
                page = 0;
            }
            var post = _dataService.getPostByTag(tagName, page, pageSize);
            if (post == null) return NotFound();
            var result = new
            {
                Total = totalPosts,
                Pages = totalPages,
                Page = page,
                Prev = Link(nameof(GetPost), page, pageSize, -1, () => page > 0),
                Next = Link(nameof(GetPost), page, pageSize, 1, () => page < totalPages - 1),
                Url = Link(nameof(GetPost), page, pageSize),
                Data = post
            };
            return Ok(result);
        }

        [HttpGet("comments/{postId}", Name = nameof(GetCommentsByPost))]
        public IActionResult GetCommentsByPost(int postId, int page = 0, int pageSize = standardPageSize)
        {
            var comments = _dataService.getCommments(postId, page, pageSize);
            if (comments == null) return NotFound();
            return Ok(comments);
        }

        [HttpGet("user/{userId}", Name = nameof(GetPostsByUserId))]
        public IActionResult GetPostsByUserId(int userId, int page = 0, int pageSize = standardPageSize)
        {
            var totalPosts = _dataService.amountPostByUser(userId);
            var totalPages = GetTotalPages(pageSize, totalPosts);
            if (page > totalPages - 1)
            {
                page = 0;
            }
            var posts = _dataService.getPostByUser(userId, page, pageSize);
            if (posts == null) return NotFound();
            var result = new
            {
                Total = totalPosts,
                Pages = totalPages,
                Page = page,
                Prev = Link(nameof(GetPost), page, pageSize, -1, () => page > 0),
                Next = Link(nameof(GetPost), page, pageSize, 1, () => page < totalPages - 1),
                Url = Link(nameof(GetPost), page, pageSize),
                Data = posts
            };
            return Ok(result);
        }

        [HttpGet("title/{substring}", Name = nameof(GetPostsByName))]
        public IActionResult GetPostsByName(string substring, int page = 0, int pageSize = standardPageSize)
        {
            var totalPosts = _dataService.amountPostWord(substring);
            var totalPages = GetTotalPages(pageSize, totalPosts);
            if (page > totalPages - 1)
            {
                page = 0;
            }
            var posts = _dataService.getPostWord(substring, page, pageSize);
            if (posts == null) return NotFound();
            var result = new
            {
                Total = totalPosts,
                Pages = totalPages,
                Page = page,
                Prev = Link(nameof(GetPost), page, pageSize, -1, () => page > 0),
                Next = Link(nameof(GetPost), page, pageSize, 1, () => page < totalPages - 1),
                Url = Link(nameof(GetPost), page, pageSize),
                Data = posts
            };
            return Ok(result);
        }

        private static int GetTotalPages(int pageSize, int total)
        {
            return (int)Math.Ceiling(total / (double)pageSize);
        }

        private string Link(string route, int page, int pageSize, int pageInc = 0, Func<bool> f = null)
        {
            if (f == null) return Url.Link(route, new { page, pageSize });

            return f()
                ? Url.Link(route, new { page = page + pageInc, pageSize })
                : null;
        }

    }
}