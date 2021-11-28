using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Sigma.Core.Interfaces;

namespace Sigma.Core.Entities
{
    [Index(nameof(Ticket), IsUnique = true)]
    public class Bond : IEntity, IAsset, IRequested
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Ticket { get; set; }
        
        [Required]
        public string ShortName { get; set; }

        public string MarketFullName { get; set; }

        public string FullName { get; set; }

        public string LatName { get; set; }
        
        public string Description { get; set; }
        
        public decimal Percent { get; set; }
        
        public decimal Price { get; set; }

        public decimal PercentChange { get; set; }
        
        public DateTime UpdateTime { get; set; }
        
        public DateTime AmortizationDate { get; set; }
        
        public decimal Nominal { get; set; }
        
        public decimal Coupon { get; set; }

        public List<PortfolioBond> PortfolioBonds { get; set; }

        public List<Coupon> Coupons { get; set; }
    }
}