using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caveman.Models
{
    public interface IMortable
    {
        float Health { get; set; }
        bool Died { get; set; }
        void ReceiveHit(IHarmful harm);
    }
}
