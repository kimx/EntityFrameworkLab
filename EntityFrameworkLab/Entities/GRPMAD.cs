//------------------------------------------------------------------------------
// <auto-generated>
//     這個程式碼是由範本產生。
//
//     對這個檔案進行手動變更可能導致您的應用程式產生未預期的行為。
//     如果重新產生程式碼，將會覆寫對這個檔案的手動變更。
// </auto-generated>
//------------------------------------------------------------------------------

namespace EntityFrameworkLab.Entities
{
    using System;
    using System.Collections.Generic;
    
    public partial class GRPMAD
    {
        public string SPM_ID { get; set; }
        public int ITEM_NO { get; set; }
        public string REC_ID { get; set; }
        public string DEL_CODE { get; set; }
        public Nullable<System.DateTime> CRT_UTC { get; set; }
        public string CRT_DEPT { get; set; }
        public string CRT_USER { get; set; }
        public Nullable<System.DateTime> MOD_UTC { get; set; }
        public string MOD_DEPT { get; set; }
        public string MOD_USER { get; set; }
    
        public virtual GRPMAH GRPMAH { get; set; }
    }
}
