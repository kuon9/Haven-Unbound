using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;
using System;
using System.Text;

public class JsonDataService : IDataService
{
    private const string KEY = "m6JvE15LQhJM910VHjx1cC0YCVDdBQisMZgdYSpGmLY=";
    private const string IV = "EAywxOZS9I/OqJEZnlBoQg==";

    public bool SaveData<T>(string RelativePath, T Data, bool Encrypted)
    {
        string path = Application.persistentDataPath + RelativePath;

        try
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }
            else
            {

            }
            using FileStream stream = File.Create(path);
            if (Encrypted)
            {
                WriteEncryptedData(Data, stream);
            }
            else
            {
                stream.Close();
                File.WriteAllText(path, JsonConvert.SerializeObject(Data, Formatting.Indented));
            }
            return true;
        }
        catch (Exception e)
        {
            Debug.LogError($"Unable to save data due to : {e.Message}{e.StackTrace}");
            return false;
        }

    }

    private void WriteEncryptedData<T>(T Data, FileStream Stream)
    {
        using Aes aeasProvider = Aes.Create();
        aeasProvider.Key = Convert.FromBase64String(KEY);
        aeasProvider.IV = Convert.FromBase64String(IV);
        using ICryptoTransform cryptoTransform = aeasProvider.CreateEncryptor();
        using CryptoStream cryptoStream = new CryptoStream(
            Stream,
            cryptoTransform,
            CryptoStreamMode.Write);

        //Debug.Log($"IV:{Convert.ToBase64String(aeasProvider.IV)}");
        //Debug.Log($"Key:{Convert.ToBase64String(aeasProvider.Key)}");
        cryptoStream.Write(Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(Data)));
    }

    public T LoadData<T>(string RelativePath, bool Encrypted)
    {
        string path = Application.persistentDataPath + RelativePath;

        if (!File.Exists(path))
        {
            Debug.LogError($"Cannot load file at {path}. File does not exist");
            throw new FileNotFoundException($"{path} does not exist");
        }

        try
        {
            T data;
            if (Encrypted)
            {
                data = ReadEncryptedData<T>(path);
            }
            else
            {
                data = JsonConvert.DeserializeObject<T>(File.ReadAllText(path));
            }
            return data;
        }
        catch (Exception e)
        {
            Debug.LogError($"Unable to load data due to : {e.Message}{e.StackTrace}");
            throw e;
        }
    }
    private T ReadEncryptedData<T>(string Path)
    {
        byte[] fileBytes = File.ReadAllBytes(Path);
        using Aes aesProvider = Aes.Create();

        aesProvider.Key = Convert.FromBase64String(KEY);
        aesProvider.IV = Convert.FromBase64String(IV);

        using ICryptoTransform cryptoTransform = aesProvider.CreateDecryptor(
            aesProvider.Key,
            aesProvider.IV);

        using MemoryStream decryptionStream = new MemoryStream(fileBytes);
        using CryptoStream cryptoStream = new CryptoStream(
            decryptionStream,
            cryptoTransform,
            CryptoStreamMode.Read);
        using StreamReader reader = new StreamReader(cryptoStream);
        string reasult = reader.ReadToEnd();
        return JsonConvert.DeserializeObject<T>(reasult);
    }
}
