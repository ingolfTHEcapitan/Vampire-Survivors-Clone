using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Infrastructure.Game
{
    public class GameBootstrapper : IInitializable
    {
        public GameBootstrapper()
        {
            
        }

        public void Initialize() => 
            InitializeAsync().Forget();

        private async UniTaskVoid InitializeAsync()
        {
            try
            {
               
            }
            catch (Exception e)
            {
                Debug.LogError($"[GAME BOOTSTRAPPER] init error: {e}");
            }
        }
    }
}