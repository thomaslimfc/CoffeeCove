//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CoffeeCove
{
    using System;
    using System.Collections.Generic;
    
    public partial class OrderedItem
    {
        public string ProductID { get; set; }
        public int OrderID { get; set; }
        public Nullable<int> Quantity { get; set; }
        public string Size { get; set; }
        public string Flavour { get; set; }
        public string IceLevel { get; set; }
        public string AddOn { get; set; }
        public string Instruction { get; set; }
        public Nullable<decimal> Price { get; set; }
    
        public virtual Product Product { get; set; }
        public virtual OrderPlaced OrderPlaced { get; set; }
    }
}