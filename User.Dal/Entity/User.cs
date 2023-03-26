namespace User.Dal.Entity;

public class User
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string SecondName { get; set; }
    public string Login { get; init; }
    public string Password { get; init; }
}