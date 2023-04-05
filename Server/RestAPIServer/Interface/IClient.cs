namespace RestAPIServer.Interface;

public interface IClient
{
    long? Id { get; set; }
    string FirstName { get; set; }
    string LastName { get; set; }
    string DateOfBirth { get; set; }
}
