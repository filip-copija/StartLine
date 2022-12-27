using System.Security.Cryptography;
using System.Text;

namespace StartLine_social_network
{
    public static class Hashing
    {
        /* Converts the value of an array of 8-bit unsigned integers to its 
         equivalent string representation that is encoded with base-64 digits.
        The ComputeHash method of HashAlgorithm computes a hash. It takes a byte 
        array or stream as an input and returns a hash in the form of a byte array of 256 bits. */

        public static string Hash(string value) =>
            Convert.ToBase64String(SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(value)));
    }
}
