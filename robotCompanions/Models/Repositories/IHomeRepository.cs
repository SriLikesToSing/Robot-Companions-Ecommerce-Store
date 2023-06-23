namespace robotCompanions
{
    public interface IHomeRepository
    {
        Task<IEnumerable<Robots>> getRobots(string sTerm="", int sizeId = 0);
        Task<IEnumerable<robotSize>> robotSizes();
    }
}