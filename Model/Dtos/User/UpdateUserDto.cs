namespace Model.Dtos.User;

public class UpdateUserDto
{
    public int Id { get; set; }
    public string UserName { get; set; } = String.Empty;
    public string Password { get; set; } = String.Empty;
}