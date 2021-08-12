using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ParkyApi.Models;
using ParkyApi.Models.Dtos;
using ParkyApi.Models.Repository.IRepository;

namespace ParkyApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[ApiExplorerSettings(GroupName = "ParkyOpenAPINationalPark")]
    public class NationalParksController : Controller
    {
        private readonly INationalParkRepository _npRepo;
        private readonly IMapper _mapper;

        public NationalParksController(INationalParkRepository npRepo ,IMapper mapper)
        {
            _mapper = mapper;
            _npRepo = npRepo; 
        }
        
 
        [HttpGet]
        public IActionResult GetNationalParks()
        {
            var objModels = _npRepo.GetNationalParks();

            var objDtos = new List<NationalParkDto>(); 
            
            foreach(var obj in objModels)
            {
                objDtos.Add(_mapper.Map<NationalParkDto>(obj)); 
            }

            return Ok(objDtos); 
        }
       
        [HttpGet("{nationalParkId:int}",Name = "GetNationalPark")]
        public IActionResult GetNationalPark(int nationalParkId = 3)
        {
            var objModel = _npRepo.GetNationalPark(nationalParkId);

            if(objModel == null)
            {
                return NotFound();
            }
            var objDto = _mapper.Map<NationalParkDto>(objModel);

            return Ok(objDto); 
        }

        [HttpPost]
        public IActionResult CreateNationalPark([FromBody] NationalParkDto nationalParkDto)
        {
            if(nationalParkDto == null)
            {
                return BadRequest(ModelState); //modelstate has all the necesary errors wich should be displayed
            }

            if (_npRepo.NationalParkExist(nationalParkDto.Name))
            {
                ModelState.AddModelError("", "National Park alery exist in our database!!!");
                return StatusCode(404, ModelState);
            }

            //first we have to convert dto object in model object
            NationalPark nationalParkModelObj = _mapper.Map<NationalPark>(nationalParkDto);

            if (!_npRepo.CreateNationalPark(nationalParkModelObj))
            {
               ModelState.AddModelError("",$"somtething went wrong with {nationalParkModelObj.Name}");
               return StatusCode(500, ModelState);
            }
            // return Ok(); we can use return Ok but it is better practise to retutrn value of created objekct with CreatedAtRoute 
            return CreatedAtRoute("GetNationalPark",
                new{ nationalParkId = nationalParkModelObj.Id },nationalParkModelObj);
           //first parametar call get action method with name GetNationalPark and with Id parametar
          //in new anonymous object, and after that gives object a vaule of created object 
          //nationalParkModelObj

          //Ok() returns 200 status response, but CreatedAtRoute returns 201 status resposne
          //and 201 is probably better pratices to use
         }

        [HttpPatch("{nationalParkId:int}",Name ="UpdateNationalPark")]
        public IActionResult UpdateNationalPark(int nationalParkId,[FromBody] NationalParkDto nationalParkDto)
        {
            if (nationalParkDto == null || nationalParkId != nationalParkDto.Id )
            {
                return BadRequest(ModelState);
            }

            

            NationalPark nationalParkModelObj = _mapper.Map<NationalPark>(nationalParkDto);

            if (!_npRepo.UpdateNationalPark(nationalParkModelObj))
            {
                ModelState.AddModelError("", $"somtething went wrong with {nationalParkModelObj.Name}");
                return StatusCode(500, ModelState);
            }

            return NoContent(); //with patch request usually just returns noContent()
        }
        
        
        [HttpDelete("{nationalParkId:int}")]
        public IActionResult DeleteNacionalPark(int nationalParkId)
        {
            if (!_npRepo.NationalParkExist(nationalParkId))
            {
                return NotFound(); 
            }

            NationalPark nationalParkModelObj = _npRepo.GetNationalPark(nationalParkId);

            if (!_npRepo.DeleteNationalPark(nationalParkModelObj))
            {
                ModelState.AddModelError("", $"somtething went wrong with {nationalParkModelObj.Name}");
                return StatusCode(500, ModelState);
            }
            return NoContent(); 

        }
    
    
    
    
    
    
    
    
    
    }
}
