#region Authoring Description
/* 
* =================================================================================
* Author:		Fabian Andres Moreno chacon
* Create date:  Sept 2, 2017
* Description:	
* =================================================================================
* ============================= CHANGES ===========================================
* Author:		
* Create date: 
* Description:	
* =================================================================================
*/
#endregion
using System.Runtime.Serialization;

namespace Common.Entities
{
    [DataContract]
    public class CustomerPhone : Customer
    {
        [DataMember]
        public int InternalId { get; set; }
        [DataMember]
        public byte PhoneState { get; set; }
        [DataMember]
        public decimal MinuteBalance { get; set; }
        [DataMember]
        public decimal MinutesUsed { get; set; }
    }
}
