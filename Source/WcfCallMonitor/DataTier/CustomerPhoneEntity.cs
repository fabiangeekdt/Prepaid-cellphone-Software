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
    
    public partial class CustomerPhoneEntity
    {
        public int Internal_Id { get; set; }
        public int Customer_Id { get; set; }
        public string Phone_Number { get; set; }
        public byte Phone_state { get; set; }
        public decimal Minute_Balance { get; set; }
        public decimal Minutes_Use { get; set; }
    
        public virtual CustomerEntity CUSTOMER { get; set; }
    }
}
