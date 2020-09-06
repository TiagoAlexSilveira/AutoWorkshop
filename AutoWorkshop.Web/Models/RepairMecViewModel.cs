﻿using AutoWorkshop.Web.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoWorkshop.Web.Models
{
    public class RepairMecViewModel : Repair
    {
        public IEnumerable<Repair> Repairs { get; set; }

        public IEnumerable<Appointment> Appointments { get; set; }

    }
}
