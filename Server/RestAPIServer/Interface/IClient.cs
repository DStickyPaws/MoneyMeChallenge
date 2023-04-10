namespace RestAPIServer.Interface;

/**
    <summary>
        A data record representation of a client.
    </summary>
*/
public interface IClient
{
    /**
        <summary>
            The ID representing the data record of a client.
        </summary>
    */
    long? Id { get; set; }
    /**
        <summary>
            The First Name of the Client.
        </summary>
    */
    string FirstName { get; set; }
    /**
        <summary>
            The Last Name of the Client.
        </summary>
    */
    string LastName { get; set; }
    /**
        <summary>
            The client's date of birth.
        </summary>
    */
    string DateOfBirth { get; set; }
}
