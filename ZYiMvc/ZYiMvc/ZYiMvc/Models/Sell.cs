//------------------------------------------------------------------------------
// <auto-generated>
//    此代码是根据模板生成的。
//
//    手动更改此文件可能会导致应用程序中发生异常行为。
//    如果重新生成代码，则将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace ZYiMvc.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Sell
    {
        public int ID { get; set; }
        public int MemberID { get; set; }
        public string Title { get; set; }
        public int Type { get; set; }
        public int Category { get; set; }
        public string Content { get; set; }
        public string Quantity { get; set; }
        public string Unit { get; set; }
        public int Click { get; set; }
        public string Price { get; set; }
        public string Img1 { get; set; }
        public string Img2 { get; set; }
        public string Img3 { get; set; }
        public string IsTop { get; set; }
        public string Status { get; set; }
        public System.DateTime ValidityDate { get; set; }
        public System.DateTime CreateDate { get; set; }
        public System.DateTime ModifyDate { get; set; }
    
        public virtual Member Member { get; set; }
    }
}
