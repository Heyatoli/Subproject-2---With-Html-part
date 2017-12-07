using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Subproject_2
{
    public class Post
    {

        [Column("posts_id")]
        public int id { get; set; }

        [Column("post_type_id")]
        public int type { get; set; }

        [Column("parent_id")]
        public int? parent_id { get; set; }

        [Column("accepted_answer_id")]
        public int? answer_id { get; set; }

        [Column("post_creation_date")]
        public DateTime? creationDate { get; set; }

        [Column("post_score")]
        public int? score { get; set; }

        [Column("post_text")]
        public string text { get; set; }

        [Column("post_closed_date")]
        public DateTime? closedDate { get; set; }

        [Column("post_title")]
        public string title { get; set; }

        [ForeignKey("user_id")]
        public User user { get; set; }

        public List<Tag> tags { get; set; }

        public List<Comment> comments { get; set; }
    }
}
