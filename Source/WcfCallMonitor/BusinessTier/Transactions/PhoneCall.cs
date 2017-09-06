#region Authoring Description
/* 
* =================================================================================
* Author:		Fabian Andres Moreno chacon
* Create date:  Sept 2, 2017
* Description:	Execute a call Transaction Request
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
using Common.Helpers;
using Common.Validations;
using BusinessTier.Services.Call.Simulator.Mock;
using BusinessTier.Services.Call.Simulator.Mock.Entity;
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
        /// Execute an incoming Call Request. Calculating:
        /// 1.Call Duration.
        /// 2.Call Cost.
        /// 3.Minutes Used.
        /// 4.Minutes Balance.
        /// </summary>
        /// <param name="data">Stream with Json Data; Object: Call Entity</param>
        /// <returns>Stream with Json Data; Object: Response Entity</returns>
        public Stream startPhoneCall(Stream data)
        {
            try
            {
                decimal callLast = 0;
                call = SerializationHelpers.DeserializeJson<Call>(data);

                cus = dataAccess.getCustomerPerPhone(call.PhoneNumber);
                transValidations.validateCustomerSubscription(cus);

                cusPhone = dataAccess.getBalance(cus.Id, cus.PhoneNumber);
                if (cusPhone != null)
                {
                    if (!transValidations.validateMinLeft(cusPhone.MinuteBalance)) { 
                        resp.idResponse = 0;
                        resp.response = "Customer does not have minutes for making this call.";
                        resp.exception = null;
                        return SerializationHelpers.GenerateStreamFromString(SerializationHelpers.SerializeJson<Response>(resp));
                    }
                }
                else
                {
                    resp.idResponse = 0;
                    resp.response = "Customer does not have minutes for making this call.";
                    resp.exception = null;
                    return SerializationHelpers.GenerateStreamFromString(SerializationHelpers.SerializeJson<Response>(resp));
                }

                Price p = dataAccess.getPrice(Convert.ToInt32(2));

                //Call simulation
                var makecall = new CallSimulatorMock();
                var callreq = new CallSimulatorRequest { toPhoneNumber = cus.PhoneNumber, fromPhoneNumber = call.DestinationNumber, startCall = DateTime.Now, minutesLet = cusPhone.MinuteBalance };
                var callresp = new CallSimulatorResponse();
                callresp = makecall.StartPhoneCall(callreq);
                
                TimeSpan duCall = BussinessOps.GetDurationCall(callresp);
                if (Convert.ToDecimal(duCall.TotalSeconds) >= (cusPhone.MinuteBalance * 60))
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

                var callCost = BussinessOps.GetCallCost(p.Prices, callLast);
                var c = new Call { Id = cus.Id, PhoneNumber = cus.PhoneNumber, DestinationNumber = call.DestinationNumber, InitialDatetime = callresp.startCall, FinalDatetime = callresp.endCall, Duration = callLast, Cost = callCost, State = callresp.answerType };
                var res = dataAccess.initPhoneCall(c);

                resp.idResponse = 0;
                resp.response = "From Phone Number: " + cus.PhoneNumber + " \nTo from Phone Number: " + call.DestinationNumber + " \nStart Call DateTime: " + c.InitialDatetime.ToString() + " \nEndCall DateTime : " + c.FinalDatetime.ToString() +
                                " \nCall Duration: " + c.Duration.ToString() + "sec. \n Call Cost: " + c.Cost + " \n Call State: " + callresp.answerDesc;
                resp.exception = null;
                return SerializationHelpers.GenerateStreamFromString(SerializationHelpers.SerializeJson<Response>(resp));
            }
            catch (Exception ex)
            {
                resp.idResponse = 400;
                resp.response = "Cannot finalize transaction: PhoneCall";
                resp.exception = ex.Message;
                return SerializationHelpers.GenerateStreamFromString(SerializationHelpers.SerializeJson<Response>(resp));
            }
        }
    }
}
