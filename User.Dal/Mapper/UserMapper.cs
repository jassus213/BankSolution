namespace User.Dal.Mapper;

public static class UserMapper
{
    public static User.Entity.UserInfo Map(Entity.User data)
    {
        var result = new User.Entity.UserInfo
        {
            Name = data.Name,
            SecondName = data.SecondName,
        };

        return result;
    }
    
    public static Entity.User MapBack(User.Entity.UserInfo data)
    {
        var result = new Entity.User
        {
            Name = data.Name,
            SecondName = data.SecondName,
        };

        return result;
    }
    
}