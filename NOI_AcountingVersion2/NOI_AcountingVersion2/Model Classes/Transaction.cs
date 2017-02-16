using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NOI_AcountingVersion2.Model_Classes
{
    public class Transaction
    {
        public int TransactionId { get; set; }
        public bool IsRevenue { get; set; }
        public string Name { get; set; }
        public double Amount { get; set; }
        public DateTime Date { get; set; }
        public int CompanyId { get; set; }
    }
}
