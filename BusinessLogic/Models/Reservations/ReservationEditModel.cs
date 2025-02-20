using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Models.Reservations
{
    public class ReservationEditModel
    {
        public decimal Price { get; set; }
        public int Adults { get; set; }
        public int Children { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int PansionId { get; set; }
        public int Id { get; set; }
    }
}
