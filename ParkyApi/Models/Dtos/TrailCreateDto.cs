using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using static ParkyApi.Models.Trail;

namespace ParkyApi.Models.Dtos
{
    public class TrailCreateDto
    {
        //public int Id { get; set; }
        //we dont need id when we crate new trail because new id will be automaticly given to new
        //trail when we create new one with post request

        [Required]
        public string Name { get; set; }

        [Required]
        public double Distance { get; set; }

        public DifficultyType Difficulty { get; set; }


        [Required]
        public int NationalParkId { get; set; }//foreign key refrenece to a national park

        // public NationalParkDto NationalPark { get; set; }
        //here we do not need a national park object while updating because we dont wont when we 
        //create trail , to also have to pass national park object with all properties
        //we just wont to pass id to that national park where trail is 
    }
}
