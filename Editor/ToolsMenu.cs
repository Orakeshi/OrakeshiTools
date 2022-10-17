using UnityEditor;
using static System.IO.Directory;
using static System.IO.Path;
using static UnityEditor.AssetDatabase;
using static UnityEngine.Application;

namespace Orakeshi.OrakeshiTools.Editor
{
    public static class ToolsMenu
    {
        [MenuItem("Tools/Setup/Create Default Folders")]
        public static void CreateDefaultFolders()
        {
            CreateDirectories("_SFS", "Scripts", "Scenes", "Prefabs", "Materials", "Sprites", "Textures", "Animations", "Models");
            Refresh();
        }

        public static void CreateDirectories(string root, params string[] directories)
        {
            string path = Combine(dataPath, root);

            foreach (string directory in directories)
            {
                CreateDirectory(Combine(path, directory));
            }
        }
    }
}