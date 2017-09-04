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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Entities;
using DataTier;
using Common.Enum;

namespace BusinessTier.Operations
{
    public static class BussinessOps
    {
        public static decimal averageRechargeValue(Customer customer)
        {
            try
            {
                DAO data = new DAO();
                List<Recharge> re = new List<Recharge>();
                re = data.getAllCustomerRecharges(customer);
                decimal rechargesSum = re.Sum(c => c.Value);
                return rechargesSum / re.Count();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static decimal minRechargeValue(Customer customer)
        {
            try
            {
                DAO data = new DAO();
                Recharge re = new Recharge();
                return data.getAllCustomerRecharges(customer).Where(c => c.Date < DateTime.Today.AddDays(-7)).FirstOrDefault().Value;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static int callCountDown(Call call, CustomerPhone cusPhone)
        {
            try
            {
                throw new NotImplementedException();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public static decimal calculateBonus(decimal avg, PromotionType ptype, int promValue)
        {
            switch (ptype)
            {
                case PromotionType.Percentaje:
                    return avg * (promValue / 100);
                default:
                    return 0;
            }
        }

    }
}
