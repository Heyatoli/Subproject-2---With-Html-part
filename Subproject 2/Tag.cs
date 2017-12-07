using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;

namespace Subproject_2
{
    public class Tag
    {
        [Column("tags_id")]
        public int id { get; set; }

        [Column("tags_name")]
        public string name { get; set; }
    }
}
