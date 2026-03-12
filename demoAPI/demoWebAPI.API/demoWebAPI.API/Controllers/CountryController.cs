/*!
 * \class CountryController
 * \brief Handles HTTP requests related to Country operations.
 *
 * \details
 * This controller provides endpoints to manage country data
 * such as creating new country records. It uses the repository
 * pattern to abstract database operations.
 */
using demoWebAPI.API.model.domain;
using demoWebAPI.API.model.DTO;
using demoWebAPI.API.Repositories.Implementation;
using demoWebAPI.API.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
[Produces("application/json")]
public class CountryController : ControllerBase
{
    private readonly IcountryRepository countryRepository;

    /*!
     * \brief Constructor for CountryController.
     *
     * \details
     * Dependency Injection is used to inject the country repository,
     * which handles all database operations related to Country entity.
     *
     * \param countryRepository Repository instance for Country operations.
     */
    public CountryController(IcountryRepository countryRepository)
    {
        this.countryRepository = countryRepository;
    }

    /*!
     * \brief Adds a new country to the database.
     *
     * \details
     * This endpoint performs the following steps:
     * - Validates whether the country already exists.
     * - Maps the incoming DTO to a domain model.
     * - Saves the entity using repository.
     * - Returns a response DTO.
     *
     * \param request DTO containing country details.
     * \return IActionResult:
     *         - 200 OK with created country data
     *         - 400 BadRequest if country already exists
     */
    [HttpPost]
    public async Task<IActionResult> AddCountry(AddCountryRequestDto request)
    {
        /*!
         * \step Step 1: Check if country already exists.
         *
         * \code
         * var isExist = await countryRepository.checkCountry(request.CountryName);
         * if (isExist != null)
         * {
         *     return BadRequest("The country name already exists in the table.");
         * }
         * \endcode
         */
        var isExist = await countryRepository.checkCountry(request.CountryName);
        if (isExist != null)
        {
            return BadRequest(new
            {
                message = "Country name is already exist."
            });
        }

        /*!
         * \step Step 2: Map DTO to Domain Model.
         *
         * \code
         * var country = new Country
         * {
         *     CountryName = request.CountryName
         * };
         * \endcode
         */
        var country = new Country
        {
            CountryName = request.CountryName
        };

        /*!
         * \step Step 3: Save country using repository.
         *
         * \code
         * await countryRepository.CreateAsync(country);
         * \endcode
         */
        await countryRepository.CreateAsync(country);

        /*!
         * \step Step 4: Map Domain Model to Response DTO.
         *
         * \code
         * var response = new CountryDto
         * {
         *     Id = country.Id,
         *     CountryName = country.CountryName
         * };
         * \endcode
         */
        var response = new CountryDto
        {
            Id = country.Id,
            CountryName = country.CountryName
        };

        /*!
         * \step Step 5: Return success response.
         *
         * \code
         * return Ok(response);
         * \endcode
         */
        return Ok(response);


    }


    [HttpGet]
    public async Task<IActionResult> getAllCountry()
    {

        var countrys = await countryRepository.GetAll();

        var response = new List<CountryDto>();
        foreach (var country in countrys)
        {
            response.Add(new CountryDto
            {
                Id = country.Id,
                CountryName = country.CountryName
            });
        }
        return Ok(response);
    }

    //get by id

    [HttpGet]
    [Route("{id:Guid}")]
    public async Task<IActionResult> getAcountry([FromRoute] Guid id)
    {
        var country = await countryRepository.GetById(id);
        if(country == null)
        {
            return NotFound();
        }
        var response = new CountryDto
        {
            Id = country.Id,
            CountryName = country.CountryName
        };
        return Ok(response);

    }

    [HttpPut]
    [Route("{id:Guid}")]

    public async Task<IActionResult> updateCountry([FromRoute] Guid id , updateCountryRequestDto request)
    {
        var country = new Country
        {
            Id = id,
            CountryName = request.CountryName
        };
        var isExist = await countryRepository.checkCountry(country.CountryName);
        if (isExist != null)
        {
            return BadRequest(new
            {
                message = "Country name is already exist."
            });
        }
        country = await countryRepository.EditbyId(country);
        if(country == null)
        {
            return NotFound();
        }
        
        var response = new CountryDto
        {
            Id = country.Id,
            CountryName = country.CountryName
        };
        return Ok(response);


    }

    [HttpDelete]
    [Route("{id:Guid}")]

    public async Task<IActionResult> Deletecountry([FromRoute] Guid id)
    {
        var country = await countryRepository.DeleteById(id);
        if(country == null)
        {
            return NotFound();
        }
        var response = new CountryDto
        {
            Id = country.Id,
            CountryName = country.CountryName
        };
        return Ok(response);

    }


}