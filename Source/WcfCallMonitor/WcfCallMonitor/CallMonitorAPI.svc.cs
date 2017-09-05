#region Authoring Description
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using BusinessTier.Interfaces;
using BusinessTier.Factory;
using System.IO;

namespace WcfCallMonitor
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "LogMessageAPI1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select LogMessageAPI1.svc or LogMessageAPI1.svc.cs at the Solution Explorer and start debugging.
    // Start the service and browse to http://<machine_name>:<port>/Service1/help to view the service's generated help page
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class CallMonitorAPI : ICallMonitorAPI
    {
        TransactionFactory transaction;
        public CallMonitorAPI()
        {
            transaction = new TransactionFactory();
        }

        public string subscribeCustomer(Stream Customer)
        {
            return transaction.getTransaction("subscribeCustomer", Customer);
        }

        public string rechargePhoneNumber(Stream recharge)
        {
            return transaction.getTransaction("rechargePhoneNumber", recharge);
        }

        //public string startPhoneCall(string call)
        //{
        //    return transaction.getTransaction("startPhoneCall", call);
        //}

        //public string endPhoneCall(string call)
        //{
        //    return transaction.getTransaction("endPhoneCall", call);
        //}

        //public string getPhoneBalance(string customer)
        //{
        //    return transaction.getTransaction("getPhoneBalance", customer);
        //}

        public string getPricePerMinute(Stream idPrice)
        {
            return transaction.getTransaction("getPricePerMinute", idPrice);
        }
    }
}
