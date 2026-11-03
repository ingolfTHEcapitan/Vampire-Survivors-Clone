using _Project.Scripts.Services.SaveLoad.LocalSave;
using UnityEngine;
namespace _Project.Scripts.Configs
{
    [CreateAssetMenu(fileName = "LocalSaveTypeConfig", menuName = "Project configs/LocalSave", order = 0)]
    public class LocalSaveTypeConfig: ScriptableObject
    {
        [field: SerializeField] public LocalSaveType SaveType { get; private set; } = LocalSaveType.PlayerPrefs;
    }
}