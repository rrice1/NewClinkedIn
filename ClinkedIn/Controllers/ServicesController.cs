﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClinkedIn.Data;
using ClinkedIn.Models;
using ClinkedIn.Validators;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClinkedIn.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicesController : ControllerBase
    {
        readonly ServiceRepository _serviceRepository;
        readonly CreateServiceRequestValidator _validator;
        public ServicesController()
        {
            _validator = new CreateServiceRequestValidator();
            _serviceRepository = new ServiceRepository();

        }

        [HttpPost("service")]
        //POST to add services
        public ActionResult AddService(CreateServiceRequest createRequest)
        {
            if (!_validator.Validate(createRequest))
            {
                return BadRequest(new { error = "users must have a name for their service and a cost" });
            }

            var newService = _serviceRepository.AddService(createRequest.Name, createRequest.Cost);

            return Created($"api/services/{newService.Id}", newService);
        }
        //GET all services
        [HttpGet("getServices")]

        public ActionResult getService()
        {
            var allServices = _serviceRepository.GetServices();
            
            return Ok(allServices);
            
        }
        //GET services by name
        [HttpGet("getServicesByName/{serviceName}")]

        public ActionResult getServiceByName(string serviceName)
        {
            var allServices = _serviceRepository.GetServices();            

            var limitedServices = (from service in allServices
                                   where (service.Name == serviceName)
                                   select service).ToList();

            return Ok(limitedServices);
        }

        //UPDATE service
        [HttpPut("updateService/{serviceName}/{serviceCost}")]
        public ActionResult UpdateService(string serviceName, double serviceCost)
        {
            var listOfServices = _serviceRepository.UpdateService();

            var serviceToUpdate = (from service in listOfServices
                                  where (service.Name == serviceName)
                                  select service).SingleOrDefault();

            serviceToUpdate.Cost = serviceCost;




            return Accepted(serviceToUpdate);
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteService(int id)
        {
            var servicesListAfterDeletion = _serviceRepository.DeleteService(id);
            return Ok(servicesListAfterDeletion);
        }


        [HttpPost("register")]
        public ActionResult<int> AddService(CreateNewServiceRequest createRequest)
        {

            //if (!_validator.ValidateInterest(createRequest))
            //{
            //    return BadRequest(new { error = "users must have a username and password" });
            //}

            var newService = _serviceRepository.AddService(createRequest.Name);

            return Created($"api/services/{newService.Id}", newService);
        }

        [HttpGet]
        public ActionResult GetAllServices()
        {
            var services = _serviceRepository.GetAll();

            return Ok(services);
        }
    }
}