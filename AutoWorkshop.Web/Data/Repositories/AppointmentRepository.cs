﻿using AutoWorkshop.Web.Data.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoWorkshop.Web.Data.Repositories
{
    public class AppointmentRepository : GenericRepository<Appointment>, IAppointmentRepository
    {
        private readonly DataContext _context;

        public AppointmentRepository(DataContext context) : base(context)
        {
            _context = context;
        }


        public IEnumerable<SelectListItem> GetComboUserAppointment(string username)
        {
            var list = _context.Appointments.Where(p => p.Mechanic.User.Email == username && p.IsConfirmed == true).Select(b => new SelectListItem
            {
                Text = b.StartTime.ToString("dd/MM/yyyy hh:mm"),
                Value = b.Id.ToString()
            }).ToList();

            if (list.Count < 1)
            {
                list.Insert(0, new SelectListItem
                {
                    Text = "No appointments exist",
                    Value = "0"
                });
            }
            list.Insert(0, new SelectListItem
            {
                Text = "(Select an Appointment)",
                Value = "0"
            });

            return list;
        }


        public IEnumerable<SelectListItem> GetComboAppointment()
        {
            var list = _context.Appointments.Select(b => new SelectListItem
            {
                Text = b.StartTime.ToString("dd/MM/yyyy hh:mm"),
                Value = b.Id.ToString()
            }).ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "(Select an Appointment)",
                Value = "0"
            });

            return list;
        }
    }
}
