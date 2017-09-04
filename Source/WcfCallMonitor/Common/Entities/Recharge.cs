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
using System;
using System.Runtime.Serialization;

namespace Common.Entities
{
    [DataContract]
    public class Recharge : Customer
    {
        [DataMember]
        public long Code{ get; set; }
        [DataMember]
        public decimal Value { get; set; }
        [DataMember]
        public DateTime Date { get; set; }
        [DataMember]
        public int State { get; set; }
    }
}
