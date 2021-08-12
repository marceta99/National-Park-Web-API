using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkyApi.Models.Repository.IRepository
{
   public  interface ITrailRepository
    {
        public ICollection<Trail> GetTrails();
        public Trail GetTrail(int trailID);
        public ICollection<Trail> GetTrailsInNationalPark(int nationalParkId);


        public bool TrailExist(string name);//check if park exist with name
        public bool TrailExist(int id);//check if park exist with id


        public bool CreateTrail(Trail trail);
        public bool UpdateTrail(Trail trail);
        public bool DeleteTrail(Trail trail);

        bool Save();



    }
}
