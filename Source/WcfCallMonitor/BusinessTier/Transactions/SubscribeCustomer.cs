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
using Common.Entities;
using Common.Helpers;
using DataTier;
using System;
using System.IO;

namespace BusinessTier.Transactions
{
    public class SubscribeCustomer
    {
        DAO dataAccess;
        Customer cus;
        Response resp;

        public SubscribeCustomer(Stream data)
        {
            dataAccess = new DAO();
            resp = new Response();
            cus = SerializationHelpers.DeserializeJson<Customer>(data);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string subscribe()
        {
            try
            {
                int res = dataAccess.subscribeCustomer(cus);
                if (res != 0) { 
                    resp.idResponse = res;
                    resp.response = "Customer: " + cus.FirstName + " " + cus.SecondName + " " + cus.LastName +" created.";
                    resp.exception = null;
                }
                else
                {
                    resp.idResponse = res;
                    resp.response = "Customer: " + cus.FirstName + " " + cus.SecondName + " " + cus.LastName + " not created.";
                    resp.exception = null;
                }
                return SerializationHelpers.SerializeJson<Response>(resp);
            }
            catch (Exception ex)
            {
                resp.idResponse = 400;
                resp.response = "Cannot finalize transaction: subscribe.";
                resp.exception = ex;
                return SerializationHelpers.SerializeJson<Response>(resp);
            }
        }
    }
}
