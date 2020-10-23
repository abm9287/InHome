using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace In_Home.Models
{
    public class DataPaginador<T>
    {
        public List<T> List { get; set; }
        public string Pagina_info { get; set; }
        public string Pagina_navegacion { get; set; }
        public T Input { get; set; }
        public string Search { get; set; } 
    }
}
