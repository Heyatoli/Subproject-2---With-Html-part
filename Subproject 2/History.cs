using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Subproject_2
{
    public class History 
    {
        [Column("history_id")]
        public int id { get; set; }

        [Column("user_id")]
        public int userId { get; set; }

        [Column("search")]
        public string searchWord { get; set; }
    }
}
