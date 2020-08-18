using AutoMapper;
using Contracts;
using Entities.Dtos;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AddressAddressServer.Controllers
{
    [Route(mainRoute)]
    [ApiController]
    public class AddressesController : ControllerBase
    {
        private const string mainRoute = "Api/V1/Addresses";
        private readonly ILoggerManager _logger;
        private readonly IRepositoryWrapper _repository;
        private readonly IMapper _mapper;

        public AddressesController(ILoggerManager logger, IRepositoryWrapper repository, IMapper mapper)
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
                var addresses = _repository.Address.GetAllAddresses();

                _logger.LogInfo($"Retrived all addresses from database");

                var addressesResult = _mapper.Map<IEnumerable<AddressDto>>(addresses);

                return Ok(addressesResult);

            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in GetAllAddress() action: {ex.Message}");
                if (ex.InnerException != null)
                    _logger.LogError($"Inner Exception: {ex.InnerException.Message}");
                return StatusCode(500, "Internal Server Error, you can show error details in logfile.txt");
            }
        }

        [HttpGet("GetById/{id}", Name = "GetAddressById")]
        public IActionResult GetById(Guid id)
        {
            try
            {
                var address = _repository.Address.GetAddressById(id);

                if (address == null)
                {
                    _logger.LogError($"address with id {id}, has't found in database");
                    return NotFound();
                }

                _logger.LogInfo($"Retrived address with id {id}");

                var addressResult = _mapper.Map<AddressDto>(address);

                return Ok(addressResult);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in GetAddressById() action: {ex.Message}");
                if (ex.InnerException != null)
                    _logger.LogError($"Inner Exception: {ex.InnerException.Message}");
                return StatusCode(500, "Internal Server Error, you can show error details in logfile.txt");
            }
        }

        [HttpPost("Create")]
        public IActionResult CreateAddress([FromBody] AddressForCreationDto address)
        {
            try
            {
                if (address == null)
                {
                    _logger.LogError("Address object sent from client is null.");
                    return BadRequest("Address object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid address object sent from client.");
                    return BadRequest("Invalid model object");
                }

                if (IsAddressAdded(null, address.Title, address.PersonId))
                {
                    _logger.LogError($"Address with title: {address.Title} added before to person Id: {address.PersonId}.");
                    return BadRequest("Address added before");
                }

                var addressEntity = _mapper.Map<Address>(address);

                _repository.Address.CreateAddress(addressEntity);
                _repository.Save();

                var createdAddress = _mapper.Map<AddressDto>(addressEntity);

                return CreatedAtRoute("GetAddressById", new { id = createdAddress.Id }, createdAddress);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside CreateAddress action: {ex.Message}");
                if (ex.InnerException != null)
                    _logger.LogError($"Inner Exception: {ex.InnerException.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("Update/{id}")]
        public IActionResult UpdateAddress(Guid id, [FromBody] AddressForUpdateDto address)
        {
            try
            {
                if (id == null || address == null)
                {
                    _logger.LogError("Address object sent from client is null.");
                    return BadRequest("Address object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid address object sent from client.");
                    return BadRequest("Invalid model object");
                }

                var addressEntity = _repository.Address.GetAddressById(id);
                if (addressEntity == null)
                {
                    _logger.LogError($"Address with id: {id}, hasn't been found in db.");
                    return NotFound();
                }

                if (IsAddressAdded(address.Id, address.Title, address.PersonId))
                {
                    _logger.LogError($"Address with title: {address.Title} added before to person Id: {address.PersonId}.");
                    return BadRequest($"Address added before to person Id: {address.PersonId}");
                }

                _mapper.Map(address, addressEntity);

                _repository.Address.UpdateAddress(addressEntity);
                _repository.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside UpdateAddress action: {ex.Message}");
                if (ex.InnerException != null)
                    _logger.LogError($"Inner Exception: {ex.InnerException.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("Delete/{id}")]
        public IActionResult DeleteAddress(Guid id)
        {
            try
            {
                var addressEntity = _repository.Address.GetAddressById(id);
                if (addressEntity == null)
                {
                    _logger.LogError($"Address with id: {id}, hasn't been found in db.");
                    return NotFound();
                }

                _repository.Address.DeleteAddress(addressEntity);
                _repository.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside DeleteAddress action: {ex.Message}");
                if (ex.InnerException != null)
                    _logger.LogError($"Inner Exception: {ex.InnerException.Message}");
                if (ex.InnerException != null)
                    _logger.LogError($"Inner Exception: {ex.InnerException.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost("IsAddressAdded")]
        public IActionResult IsAddressAdded([FromBody] AddressIsAddedDto address)
        {
            try
            {
                if (address == null)
                {
                    _logger.LogError("Address object sent from client is null.");
                    return BadRequest("Address object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid address object sent from client.");
                    return BadRequest("Invalid model object");
                }

                return Ok(IsAddressAdded(address.Id, address.Title, address.PersonId));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside IsAddressAdded action: {ex.Message}");
                if (ex.InnerException != null)
                    _logger.LogError($"Inner Exception: {ex.InnerException.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        private bool IsAddressAdded(Guid? id, string title, Guid personId)
        {
            title = title.ToLower();
            var addressIsAdded = _repository.Address.GetAllAddressesByConditions(x =>
                x.Title.ToLower().Equals(title) &&
                x.PersonId.Equals(personId) &&
                !x.Id.Equals(id)).Any();
            return addressIsAdded;
        }
    }
}
