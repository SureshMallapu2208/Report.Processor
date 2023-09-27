namespace Report.Processor.Services
{
    public interface IUserService
    {
        Task<List<User>> GetAllusers();
        //Task<User> Createuser(User user);

    }
}
