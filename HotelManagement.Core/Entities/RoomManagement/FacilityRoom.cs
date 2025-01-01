﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.Core.Entities.RoomManagement
{
    public class FacilityRoom : BaseEntity
    {
        [ForeignKey("Facility")]
        public int FacilityId { get; set; }

        public Facility Facility { get; set; }

        [ForeignKey("Room")]
        public int RoomId { get; set; }

        public Room Room { get; set; }
    }
}
