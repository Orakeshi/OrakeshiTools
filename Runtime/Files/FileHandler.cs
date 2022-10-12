using System.Collections.Generic;
using System.IO;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Orakeshi.OrakeshiTools.Files
{
    public class FileHandler
    {
                /// <summary>
        /// Reads all file contents from a given directory path.
        /// </summary>
        /// <param name="path"></param>
        /// <param name="fileExtension"></param>
        /// <returns></returns>
        public async UniTask<List<string>> ReadFiles(string path, string fileExtension = "")
        {
            DirectoryInfo info = new DirectoryInfo(path);
            FileInfo[] fileInfos = info.GetFiles();

            if (fileInfos.Length <= 0) return null;
            List<string> tempList = new();
            
            foreach (FileInfo fileInfo in fileInfos)
            {
                if (!string.IsNullOrEmpty(fileExtension))
                {
                    if (fileInfo.Extension != $".{fileExtension}") continue;
                }
                string fileContents = await File.ReadAllTextAsync($"{path}/{fileInfo.Name}");
                tempList.Add(fileContents);
            }

            return tempList;
        }

        /// <summary>
        /// Read files content.
        /// </summary>
        /// <param name="path"></param>
        /// <param name="fileName"></param>
        /// <param name="fileExtension"></param>
        /// <returns></returns>
        public async UniTask<string> ReadFile(string path, string fileName, string fileExtension = "")
        {
            string newPath = $"{path}/{fileName}.{fileExtension}";
            if (!File.Exists(newPath)) await File.Create(newPath).DisposeAsync();
            
            string fileContents = await File.ReadAllTextAsync(newPath);
            return fileContents;
        }

        /// <summary>
        /// Write data to json file.
        /// </summary>
        /// <param name="path"></param>
        /// <param name="fileName"></param>
        /// <param name="dataToWrite"></param>
        public async UniTask WriteJson(string path, string fileName, object dataToWrite)
        {
            string newData = JsonUtility.ToJson(dataToWrite);
            await File.WriteAllTextAsync($"{path}/{fileName}.json", newData);
        }
    }
}