using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustardCards.Models.ViewModels
{
    public class RoomViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public List<UserViewModel> Users { get; set; } = new List<UserViewModel>();
    }
}
