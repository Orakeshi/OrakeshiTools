using System.IO;
using UnityEngine.Networking;

namespace Orakeshi.OrakeshiTools.Data
{
    /// <summary>
    /// Data operations.
    /// </summary>
    public class DataHelper
    {
        /// <summary>
        /// Handles transferring data of a file between two locations
        /// </summary>
        /// <param name="dataToTransferPath">Path to the files data to transfer</param>
        /// <param name="destinationDataPath">Path to the destination file</param>
        /// <returns></returns>
        public bool TransferDataToPath(string dataToTransferPath, string destinationDataPath)
        {
            // If audio is not in file location => Download it from streaming assets
            UnityWebRequest loadingRequest = UnityWebRequest.Get(dataToTransferPath);
            loadingRequest.SendWebRequest();
        
            while (!loadingRequest.isDone) {
                if (loadingRequest.isNetworkError || loadingRequest.isHttpError) {
                    break;
                }
            }
            if (loadingRequest.isNetworkError || loadingRequest.isHttpError) {
 
            } else {
                File.WriteAllBytes(destinationDataPath, loadingRequest.downloadHandler.data);
            }

            return true;
        }
    }
}