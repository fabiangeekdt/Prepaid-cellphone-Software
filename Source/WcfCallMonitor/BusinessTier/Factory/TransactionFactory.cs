#region Authoring Description
/* 
* =================================================================================
* Author:		Fabian Andres Moreno chacon
* Create date:  Sept 3, 2017
* Description:	Transaction Class is in charge of create the transactions 
*               objects for all incoming request.
* =================================================================================
* ============================= CHANGES ===========================================
* Author:		
* Create date: 
* Description:	
* =================================================================================
*/
#endregion
using System.IO;
using BusinessTier.Interfaces;
using BusinessTier.Transactions;

namespace BusinessTier.Factory
{
    public class TransactionFactory : ITransactionFactory
    {
        /// <summary>
        /// Create a new instance of the incoming request
        /// </summary>
        /// <param name="transType">Transaction Name</param>
        /// <param name="data">Stream with Json Data</param>
        /// <returns>Stream with Json Data</returns>
        public Stream getTransaction(string transType, Stream data)
        {
            if (transType == null)
                return null;

            switch (transType)
            {
                case "subscribeCustomer":
                    return new SubscribeCustomer(data).subscribe();
                case "rechargePhoneNumber":
                    return new PhoneTransactions().rechargePhoneNumber(data);
                case "startPhoneCall":
                    return new PhoneCall().startPhoneCall(data);
                case "getPhoneBalance":
                    return new PhoneTransactions().getPhoneBalance(data);
                case "getPricePerMinute":
                    return new RequestPrice().GetPrice(data);
                default:
                    return null;
            }
        }
    }
}
