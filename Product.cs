public class Product
 {
    public int ID { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int Price { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime LastEdited { get; set; }
    
    public Product()
       {          
         this.CreatedDate  = DateTime.Now;
         this.LastEdited = DateTime.Now;
       }
 }
