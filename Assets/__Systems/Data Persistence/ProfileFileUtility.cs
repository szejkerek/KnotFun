using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace PlaceHolders.DataPersistence
{
    public class ProfileFileUtility<T>
    {
        string fileName;
        bool shouldEncrypt;
        string encryptWord;
        string profileID;

        public ProfileFileUtility(string fileName, bool shouldEncrypt, string encryptWord)
        {
            this.encryptWord = encryptWord;
            this.shouldEncrypt = shouldEncrypt;
            this.fileName = fileName;
        }

        public void Save(T data, string profileID)
        {
            this.profileID = profileID;
            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(GetFullPath()));
                File.WriteAllText(GetFullPath(), EncryptDecrypt(JsonUtilityEx.ToJson(data, prettyPrint: true)));
            }
            catch (Exception e)
            {
                Debug.LogWarning($"Failed to write to {GetFullPath()} with exception {e}");
            }
        }

        public T Load(string profileID)
        {
            this.profileID = profileID;
            try
            {
                return JsonUtilityEx.FromJson<T>(EncryptDecrypt(File.ReadAllText(GetFullPath())));
            }
            catch (Exception e)
            {
                Debug.LogWarning($"Failed to read from {GetFullPath()} with exception {e}");
                return default;
            }
        }

        string EncryptDecrypt(string data)
        {
            if (!shouldEncrypt)
                return data;

            string modifiedData = "";

            for (int i = 0; i < data.Length; i++)
            {
                modifiedData += (char)(data[i] ^ encryptWord[i % encryptWord.Length]);
            }

            return modifiedData;
        }

        public Dictionary<string, T> LoadAllProfilesData()
        {
            Dictionary<string, T> profilesDictionary = new();

            IEnumerable<DirectoryInfo> dirInfos = new DirectoryInfo(Application.persistentDataPath).EnumerateDirectories();
            foreach (DirectoryInfo dirInfo in dirInfos)
            {
                string profileID = dirInfo.Name;
                if (!File.Exists(GetFullPath()))
                {
                    Debug.LogWarning("Skipping directory, it does not contain data");
                    continue;
                }

                T profileData = Load(profileID);

                if(profileData != null) 
                {
                    profilesDictionary.Add(profileID, profileData);
                }
                else
                {
                    Debug.LogError($"Failed to load data of {profileID} profile id");
                }
            }

            return profilesDictionary;
        }

        string GetFullPath()
        {
            return $"{Application.persistentDataPath}/{profileID}/{fileName}";
        }
    }  
}