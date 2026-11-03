using _Project.Scripts.Infrastructure.AssetManagement;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Services
{
    public class EffectsService
    {
        private readonly IInstantiator _container;
        private readonly IAssetProvider _assetProvider;

        public EffectsService(IInstantiator container, IAssetProvider assetProvider)
        {
            _assetProvider = assetProvider;
            _container = container;
        }

        public UniTask WarmUpAsync()
        {
            //await _assetProvider.LoadAsync<GameObject>(AssetAddress.HitFx);
            return UniTask.CompletedTask;
        }

        /*public async UniTask PlayHitFxAsync(Vector3 position, Transform parent)
        {
            Quaternion rotation = Quaternion.Euler(0, 90, 0);
            
            GameObject fxPrefab = await _assetProvider.LoadAsync<GameObject>(AssetAddress.HitFx);
            GameObject fxGameObject = _container.InstantiatePrefab(fxPrefab, position , rotation, parent);
            fxGameObject.transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);
        }*/
    }
}