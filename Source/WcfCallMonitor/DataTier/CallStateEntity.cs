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
    
    public partial class CallStateEntity
    {
        public CallStateEntity()
        {
            this.CALL = new HashSet<CallEntity>();
        }
    
        public int State_Id { get; set; }
        public string State_Description { get; set; }
    
        public virtual ICollection<CallEntity> CALL { get; set; }
    }
}