#region Authoring Description
/* 
* =================================================================================
* Author:		Fabian Andres Moreno chacon
* Create date:  Sept 2, 2017
* Description:	business Validations class that contains the validations used by the 
*               transactions.
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

namespace Common.Validations
{
    public class BusinessValidations
    {
        /// <summary>
        /// Validate if an object is null
        /// </summary>
        public void validateCustomerSubscription(object cus)
        {
            if (cus == null)
                throw new Exception("Customer is not Subscribed.");
        }

        /// <summary>
        /// Validate if the customer has no bonuses granted today.
        /// </summary>
        /// <param name="bonusActivation">last bonus activation day</param>
        /// <returns>return true if the customer has no bonus granted today.</returns>
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
                throw new Exception("Validation bonus error: " + ex.Message);
            }
        }

        /// <summary>
        /// Validate if the recharged value is major than the average recharge values
        /// </summary>
        /// <param name="rechargeValue"></param>
        /// <param name="prevrechargeValues"></param>
        /// <returns>return true if the recharge value is major than the average recharge values</returns>
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
                throw new Exception("Validtion Average error: " + ex.Message);
            }
        }

        /// <summary>
        /// validate the minimun value of recharges depending on the period time
        /// </summary>
        /// <param name="period">Period Enum</param>
        /// <param name="minRechargeValue">minimun recharged value</param>
        /// <param name="rechargeValue">actual recharge value</param>
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
                throw new Exception("Validtion Minimun Recharges error: " + ex.Message);
            }
        }

        /// <summary>
        /// Validate if the customer has minutes left for making the call.
        /// </summary>
        /// <param name="MinuteBalance">actual customer's balance.</param>
        public bool validateMinLeft(decimal MinuteBalance)
        {
            if (MinuteBalance == 0)
                return false;
            else
                return true;
        }
    }
}
