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
        private const int startAmount = 10;
        private IDataServicePost _dataService;

        public PostController(IDataServicePost dataService)
        {
            _dataService = dataService;
        }

        [HttpGet(Name = nameof(GetPostQ))]
        public IActionResult GetPostQ(int page = 0, int pageSize = standardPageSize)
        {
            var totalPostsQ = _dataService.amountPostQ();
            var totalPages = GetTotalPages(pageSize, totalPostsQ);
            if (page > totalPages - 1)
            {
                page = 0;
            }
            var posts = _dataService.getPostQ(page, pageSize)
            .Select(x => new
            {
                Link = Url.Link(nameof(GetSpecificPost), new { id = x.id }),
                Body = x.text,
                Date = x.creationDate,
                Score = x.score,
                id = x.id,
                x.title
            });
            if (posts == null) return NotFound();
            var result = new
            {
                Total = totalPostsQ,
                Pages = totalPages,
                Page = page,
                Prev = Link(nameof(GetPostQ), page, pageSize, -1, () => page > 0),
                Next = Link(nameof(GetPostQ), page, pageSize, 1, () => page < totalPages - 1),
                Url = Link(nameof(GetPostQ), page, pageSize),
                Data = posts
            };
            return Ok(result);
        }

        [HttpGet("answers/{id}", Name = nameof(GetPostA))]
        public IActionResult GetPostA(int id ,int page = 0, int pageSize = standardPageSize)
        {
            var totalPostsA = _dataService.amountPostA(id);
            var totalPages = GetTotalPages(pageSize, totalPostsA);
            if (page > totalPages - 1)
            {
                page = 0;
            }
            var posts = _dataService.getPostA(id, page, pageSize)
            .Select(x => new
            {
                Link = Url.Link(nameof(GetSpecificPost), new { id = x.id }),
                CommentsLink = Url.Link("GetCommentsByPost", new { postId = x.id }),
                Body = x.text,
                x.title,
            });
            if (posts == null) return NotFound();
            var result = new
            {
                Total = totalPostsA,
                Pages = totalPages,
                Page = page,
                Prev = Link(nameof(GetPostA), page, pageSize, -1, () => page > 0),
                Next = Link(nameof(GetPostA), page, pageSize, 1, () => page < totalPages - 1),
                Url = Link(nameof(GetPostA), page, pageSize),
                Data = posts
            };
            return Ok(result);
        }

        [HttpGet("{id}", Name = nameof(GetSpecificPost))]
        public IActionResult GetSpecificPost(int id)
        {
            var post = _dataService.getPostById(id)
                .Select(x => new
                {
                    CommentsLink = Url.Link("GetCommentsByPost", new { postId = id }),
                    AnswersLink = Url.Link("GetPostA", new { postId = id}),
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
            var post = _dataService.getPostByTag(tagName, page, pageSize)
                .Select(x => new
                {
                    Link = Url.Link(nameof(GetSpecificPost), new { id = x.id }),
                    Body = x.text,
                    x.title
                });
            if (post == null) return NotFound();
            var result = new
            {
                Total = totalPosts,
                Pages = totalPages,
                Page = page,
                Prev = Link(nameof(GetPostByTag), page, pageSize, -1, () => page > 0),
                Next = Link(nameof(GetPostByTag), page, pageSize, 1, () => page < totalPages - 1),
                Url = Link(nameof(GetPostByTag), page, pageSize),
                Data = post
            };
            return Ok(result);
        }

        [HttpGet("comments/{postId}", Name = nameof(GetCommentsByPost))]
        public IActionResult GetCommentsByPost(int postId, int page = 0, int pageSize = standardPageSize)
        {

            var totalComments = _dataService.amountComments(postId);
            var totalPages = GetTotalPages(pageSize, totalComments);
            if (page > totalPages - 1)
            {
                page = 0;
            }

            var comments = _dataService.getCommments(postId, page, pageSize);
            if (comments == null) return NotFound();
            var result = new
            {
                Total = totalComments,
                Pages = totalPages,
                Page = page,
                Prev = Link(nameof(GetCommentsByPost), page, pageSize, -1, () => page > 0),
                Next = Link(nameof(GetCommentsByPost), page, pageSize, 1, () => page < totalPages - 1),
                Url = Link(nameof(GetCommentsByPost), page, pageSize),
                Data = comments
            };
            return Ok(result);
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
                Prev = Link(nameof(GetPostQ), page, pageSize, -1, () => page > 0),
                Next = Link(nameof(GetPostQ), page, pageSize, 1, () => page < totalPages - 1),
                Url = Link(nameof(GetPostQ), page, pageSize),
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
            var posts = _dataService.getPostWord(substring, page, pageSize)
               .Select(x => new
               {
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
                Prev = Link(nameof(GetPostsByName), page, pageSize, -1, () => page > 0),
                Next = Link(nameof(GetPostsByName), page, pageSize, 1, () => page < totalPages - 1),
                Url = Link(nameof(GetPostsByName), page, pageSize),
                Data = posts
            };
            return Ok(result);
        }

        [HttpGet("words/{search}", Name = nameof(GetWordsForWordCloud))]
        public IActionResult GetWordsForWordCloud(string search, int amount = startAmount)
        {
            var words = _dataService.wordCloud(search).Take(amount);

            if (words == null) return NotFound();
            else
            {
                return Ok(words);
            }
        }

        [HttpGet("weights/{search}", Name = nameof(GetWeightedPostList))]
        public IActionResult GetWeightedPostList(string search, int page = 0, int pageSize = standardPageSize)
        {
            var totalPosts = _dataService.amountWeightPosts(search);
            var totalPages = GetTotalPages(pageSize, totalPosts);
            if (page > totalPages - 1)
            {
                page = 0;
            }
            var posts = _dataService.getWeightedPosts(search, page, pageSize)
               .Select(x => new
                {
                    Link = Url.Link(nameof(GetSpecificPost), new { id = x.postId }),
                    title = x.title,
                    weight = x.weight
                });

            if (posts == null) return NotFound();
            var result = new
            {
                Total = totalPosts,
                Pages = totalPages,
                Page = page,
                Prev = Link(nameof(GetWeightedPostList), page, pageSize, -1, () => page > 0),
                Next = Link(nameof(GetWeightedPostList), page, pageSize, 1, () => page < totalPages - 1),
                Url = Link(nameof(GetWeightedPostList), page, pageSize),
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