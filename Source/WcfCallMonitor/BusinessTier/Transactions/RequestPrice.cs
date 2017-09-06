#region Authoring Description
/* 
* =================================================================================
* Author:		Fabian Andres Moreno chacon
* Create date:  Sept 2, 2017
* Description:	Execute a get Price Transaction Request
* =================================================================================
* ============================= CHANGES ===========================================
* Author:		
* Create date: 
* Description:	
* =================================================================================
*/
#endregion
using Common.Entities;
using DataTier;
using System;
using Common.Helpers;
using System.IO;

namespace BusinessTier.Transactions
{
    public class RequestPrice
    {
        DAO dataAccess;
        Response resp;

        public RequestPrice()
        {
            dataAccess = new DAO();
            resp = new Response();
        }

        /// <summary>
        /// Execute an incoming Get Price request for retrieving the price of: 
        /// 1. Minutes
        /// 2. Seconds
        /// </summary>
        /// <param name="idPrice">Stream with Json Data; Object: Prices Data</param>
        /// <returns>Stream with Json Data; Object: Response Data</returns>
        public Stream GetPrice(Stream idPrice)
        {
            try
            {
                Price price = SerializationHelpers.DeserializeJson<Price>(idPrice);
                Price p = dataAccess.getPrice(Convert.ToInt32(price.Id));
                if (p != null)
                {
                    resp.idResponse = 0;
                    resp.response = p.Description + " " + p.Prices.ToString();
                    resp.exception = null;
                    return SerializationHelpers.GenerateStreamFromString(SerializationHelpers.SerializeJson(resp));
                }
                else
                {
                    resp.idResponse = 1;
                    resp.response = "Id Price does not exist";
                    resp.exception = null;
                    return SerializationHelpers.GenerateStreamFromString(SerializationHelpers.SerializeJson(resp));
                }
            }
            catch (Exception ex)
            {
                resp.idResponse = 400;
                resp.response = "";
                resp.exception = ex.Message;
                return SerializationHelpers.GenerateStreamFromString(SerializationHelpers.SerializeJson(resp));
            }
        }
    }
}
