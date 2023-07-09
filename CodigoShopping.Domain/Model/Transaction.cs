using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodigoShopping.Domain.Model
{
    public class Transaction
    {
        public int Id { get; set; }
        public string TransactionRefno { get; set; }
        public string TransactionType { get; set; }
        public decimal TotalTransactionAmount { get; set; }
        public DateTime TransactionDateTime { get; set; }
    }

    public class TransactionDetails
    {
        public int Id { get; set; }
      
        public string TransactionId { get; set; }

        public Transaction Transaction { get; set; }

        public  int CatelogItemId { get; set; }

        public CatalogItem CatalogItem { get; set; }

        public decimal TransactionAmount { get; set; }

    }
}
