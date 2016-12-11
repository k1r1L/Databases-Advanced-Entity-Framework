namespace MassDefect.Client
{
    using Data;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Startup
    {
        public static void Main(string[] args)
        {
            MassDefectContext context = new MassDefectContext();
            context.Database.Initialize(true);
        }
    }
}
