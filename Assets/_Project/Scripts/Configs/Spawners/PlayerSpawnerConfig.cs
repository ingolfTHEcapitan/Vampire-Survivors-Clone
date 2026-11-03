using UnityEngine;

namespace _Project.Scripts.Configs.Spawners
{
    [CreateAssetMenu(fileName = "PlayerSpawnerConfig", menuName = "Project configs/PlayerSpawner", order = 0)]
    public class PlayerSpawnerConfig: ScriptableObject
    {
        [field: SerializeField] public Vector3 SpawnPosition { get; private set; }
    }
}