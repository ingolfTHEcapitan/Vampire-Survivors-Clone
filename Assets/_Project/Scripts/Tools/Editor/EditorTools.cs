using System;
using System.IO;
using _Project.Scripts.Services.SaveLoad.LocalSave;
using Cysharp.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace _Project.Scripts.Tools.Editor
{
    public class EditorTools
    {
        
        [MenuItem("Tools/Project/Clear file save")]
        public static void ClearLocalFileSave()
        {
            string saveDirectoryPath = Path.Combine(Application.persistentDataPath, FileSaveService.FolderName);

            if (Directory.Exists(saveDirectoryPath))
            {
                Directory.Delete(saveDirectoryPath, recursive: true);
                Debug.Log($"[EDITOR TOOLS] Progress was deleted from file\nPath: {saveDirectoryPath}");
            }
            else
                Debug.Log("[EDITOR TOOLS] Nothing to delete — folder not exist");
        }

        [MenuItem("Tools/Project/Clear playerPrefs save")]
        public static void ClearLocalPlayerPrefsSave()
        {
           PlayerPrefs.DeleteAll();
           Debug.Log("[EDITOR TOOLS] Progress was deleted from playerPrefs");
        }
    }
}