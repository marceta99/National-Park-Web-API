using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ParkyApi.Models
{
    public class NationalPark
    {
        [Key] //primary key
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string State { get; set; }

        public DateTime Created { get; set; }//when national park is found
        public DateTime Establshed { get; set; }//when is officaly became national park
   
        public byte[] Picture { get; set; }
    
    }
}
