﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using MySql.Data.MySqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Subproject_2
{
    public class DataservicePost : IDataServicePost
    {
        public List<Word> wordCloud(string search)
        {

            List<Word> words = new List<Word>();
            using (var db = new stackOverflowContext())
            {
                var conn = (MySqlConnection)db.Database.GetDbConnection();
                conn.Open();
                var cmd = new MySqlCommand();
                cmd.Connection = conn;

                cmd.Parameters.Add("@1", DbType.String);

                cmd.Parameters["@1"].Value = search;

                cmd.CommandText = "call subpj3_b6(@1)";

                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Word word = new Word
                    {
                        Name = reader.GetString(0),
                        weight = reader.GetDouble(1)
                    };
                    words.Add(word);
                }
            }
            
            return words;
        }

        public Post getCommments(int postid, int page, int pageSize)
        {
            using (var db = new stackOverflowContext())
            {
                var query1 =
                    (from c in db.comments
                    where c.postId == postid
                    select new Comment
                    {
                        score = c.score,
                        text = c.text,
                        creationDate = c.creationDate,
                    })
                    .Skip(page * pageSize)
                    .Take(pageSize)
                    .ToList();

                var query2 =
                    (from p in db.posts
                     where p.id == postid
                     select new Post
                     {
                         score = p.score,
                         text = p.text,
                         creationDate = p.creationDate,
                         comments = query1
                     });

                return query2.FirstOrDefault();
            }
        }

        public List<Post> getPostById(int id)
        {
            using (var db = new stackOverflowContext())
            {
                var query =
                    (from u in db.posts
                     where u.id.Equals(id)  //Makes sure it's only questions and not answers - A good solution imo would be to include a boolean (withAnswers or something)
                     select new Post
                     {
                         id = u.id,
                         type = u.type,
                         parent_id = u.parent_id,
                         answer_id = u.answer_id,
                         creationDate = u.creationDate,
                         score = u.score,
                         text = u.text,
                         closedDate = u.closedDate,
                         title = u.title
                     })
                     .ToList();

                return query;
            }
        }

        public List<Post> getPostByTag(string tag, int page, int pageSize)
        {
            using (var db = new stackOverflowContext())
            {
                var query =
                    (from t in db.tags
                     join c in db.combinations
                     on t.id equals c.tags_id
                     join p in db.posts
                     on c.post_id equals p.id
                     where t.name == tag
                     select new Post
                     {
                         id = p.id,
                         type = p.type,
                         parent_id = p.parent_id,
                         answer_id = p.answer_id,
                         creationDate = p.creationDate,
                         score = p.score,
                         text = p.text,
                         closedDate = p.closedDate,
                         title = p.title
                     }).GroupBy(u => u.id)
                     .Select(o => o.FirstOrDefault())
                     .Skip(page * pageSize)
                     .Take(pageSize)
                     .ToList();

                return query;
            }
        }

        public List<Post> getPostByUser(int postuserid, int page, int pageSize)
        {
            using (var db = new stackOverflowContext())
            {
                var query =
                    (from u in db.posts
                     join us in db.user
                     on u.user.id equals us.id
                     where us.id == postuserid 
                     select new Post
                     {
                         type = u.type,
                         parent_id = u.parent_id,
                         answer_id = u.answer_id,
                         creationDate = u.creationDate,
                         score = u.score,
                         text = u.text,
                         closedDate = u.closedDate,
                         title = u.title
                     }).OrderBy(u => u.id)
                     .Skip(page * pageSize)
                     .Take(pageSize)
                     .ToList();
                Console.WriteLine(query.Count());

                return query;
            }
        }

        public List<Post> getPostWord(string postword, int page, int pageSize)
        {
            using (var db = new stackOverflowContext())
            {
                var query =
                    (from p in db.posts
                     where p.title.Contains(postword) 
                     select new Post
                     {
                         id = p.id,
                         type = p.type,
                         parent_id = p.parent_id,
                         answer_id = p.answer_id,
                         creationDate = p.creationDate,
                         score = p.score,
                         text = p.text,
                         closedDate = p.closedDate,
                         title = p.title
                     }).OrderBy(u => u.id)
                     .Skip(page * pageSize)
                     .Take(pageSize)
                     .ToList();

                return query;
            }
        }

        public int amountPostWord(string postWord)
        {
            using (var db = new stackOverflowContext())
            {
                var query =
                    (from p in db.posts
                     where p.title.Contains(postWord)
                     select new Post
                     {
                         id = p.id,

                     }).ToList();

                return query.Count();
            }
        }

        public int amountPostByTag(string tag)
        {
            using (var db = new stackOverflowContext())
            {
                var query =
                    (from t in db.tags
                     join c in db.combinations
                     on t.id equals c.tags_id
                     join p in db.posts
                     on c.post_id equals p.id
                     where t.name == tag
                     select new Post
                     {
                         id = p.id
                     }).ToList();

                return query.Count();
            }
        }

        public int amountPostByUser(int postuserid)
        {
            using (var db = new stackOverflowContext())
            {
                var query =
                    (from u in db.posts
                     join us in db.user
                     on u.user.id equals us.id
                     where us.id == postuserid
                     select new Post
                     {
                         id = u.id
                     }).ToList();

                return query.Count();
            }
        }

        public int amountComments(int postid)
        {
            using (var db = new stackOverflowContext())
            {
                var q =
                    (from c in db.comments
                     where c.postId == postid
                     select new Post
                     {
                         score = c.score
                     }).ToList();

                return q.Count();
            }
        }

        public int amountWeightPosts(string search)
        {
            List<WeightedPost> posts = new List<WeightedPost>();
            using (var db = new stackOverflowContext())
            {
                var conn = (MySqlConnection)db.Database.GetDbConnection();
                conn.Open();
                var cmd = new MySqlCommand();
                cmd.Connection = conn;

                cmd.Parameters.Add("@1", DbType.String);

                cmd.Parameters["@1"].Value = search;

                cmd.CommandText = "call subpj3_b4(@1)";

                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    WeightedPost post = new WeightedPost
                    {
                        postId = reader.GetInt32(0),
                    };
                    posts.Add(post);
                }
            }

            return posts.Count();
        }

        public List<Post> getPostQ(int page, int pageSize)
        {
            using (var db = new stackOverflowContext())
            {
                var query =
                    (from u in db.posts
                     where u.type == 1  //Makes sure it's only questions and not answers - A good solution imo would be to include a boolean (withAnswers or something)
                     select new Post
                     {
                         id = u.id,
                         type = u.type,
                         parent_id = u.parent_id,
                         answer_id = u.answer_id,
                         creationDate = u.creationDate,
                         score = u.score,
                         text = u.text,
                         closedDate = u.closedDate,
                         title = u.title
                     }).OrderBy(u => u.id)
                     .Skip(page * pageSize)
                     .Take(pageSize)
                     .ToList();

                return query;
            }
        }
    
        public List<Post> getPostA(int id, int page, int pageSize)
        {
            using (var db = new stackOverflowContext())
            {
                var query =
                    (from u in db.posts
                     where u.type == 2 && u.parent_id == id
                     select new Post
                     {
                         id = u.id,
                         type = u.type,
                         parent_id = u.parent_id,
                         answer_id = u.answer_id,
                         creationDate = u.creationDate,
                         score = u.score,
                         text = u.text,
                         closedDate = u.closedDate,
                         title = u.title
                     }).OrderBy(u => u.id)
                     .Skip(page * pageSize)
                     .Take(pageSize)
                     .ToList();

                return query;
            }
        }

        public int amountPostQ()
        {
            using (var db = new stackOverflowContext())
            {
                var q =
                    (from p in db.posts
                     where p.type == 1
                     select new Post
                     {
                         id = p.id
                     }).ToList();
                return q.Count();
            }
        }

        public int amountPostA(int id)
        {
            using (var db = new stackOverflowContext())
            {
                var q =
                    (from p in db.posts
                     where p.type == 2 && p.parent_id == id 
                     select new Post
                     {
                         id = p.id
                     }).ToList();
                return q.Count();
            }
        }

        public List<WeightedPost> getWeightedPosts(string search, int page, int pageSize)
        {
            List<WeightedPost> posts = new List<WeightedPost>();
            using (var db = new stackOverflowContext())
            {
                var conn = (MySqlConnection)db.Database.GetDbConnection();
                conn.Open();
                var cmd = new MySqlCommand();
                cmd.Connection = conn;

                cmd.Parameters.Add("@1", DbType.String);

                cmd.Parameters["@1"].Value = search;

                cmd.CommandText = "call subpj3_b4(@1)";

                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    WeightedPost post = new WeightedPost
                    {
                        postId = reader.GetInt32(0),
                        title = reader.GetString(1),
                        weight = reader.GetDouble(3)
                    };
                    posts.Add(post);
                }
            }

            List<WeightedPost> trimmedPostList = new List<WeightedPost>();

            for (int i = page*pageSize; i < (page * pageSize) + pageSize; i++)
            {
                if(i == posts.Count())
                {
                    break;
                }
                trimmedPostList.Add(posts[i]);
            }

            return trimmedPostList;
        }
    }
}



