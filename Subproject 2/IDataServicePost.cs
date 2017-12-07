using System;
using System.Collections.Generic;
using System.Text;

namespace Subproject_2
{
    public interface IDataServicePost
    {
        List<Post> getPost(int pageint, int pageSize);


        List<Post> getPostWord(string postword, int page, int pageSize);

        List<Post> getPostByTag(string tag, int page, int pageSize);

        List<Post> getPostByUser(int postuserid, int page, int pageSize);

        Post getCommments(int postid, int page, int pageSize);

        int amountPost();

        int amountPostWord(string postWord);

        int amountPostByTag(string tag);

        int amountPostByUser(int postuserid);

        int amountComments(int postid);
    }       
}
