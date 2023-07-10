namespace Mobile.API.Model
{
    public class CheckOutResponse
    {
        public string ErrorCode { get; set; }

        public string ErrorMessage { get; set; }
        public int TransactonId { get; set; }

        public string TransactionRefno { get; set; }

        public decimal TotalAmount { get; set; }

        public DateTime TranactionDateTime { get; set; }

        public int TranactionPoint { get; set; }

    }
}
