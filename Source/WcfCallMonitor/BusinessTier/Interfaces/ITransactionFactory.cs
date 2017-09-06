#region Authoring Description
/* 
* =================================================================================
* Author:		Fabian Andres Moreno chacon
* Create date:  Sept 2, 2017
* Description:	Transaction's interface used to implement the transactions methods 
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
        Stream getTransaction(string transType, Stream data);
    }
}
