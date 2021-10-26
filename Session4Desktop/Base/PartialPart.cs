using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Session4Desktop.Base
{
    public partial class Parts
    {
        public decimal? CurrentStock { get; set; }
        public decimal? ReceivedStock { get; set; }

        public bool BatchNumberIsEnabled => BatchNumberHasRequired == true ? true : false;
    }
}
