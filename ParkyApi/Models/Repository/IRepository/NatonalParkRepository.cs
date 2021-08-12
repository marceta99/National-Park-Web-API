using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ParkyApi.Data;
using ParkyApi.Models.Dtos;

namespace ParkyApi.Models.Repository.IRepository
{
    public class NatonalParkRepository : INationalParkRepository
    {

        private readonly ApplicationDbContext _db; 
        public NatonalParkRepository(ApplicationDbContext db)
        {
            _db = db; 
        }
        public bool CreateNationalPark(NationalPark nationalPark)
        {
            _db.NationalParks.Add(nationalPark);

            return Save(); 
        }

        public bool DeleteNationalPark(NationalPark nationalPark)
        {
            _db.NationalParks.Remove(nationalPark);

            return Save();
        }

        public NationalPark GetNationalPark(int nationalParkID)
        {
            return _db.NationalParks.FirstOrDefault(np => np.Id == nationalParkID); 
        }

        public ICollection<NationalPark> GetNationalParks()
        {
            return _db.NationalParks.OrderBy(np => np.Name).ToList(); 
        }

        public bool NationalParkExist(string name)
        {
            bool value = _db.NationalParks.Any(np => np.Name.ToLower().Trim() == name.ToLower().Trim());
            return value; 
        }

        public bool NationalParkExist(int id)
        {
            bool value = _db.NationalParks.Any(np => np.Id == id);
            return value; 
        }

        public bool Save()
        {
            return (_db.SaveChanges() >= 0) ? true : false ;
        }

        public bool UpdateNationalPark(NationalPark nationalPark)
        {
            _db.NationalParks.Update(nationalPark);
            return Save(); 
        }
    }
}
