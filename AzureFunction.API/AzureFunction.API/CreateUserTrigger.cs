using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using AzureFunction.Domain.Entities;
using AzureFunction.Application.Interface;

namespace AzureFunction.API
{
    public class CreateUserTrigger
    {
        private readonly IUserRepository _userRepository;
        public CreateUserTrigger(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [FunctionName("CreateUser")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            User user = JsonConvert.DeserializeObject<User>(requestBody);

            if (user == null || string.IsNullOrEmpty(user.UserName))
            {
                return new BadRequestObjectResult("Please pass a valid user in the request body");
            }

            User createdUser = await _userRepository.CreateUser(user);

            return new OkObjectResult(createdUser);
        }
    }
}
