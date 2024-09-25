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
    
    public partial class OrderPlaced
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public OrderPlaced()
        {
            this.OrderedItems = new HashSet<OrderedItem>();
            this.PaymentDetails = new HashSet<PaymentDetail>();
        }
    
        public int OrderID { get; set; }
        public int CusID { get; set; }
        public Nullable<int> StoreID { get; set; }
        public string DeliveryAddress { get; set; }
        public System.DateTime OrderDateTime { get; set; }
        public decimal TotalAmount { get; set; }
        public string OrderStatus { get; set; }
        public string OrderType { get; set; }
    
        public virtual Customer Customer { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderedItem> OrderedItems { get; set; }
        public virtual Store Store { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PaymentDetail> PaymentDetails { get; set; }
    }
}
