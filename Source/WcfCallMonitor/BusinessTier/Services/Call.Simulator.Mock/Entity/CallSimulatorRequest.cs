#region Authoring Description
/* 
* =================================================================================
* Author:		Fabian Andres Moreno chacon
* Create date:  Sept 2, 2017
* Description:	Entity PhoneCall Simulator(Mock) Request. 
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
    public class CallSimulatorRequest
    {
        public string fromPhoneNumber { get; set; }
        public string toPhoneNumber { get; set; }
        public DateTime startCall { get; set; }
        public decimal minutesLet { get; set; }
    }
}
