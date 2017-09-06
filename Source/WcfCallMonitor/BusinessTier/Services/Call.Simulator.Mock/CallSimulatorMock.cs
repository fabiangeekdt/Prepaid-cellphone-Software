#region Authoring Description
/* 
* =================================================================================
* Author:		Fabian Andres Moreno chacon
* Create date:  Sept 2, 2017
* Description:	PhoneCall Simulator(Mock) Class. 
* =================================================================================
* ============================= CHANGES ===========================================
* Author:		
* Create date: 
* Description:	
* =================================================================================
*/
#endregion
using System;
using BusinessTier.Services.Call.Simulator.Mock.Entity;
using System.Collections.Generic;
using System.Linq;

namespace BusinessTier.Services.Call.Simulator.Mock
{
    public class CallSimulatorMock
    {
        public CallSimulatorMock()
        {
        }

        /// <summary>
        /// Call simulator Method
        /// </summary>
        /// <param name="call">Object: CallSimulatorRequest with Call data</param>
        /// <returns>Object: CallSimulatorResponse with Hangout Call data</returns>
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

        /// <summary>
        /// Method for simulating a PhoneCall. this method choose randomly an answer call and retrieve the information.
        /// </summary>
        /// <param name="e">Object: CallSimulatorRequest with Call data</param>
        /// <returns>Object: CallSimulatorResponse with Hangout Call data</returns>
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
                var endc = e.startCall.AddMinutes(c.Next(1, 20));
                switch (h)
                {
                    case 0:
                        response.startCall = e.startCall;
                        response.endCall = endc;
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
                        response.endCall = endc;
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
