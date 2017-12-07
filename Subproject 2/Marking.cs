using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Subproject_2
{
    public class Marking
    {
        [Key, Column("posts_id", Order = 0)]
        public int postId { get; set; }

        [Key, Column("user_id", Order = 1)]
        public int userID { get; set; }

        public string note { get; set; }
    }
}
