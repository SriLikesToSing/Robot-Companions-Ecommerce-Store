namespace robotCompanions.Models.Repositories
{
    public interface IUserOrderRepository
    {
        Task<IEnumerable<Order>> userOrders();
    }
}