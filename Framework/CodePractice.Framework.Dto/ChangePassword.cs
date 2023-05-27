namespace CodePractice.Framework.Dto;

public class ChangePassword
{
    public long UserId { get; set; }
    public string OldPassword { get; set; }
    public string Password { get; set; }
    public string RePassword { get; set; }
}