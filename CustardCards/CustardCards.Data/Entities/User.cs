using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustardCards.Data.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Guid RoomId { get; set; }
        public bool IsModerator { get; set; }
        public virtual Room Room { get; set; }

    }
}
