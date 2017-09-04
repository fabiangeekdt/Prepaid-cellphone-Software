#region Authoring Description
/* 
* =================================================================================
* Author:		Fabian Andres Moreno chacon
* Create date:  Sept 3, 2017
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
    public class Response
    {
        [DataMember]
        public int idResponse { get; set; }
        [DataMember]
        public string response { get; set; }
        [DataMember]
        public Exception exception { get; set; }
    }
}
