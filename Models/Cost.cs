﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackMyMoney.Models
{
    public class Cost
    {
        public int Id { get; set; }
        public DateTime Date { get; set; } 
        public int CategoryId { get; set; } 
        public string Subject { get; set; } 
        public decimal Amount { get; set; } 
    }
}
