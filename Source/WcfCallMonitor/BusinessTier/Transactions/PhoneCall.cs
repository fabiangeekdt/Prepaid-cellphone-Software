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
using BusinessTier.Operations;
using Common.Entities;
using Common.Enum;
using Common.Helpers;
using Common.Validations;
using BusinessTier.Services.Call.Simulator.Proxy;
using BusinessTier.Services.Call.Simulator.Proxy.Entity;
using DataTier;
using System;
using System.IO;

namespace BusinessTier.Transactions
{
    public class PhoneCall
    {
        DAO dataAccess;
        Response resp;
        Call call;
        Customer cus;
        CustomerPhone cusPhone;
        Recharge rec;
        BusinessValidations transValidations;
        CustomerBonus cusBonus;

        public PhoneCall()
        {
            dataAccess = new DAO();
            cus = new Customer();
            resp = new Response();
            cusPhone = new CustomerPhone();
            rec = new Recharge();
            call = new Call();
            cusBonus = new CustomerBonus();
            transValidations = new BusinessValidations();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public Stream startPhoneCall(Stream data)
        {
            try
            {
                call = SerializationHelpers.DeserializeJson<Call>(data);

                cus = dataAccess.getCustomerPerPhone(call.PhoneNumber);
                transValidations.validateCustomerSubscription(cus);

                cusPhone = dataAccess.getBalance(cus.Id, cus.PhoneNumber);
                transValidations.validateMinLeft(cusPhone.MinuteBalance);

                Price p = dataAccess.getPrice(Convert.ToInt32(2));

                //Call simulation
                var makecall = new CallSimulatorProxy();
                var callreq = new CallSimulatorRequest { toPhoneNumber = cus.PhoneNumber, fromPhoneNumber = call.DestinationNumber, startCall = DateTime.Now, minutesLet = cusPhone.MinuteBalance };
                var callresp = new CallSimulatorResponse();
                callresp = makecall.StartPhoneCall(callreq);
                decimal callLast = 0;
                TimeSpan duCall = callresp.endCall - callresp.startCall;
                if(Convert.ToDecimal(duCall.TotalSeconds) >= (cusPhone.MinuteBalance * 60))
                {
                    callLast = cusPhone.MinuteBalance;
                    cusPhone.MinuteBalance = 0;
                    cusPhone.MinutesUsed = callLast;
                    dataAccess.updBalance(cusPhone);
                }
                else
                {
                    callLast = Convert.ToDecimal(duCall.TotalSeconds);
                    cusPhone.MinuteBalance -= callLast / 60; 
                    cusPhone.MinutesUsed += callLast / 60;
                    dataAccess.updBalance(cusPhone);                 
                }
                var callCost = callLast * p.Prices;
                var c = new Call { Id = cus.Id, PhoneNumber = cus.PhoneNumber, DestinationNumber = call.DestinationNumber, InitialDatetime = callresp.startCall, FinalDatetime = callresp.endCall, Duration = callLast, Cost = callCost, State = callresp.answerType };
                var res = dataAccess.initPhoneCall(c);

                resp.idResponse = 0;
                resp.response = "From Phone Number: " + cus.PhoneNumber + " \nTo from Phone Number: " + call.DestinationNumber + " \nStart Call DateTime: " +c.InitialDatetime.ToString() + " \nEndCall DateTime : " + c.FinalDatetime.ToString() + 
                                " \nCall Duration: " + c.Duration.ToString() + "sec. \n Call Cost: " + c.Cost + " \n Call State: " + callresp.answerDesc;
                resp.exception = null;
                return SerializationHelpers.GenerateStreamFromString(SerializationHelpers.SerializeJson<Response>(resp));
            }
            catch (Exception ex)
            {
                resp.idResponse = 400;
                resp.response = "Cannot finalize transaction: PhoneCall";
                resp.exception = ex;
                return SerializationHelpers.GenerateStreamFromString(SerializationHelpers.SerializeJson<Response>(resp));
            }
        }
    }
}
