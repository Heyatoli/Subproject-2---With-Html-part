using System;
using System.Collections.Generic;
using System.Text;

namespace Subproject_2
{
    public interface IDataServiceUser
    {

        int userAmount();

        List<User> getUser(int page, int pageSize);

        int userNameAmount(string name);
        
        List<User> getUsername(string name, int page, int pageSize);

        int historyAmount(int id);
        
        List<History> getHistory(int id, int page, int pageSize);

        int markingAmount(int id);
        List<Marking> getFavourites(int id, int page, int pageSize);
        
        History createHistory(int userid, string search);

        Marking createMarking(int userid, int postid, string note);

        bool updateMarking(int userid, int postid, string note);

        bool deleteHistory(int histId);

        bool deleteFavourites(int userid, int postid);
    }
}
