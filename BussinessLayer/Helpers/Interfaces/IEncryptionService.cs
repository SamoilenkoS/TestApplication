namespace BusinessLayer.Helpers.Interfaces
{
    public interface IEncryptionService
    {
        string Encrypt(string plainText);
        string Decrypt(string encrypted);
    }
}
