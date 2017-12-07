using System;
using Xunit;
using System.Linq;

namespace DataServiceTest
{
    public class UnitTest1
    {
        [Fact]
        public void GetPostWithCommentCountComments()
        {
            var service = new Subproject_2.DataservicePost();
            var post = service.getCommments(19, 0, 15);
            Assert.Equal(16, post.comments.Count);
        }

        [Fact]
        public void GetPostListReturnsPostById()
        {
            var service = new Subproject_2.DataservicePost();
            var post = service.getPost(0, 14000);
            Assert.Equal(13629, post.Count);
            Assert.Equal(19, post.First().id);
        }

        [Fact]
        public void CreateUserHistoryReturnNewHistory()
        {
            var service = new Subproject_2.DataserviceUser();
            var user = service.createHistory(1, "What");
            Assert.Equal("What", user.searchWord);
            Assert.True(user.id>0);
            service.deleteHistory(user.id);
        }

        [Fact]
        public void UpdateMarkingWithNewNoteValue()
        {
            var service = new Subproject_2.DataserviceUser();
            var user = service.createMarking(1, 19, "What");
            var user2 = service.updateMarking(1, 19, "Who");
            Assert.True(user2);
            var marking = service.getFavourites(1, 0, 15);
            Assert.Equal("Who", marking.Last().note);
            service.deleteFavourites(user.userID, user.postId);
        }
    }
}
