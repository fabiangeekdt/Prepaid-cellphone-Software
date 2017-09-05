using System;

namespace CallSimulatorLibrary.Entity
{
    public class CallSimulatorResponse
    {
        public DateTime startCall { get; set; }
        public DateTime endCall { get; set; }
        public int answerType { get; set; }
        public string answerDesc { get; set; }
    }
}
