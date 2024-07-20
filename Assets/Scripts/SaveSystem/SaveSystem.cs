using UnityEngine;
using System.IO;
using Newtonsoft.Json;
using System.Threading.Tasks;

public static class SaveSystem
{
    private static string filePath = Application.persistentDataPath + "/savefile.json";

    public static async Task SaveDataAsync(Save data)
    {
        string json = JsonConvert.SerializeObject(data);
        json = EncryptionUtility.Encrypt(json);
        await File.WriteAllTextAsync(filePath, json);
    }

    public static Save LoadData()
    {
        if (File.Exists(filePath))
        {
            string encryptedJson = File.ReadAllText(filePath);
            encryptedJson = EncryptionUtility.Decrypt(encryptedJson);
            return JsonConvert.DeserializeObject<Save>(encryptedJson);
        }
        return null;
    }

    public static void DeleteData()
    {
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }
    }
}
