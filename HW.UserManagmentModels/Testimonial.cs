﻿using System;
using System.Collections.Generic;
using System.Text;

namespace HW.UserManagmentModels
{
    public class Testimonial
    {
        public long TestimonialsId { get; set; }
        public string Url { get; set; }
        public int UserType { get; set; }
        public int Testimonialtype { get; set; }

        public string Description { get; set; }
        public string Title { get; set; }
        public bool? Active { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }

    }
}
