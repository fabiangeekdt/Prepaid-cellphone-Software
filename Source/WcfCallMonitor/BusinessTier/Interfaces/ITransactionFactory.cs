﻿#region Authoring Description
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
using System.IO;

namespace BusinessTier.Interfaces
{
    public interface ITransactionFactory
    {
        string getTransaction(string transType, Stream data);
    }
}
