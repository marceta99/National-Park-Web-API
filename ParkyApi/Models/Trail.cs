using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ParkyApi.Models
{
    public class Trail
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set;  }
        
        [Required]
        public double Distance { get; set; }

        public enum DifficultyType 
        { 
            Easy,Moderate,Difficult,Expert
        }
        public DifficultyType Difficulty { get; set; }

    
        [Required]
        public int NationalParkId { get; set; }//foreign key refrenece to a national park

        [ForeignKey("NationalParkId")]//foreign key to table NationalPark is NationalParkId
        public NationalPark NationalPark { get; set; }


        public DateTime DateCreated { get; set; } //this property will not be in Dto and we dont wont to share this property with our users





    }
}
