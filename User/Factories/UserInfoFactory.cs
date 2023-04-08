using User.Entity;

namespace User.Factories;

public class UserInfoFactory
{
    public UserInfo Create(string name, string secondName)
    {
        return new UserInfo()
        {
            Name = name,
            SecondName = secondName
        };
    }
}