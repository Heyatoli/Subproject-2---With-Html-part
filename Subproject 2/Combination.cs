using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;


namespace Subproject_2
{
    class Combination
    {
        [Column("tags_id")]
        public int tags_id { get; set; }
        [Column("posts_id")]
        public int post_id { get; set; }
    }
}
