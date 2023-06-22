using System.Security.Cryptography;
using System.Text;
using MeuMenu.CrossCutting.AppSettings;
using Microsoft.Extensions.Options;

namespace MeuMenu.Api.Infra;

public class CriptografiaLoginService
{
    private readonly AppSettings _appSettings;

    public CriptografiaLoginService(IOptions<AppSettings> opt)
    {
        _appSettings = opt.Value;
    }

    /// <summary>
    ///     Método responsável por Criptografar um valor passado como parâmetro com base na configuração de login
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public string? Descriptografar(string? value)
    {
        // Check arguments.  
        if (string.IsNullOrEmpty(value)) return null;

        var a = _appSettings.Login?.SecretKey;
        var key = Encoding.UTF8.GetBytes(a!);

        var encrypted = Convert.FromBase64String(value);

        // Declare the string used to hold  
        // the decrypted text.  
        string? plaintext;

        // with the specified key and IV.  
        using var aes = Aes.Create();
        //Settings  
        aes.Mode = CipherMode.CBC;
        aes.Padding = PaddingMode.PKCS7;
        aes.BlockSize = 128;

        aes.Key = key;
        aes.IV = key;

        // Create a decrytor to perform the stream transform.  
        var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

        try
        {
            // Create the streams used for decryption.  
            using var msDecrypt = new MemoryStream(encrypted);
            using var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read);
            using var srDecrypt = new StreamReader(csDecrypt);
            // Read the decrypted bytes from the decrypting stream  
            // and place them in a string.  
            plaintext = srDecrypt.ReadToEnd();
        }
        catch
        {
            plaintext = "keyError";
        }

        return plaintext;
    }
}