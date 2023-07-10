namespace Mobile.API.Model
{
    public class CheckOutRequest
    {
        public int AppUserId { get; set; }

        public decimal TotalAmount { get; set; }
        public string CheckOutType { get; set; }
        public List<CheckOutItems> Items { get; set; } = new();
    }
}
