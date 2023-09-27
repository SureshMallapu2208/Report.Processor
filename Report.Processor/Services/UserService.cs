using Amazon.DynamoDBv2.DataModel;
using System.Text.Json;

namespace Report.Processor.Services
{
    public class UserService : IUserService
    {
        private readonly IDynamoDBContext _dbContext;

        public UserService(IDynamoDBContext context)
        {
            _dbContext = context;
        }
        //public async Task<User> Createuser(User user)
        //{
        //    var userData = await _dbContext.LoadAsync<User>(user.Id);
        //    if (userData != null) return null;
        //    await _dbContext.SaveAsync(user);
        //    return user;
        //}

        public async Task<List<User>> GetAllusers()
        {
            return await _dbContext.ScanAsync<User>(default).GetRemainingAsync();
        }
    }
}
