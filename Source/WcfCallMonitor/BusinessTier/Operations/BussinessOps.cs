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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="call"></param>
        /// <param name="cusPhone"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="avg"></param>
        /// <param name="ptype"></param>
        /// <param name="promValue"></param>
        /// <returns></returns>
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
    }
}
