using System;

namespace CallSimulatorLibrary.Entity
{
    public class CallSimulatorRequest
    {
        public string fromPhoneNumber { get; set; }
        public string toPhoneNumber { get; set; }
        public DateTime startCall { get; set; }
        public int minutesLet { get; set; }
    }
}
