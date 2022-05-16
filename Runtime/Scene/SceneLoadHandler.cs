using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace Orakeshi.OrakeshiTools.Scene
{
    /// <summary>
    /// Handles loading scene asynchronously
    /// </summary>
    public class SceneLoadHandler
    {
        /// <summary>
        /// Loads a scene with a given string name.
        /// </summary>
        /// <param name="sceneToLoad"></param>
        /// <returns></returns>
        public async UniTask<bool> LoadScene(string sceneToLoad)
        {
            await SceneManager.LoadSceneAsync(sceneToLoad);
            
            return true;
        }
    }
}