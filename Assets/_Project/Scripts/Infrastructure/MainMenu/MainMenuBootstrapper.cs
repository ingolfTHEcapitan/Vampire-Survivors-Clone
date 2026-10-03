using System;

using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Infrastructure.MainMenu
{
    public class MainMenuBootstrapper: IInitializable
    {
        public MainMenuBootstrapper()
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
                Debug.LogError($"[MAIN MENU BOOTSTRAPPER] init error: {e}");
            }
        }
    }
}