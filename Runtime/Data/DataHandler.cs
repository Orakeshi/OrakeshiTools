using System;
using System.IO;
using Cysharp.Threading.Tasks;
using UnityEngine.Networking;

namespace Orakeshi.OrakeshiTools.Data
{
    /// <summary>
    /// Data operations.
    /// </summary>
    public class DataHandler
    {
        /// <summary>
        /// Handles transferring data of a file between two locations
        /// </summary>
        /// <param name="dataToTransferPath">Path to the files data to transfer</param>
        /// <param name="destinationDataPath">Path to the destination file</param>
        /// <returns></returns>
        public async UniTask<bool> TransferDataToPath(string dataToTransferPath, string destinationDataPath)
        {
            // If audio is not in file location => Download it from streaming assets
            UnityWebRequest loadingRequest = UnityWebRequest.Get(dataToTransferPath);
            await loadingRequest.SendWebRequest();

            switch (loadingRequest.result)
            {
                case UnityWebRequest.Result.InProgress:
                    break;
                case UnityWebRequest.Result.Success:
                    await File.WriteAllBytesAsync(destinationDataPath, loadingRequest.downloadHandler.data);
                    break;
                case UnityWebRequest.Result.ConnectionError:
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    break;
                case UnityWebRequest.Result.DataProcessingError:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            return true;
        }
    }
}