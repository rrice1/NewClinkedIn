using System;
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
    public class UserInterestsController : ControllerBase
    {
        readonly UserInterestRepository _userInterestRepository;
        readonly UserRepository _userRepository;
        //readonly CreateUserInterestRequestValidator _validator;
        readonly InterestRepository _interestRepository;
        public UserInterestsController()
        {
            //_validator = new CreateUserInterestRequestValidator();
            _userInterestRepository = new UserInterestRepository();
            _userRepository = new UserRepository();
            _interestRepository = new InterestRepository();
        }

        [HttpPost("userInterest")]

        //public ActionResult AddUserService(CreateUserServiceRequest createRequest)
        //{
        //    //if (!_validator.Validate(createRequest))
        //    //{
        //    //    return BadRequest(new { error = "userServices must have an id and a userId" });
        //    //}

        //    var newUserService = _userServiceRepository.AddUserService(createRequest.Id, createRequest.UserId, createRequest.ServiceId);

        //    return Created($"api/userServices/{newUserService.Id}", newUserService);
        //}

        //[HttpGet("getUserServices")]
        ////GET data from userServices (helps with figuring out what ID's to use in the below GET) 
        //public ActionResult getUserService()
        //{
        //    var allServices = _userServiceRepository.GetUserServices();

        //    return Ok(_userServiceRepository.GetUserServices());

        //}

        //[HttpGet("getUserServicesById/{userId}")]
        ////GET services by userID
        //public ActionResult getUserServices(int userId)
        //{
        //    var allUserServices = _userServiceRepository.GetUserServices();
        //    var allUsers = _userRepository.GetAllUsers();
        //    var allServices = _serviceRepository.GetServices();

        //    var limitedUserServices = (from userService in allUserServices
        //                               join user in allUsers on userService.UserId equals user.Id
        //                               join service in allServices on userService.ServiceId equals service.Id
        //                               where (userService.UserId == userId)
        //                               select new { user.Id, user.DisplayName, service.Name });

        //    return Ok(limitedUserServices);
        //}

        //[HttpGet("getAllUserAndServices")]
        ////GET services by userID
        //public ActionResult getUserServices()
        //{
        //    var allUserServices = _userServiceRepository.GetUserServices();
        //    var allUsers = _userRepository.GetAllUsers();
        //    var allServices = _serviceRepository.GetServices();

        //    var allUsersAndServices = (from userService in allUserServices
        //                               join user in allUsers on userService.UserId equals user.Id
        //                               join service in allServices on userService.ServiceId equals service.Id
        //                               select new { userService.Id, userService.UserId, user.Username, userService.ServiceId, service.Name, service.Cost });

        //    return Ok(allUsersAndServices);
        //}

        ////UPDATE service
        //[HttpPut("updateUserService/{userServiceId}/{serviceId}")]
        //public ActionResult UpdateUserService(int userServiceId, int serviceId)
        //{
        //    var listOfUserServices = _userServiceRepository.UpdateUserService();

        //    var userServiceToUpdate = (from userService in listOfUserServices
        //                               where (userService.Id == userServiceId)
        //                               select userService).SingleOrDefault();

        //    userServiceToUpdate.ServiceId = serviceId;




        //    return Accepted(userServiceToUpdate);
        //}
        ////DELETE userService
        //[HttpDelete("{id}")]
        //public ActionResult DeleteUserService(int id)
        //{
        //    var userServicesListAfterDeletion = _userServiceRepository.DeleteUserService(id);
        //    return Ok(userServicesListAfterDeletion);
        //}

        [HttpPost("register")]
        public ActionResult<int> AddUserService(CreateNewUserInterestRequest createRequest)
        {

            //if (!_validator.ValidateInterest(createRequest))
            //{
            //    return BadRequest(new { error = "users must have a username and password" });
            //}

            var newUserInterest = _userInterestRepository.AddUserInterest(createRequest.UserId, createRequest.InterestId);

            return Created($"api/userInterests/{newUserInterest.Id}", newUserInterest);
        }

        [HttpGet]
        public ActionResult GetAllUserInterests()
        {
            var userInterests = _userInterestRepository.GetAll();

            return Ok(userInterests);
        }

    }
}