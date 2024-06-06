﻿using System.ComponentModel.DataAnnotations;

namespace Event_API.Models
{
    public class CommentsCreateForm
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public string Message { get; set; }
        [Required]
        public int EventId { get; set; }
        [Required]
        public int PersonId { get; set; }

    }
}
