﻿#region Authoring Description
/* 
* =================================================================================
* Author:		Fabian Andres Moreno chacon
* Create date:  Sept 3, 2017
* Description:	Response Entity to retrieve the information to the Client.
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
    [DataContract(Name = "Response")]
    public class Response
    {
        [DataMember]
        public int idResponse { get; set; }
        [DataMember]
        public string response { get; set; }
        [DataMember]
        public string exception { get; set; }
    }
}
