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
using System.IO;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace BusinessTier.Interfaces
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ILogMessageAPI1" in both code and config file together.
    [ServiceContract(Namespace = "http://tempuri.org")]
    public interface ICallMonitorAPI
    {
        [OperationContract]
        [WebInvoke(Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "Register")]
        string subscribeCustomer(Stream Customer);

        [OperationContract]
        [WebInvoke(Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped,
            UriTemplate = "Recharge")]
        string rechargePhoneNumber(Stream RechargePhone);

        [OperationContract]
        [WebInvoke(Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "/StartPhoneCall")]
        string startPhoneCall(Stream Start);

        [OperationContract]
        [WebInvoke(Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "GetPhoneBalance")]
        string getPhoneBalance(Stream CustomerBalance);

        [OperationContract]
        [WebInvoke(Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "getPricePerMinute")]
        string getPricePerMinute(Stream idprice);
    }
}
