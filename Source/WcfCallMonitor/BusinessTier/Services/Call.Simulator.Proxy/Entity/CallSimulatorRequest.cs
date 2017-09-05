using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessTier.Services.Call.Simulator.Proxy.Entity
{
    public class CallSimulatorRequest
    {
        public string fromPhoneNumber { get; set; }
        public string toPhoneNumber { get; set; }
        public DateTime startCall { get; set; }
        public int minutesLet { get; set; }
    }
}
