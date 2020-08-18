using AutoMapper;
using Contracts;
using Entities.Dtos;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AddressPersonServer.Controllers
{
    [Route(mainRoute)]
    [ApiController]
    public class PersonsController : ControllerBase
    {
        private const string mainRoute = "Api/V1/Persons";
        private readonly ILoggerManager _logger;
        private readonly IRepositoryWrapper _repository;
        private readonly IMapper _mapper;

        public PersonsController(ILoggerManager logger, IRepositoryWrapper repository, IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            try
            {
                var persons = _repository.Person.GetAllPersons();

                _logger.LogInfo($"Retrived all persons from database");

                var personsResult = _mapper.Map<IEnumerable<PersonDto>>(persons);

                return Ok(personsResult);

            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in GetAllPerson() action: {ex.Message}");
                if (ex.InnerException != null)
                    _logger.LogError($"Inner Exception: {ex.InnerException.Message}");
                return StatusCode(500, "Internal Server Error, you can show error details in logfile.txt");
            }
        }

        [HttpGet("GetById/{id}", Name = "GetPersonById")]
        public IActionResult GetById(Guid id)
        {
            try
            {
                var person = _repository.Person.GetPersonById(id);

                if (person == null)
                {
                    _logger.LogError($"person with id {id}, has't found in database");
                    return NotFound();
                }

                _logger.LogInfo($"Retrived person with id {id}");

                var personResult = _mapper.Map<PersonDto>(person);

                return Ok(personResult);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in GetPersonById() action: {ex.Message}");
                if (ex.InnerException != null)
                    _logger.LogError($"Inner Exception: {ex.InnerException.Message}");
                return StatusCode(500, "Internal Server Error, you can show error details in logfile.txt");
            }
        }

        [HttpGet("GetByIdWithAddresses/{id}")]
        public IActionResult GetPersonWithAddresses(Guid id)
        {
            try
            {
                var person = _repository.Person.GetPersonWithAddresses(id);

                if (person == null)
                {
                    _logger.LogError($"person with addresses with id {id}, has't found in database");
                    return NotFound();
                }

                _logger.LogInfo($"Retrived person with addresses with id {id}");

                var personResult = _mapper.Map<PersonDto>(person);

                return Ok(personResult);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in GetPersonWithAddresses() action: {ex.Message}");
                if (ex.InnerException != null)
                    _logger.LogError($"Inner Exception: {ex.InnerException.Message}");
                return StatusCode(500, "Internal Server Error, you can show error details in logfile.txt");
            }
        }

        [HttpPost("Create")]
        public IActionResult CreatePerson([FromBody] PersonForCreationDto person)
        {
            try
            {
                if (person == null)
                {
                    _logger.LogError("Person object sent from client is null.");
                    return BadRequest("Person object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid person object sent from client.");
                    return BadRequest("Invalid model object");
                }

                if (IsPersonAdded(null, person.FullName))
                {
                    _logger.LogError($"person with full name: {person.FullName} added before.");
                    return BadRequest("Person added before");
                }

                var personEntity = _mapper.Map<Person>(person);

                _repository.Person.CreatePerson(personEntity);
                _repository.Save();

                var createdPerson = _mapper.Map<PersonDto>(personEntity);

                return CreatedAtRoute("GetPersonById", new { id = createdPerson.Id }, createdPerson);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside CreatePerson action: {ex.Message}");
                if (ex.InnerException != null)
                    _logger.LogError($"Inner Exception: {ex.InnerException.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("Update/{id}")]
        public IActionResult UpdatePerson(Guid id, [FromBody] PersonForUpdateDto person)
        {
            try
            {
                if (id == null || person == null)
                {
                    _logger.LogError("Person object sent from client is null.");
                    return BadRequest("Person object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid person object sent from client.");
                    return BadRequest("Invalid model object");
                }

                if (IsPersonAdded(person.Id, person.FullName))
                {
                    _logger.LogError($"person with full name: {person.FullName} added before.");
                    return BadRequest("Person added before");
                }

                var personEntity = _repository.Person.GetPersonById(id);
                if (personEntity == null)
                {
                    _logger.LogError($"Person with id: {id}, hasn't been found in db.");
                    return NotFound();
                }

                _mapper.Map(person, personEntity);

                _repository.Person.UpdatePerson(personEntity);
                _repository.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside UpdatePerson action: {ex.Message}");
                if (ex.InnerException != null)
                    _logger.LogError($"Inner Exception: {ex.InnerException.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("Delete/{id}")]
        public IActionResult DeletePerson(Guid id)
        {
            try
            {
                var personEntity = _repository.Person.GetPersonById(id);
                if (personEntity == null)
                {
                    _logger.LogError($"Person with id: {id}, hasn't been found in db.");
                    return NotFound();
                }

                if (_repository.Address.AddressesByPerson(id).Any())
                {
                    _logger.LogError($"Cannot delete person with id: {id}. It has related addresses. Delete those addresses first");
                    return BadRequest("Cannot delete person. It has related addresses. Delete those addresses first");
                }

                _repository.Person.DeletePerson(personEntity);
                _repository.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside DeletePerson action: {ex.Message}");
                if (ex.InnerException != null)
                    _logger.LogError($"Inner Exception: {ex.InnerException.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost("IsPersonAdded")]
        public IActionResult IsPersonAdded([FromBody] PersonIsAddedDto person)
        {
            try
            {
                if (person == null)
                {
                    _logger.LogError("Person object sent from client is null.");
                    return BadRequest("Person object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid person object sent from client.");
                    return BadRequest("Invalid model object");
                }

                return Ok(IsPersonAdded(person.Id, person.FullName));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside IsPersonAdded action: {ex.Message}");
                if (ex.InnerException != null)
                    _logger.LogError($"Inner Exception: {ex.InnerException.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        private bool IsPersonAdded(Guid? id, string fullName)
        {
            fullName = fullName.ToLower();
            var personIsAdded = _repository.Person.GetAllPersonsByConditions(x => x.FullName.ToLower().Equals(fullName) && !x.Id.Equals(id)).Any();
            return personIsAdded;
        }
    }
}
