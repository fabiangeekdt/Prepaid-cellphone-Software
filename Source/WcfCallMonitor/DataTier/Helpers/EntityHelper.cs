#region Authoring Description
/* 
* =================================================================================
* Author:		Fabian Andres Moreno Chacon
* Create date:  Sept 3, 2017
* Description:	Entity helper class to convert all the database entities into 
*               WCF Entities
* =================================================================================
* ============================= CHANGES ===========================================
* Author:		
* Create date: 
* Description:	
* =================================================================================
*/
#endregion
using Common.Entities;
using Common.Enum;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataTier.Helpers
{
    public class EntityHelper
    {
        /// <summary>
        /// Method that convert a CustomerEntity(Database) into Customer
        /// </summary>
        /// <param name="customer">CustomerEntity</param>
        /// <returns>Customer</returns>
        public Customer convertToEntity(CustomerEntity customer)
        {
            try
            {
                Customer cus = new Customer();
                cus.Id = customer.Id;
                cus.Id_Type = customer.Id_Type;
                cus.FirstName = customer.First_Name;
                cus.SecondName = customer.Second_Name;
                cus.LastName = customer.Last_Name;
                cus.PhoneNumber = customer.Phone_Number;
                return cus;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Method that convert a CustomerPhoneEntity(Database) into CustomerPhone
        /// </summary>
        /// <param name="cus">CustomerPhoneEntity</param>
        /// <returns>CustomerPhone</returns>
        public CustomerPhone convertToEntity(CustomerPhoneEntity cus)
        {
            try
            {
                CustomerPhone cusPhone = new CustomerPhone();
                cusPhone.Id = cus.Customer_Id;
                cusPhone.PhoneNumber = cus.Phone_Number;
                cusPhone.MinuteBalance = cus.Minute_Balance;
                cusPhone.MinutesUsed = cus.Minutes_Use;
                return cusPhone;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Method that convert a CallEntity(Database) into Call
        /// </summary>
        /// <param name="cEntity">CallEntity</param>
        /// <returns>Call</returns>
        public Call convertToEntity(CallEntity cEntity)
        {
            try
            {
                Call c = new Call();
                c.CallId = cEntity.PhoneCall_Id;
                c.Cost = cEntity.Call_Cost;
                c.Duration = cEntity.Call_Duration;
                c.State = cEntity.Call_State;
                c.PhoneNumber = cEntity.Phone_Number;
                c.Id = cEntity.Customer_id;
                c.InitialDatetime = cEntity.Initial_DateTime;
                c.FinalDatetime = cEntity.Final_DateTime;
                c.DestinationNumber = cEntity.Destination_Number;
                return c;   
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Method that convert a List<RechargeEntity>(Database) into List<Recharge>
        /// </summary>
        /// <param name="list">List<RechargeEntity></param>
        /// <returns>List<Recharge></returns>
        public List<Recharge> convertToEntity(List<RechargeEntity> list)
        {
            try
            {
                List<Recharge> lrecharge = new List<Recharge>();
                foreach(var l in list)
                {
                    Recharge r = new Recharge();
                    r.Id = l.Customer_Id;
                    r.PhoneNumber= l.Phone_Number;
                    r.Code = l.Recharge_Code;
                    r.Value = l.Recharge_value;
                    r.Date = l.Recharge_Date;
                    r.State = l.Recharge_State;
                    lrecharge.Add(r);
                }
                return lrecharge;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Method that convert a CustomerBonusEntity(Database) into CustomerBonus
        /// </summary>
        /// <param name="cusBonus">CustomerBonusEntity</param>
        /// <returns>CustomerBonus</returns>
        public CustomerBonus convertToEntity(CustomerBonusEntity cusBonus)
        {
            try
            {
                CustomerBonus cBonus = new CustomerBonus();
                cBonus.Id = cusBonus.Customer_Id;
                cBonus.PhoneNumber = cusBonus.Phone_Number;
                cBonus.BonusCode = cusBonus.Promotion_Id;
                cBonus.ActivationDay = cusBonus.Activation_day;
                return cBonus;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Method that convert a PriceEntity(Database) into Price
        /// </summary>
        /// <param name="price">PriceEntity</param>
        /// <returns>Price</returns>
        public Price convertToEntity(PriceEntity price)
        {
            try
            {
                Price p = new Price();
                p.Id = price.Id;
                p.Prices = price.Price1;
                p.Description = price.Description;
                return p;
            }
            catch (Exception ex)
            {
                throw ex; 
            }
        }

        /// <summary>
        /// Method that convert a MinimunRechargeEntity(Database) into MinimunRecharge
        /// </summary>
        /// <param name="minR">MinimunRechargeEntity</param>
        /// <returns>MinimunRecharge</returns>
        public MinimunRecharge convertToEntity(MinimunRechargeEntity minR)
        {
            try
            {
                MinimunRecharge minRecharge = new MinimunRecharge();
                minRecharge.id = minR.Id;
                minRecharge.minValue = minR.MinimunValue;
                minRecharge.Period = minRecharge.Period;
                return minRecharge;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Method that convert a List<PromotionEntity>(Database) into List<Promotion>
        /// </summary>
        /// <param name="list">List<PromotionEntity></param>
        /// <returns>List<Promotion></returns>
        public List<Promotion> convertToEntity(List<PromotionEntity> list)
        {
            try
            {
                List<Promotion> lpro = new List<Promotion>();
                foreach(var l in list)
                {
                    Promotion pro = new Promotion();
                    pro.Id = l.Id;
                    pro.Description = l.Description;
                    pro.Value = l.Value;
                    pro.ValueType = (PromotionType)l.Value_Type;
                    lpro.Add(pro);
                }
                return lpro;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Method that convert a IQueryable<CustomerBonusEntity>(Database) into List<CustomerBonus>
        /// </summary>
        /// <param name="queryable">IQueryable<CustomerBonusEntity></param>
        /// <returns>List<CustomerBonus></returns>
        public List<CustomerBonus> convertToEntity(IQueryable<CustomerBonusEntity> queryable)
        {
            try
            {
                List<CustomerBonus> lcusb = new List<CustomerBonus>();
                foreach (var item in queryable)
                {
                    var cusBonus = new CustomerBonus();
                    cusBonus.ActivationDay = item.Activation_day;
                    cusBonus.BonusCode = item.Bonus_Code;
                    cusBonus.Id = item.Customer_Id;
                    cusBonus.PhoneNumber = item.Phone_Number;
                    cusBonus.PromotionId = item.Promotion_Id;
                    lcusb.Add(cusBonus);
                }
                return lcusb;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

}
