using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace In_Home.Areas.Customers.Pages.Account
{
    public class TClients
    {
        [Key]
        public int IdClient {get;set;}
        public string CI { get; set; } 
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Direction { get; set; }
        public string Phone { get; set; }
        public DateTime Date { get; set; } 
        public bool Credit { get; set; }
        public byte[] Image { get; set; } 
        public List<TReports_clients> TReport_cliente { get; set; }
    }
}