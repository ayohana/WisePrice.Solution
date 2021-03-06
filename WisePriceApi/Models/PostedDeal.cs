using Newtonsoft.Json;

namespace WisePriceApi.Models
{
  public class PostedDeal
  {
    public int PostedDealId { get; set; }
    public string UserId { get; set; }
    public int DealId { get; set; }

    public virtual User User {get; set;}

    public virtual Deal Deal {get; set;}
  }
}