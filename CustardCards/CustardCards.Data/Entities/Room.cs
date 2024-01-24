using System.ComponentModel.DataAnnotations.Schema;

namespace CustardCards.Data.Entities
{
    public class Room
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
