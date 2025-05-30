﻿using System.ComponentModel.DataAnnotations;

namespace WebApp.DTOs
{
    public class PaymentDto
    {
        public int Id { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        public string Method { get; set; }

        [Required]
        public DateTime PaymentDate { get; set; }

        [Required]
        public int OrderId { get; set; }
    }
}
