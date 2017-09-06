#region Authoring Description
/* 
* =================================================================================
* Author:		Fabian Andres Moreno chacon
* Create date:  Sept 2, 2017
* Description:	business operations class is used to host the methods that execute
*               or perform a common operation in the transactions or the solution.
* =================================================================================
* ============================= CHANGES ===========================================
* Author:		
* Create date: 
* Description:	
* =================================================================================
*/
#endregion
using System;
using Common.Enum;
using BusinessTier.Services.Call.Simulator.Mock.Entity;

namespace BusinessTier.Operations
{
    public static class BussinessOps
    {
        /// <summary>
        /// Calculate the bonus according the type and value of the promotion.
        /// this method is open so, any dev can add more promotion type depending on the clients needs.
        /// </summary>
        /// <param name="avg">Customer Recharge Value</param>
        /// <param name="ptype">Promotion type</param>
        /// <param name="promValue">Promotion Value</param>
        /// <returns>Value awarded</returns>
        public static decimal calculateBonus(decimal avg, PromotionType ptype, int promValue)
        {
            try
            {
                switch (ptype)
                {
                    case PromotionType.Percentaje:
                        return (avg * promValue) / 100;
                    default:
                        return 0;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("calculateBonus error: " + ex.Message);
            }
        }

        /// <summary>
        /// Calculate the phone call cost.
        /// </summary>
        /// <param name="p">Price per Call Fraction</param>
        /// <param name="callLast">Call Duration</param>
        /// <returns>Call Cost</returns>
        public static decimal GetCallCost(Decimal price, decimal callLast)
        {
            if (callLast != 0 && price != 0)
                return callLast * price;
            else
                return 0;
        }

        /// <summary>
        /// Calculate the duration of the Phone Call
        /// </summary>
        /// <param name="callresp">Call Information</param>
        /// <returns>Duration Call</returns>
        public static TimeSpan GetDurationCall(CallSimulatorResponse callresp)
        {
            return callresp.endCall - callresp.startCall;
        }
    }
}
