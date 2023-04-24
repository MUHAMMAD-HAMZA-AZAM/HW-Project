namespace HW.UserViewModels
{
    public class FavoritesVM
    {
        public string TradesmanName { get; set; }
        public byte[] TradesmanImage { get; set; }
        public string TradesmanImageName { get; set; }
        public long TradesmanId { get; set; }
        public bool IsFavorite { get; set; }
        public bool IsSelected { get; set; }
    }
}
