using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Subproject_2
{
    public class User
    {
        [Column("user_id")]
        public int id { get; set; }

        [Column("user_display_name")]
        public string name { get; set; }

        [Column("user_creation_date")]
        public DateTime creationDate { get; set; }

        [Column("user_location")]
        public string location { get; set; }

        [Column("user_age")]
        public int? age { get; set; }

        public List<Marking> marks { get; set; }

        public List<History> history { get; set; }
    }
}
