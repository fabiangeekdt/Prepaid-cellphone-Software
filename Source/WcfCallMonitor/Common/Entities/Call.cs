#region Authoring Description
/* 
* =================================================================================
* Author:		Fabian Andres Moreno chacon
* Create date:  Sept 2, 2017
* Description:	Call Entity for keeping the it's data. 
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
    public class Call : Customer
    {
        [DataMember]
        public int CallId { get; set; }
        [DataMember]
        public string DestinationNumber { get; set; }
        [DataMember]
        public DateTime InitialDatetime { get; set; }
        [DataMember]
        public DateTime FinalDatetime { get; set; }
        [DataMember]
        public decimal Duration { get; set; }
        [DataMember]
        public decimal Cost { get; set; }
        [DataMember]
        public int State { get; set; }
    }
}
