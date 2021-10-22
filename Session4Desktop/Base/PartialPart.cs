using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Session4Desktop.Base
{
    public partial class Parts
    {
        public int CurrentStock => OrderItems.Where(p => p.Orders.DestinationWarehouseID == null).Count();
        public int ReceivedStock => OrderItems.Where(p => p.Orders.DestinationWarehouseID != null).Count();
    }
}
