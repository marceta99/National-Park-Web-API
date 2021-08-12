using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ParkyApi.Models.Dtos;

namespace ParkyApi.Models.Repository.IRepository
{
    public interface INationalParkRepository
    {
        public ICollection<NationalPark> GetNationalParks();
        public NationalPark GetNationalPark(int nationalParkID);
        public bool NationalParkExist(string name);//check if park exist with name

        public bool NationalParkExist(int id);//check if park exist with id
        public bool CreateNationalPark(NationalPark nationalPark);
        public bool UpdateNationalPark(NationalPark nationalPark);
        public bool DeleteNationalPark(NationalPark nationalPark);

        bool Save(); 
        


    }
}
