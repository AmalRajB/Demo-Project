using demoWebAPI.API.model.domain;
using demoWebAPI.API.model.DTO;
using demoWebAPI.API.Repositories.Implementation;
using demoWebAPI.API.Repositories.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography.X509Certificates;
using System.Linq;

namespace demoWebAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StateController : ControllerBase
    {
        private readonly IStateRepository stateRepository;
        private readonly IcountryRepository countryRepository;

        public StateController(IStateRepository stateRepository, IcountryRepository countryRepository)
        {
            this.stateRepository = stateRepository;
            this.countryRepository = countryRepository;
        }


        [HttpPost]
        public async Task<IActionResult> AddState([FromBody] AddStateRequestDto request)
        {


            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var iscountryExist = await countryRepository.ExistsAsync(request.CountryId);
            if (!iscountryExist)
            {
                return BadRequest(new
                {
                    message = "the country id is invalid...."
                });
            }


            var state = new State
            {
                Id = Guid.NewGuid(),
                StateName = request.StateName,
                CountryId = request.CountryId,
            };

            var createdState = await stateRepository.CreateAsync(state);



            var response = new StateDto
            {
                Id = createdState.Id,
                StateName = createdState.StateName,
                CountryId = createdState.CountryId,
                Country = new CountryDto
                {
                    Id = state.Country.Id,
                    CountryName = state.Country.CountryName
                }
            };

            return Ok(response);
        }




        [HttpGet]
        public async Task<IActionResult> getallStates([FromQuery] paginationDto pagination)
        {
            var states = await stateRepository.getAll(pagination.PageNumber,pagination.PageSize);
            var totalRecord = await stateRepository.getTotalCount();

            var response = new List<StateDto>();
            foreach (var state in states)
            {
                response.Add(new StateDto
                {
                    Id = state.Id,
                    StateName = state.StateName,
                    CountryId = state.CountryId,
                    Country = new CountryDto
                    {
                        Id = state.Country.Id,
                        CountryName = state.Country.CountryName
                    }

                });
            }
            return Ok(new
            {
                totalRecord,
                pageNumber = pagination.PageNumber,
                pageSize = pagination.PageSize,
                data = response
            });
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> getByid([FromRoute] Guid id)
        {
            var state = await stateRepository.GetById(id);
            if(state == null)
            {
                return NotFound();
            }
            var response = new StateDto
            {
                Id = state.Id,
                StateName = state.StateName,
                CountryId = state.CountryId,
                Country = new CountryDto
                {
                    Id = state.Country.Id,
                    CountryName = state.Country.CountryName
                }

            };
            return Ok(response);

        }


        [HttpPut]
        [Route("{id:Guid}")]

        public async Task<IActionResult> editState([FromRoute] Guid id , updateStateRequestDto request)
        {
            var state = new State
            {
                Id = id,
                StateName = request.StateName,
                CountryId = request.CountryId,
            };
            var updateState = await stateRepository.EditStateById(state);
            if(updateState == null)
            {
                return NotFound();
            }
            var response = new StateDto
            {
                Id = updateState.Id,
                StateName = updateState.StateName,
                CountryId = updateState.CountryId,
                Country = new CountryDto
                {
                    Id = updateState.Country.Id,
                    CountryName = updateState.Country.CountryName
                }
            };
            return Ok(response);

        }


        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteState([FromRoute] Guid id)
        {
            var state = await stateRepository.DeleteById(id);
            if(state == null)
            {
                return NotFound();

            }
            var response = new StateDto
            {
                Id = state.Id,
                StateName = state.StateName
            };
            return Ok(response);


        }

        [HttpGet("by-country/{id}")]
        public async Task<IActionResult> GetStateByCountryId(Guid id)
        {
            var result = await stateRepository.getByCountryId(id);

            if (result == null)
            {
                return NotFound();
            }

            var response = result.Select(state => new StateDto
            {
                Id = state.Id,
                StateName = state.StateName,
                CountryId = state.CountryId,
                Country = new CountryDto
                {
                    Id = state.Country.Id,
                    CountryName = state.Country.CountryName
                }
            }).ToList();

            return Ok(response);
        }
    }
}
    

