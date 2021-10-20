using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Session4Desktop.Base
{
    public partial class Orders
    {
        public string PartName => OrderItems.FirstOrDefault().Parts.Name;
        public decimal TheAmount => OrderItems.FirstOrDefault().Amount;
    }
}
