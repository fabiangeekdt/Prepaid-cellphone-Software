#region Authoring Description
/* 
* =================================================================================
* Author:		Fabian Andres Moreno chacon
* Create date:  Sept 2, 2017
* Description:	CustomerBonus Entity for keeping the it's data. 
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
    public class CustomerBonus : Customer
    {
        [DataMember]
        public int BonusCode { get; set; }
        [DataMember]
        public int PromotionId { get; set; }
        [DataMember]
        public DateTime ActivationDay { get; set; }
    }
}
