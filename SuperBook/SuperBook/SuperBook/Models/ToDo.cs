using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace SuperBook.Models
{
    [Table("ToDo")]
    public class ToDo
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Title { get; set; }
        public bool Completed { get; set; }
    }
}
