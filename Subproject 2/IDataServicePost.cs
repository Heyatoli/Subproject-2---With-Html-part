using System;
using System.Collections.Generic;
using System.Text;

namespace Subproject_2
{
    public interface IDataServicePost
    {
        List<Post> getPostQ(int page, int pageSize);

        List<Post> getPostA(int id, int page, int pageSize);

        List<Post> getPostById(int id);

        List<Post> getPostWord(string postword, int page, int pageSize);

        List<Post> getPostByTag(string tag, int page, int pageSize);

        List<Post> getPostByUser(int postuserid, int page, int pageSize);

        Post getCommments(int postid, int page, int pageSize);

        int amountPostQ();

        int amountPostA(int id);

        int amountPostWord(string postWord);

        int amountPostByTag(string tag);

        int amountPostByUser(int postuserid);

        int amountComments(int postid);
    }       
}
