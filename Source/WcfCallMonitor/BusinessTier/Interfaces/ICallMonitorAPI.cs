#region Authoring Description
/* 
* =================================================================================
* Author:		Fabian Andres Moreno chacon
* Create date:  Sept 2, 2017
* Description:	Service's Interface which exposed all the webMethods of the 
*               RestFul service.
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
            BodyStyle = WebMessageBodyStyle.Wrapped,
            UriTemplate = "Register")]
        [return: MessageParameter(Name = "Register_Response")]
        Stream subscribeCustomer(Stream Customer);

        [OperationContract]
        [WebInvoke(Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped,
            UriTemplate = "Recharge")]
        [return: MessageParameter(Name = "Recharge_Response")]
        Stream rechargePhoneNumber(Stream RechargePhone);

        [OperationContract]
        [WebInvoke(Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped,
            UriTemplate = "/StartPhoneCall")]
        [return: MessageParameter(Name = "PhoneCall_Response")]
        Stream startPhoneCall(Stream Start);

        [OperationContract]
        [WebInvoke(Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped,
            UriTemplate = "GetPhoneBalance")]
        [return: MessageParameter(Name = "Balance_Response")]
        Stream getPhoneBalance(Stream CustomerBalance);

        [OperationContract]
        [WebInvoke(Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedResponse,
            UriTemplate = "getPricePerMinute")]
        [return: MessageParameter(Name = "Price_Response")]
        Stream getPricePerMinute(Stream idprice);
    }
}
