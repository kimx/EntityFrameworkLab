//------------------------------------------------------------------------------
// <auto-generated>
//     這個程式碼是由範本產生。
//
//     對這個檔案進行手動變更可能導致您的應用程式產生未預期的行為。
//     如果重新產生程式碼，將會覆寫對這個檔案的手動變更。
// </auto-generated>
//------------------------------------------------------------------------------

namespace EntityFrameworkLab
{
    using System;
    using System.Collections.Generic;
    
    public partial class SYSUSER
    {
        public SYSUSER()
        {
            this.SYSROLE = new HashSet<SYSROLE>();
        }
    
        public string USER_NO { get; set; }
        public string LOGIN_ID { get; set; }
        public string USER_PWD { get; set; }
        public string USER_NAME { get; set; }
        public string EMAIL_ADR { get; set; }
        public string COMP_NO { get; set; }
        public string DEP_NO { get; set; }
        public string REF_CODE { get; set; }
        public string USER_CULTURE { get; set; }
        public string USER_TIMEZONE { get; set; }
        public Nullable<int> LOGIN_PROVIDER { get; set; }
    
        public virtual ICollection<SYSROLE> SYSROLE { get; set; }
    }
}
