namespace robotCompanions.Models.Repositories
{
    public interface ICartRepository
    {
        Task<int> addItem(int robotId, int qty);
        Task<int> removeItem(int robotId);
        Task<shoppingCart> getUserCart();
        Task<int> getCartItemCount(string userId = "");
        Task<bool> doCheckout();
        Task<shoppingCart> getCart(string userId);
    }
}