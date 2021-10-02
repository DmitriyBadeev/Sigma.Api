using System;
using System.ComponentModel.DataAnnotations;

namespace Sigma.Core.Interfaces
{
    public interface IEntity
    {
        [Key]
        public Guid Id { get; set; }
    }
}