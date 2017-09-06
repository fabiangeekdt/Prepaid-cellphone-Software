#region Authoring Description
/* 
* =================================================================================
* Author:		Fabian Andres Moreno chacon
* Create date:  Sept 2, 2017
* Description:	Price Entity for keeping the it's data. 
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
    public class Price
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public decimal Prices { get; set; }
        [DataMember]
        public string Description { get; set; }
    }
}
