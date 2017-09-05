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
using Common.Entities;
using Common.Enum;
using System;
using System.Collections.Generic;

namespace DataTier.Helpers
{
    public class EntityHelper
    {
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
            throw new NotImplementedException();
        }

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
    }

}
