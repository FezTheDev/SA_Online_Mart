namespace SA_Online_Mart.Models.Cart;

public class CartItem
{
    public Product Product { get; set; }
    public int Quantity { get; set; }
}

public class Cart
{
    public List<CartItem> Items { get; set; } = new List<CartItem>();

    public void AddToCart(Product product)
    {
        var item = Items.FirstOrDefault(i => i.Product.Id == product.Id);

        if (item == null)
        {
            Items.Add(new CartItem { Product = product, Quantity = 1 });
        }
        else
        {
            item.Quantity++;
        }
    }

    public void RemoveFromCart(int productId)
    {
        var item = Items.FirstOrDefault(i => i.Product.Id == productId);

        if (item != null)
        {
            Items.Remove(item);
        }
    }

    public decimal GetTotalPrice()
    {
        return Items.Sum(i => i.Product.Price * i.Quantity);
    }
}
