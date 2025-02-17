using System.ComponentModel.DataAnnotations.Schema;

namespace Shoezone.Model
{
    public class Order
    {
        public int OrderId { get; set; }
        
        public String? UserName { get; set; }
        public  String? Product { get; set; }
        public int Quantity { get; set; }

        public DateTime OrderDate { get; set; }
            
  
        
    
    }
}
