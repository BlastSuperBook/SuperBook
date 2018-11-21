using SQLite;

namespace SuperBook.Models
{
    [Table("ToDo")]
    public class ToDo
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Title { get; set; }
        public bool IsCompleted { get; set; }
    }
}
