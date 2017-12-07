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

namespace StackoverflowWebservice.Controllers
{
    [Route("/api/users")]
    public class UserController : Controller
    {

        private const int standardPageSize = 15;

        private IDataServiceUser _dataService;

        public UserController(IDataServiceUser dataService)
        {
            _dataService = dataService;
        }

        [HttpGet(Name = nameof(GetUsers))]
        public IActionResult GetUsers(int page = 0, int pageSize = standardPageSize)
        {
            var totalUsers = _dataService.userAmount();
            var totalPages = GetTotalPages(pageSize, totalUsers);
            if (page > totalPages - 1)
            {
                page = 0;
            }
            var users = _dataService.getUser(page, pageSize);
            if (users == null) return NotFound();
            var result = new
            {
                Total = totalUsers,
                Pages = totalPages,
                Page = page,
                Prev = Link(nameof(GetUsers), page, pageSize, -1, () => page > 0),
                Next = Link(nameof(GetUsers), page, pageSize, 1, () => page < totalPages - 1),
                Url = Link(nameof(GetUsers), page, pageSize),
                Data = users
            };
            return Ok(result);
        }

        [HttpGet("{name}", Name = nameof(GetUsersByName))]
        public IActionResult GetUsersByName(string name, int page = 0, int pageSize = standardPageSize)
        {
            var totalUsers = _dataService.userNameAmount(name);
            var totalPages = GetTotalPages(pageSize, totalUsers);
            if (page > totalPages - 1)
            {
                page = 0;
            }
            var users = _dataService.getUsername(name, page, pageSize);
            if (users == null) return NotFound();
            var result = new
            {
                Total = totalUsers,
                Pages = totalPages,
                Page = page,
                Prev = Link(nameof(GetUsersByName), page, pageSize, -1, () => page > 0),
                Next = Link(nameof(GetUsersByName), page, pageSize, 1, () => page < totalPages - 1),
                Url = Link(nameof(GetUsersByName), page, pageSize),
                Data = users
            };
            return Ok(result);
        }

        [HttpGet("history/{userId}", Name = nameof(GetUserHistory))]
        public IActionResult GetUserHistory(int userId, int page = 0, int pageSize = standardPageSize)
        {
            var totalHistory = _dataService.historyAmount(userId);
            var totalPages = GetTotalPages(pageSize, totalHistory);
            if (page > totalPages - 1)
            {
                page = 0;
            }
            var history = _dataService.getHistory(userId, page, pageSize);
            if (history == null) return NotFound();
            var result = new
            {
                Total = totalHistory,
                Pages = totalPages,
                Page = page,
                Prev = Link(nameof(GetUserHistory), page, pageSize, -1, () => page > 0),
                Next = Link(nameof(GetUserHistory), page, pageSize, 1, () => page < totalPages - 1),
                Url = Link(nameof(GetUserHistory), page, pageSize),
                Data = history
            };
            return Ok(result);
        }

        [HttpGet("markings/{userId}", Name = nameof(GetUserMarkings))]
        public IActionResult GetUserMarkings(int userId, int page = 0, int pageSize = standardPageSize)
        {
            var totalMarkings = _dataService.markingAmount(userId);
            var totalPages = GetTotalPages(pageSize, totalMarkings);
            if (page > totalPages - 1)
            {
                page = 0;
            }
            var markings = _dataService.getFavourites(userId, page, pageSize);
            if (markings == null) return NotFound();
            var result = new
            {
                Total = totalMarkings,
                Pages = totalPages,
                Page = page,
                Prev = Link(nameof(GetUserHistory), page, pageSize, -1, () => page > 0),
                Next = Link(nameof(GetUserHistory), page, pageSize, 1, () => page < totalPages - 1),
                Url = Link(nameof(GetUserHistory), page, pageSize),
                Data = markings
            };
            return Ok(result);
        }

        [HttpDelete("history/{histId}")]
        public IActionResult DeleteUserHistory(int histId)
        {
            var delete = _dataService.deleteHistory(histId);
            if (delete == false) return NotFound();
            return Ok(histId);

        }

        [HttpDelete("markings/{postId}/{userId}")]
        public IActionResult DeleteUserMarking(int postId, int userId)
        {
            var delete = _dataService.deleteFavourites(postId, userId);
            if (delete == false) return NotFound();
            return Ok(postId);

        }

        [HttpPost("history", Name = nameof(PostUserHistory))]
        public IActionResult PostUserHistory([FromBody] History value)
        {
            var history = _dataService.createHistory(value.userId, value.searchWord);
            var url = Url.Link(nameof(GetUserHistory), new { history.userId });
            var result = new
            {
                Search = "localhost:5001/api/posts/title/" + history.searchWord,
                User = url.ToString(),
            };
            return Created(url, result);
        }

        [HttpPost("markings", Name = nameof(PostUserMarking))]
        public IActionResult PostUserMarking([FromBody] Marking value)
        {
            var marking = _dataService.createMarking(value.userID, value.postId, value.note);
            var url = Url.Link(nameof(GetUserMarkings), new { marking.userID });
            var result = new
            {
                Post = "localhost:5001/api/posts",
                User = url.ToString(),
                Note = marking.note
            };
            return Created(url, result);
        }
        [HttpPut("markings")]
        public IActionResult PutMarkings([FromBody] Marking value)
        {
            var update = _dataService.updateMarking(value.userID, value.postId, value.note);
            if (update == false) return NotFound();
            return Ok();
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
