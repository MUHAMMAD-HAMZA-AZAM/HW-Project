namespace HW.TradesmanViewModels
{
    public class TradesmanVM
    {
        public long TradesmanId { get; set; }
    }
    public class TradesmanReportbySkillVM : TradesmanModels.Tradesman
    {
        public string Name { get; set; }
        public byte[] ProfileImage { get; set; }
        public int Reviews { get; set; }
        public int Rating { get; set; }
        public long SkillId { get; set; }
    }
}
