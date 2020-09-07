﻿using System;
using System.ComponentModel.DataAnnotations;

namespace StockMarket.Dtos
{
    public class StockPriceDto
    {
        public int Id { get; set; }
        [Required]
        public float CurrentPrice { get; set; }
        [Required]
        public DateTime DateTime { get; set; }
        [Required]
        public long CompanyId { get; set; }
        [Required]
        public string StockExchangeId { get; set; }
    }
}
