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

        public string GetPrice(Stream idPrice)
        {
            try
            {
                Price price = SerializationHelpers.DeserializeJson<Price>(idPrice);
                Price p = dataAccess.getPrice(Convert.ToInt32(price.Id));
                if (p != null)
                    return SerializationHelpers.SerializeJson(p);
                else
                {
                    resp.idResponse = 0;
                    resp.response = "Id Price does not exist";
                    resp.exception = null;
                    return SerializationHelpers.SerializeJson(resp);
                }
            }
            catch (Exception ex)
            {
                resp.idResponse = 400;
                resp.response = "";
                resp.exception = ex;
                return SerializationHelpers.SerializeJson(resp);
            }
        }
    }
}
