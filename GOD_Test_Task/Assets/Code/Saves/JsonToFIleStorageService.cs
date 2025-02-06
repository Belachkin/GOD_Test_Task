using System;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

namespace Code.Saves
{
    public class JsonToFIleStorageService : IStorageService
    {
        public void Save(string key, object data, Action<bool> callback = null)
        {
            string path = BuildPath(key);
            string jsonString = JsonConvert.SerializeObject(data);
            
            if (!File.Exists(path))
            {        
                using (File.Create(path)) { }
            }
            
            using (var fileSteam = new StreamWriter(path))
            {
                fileSteam.Write(jsonString);
            }
            
            callback?.Invoke(true);
        }

        public void Load<T>(string key, Action<T> callback)
        {
            string path = BuildPath(key);
            
            if (!File.Exists(path))
            {        
                using (File.Create(path)) { }
            }
            
            using (var fileSteam = new StreamReader(path))
            {
                var jsonString = fileSteam.ReadToEnd();
                var data = JsonConvert.DeserializeObject<T>(jsonString);
                
                callback?.Invoke(data);
            }
        }

        private string BuildPath(string key)
        {
            return Path.Combine(Application.persistentDataPath, key);
        }
    }
}