using System;
using Cysharp.Threading.Tasks;
using Zenject;

namespace _Project.Scripts.Infrastructure.Project
{
    public class ProjectBootstrapper : IInitializable
    {
        public ProjectBootstrapper()
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
                
            }
        }
        
    }
}