using System;
using BusinessTier.Services.Call.Simulator.Proxy.Entity;
using System.Collections.Generic;
using System.Linq;

namespace BusinessTier.Services.Call.Simulator.Proxy
{
    public class CallSimulatorProxy
    {
        public CallSimulatorProxy()
        {
           
        }

        public CallSimulatorResponse  StartPhoneCall(CallSimulatorRequest call)
        {

            try
            {
                var response = new CallSimulatorResponse();
                response = Phonecall(call);
                return response;
            }
            catch (Exception ex)
            {
                throw new Exception("Cannot connect the phone call with the destination phone number. Exception: " + ex.Message);
            }
        }

        private CallSimulatorResponse Phonecall(CallSimulatorRequest e)
        {
            try
            {
                var response = new CallSimulatorResponse();
                Dictionary<string, int> answer = new Dictionary<string, int>();
                int h = 0;
                answer.Add("Accepted", 0);
                answer.Add("Rejected", 1);
                answer.Add("On_Hold", 2);
                answer.Add("UnReach", 3);

                Random c = new Random();
                h = c.Next(answer.Min(a => a.Value), answer.Max(a => a.Value));

                switch (h)
                {
                    case 0:
                        h = c.Next(1, 4);
                        response.startCall = e.startCall;
                        response.endCall = DateTime.Now.AddMinutes(h);
                        response.answerType = h;
                        response.answerDesc = answer.FirstOrDefault(k => k.Value == h).Key;
                        break;
                    case 1:
                        response.startCall = e.startCall;
                        response.endCall = e.startCall;
                        response.answerType = h;
                        response.answerDesc = answer.FirstOrDefault(k => k.Value == h).Key;
                        break;
                    case 2:
                        response.startCall = e.startCall;
                        response.endCall = DateTime.Now.AddMinutes(h);
                        response.answerType = h;
                        response.answerDesc = answer.FirstOrDefault(k => k.Value == h).Key;
                        break;
                    case 3:
                        response.startCall = e.startCall;
                        response.endCall = e.startCall;
                        response.answerType = h;
                        response.answerDesc = answer.FirstOrDefault(k => k.Value == h).Key;
                        break;
                }
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
