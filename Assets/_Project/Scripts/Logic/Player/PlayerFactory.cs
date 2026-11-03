using System;
using System.Data;
using _Project.Scripts.Infrastructure.AssetManagement;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Logic.Player
{
    public class PlayerFactory
    {
        private readonly IInstantiator _container;
        private readonly IAssetProvider _assetProvider;
        private readonly Transform _gameParent;
        private PlayerContainer _player;

        public PlayerFactory(IInstantiator container,Transform gameParent, IAssetProvider assetProvider)
        {
            _container = container;
            _gameParent = gameParent;
            _assetProvider = assetProvider;
        }

        public async UniTask<PlayerContainer> CreatePlayerAsync(Vector3 at)
        {
            GameObject prefab = await _assetProvider.LoadAsync<GameObject>(AssetAddress.Player);
            _player = _container.InstantiatePrefabForComponent<PlayerContainer>(prefab, at, Quaternion.identity, _gameParent);
            
            float maxHealth = 100f;
            _player.Health.Initialize(maxHealth);
            
            _player.PlayerDeath.Initialize();
            _player.Visuals.Initialize();
            
            return _player;
        }

        public PlayerContainer GetPlayer()
        {
            if (_player)
                return _player;
            
            throw new InvalidConstraintException
                ($"{_player.gameObject.name} requested before creation. Ensure it created before resolution");
        }
    }
}