using System;
using System.Collections.Generic;
using System.Text;

namespace HW.PackagesAndPaymentsViewModels.DTO
{
    public class JournalEntryLineDTO
    {
        public List<JournalEntryLineVM> journalEntry { get; set; }
    }
    public class JournalEntryLineVM
    {
        public int? id { get; set; }
        public UserAccount accountName { get; set; }
        public string description { get; set; }
        public decimal? debit { get; set; }
        public decimal? credit { get; set; }
        public decimal? tax { get; set; }
    }
    public class UserAccount
    {
        public long? id { get; set; }
        public string code { get; set; }
        public string name { get; set; }
    }
}
