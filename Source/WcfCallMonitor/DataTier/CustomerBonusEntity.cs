//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataTier
{
    using System;
    using System.Collections.Generic;
    
    public partial class CustomerBonusEntity
    {
        public int Bonus_Code { get; set; }
        public int Customer_Id { get; set; }
        public string Phone_Number { get; set; }
        public int Promotion_Id { get; set; }
        public System.DateTime Activation_day { get; set; }
    
        public virtual CustomerEntity CUSTOMER { get; set; }
        public virtual PromotionEntity PROMOTION { get; set; }
    }
}