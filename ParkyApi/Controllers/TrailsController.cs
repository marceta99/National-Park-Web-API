using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ParkyApi.Models;
using ParkyApi.Models.Dtos;
using ParkyApi.Models.Repository.IRepository;

namespace ParkyApi.Controllers
{
    [Route("api/Trails")]
    [ApiController]
    //[ApiExplorerSettings(GroupName = "ParkyOpenAPITrails")]
    public class TrailsController : Controller
    {

        
            private readonly ITrailRepository _trailRepo;
            private readonly IMapper _mapper;

            public TrailsController(ITrailRepository trailRepo, IMapper mapper)
            {
                _mapper = mapper;
                _trailRepo = trailRepo;
            }


        [HttpGet]
        public IActionResult GetTrails()
        {
            var objModels = _trailRepo.GetTrails();

            var objDtos = new List<TrailDto>();

            foreach (var obj in objModels)
            {
                objDtos.Add(_mapper.Map<TrailDto>(obj));
            }

            return Ok(objDtos);
        }

        [HttpGet("{trailId:int}", Name = "GetTrail")]
        public IActionResult GetTrail(int trailId = 3)
        {
            var objModel = _trailRepo.GetTrail(trailId);

            if (objModel == null)
            {
                return NotFound();
            }
            var objDto = _mapper.Map<TrailDto>(objModel);

            return Ok(objDto);
        }

        [HttpGet("GetTrailInNationalPark/{nationalParkId:int}")]
        public IActionResult GetTrailInNationalPark(int nationalParkId)
        {
            var objList = _trailRepo.GetTrailsInNationalPark(nationalParkId);

            if(objList == null)
            {
                return NotFound(); 
            }
            var objDto = new List<TrailDto>();
            foreach(var obj in objList)
            {
                objDto.Add(_mapper.Map<TrailDto>(obj)); 
            }

            return Ok(objDto); 

        }

        [HttpPost]
        public IActionResult CreateTrail([FromBody] TrailCreateDto trailDto)
        {
            if (trailDto == null)
            {
                return BadRequest(ModelState); //modelstate has all the necesary errors wich should be displayed
            }

            if (_trailRepo.TrailExist(trailDto.Name))
            {
                ModelState.AddModelError("", "National Park alery exist in our database!!!");
                return StatusCode(404, ModelState);
            }

            //first we have to convert dto object in model object
            Trail trailModelObj = _mapper.Map<Trail>(trailDto);

            if (!_trailRepo.CreateTrail(trailModelObj))
            {
                ModelState.AddModelError("", $"somtething went wrong with {trailModelObj.Name}");
                return StatusCode(500, ModelState);
            }
            // return Ok(); we can use return Ok but it is better practise to retutrn value of created objekct with CreatedAtRoute 
            return CreatedAtRoute("GetTrail",
                new { trailId = trailModelObj.Id }, trailModelObj);
            //first parametar call get action method with name GetTrail and with Id parametar
            //in new anonymous object, and after that gives object a vaule of created object 
            //trailModelObj

            //Ok() returns 200 status response, but CreatedAtRoute returns 201 status resposne
            //and 201 is probably better pratices to use
        }

        [HttpPatch("{trailId:int}", Name = "UpdateTrail")]
        public IActionResult UpdateTrail(int trailId, [FromBody] TrailUpdateDto trailDto)
        {
            if (trailDto == null || trailId != trailDto.Id)
            {
                return BadRequest(ModelState);
            }



            Trail trailModelObj = _mapper.Map<Trail>(trailDto);

            if (!_trailRepo.UpdateTrail(trailModelObj))
            {
                ModelState.AddModelError("", $"somtething went wrong with {trailModelObj.Name}");
                return StatusCode(500, ModelState);
            }

            return NoContent(); //with patch request usually just returns noContent()
        }


        [HttpDelete("{trailId:int}")]
        public IActionResult DeleteTrail(int trailId)
        {
            if (!_trailRepo.TrailExist(trailId))
            {
                return NotFound();
            }

            Trail trailModelObj = _trailRepo.GetTrail(trailId);

            if (!_trailRepo.DeleteTrail(trailModelObj))
            {
                ModelState.AddModelError("", $"somtething went wrong with {trailModelObj.Name}");
                return StatusCode(500, ModelState);
            }
            return NoContent();

        }


    }
    }
