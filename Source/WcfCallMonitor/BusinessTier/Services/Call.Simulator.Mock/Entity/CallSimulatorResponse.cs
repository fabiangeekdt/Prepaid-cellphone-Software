#region Authoring Description
/* 
* =================================================================================
* Author:		Fabian Andres Moreno chacon
* Create date:  Sept 2, 2017
* Description:	Entity PhoneCall Simulator(Mock) Response. 
* =================================================================================
* ============================= CHANGES ===========================================
* Author:		
* Create date: 
* Description:	
* =================================================================================
*/
#endregion
using System;

namespace BusinessTier.Services.Call.Simulator.Mock.Entity
{
    public class CallSimulatorResponse
    {
        public DateTime startCall { get; set; }
        public DateTime endCall { get; set; }
        public int answerType { get; set; }
        public string answerDesc { get; set; }
    }
}
