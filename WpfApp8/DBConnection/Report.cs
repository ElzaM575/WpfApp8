//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WpfApp8.DBConnection
{
    using System;
    using System.Collections.Generic;
    
    public partial class Report
    {
        public int report_id { get; set; }
        public string month { get; set; }
        public string type { get; set; }
        public Nullable<decimal> profit { get; set; }
        public Nullable<decimal> expenses { get; set; }
        public Nullable<int> event_id { get; set; }
    
        public virtual Event Event { get; set; }
    }
}
