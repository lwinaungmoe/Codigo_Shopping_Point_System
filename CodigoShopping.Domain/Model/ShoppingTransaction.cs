using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodigoShopping.Domain.Model
{
    public class ShoppingTransaction
    {
        public int Id { get; set; }

        public int? AppUserId { get; set; }  
        public AppUser AppUser { get; set; }
        public string TransactionRefno { get; set; }
        public string TransactionType { get; set; }
        public decimal TotalTransactionAmount { get; set; }
        public DateTime TransactionDateTime { get; set; }
        public int  TransactionPoint { get; set; }
        public bool IsDeleted { get; set; }
    }

    public class ShoppingTransactionDetails
    {
        public int Id { get; set; }
      
        public int TransactionId { get; set; }

        public ICollection<ShoppingTransaction> ShoppingTransaction { get; set; }

        public  int CatelogItemId { get; set; }

        public ICollection<CatalogItem> CatalogItem { get; set; }

        public decimal UnitPrice { get; set; }

    }
}
