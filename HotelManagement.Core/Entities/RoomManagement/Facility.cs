using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.Core.Entities.RoomManagement
{
    public class Facility : BaseEntity
    {
        [ForeignKey("FacilityCategory")]
        public int FacilityCategoryID { get; set; }


    }
}
