
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace robotCompanions.Models.Repositories
{
    public class homeRepository : IHomeRepository
    {
        private readonly ApplicationDbContext _db;
        public homeRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<IEnumerable<robotSize>> robotSizes()
        {
            return await _db.robotSize.ToListAsync();
        }

        public async Task<IEnumerable<Robots>> getRobots(string sTerm="", int sizeId = 0)
        {
            sTerm = sTerm.ToLower(); 
            IEnumerable<Robots> robots = await (from robot in _db.Robots
                  join robotSize in _db.robotSize
                  on robot.robotSize.Id equals robotSize.Id
                  where string.IsNullOrWhiteSpace(sTerm)  || (robot!=null && robot.robotName.ToLower().StartsWith(sTerm))
                  select new Robots
                  {
                      Id = robot.Id,
                      image = robot.image,
                      price = robot.price,
                      description = robot.description,
                      robotSize = robot.robotSize,
                      robotName = robot.robotName
                  }
            ).ToListAsync();

            if(sizeId > 0)
            {
                robots = robots.Where(a => a.robotSize.Id == sizeId).ToList();
            }
            return robots;
        }
    }
}
