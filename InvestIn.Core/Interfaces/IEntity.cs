using System;
using System.ComponentModel.DataAnnotations;

namespace InvestIn.Core.Interfaces
{
    public interface IEntity
    {
        [Key]
        public Guid Id { get; set; }
    }
}