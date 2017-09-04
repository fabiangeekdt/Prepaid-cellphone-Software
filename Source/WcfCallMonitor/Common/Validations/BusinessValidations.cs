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
using Common.Entities;
using Common.Enum;

namespace Common.Validations
{
    public class BusinessValidations
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public void validateCustomerSubscription(Customer cus)
        {
            if (cus == null)
                throw new Exception("Customer is not Subscribed.");
        }

        /// <summary>
        /// Validate if the customer has no bonuses granted today.
        /// </summary>
        /// <param name="bonusActivation"></param>
        /// <returns></returns>
        public bool validateGrantedBonus(DateTime activationDay)
        {
            try
            {
                if (DateTime.Today.Date != activationDay.Date)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bonus"></param>
        /// <param name="rechargedSum"></param>
        /// <param name=""></param>
        /// <returns></returns>
        public bool validateAvgRecharges(decimal rechargeValue, decimal prevrechargeValues )
        {
            try
            {
                if (rechargeValue > prevrechargeValues)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="period"></param>
        /// <param name="minRechargeValue"></param>
        /// <param name="rechargeValue"></param>
        /// <returns></returns>
        public bool validateMinRecharges(Period period, decimal minRechargeValue, decimal rechargeValue)
        {
            try
            {
                bool res = false;
                switch (period)
                {
                    case Period.Daily:
                        break;
                    case Period.Weekly:
                        if (rechargeValue > minRechargeValue)
                            res = true;
                        break;
                    case Period.biweekly:
                        break;
                    case Period.monthly:
                        break;
                }
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cus"></param>
        /// <returns></returns>
        public void validateMinLeft(decimal MinuteBalance)
        {
            if (MinuteBalance < 0)
                throw new Exception("Customer has not balance to do this call.");
        }
    }
}
