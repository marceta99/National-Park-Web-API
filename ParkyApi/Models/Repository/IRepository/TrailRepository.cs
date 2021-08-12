using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ParkyApi.Data;

namespace ParkyApi.Models.Repository.IRepository
{
    public class TrailRepository : ITrailRepository
    {
        private readonly ApplicationDbContext _db;
        public TrailRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public bool CreateTrail(Trail trail)
        {
            _db.Trails.Add(trail);

            return Save();
        }

        public bool DeleteTrail(Trail trail)
        {
            _db.Trails.Remove(trail);

            return Save();
        }

        public Trail GetTrail(int trailID)
        {
            return _db.Trails.Include(t => t.NationalPark).
                FirstOrDefault(np => np.Id == trailID);
        }

        public ICollection<Trail> GetTrails()
        {
            return _db.Trails.Include(t => t.NationalPark).
                OrderBy(np => np.Name).ToList();
        }

        public bool TrailExist(string name)
        {
            bool value = _db.Trails.Any(np => np.Name.ToLower().Trim() == name.ToLower().Trim());
            return value;
        }

        public bool TrailExist(int id)
        {
            bool value = _db.Trails.Any(np => np.Id == id);
            return value;
        }

        public bool Save()
        {
            return (_db.SaveChanges() >= 0) ? true : false;
        }

        public bool UpdateTrail(Trail trail)
        {
            _db.Trails.Update(trail);
            return Save();
        }

        public ICollection<Trail> GetTrailsInNationalPark(int nationalParkId)
        {
            return _db.Trails.Include(t=>t.NationalParkId)
                .Where(t=> t.NationalParkId == nationalParkId).ToList(); 
        }
    }
}
