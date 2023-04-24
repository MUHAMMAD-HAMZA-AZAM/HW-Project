using System;
using System.Collections.Generic;
using System.Text;

namespace HW.UserViewModels
{
  
}  public class FinishedJobDetailsVM
{
    public long JobDetailId { get; set; }
    public long CustomerId { get; set; }
    public long TradesmanId { get; set; }
    public byte[] TradesmanProfileImage { get; set; }
    public string TradesmanName { get; set; }
    public string JobTitle { get; set; }
    public DateTime JobStartingDateTime { get; set; }
    public DateTime JobEndingDateTime { get; set; }
    public bool DirectPayment { get; set; }
    public Decimal Payment { get; set; }
    public string FeedbackComments { get; set; }
    public int OverallRating { get; set; }
    public string LatLng { get; set; }
    public long JobQuotationId { get; set; }
    public string JobAddress { get; set; }
    public bool IsFavorite { get; set; }
    public bool IsFavoriteTradesman { get; set; }
    public string CustomerName {get;set;}

}
