using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessTier.Services.Call.Simulator.Proxy.Entity
{
    public class CallSimulatorResponse
    {
        public DateTime startCall { get; set; }
        public DateTime endCall { get; set; }
        public int answerType { get; set; }
        public string answerDesc { get; set; }
    }
}
