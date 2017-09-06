#region Authoring Description
/* 
* =================================================================================
* Author:		Fabian Andres Moreno chacon
* Create date:  Sept 2, 2017
* Description:	Promotion Entity for keeping the it's data. 
* =================================================================================
* ============================= CHANGES ===========================================
* Author:		
* Create date: 
* Description:	
* =================================================================================
*/
#endregion
using Common.Enum;
using System.Runtime.Serialization;

namespace Common.Entities
{
    [DataContract]
    public class Promotion
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public int Value { get; set; }
        [DataMember]
        public PromotionType ValueType { get; set; }
    }
}
