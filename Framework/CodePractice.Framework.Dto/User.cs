namespace CodePractice.Framework.Dto;

public class User
{
    public long Id { get; set; }
    public string UserName { get; set; }
    public string Mobile { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public DateTimeOffset? RegisterDate { get; set; }

    public bool? Active { get; set; }
    public long? FileId { get; set; }

    public int? Opt { get; set; }
    public string Token { get; set; }
    public DateTimeOffset? ExpireToken { get; set; }
    public List<string>? Roles { get; set; }

}

public class ClientToken
{
    public string ClientId { get; set; }
    public string Token { get; set; }
}
