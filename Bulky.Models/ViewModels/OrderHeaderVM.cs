namespace Bulky.Models.ViewModels
{
    public class OrderHeaderVM
    {
        public OrderHeader OrderHeader { get; set; }
        public IEnumerable<OrderDetail> OrderDetails { get; set; }
    }
}
