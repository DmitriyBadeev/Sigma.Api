﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using InvestIn.Core.Interfaces;

namespace InvestIn.Core.Entities
{
    public class PortfolioType : IEntity
    {
        [Key]
        public Guid Id { get; set; }
        
        public string Name { get; set; }

        public string IconUrl { get; set; }

        public List<Portfolio> Portfolios { get; set; }
    }
}