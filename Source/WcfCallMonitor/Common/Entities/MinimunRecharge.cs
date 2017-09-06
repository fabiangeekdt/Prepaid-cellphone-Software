#region Authoring Description
/* 
* =================================================================================
* Author:		Fabian Andres Moreno chacon
* Create date:  Sept 2, 2017
* Description:	MinimunRecharge Entity for keeping the it's data. 
* =================================================================================
* ============================= CHANGES ===========================================
* Author:		
* Create date: 
* Description:	
* =================================================================================
*/
#endregion
using Common.Enum;

namespace Common.Entities
{
    public class MinimunRecharge
    {
        public int id{ get; set; }
        public Period Period { get; set; }
        public decimal minValue { get; set; }
    }
}
