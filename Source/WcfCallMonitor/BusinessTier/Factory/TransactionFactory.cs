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
using System.IO;
using BusinessTier.Interfaces;
using BusinessTier.Transactions;

namespace BusinessTier.Factory
{
    public class TransactionFactory : ITransactionFactory
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="transType"></param>
        /// <param name="data"></param>
        /// <returns></returns>
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
