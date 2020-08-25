using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace MovingApp.Models
{
    public enum StatusTypes
    {
        Complete,
        Incomplete,
        Cancelled
    }
    public class MovingTask
    {
        public int ID { get; set; }
        public string Title { get; set; }

        [DataType(DataType.Date)]
        public DateTime DueDate { get; set; }
        public string? Owner { get; set; }

        public int Status { get; set; }

    }
}