#region Authoring Description
/* 
* =================================================================================
* Author:		Fabian Andres Moreno chacon
* Create date:  Sept 3, 2017
* Description:	Execute a Register Customer Transaction Request
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data">Stream with Json Data; Object: Customer Data</param>
        public SubscribeCustomer(Stream data)
        {
            dataAccess = new DAO();
            resp = new Response();
            cus = SerializationHelpers.DeserializeJson<Customer>(data);
        }

        /// <summary>
        /// Execute an incoming Register Customer request for saving the customer data into the DataBase.
        /// </summary>
        /// <returns>Stream with Json Data; Object: Response Data</returns>
        public Stream subscribe()
        {
            try
            {
                int res = dataAccess.subscribeCustomer(cus);
                if (res != 0) { 
                    resp.idResponse = 0;
                    resp.response = "Customer: " + cus.FirstName + " " + cus.SecondName + " " + cus.LastName +" created.";
                    resp.exception = null;
                }
                else
                {
                    resp.idResponse = 1;
                    resp.response = "Customer: " + cus.FirstName + " " + cus.SecondName + " " + cus.LastName + " not created.";
                    resp.exception = null;
                }
                return SerializationHelpers.GenerateStreamFromString(SerializationHelpers.SerializeJson<Response>(resp));
            }
            catch (Exception ex)
            {
                resp.idResponse = 400;
                resp.response = "Cannot finalize transaction: subscribe.";
                resp.exception = ex.Message;
                return SerializationHelpers.GenerateStreamFromString(SerializationHelpers.SerializeJson<Response>(resp));
            }
        }
    }
}
