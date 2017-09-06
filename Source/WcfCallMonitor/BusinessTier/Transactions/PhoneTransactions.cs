#region Authoring Description
/* 
* =================================================================================
* Author:		Fabian Andres Moreno chacon
* Create date:  Sept 3, 2017
* Description:	Execute a phone Transaction Request: Recharge, Balance
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
        List<CustomerBonus> listCus;

        public PhoneTransactions()
        {
            dataAccess = new DAO();
            resp = new Response();
            cus = new Customer();
            phoneBalance = new CustomerPhone();
            transValidations = new BusinessValidations();
            cusBonus = new CustomerBonus();
            rec = new Recharge();
            listCus = new List<CustomerBonus>();
        }

        /// <summary>
        /// Execute an incoming Recharge request, calculating:
        /// 1. Saving Recharge into DB
        /// 2. Validating the Promotions for the Customer
        /// </summary>
        /// <param name="data">Stream with Json Data; Object: Recharge Entity</param>
        /// <returns>Stream with Json Data; Object: Response Data</returns>
        public Stream rechargePhoneNumber(Stream data)
        {
            try
            {
                string promAdv = string.Empty;
                decimal valueProm = 0;
                recharge = SerializationHelpers.DeserializeJson<Recharge>(data);
                cus = dataAccess.getCustomerPerPhone(recharge.Id, recharge.PhoneNumber);
                transValidations.validateCustomerSubscription(cus);
                
                var res = dataAccess.phoneRecharge(recharge); 
                if (res != 0)
                {
                    listCus = dataAccess.getLastCustomerBonus(cus.Id, cus.PhoneNumber);
                    if (listCus.Count() == 0)
                        customerPromotions(ref promAdv, ref valueProm);
                    else if (transValidations.validateGrantedBonus(listCus.OrderByDescending(c => c.ActivationDay).First().ActivationDay))
                        customerPromotions(ref promAdv, ref valueProm);

                    getCustomerBalance(valueProm);

                    resp.idResponse = res;
                    resp.response = "Recharged successfull, value: " + recharge.Value + ", " + (!string.IsNullOrEmpty(promAdv) ? promAdv : "");
                    resp.exception = null;
                }
                else
                {
                    resp.idResponse = 1;
                    resp.response = "Recharged not complete";
                    resp.exception = null;
                }
                return SerializationHelpers.GenerateStreamFromString(SerializationHelpers.SerializeJson<Response>(resp));
            }
            catch (Exception ex)
            {
                resp.idResponse = 400;
                resp.response = "Cannot finalize transaction: rechargePhoneNumber, " + ex.Message + ((ex.InnerException != null) ? ex.InnerException.Message : "");
                resp.exception = "Customer No Exist: " + ex.Message;
                return SerializationHelpers.GenerateStreamFromString(SerializationHelpers.SerializeJson<Response>(resp));
            }
        }

        /// <summary>
        /// Retrieve the actual customer's balance and insert it into the database if the 
        /// customer don't have a balance yet. otherwise the balance it is updated.
        /// </summary>
        /// <param name="valueProm">Balance value</param>
        private void getCustomerBalance(decimal valueProm)
        {
            try
            {
                Price p = dataAccess.getPrice(Convert.ToInt32(1));
                var balance = dataAccess.getBalance(cus.Id, cus.PhoneNumber);
                if (balance == null)
                {
                    balance = new CustomerPhone { Id = cus.Id, PhoneNumber = cus.PhoneNumber, MinuteBalance = ((recharge.Value + valueProm) / p.Prices), MinutesUsed = 0 };
                    var resBalance = dataAccess.customerPhoneBalance(balance);
                }
                else
                {
                    balance.MinuteBalance += ((recharge.Value + valueProm) / p.Prices);
                    dataAccess.updBalance(balance);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("getCustomerBalance Error: " + ex.Message);
            }
        }

        /// <summary>
        /// Execute an incoming Request, for retrieving the customer balance.
        /// </summary>
        /// <param name="data">Stream with Json Data; Object: CustomerPhone Entity</param>
        /// <returns>Stream with Json Data; Object: Response Data</returns>
        public Stream getPhoneBalance(Stream data)
        {
            try
            {
                phoneBalance = SerializationHelpers.DeserializeJson<CustomerPhone>(data);
                cus = dataAccess.getCustomerPerPhone(phoneBalance.Id, phoneBalance.PhoneNumber);
                transValidations.validateCustomerSubscription(cus);
                
                var res = dataAccess.getBalance(phoneBalance.Id, phoneBalance.PhoneNumber);
                if(res != null)
                {
                    resp.idResponse = 0;
                    resp.response = "Customer: " + cus.FirstName + " " + cus.SecondName + " " + cus.LastName + "\n Balance(min): " + res.MinuteBalance.ToString() + "\n Minutes used: " + res.MinutesUsed;
                    resp.exception = null;
                }
                else
                {
                    resp.idResponse = 1;
                    resp.response = "Customer: " + cus.FirstName + " " + cus.SecondName + " " + cus.LastName + "\n Balance(min): 0 \n Minutes used: 0";
                    resp.exception = null;
                }
                return SerializationHelpers.GenerateStreamFromString(SerializationHelpers.SerializeJson<Response>(resp));
            }
            catch (Exception ex)
            {
                resp.idResponse = 400;
                resp.response = "Cannot finalize transaction: getPhoneBalance, " + ex.Message + ((ex.InnerException != null) ? ex.InnerException.Message : "");
                resp.exception = ex.Message;
                return SerializationHelpers.GenerateStreamFromString(SerializationHelpers.SerializeJson<Response>(resp));
            }
        }

        /// <summary>
        /// Method that iterates into each promotion.
        /// </summary>
        /// <param name="promAdv">Reference value for warning the customer if he/she won an promotion</param>
        /// <param name="valueProm">Reference value for retrieving the value won.</param>
        private void customerPromotions(ref string promAdv, ref decimal valueProm)
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
                            bonusGranted = saveGrantedBonus(ref promAdv, ref valueProm, p, sumRecharge);
                            break;
                        case 2:
                            MinimunRecharge minRec = dataAccess.getMinRechargePerPeriod(Convert.ToInt32(Period.Weekly));
                            DateTime dt1 = DateTime.Today.AddDays(-7);

                            var lRecharges = dataAccess.getAllCustomerRecharges(cus).Where(c => c.Date >= dt1 &&
                                                                                            c.Date <= DateTime.Today &&
                                                                                            c.Value == minRec.minValue).ToList();
                            decimal avgRecharge = 0;
                            if (lRecharges.Count() >0 )
                                avgRecharge = lRecharges.Average(c => c.Value);

                            bonusGranted = saveGrantedBonus(ref promAdv, ref valueProm, p, avgRecharge);
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
                throw new Exception("customerPromotions Error: " + ex.Message);
            }
        }

        /// <summary>
        /// Validate if the Customer won a Promotion for calculating the VAlue awarded and save it into the DB
        /// </summary>
        /// <param name="promAdv">Reference value for warning the customer if he/she won an promotion</param>
        /// <param name="valueProm">Reference value for retrieving the value won.</param>
        /// <param name="p">Promotion Enum type</param>
        /// <param name="valueRecharge">Value Recharged by the Customer</param>
        /// <returns>true if the customer won a promotion, otherwise exit method.</returns>
        private bool saveGrantedBonus(ref string promAdv, ref decimal valueProm, Promotion p, decimal valueRecharge)
        {
            try
            {
                bool bonusGranted = (valueRecharge > 0) ? true : false;
                if (bonusGranted)
                {
                    //calcule bonus
                    valueProm = BussinessOps.calculateBonus(valueRecharge, p.ValueType, p.Value);
                    rec.Value = valueProm;
                    rec.Date = DateTime.Now;
                    rec.Id = cus.Id;
                    rec.PhoneNumber = cus.PhoneNumber;
                    rec.State = 1;
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
            catch (Exception ex)
            {
                throw new Exception("saveGrantedBonus Error: " + ex.Message);
            }
        }
    }
}
