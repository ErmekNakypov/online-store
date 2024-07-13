using Model.Common;

namespace Model.Entities;

public class User : BaseEntity
{
    public string UserName { get; set; } = String.Empty;
    public string Password { get; set; } = String.Empty;
}