namespace BussinessLayer.Interfaces
{
    public interface IHashService
    {
        string HashString(string stringToHash);
        bool ValidateHash(string hashedString, string stringToHash);
    }
}
