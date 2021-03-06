﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using MySql.Data.MySqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Subproject_2
{
    public class DataserviceUser : IDataServiceUser
    {

        public History createHistory(int id, string search)
        {

            using (var db = new stackOverflowContext())
            {

                var query = new History
                {
                    userId = id,
                    searchWord = search
                };

                db.history.Add(query);
                db.SaveChanges();
                Console.WriteLine("Succesfully created");
                return query;
            }
        }

        public Marking createMarking(int userid, int postid, string note)
        {
            using (var db = new stackOverflowContext())
            {

                var query = new Marking
                {
                    postId = postid,
                    userID = userid,
                    note = note
                };

                db.marking.Add(query);
                db.SaveChanges();
                return query;

            }
        }

        public bool deleteFavourites(int userid, int postid)
        {
            using (var db = new stackOverflowContext())
            {
                var delete = new Marking
                {
                    userID = userid,
                    postId = postid
                };


                try
                {
                    db.marking.Remove(delete);
                    db.SaveChanges();
                    Console.WriteLine("Deletion complete");
                    return true;
                }
                catch (Exception e)
                {
                    Console.WriteLine("Couldn't delete");
                    return false;
                    throw;
                }
            }
        }

        public bool deleteHistory(int id)
        {
            using (var db = new stackOverflowContext())
            {

                var delete = new History
                {
                    id = id
                };

                try
                {
                    db.history.Remove(delete);
                    db.SaveChanges();
                    Console.WriteLine("Succesfully deleted");
                    return true;

                }
                catch (Exception e)
                {
                    Console.WriteLine("Couldn't delete");
                    return false;
                }


            }
        }

        public int markingAmount(int userid)
        {
            using (var db = new stackOverflowContext())
            {
                var query = (
                    from m in db.marking
                    where m.userID == userid
                    select new Marking
                    {
                        postId = m.postId
                    }).ToList();

                return query.Count();
            }
        }

        public List<Marking> getFavourites(int id, int page, int pageSize)
        {
            using (var db = new stackOverflowContext())
            {
                var q =
                    (from m in db.marking
                     where m.userID == id
                     select new Marking
                     {
                         postId = m.postId,
                         userID = m.userID,
                         note = m.note
                     }).OrderBy(u => u.userID)
                     .Skip(page * pageSize)
                     .Take(pageSize)
                     .ToList();
                return q;
            }
        }

        public int historyAmount(int userid)
        {
            using (var db = new stackOverflowContext())
            {
                var query = (
                    from h in db.history
                    where h.userId == userid
                    select new History
                    {
                        userId = h.userId
                    }).ToList();

                return query.Count();
            }
        }

        public List<History> getHistory(int id, int page, int pageSize)
        {
            using (var db = new stackOverflowContext())
            {

                var q =
                    (from h in db.history
                     where h.userId == id
                     select new History
                     {
                         id = h.id,
                         userId = h.userId,
                         searchWord = h.searchWord
                     }).OrderBy(u => u.id)
                     .Skip(page * pageSize)
                     .Take(pageSize)
                     .ToList();

                return q;
            }
        }

        public int userAmount()
        {
            using (var db = new stackOverflowContext())
            {
                var query = (
                    from u in db.user
                    select new User
                    {
                        id = u.id
                    }).ToList();

                return query.Count();
            }
        }

        public List<User> getUser(int page, int pageSize)
        {
            using (var db = new stackOverflowContext())
            {
                var q =
                    (from u in db.user
                     select new User
                     {
                         age = u.age,
                         creationDate = u.creationDate,
                         location = u.location,
                         name = u.name,
                         id = u.id
                     }).OrderBy(u => u.id)
                     .Skip(page * pageSize)
                     .Take(pageSize)
                     .ToList();

                Console.WriteLine(q.FirstOrDefault());
                return q;
            }
        }

        public int userNameAmount(string s)
        {
            using (var db = new stackOverflowContext())
            {
                var query = (
                    from u in db.user
                    where u.name.Contains(s)
                    select new User
                    {
                        name = u.name
                    }).ToList();

                return query.Count();
            }
        }

        public List<User> getUsername(string s, int page, int pageSize)
        {
            using (var db = new stackOverflowContext())
            {
                var users =
                    (from n in db.user
                     where n.name.Contains(s)
                     select new User
                     {
                         age = n.age,
                         creationDate = n.creationDate,
                         location = n.location,
                         name = n.name
                     }).OrderBy(u => u.id)
                     .Skip(page * pageSize)
                     .Take(pageSize)
                     .ToList();
                return users;
            }
        }

        public bool updateMarking(int userid, int postid, string note)
        {
            using (var db = new stackOverflowContext())
            {

                var update = new Marking
                {
                    postId = postid,
                    userID = userid,
                    note = note
                };

                try
                {
                    db.marking.Update(update);
                    db.SaveChanges();
                    Console.WriteLine("Updated");
                    return true;
                }
                catch (Exception e)
                {

                    Console.WriteLine("Couldn't update");
                    return false;
                }

            }
        }

        public List<User> getUserById(int id)
        {
            using (var db = new stackOverflowContext())
            {
                var query =
                    (from u in db.user
                     where u.id.Equals(id)  //Makes sure it's only questions and not answers - A good solution imo would be to include a boolean (withAnswers or something)
                     select new User
                     {
                         id = u.id,
                         name = u.name,
                         location = u.location,
                         age = u.age,
                         creationDate = u.creationDate,
                     })
                     .ToList();
                return query;
            }
        }
    }
}
