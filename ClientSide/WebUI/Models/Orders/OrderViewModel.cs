namespace WebUI.Models.Orders;

public class OrderViewModel
{
    public int Id { get; set; }
    public DateTime CreatedDate { get; set; }

    //Ödeme geçmişimde adress alanına ihtiyaç olmadığından dolayı alınmadı
    // public AddressDto Address { get; set; }

    public string BuyerId { get; set; }

    public List<OrderItemViewModel> OrderItems { get; set; }
}