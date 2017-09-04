#region Authoring Description
/* 
* =================================================================================
* Author:		Fabian Andres Moreno chacon
* Create date:  Sept 3, 2017
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
using DataTier;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BusinessTier.Transactions
{
    public class PhoneTransactions
    {
        DAO dataAccess;
        Customer cus;
        Recharge recharge;
        CustomerPhone phoneBalance;
        Response resp;
        BusinessValidations transValidations;
        CustomerBonus cusBonus;
        Recharge rec;

        public PhoneTransactions()
        {
            dataAccess = new DAO();
            resp = new Response();
            cus = new Customer();
            phoneBalance = new CustomerPhone();
            transValidations = new BusinessValidations();
            cusBonus = new CustomerBonus();
            rec = new Recharge();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string rechargePhoneNumber(Stream data)
        {
            try
            {
                string promAdv = string.Empty;
                recharge = SerializationHelpers.DeserializeJson<Recharge>(data);
                cus = dataAccess.getCustomerPerPhone(recharge.Id_Type, recharge.Id, recharge.PhoneNumber);
                transValidations.validateCustomerSubscription(cus);
                
                var res = dataAccess.phoneRecharge(recharge); 
                if (res != 0)
                {
                    cusBonus = dataAccess.getLastCustomerBonus(cus.Id, cus.PhoneNumber);
                    if (transValidations.validateGrantedBonus(cusBonus.ActivationDay))
                    {
                        customerPromotions(ref promAdv);
                    }

                    resp.idResponse = res;
                    resp.response = "Recharged successfull, value: " + recharge.Value + (!string.IsNullOrEmpty(promAdv) ? promAdv : "" );
                    resp.exception = null;
                }
                else
                {
                    resp.idResponse = 0;
                    resp.response = "Recharged not complete";
                    resp.exception = null;
                }
                return SerializationHelpers.SerializeJson<Response>(resp);
            }
            catch (Exception ex)
            {
                resp.idResponse = 400;
                resp.response = "Cannot finalize transaction: rechargePhoneNumber, " + ex.Message + ((ex.InnerException != null) ? ex.InnerException.Message : "");
                resp.exception = ex;
                return SerializationHelpers.SerializeJson<Response>(resp);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string getPhoneBalance(Stream data)
        {
            try
            {
                phoneBalance = SerializationHelpers.DeserializeJson<CustomerPhone>(data);
                cus = dataAccess.getCustomerPerPhone(phoneBalance.Id_Type, phoneBalance.Id, phoneBalance.PhoneNumber);
                transValidations.validateCustomerSubscription(cus);
                
                var res = dataAccess.getBalance(phoneBalance.Id, phoneBalance.PhoneNumber);
                
                return SerializationHelpers.SerializeJson<Response>(resp);
            }
            catch (Exception ex)
            {
                resp.idResponse = 400;
                resp.response = "Cannot finalize transaction: getPhoneBalance, " + ex.Message + ((ex.InnerException != null) ? ex.InnerException.Message : "");
                resp.exception = ex;
                return SerializationHelpers.SerializeJson<Response>(resp);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="promAdv"></param>
        private void customerPromotions(ref string promAdv)
        {
            try
            {
                bool bonusGranted = false;
                List<Promotion> prom = dataAccess.getPromotions();
                foreach (var p in prom)
                {
                    switch (p.Id)
                    {
                        case 1:
                            decimal sumRecharge = dataAccess.getAllCustomerRecharges(cus).Sum(c => c.Value);
                            bonusGranted = saveGrantedBonus(ref promAdv, p, sumRecharge);
                            break;
                        case 2:
                            MinimunRecharge minRec = dataAccess.getMinRechargePerPeriod(Convert.ToInt32(Period.Weekly));
                            DateTime dt1 = DateTime.Today.AddDays(-7);

                            decimal avgRecharge = dataAccess.getAllCustomerRecharges(cus).Where(c => c.Date >= dt1 &&
                                                                                            c.Date <= DateTime.Today &&
                                                                                            c.Value == minRec.minValue).ToList().Average(c => c.Value);
                            bonusGranted = saveGrantedBonus(ref promAdv, p, avgRecharge);
                            #region erase when test  passes
                            //bonusGranted = (avgRecharge > 0) ? true : false;
                            //if (bonusGranted)
                            //{
                            //    //calcule bonus
                            //    rec.Value = BussinessOps.calculateBonus(avgRecharge, p.ValueType, p.Value);
                            //    int i = dataAccess.phoneRecharge(rec);

                            //    cusBonus = new CustomerBonus();
                            //    cusBonus.Id = rec.Id;
                            //    cusBonus.PhoneNumber = rec.PhoneNumber;
                            //    cusBonus.PromotionId = p.Id;
                            //    cusBonus.ActivationDay = DateTime.Now;
                            //    int res = dataAccess.customerBonus(cusBonus);
                            //    promAdv = "You have won a recharge bonus of " + rec.Value;
                            //}
                            #endregion
                            break;
                        default:
                            bonusGranted = false;
                            break;
                    }
                    if (bonusGranted)
                        break;

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="promAdv"></param>
        /// <param name="p"></param>
        /// <param name="valueRecharge"></param>
        /// <returns></returns>
        private bool saveGrantedBonus(ref string promAdv, Promotion p, decimal valueRecharge)
        {
            bool bonusGranted = (valueRecharge > 0) ? true : false;
            if (bonusGranted)
            {
                //calcule bonus
                rec.Value = BussinessOps.calculateBonus(valueRecharge, p.ValueType, p.Value);
                int i = dataAccess.phoneRecharge(rec);

                cusBonus = new CustomerBonus();
                cusBonus.Id = rec.Id;
                cusBonus.PhoneNumber = rec.PhoneNumber;
                cusBonus.PromotionId = p.Id;
                cusBonus.ActivationDay = DateTime.Now;
                int res = dataAccess.customerBonus(cusBonus);
                promAdv = "You have won a recharge bonus of " + rec.Value;
            }

            return bonusGranted;
        }
    }
}
