using AzureFunction.Application.Interface;
using AzureFunction.Domain.Entities;
using AzureFunction.Persistance.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace POS.AzureFunctionApp
{
    public class Function1
    {
        private readonly ILogger<Function1> _logger;
        private readonly IUserRepository _userRepo;

        public Function1(ILogger<Function1> logger, IUserRepository userRepo)
        {
            _logger = logger;
            _userRepo = userRepo;
        }

        [Function("CreateUserTrigger")]
        public async Task<IActionResult> RunAsync([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequest req)
        {
            //throw new NotImplementedException();
            _logger.LogInformation("Called the Create User Trigger");
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            User user = JsonConvert.DeserializeObject<User>(requestBody);
            if (user == null || string.IsNullOrEmpty(user.UserName))
            {
                return new BadRequestObjectResult("Please pass a valid user in the request body");
            }
            User createdUser = await _userRepo.CreateUser(user);
            return new OkObjectResult(createdUser);
        }
    }
}
