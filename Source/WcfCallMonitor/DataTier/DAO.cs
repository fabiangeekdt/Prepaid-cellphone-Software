﻿#region Authoring Description
/* 
* =================================================================================
* Author:		Fabian Andres Moreno chacon
* Create date:  Sept 2, 2017
* Description:	Data access class for execute CRUD operations in the 
*               CallMonitor database
* =================================================================================
* ============================= CHANGES ===========================================
* Author:		
* Create date: 
* Description:	
* =================================================================================
*/
#endregion
using System;
using System.Linq;
using Common.Entities;
using System.Collections.Generic;
using DataTier.Helpers;

namespace DataTier
{
    public class DAO
    {
        EntityHelper eHelper;
        public DAO()
        {
            eHelper = new EntityHelper();
        }
        /// <summary>
        /// insert the customer into the database company
        /// </summary>
        /// <param name="customer">Customer</param>
        /// <returns>int</returns>
        public int subscribeCustomer(Customer customer)
        {
            int res = 0;
            try
            {
                using (var dbCtx = new CallMonitorModelEntities())
                {
                    dbCtx.CUSTOMER.Add(new CustomerEntity
                    {
                        Id = customer.Id,
                        Id_Type = customer.Id_Type,
                        First_Name = customer.FirstName,
                        Second_Name = customer.SecondName,
                        Last_Name = customer.LastName,
                        Phone_Number = customer.PhoneNumber
                    });
                    res = dbCtx.SaveChanges();
                }
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// retrive the information of the customer by type id, id and phone number
        /// </summary>
        /// <param name="Customer"></param>
        /// <returns></returns>
        public Customer getCustomerPerPhone(int Id_Type, int Id, string PhoneNumber)
        {
            try
            {
                var dbCtx = new CallMonitorModelEntities();
                return eHelper.convertToEntity(dbCtx.CUSTOMER.Where(c => c.Id_Type == Id_Type &&
                                                c.Id == Id &&
                                                c.Phone_Number == PhoneNumber).FirstOrDefault());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// start the phone call made by the customer
        /// </summary>
        /// <param name="call"></param>
        /// <returns></returns>
        public int initPhoneCall(Call call)
        {
            try
            {
                int res = 0;
                using(var dbCtx = new CallMonitorModelEntities())
                {
                    dbCtx.CALL.Add(new CallEntity
                    {
                        Customer_id = call.Id,
                        Phone_Number = call.PhoneNumber,
                        Destination_Number = call.DestinationNumber,
                        Initial_DateTime = call.InitialDatetime,
                        Final_DateTime = call.FinalDatetime,
                        Call_Duration = call.Duration,
                        Call_Cost = call.Cost,
                        Call_State = call.State
                    });
                    res = dbCtx.SaveChanges();  
                }
                res = getLastCall(call).CallId;
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// finish the phone call made by the customer
        /// </summary>
        /// <param name="call"></param>
        /// <returns></returns>
        public int endPhoneCall(Call call)  
        {
            try
            {
                var dbCtx = new CallMonitorModelEntities();
                CallEntity cuscall = dbCtx.CALL.Where(c => c.Customer_id == call.Id &&
                                                        c.Phone_Number == call.PhoneNumber).FirstOrDefault();

                cuscall.Final_DateTime = call.FinalDatetime;
                cuscall.Call_Duration = call.Duration;
                cuscall.Call_Cost = call.CallId;
                cuscall.Call_State = call.State;
                return dbCtx.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            throw new NotImplementedException();
        }

        /// <summary>
        /// retrieve the last call made by the customer
        /// </summary>
        /// <param name="call"></param>
        /// <returns></returns>
        private Call getLastCall(Call call)
        {
            try
            {
                var dbCtx = new CallMonitorModelEntities();
                return eHelper.convertToEntity(dbCtx.CALL.Where(c => c.Customer_id == call.Id &&
                                        c.Phone_Number == call.PhoneNumber).OrderByDescending(c => c.PhoneCall_Id).First());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// insert a recharge request from the customer into the database 
        /// </summary>
        /// <param name="charge"></param>
        /// <returns></returns>
        public int phoneRecharge(Recharge charge)
        {
            try
            {
                var dbCtx = new CallMonitorModelEntities();
                RechargeEntity reCharge = dbCtx.RECHARGE.Where(c => c.Customer_Id == charge.Id && c.Phone_Number == charge.PhoneNumber).FirstOrDefault();
                reCharge.Customer_Id = charge.Id;
                reCharge.Phone_Number = charge.PhoneNumber;
                reCharge.Recharge_value = charge.Value;
                reCharge.Recharge_Date = charge.Date;
                reCharge.Recharge_State = charge.State;

                return dbCtx.SaveChanges();
                //using (var dbCtx = new CallMonitorModelEntities())
                //{
                //    dbCtx.RECHARGE.Add(new RechargeEntity
                //    {
                //        Customer_Id = charge.Id,
                //        Phone_Number = charge.PhoneNumber,
                //        Recharge_value = charge.Value,
                //        Recharge_Date = charge.Date,
                //        Recharge_State = charge.State
                //    });
                //    res = dbCtx.SaveChanges();
                //}
                //return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Retrieve all the Recharges that the customer have done.
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        public List<Recharge> getAllCustomerRecharges(Customer customer)
        {
            try
            {
                var dbCtx = new CallMonitorModelEntities();
                return eHelper.convertToEntity(dbCtx.RECHARGE.Where(c => c.Customer_Id == customer.Id &&
                                                c.Phone_Number == customer.PhoneNumber
                                            ).OrderByDescending(c=> c.Recharge_Date).ToList());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// insert the initial customer's balance
        /// </summary>
        /// <param name="cusPhone"></param>
        /// <returns></returns>
        public int customerPhoneBalance(CustomerPhone cusPhone)
        {
            int res = 0;
            try
            {
                using(var dbCtx = new CallMonitorModelEntities())
                {
                    dbCtx.CUSTOMER_PHONE.Add(new CustomerPhoneEntity
                    {
                        Customer_Id = cusPhone.Id,
                        Phone_Number = cusPhone.PhoneNumber,
                        Phone_state = cusPhone.PhoneState,
                        Minute_Balance = cusPhone.MinuteBalance,
                        Minutes_Use = cusPhone.MinutesUsed
                    });
                    res = dbCtx.SaveChanges();
                }
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// retrieve the actual customer's minutes balance 
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        public CustomerPhone getBalance(int Id, string PhoneNumber)
        {
            try
            {
                var dbCtx = new CallMonitorModelEntities();
                var entityHelper = new EntityHelper();
                return entityHelper.convertToEntity(dbCtx.CUSTOMER_PHONE.Where(c => c.Customer_Id == Id &&
                                                    c.Phone_Number == PhoneNumber).FirstOrDefault());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// update the customer's minute balance
        /// </summary>
        /// <param name="cusPhone"></param>
        /// <returns></returns>
        public int updBalance(CustomerPhone cusPhone)
        {
            try
            {
                var dbCtx = new CallMonitorModelEntities();
                CustomerPhoneEntity cPhone = dbCtx.CUSTOMER_PHONE.Where(c => c.Customer_Id == cusPhone.Id &&
                                                    c.Phone_Number == cusPhone.PhoneNumber).FirstOrDefault();

                cPhone.Minutes_Use = cusPhone.MinutesUsed;
                cusPhone.MinuteBalance = cusPhone.MinuteBalance;
                return dbCtx.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// insert an awarded bonus to a customer
        /// </summary>
        /// <param name="cusBonus"></param>
        /// <returns></returns>
        public int customerBonus(CustomerBonus cusBonus)
        {
            int res = 0;
            try
            {
                using (var dbCtx = new CallMonitorModelEntities())
                {
                    dbCtx.CUSTOMER_BONUS.Add(new CustomerBonusEntity
                    {
                        Customer_Id = cusBonus.Id,
                        Phone_Number = cusBonus.PhoneNumber,
                        Promotion_Id = cusBonus.PromotionId,
                        Activation_day = cusBonus.ActivationDay
                    });
                    res = dbCtx.SaveChanges();
                }
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// retrieve the last customer's awarded bonus 
        /// </summary>
        /// <param name="cusBonus"></param>
        /// <returns></returns>
        public CustomerBonus getLastCustomerBonus(int Id, string PhoneNumber)
        {
            try
            {
                var dbCtx = new CallMonitorModelEntities();
                return eHelper.convertToEntity(dbCtx.CUSTOMER_BONUS.Where(c => c.Customer_Id == Id &&
                                                    c.Phone_Number == PhoneNumber).OrderByDescending(c => c.Activation_day).First());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// retrieve the minute price
        /// </summary>
        /// <param name="idPrice"></param>
        /// <returns></returns>
        public Price getPrice(int idPrice)
        {
            try
            {
                var dbCtx = new CallMonitorModelEntities();
                return eHelper.convertToEntity(dbCtx.PRICE.Where(c => c.Id == idPrice).FirstOrDefault());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// retrieve the minimun rechard value per period, for validating bonuses
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MinimunRecharge getMinRechargePerPeriod(int id)
        {
            try
            {
                var dbCtx = new CallMonitorModelEntities();
                return eHelper.convertToEntity(dbCtx.MINIMUN_RECHARGE.Where(c => c.Id == id).FirstOrDefault());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// retrieve the list of promotions availables
        /// </summary>
        /// <returns></returns>
        public List<Promotion> getPromotions()
        {
            try
            {
                var dbCtx = new CallMonitorModelEntities();
                return eHelper.convertToEntity(dbCtx.PROMOTION.OrderByDescending(c => c.Id).ToList());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
