using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Session4Desktop.Base
{
    public class AppData
    {
        private static KazanNeftSession4DBEntities context;
        public static KazanNeftSession4DBEntities GetContext()
        {
            if (context == null)
                context = new KazanNeftSession4DBEntities();
            return context;
        }
    }
}
