using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sigma.Core.Interfaces
{
    public interface IOperation
    {
        public Guid PortfolioId { get; set; }
    }
}
