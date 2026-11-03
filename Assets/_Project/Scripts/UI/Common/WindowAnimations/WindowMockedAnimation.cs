using Cysharp.Threading.Tasks;

namespace _Project.Scripts.UI.Common.WindowAnimations
{
    public class WindowMockedAnimation: WindowAnimationBase
    {
        public override UniTask AnimateOpenAsync() => 
            UniTask.CompletedTask;

        public override UniTask AnimateCloseAsync() => 
            UniTask.CompletedTask;
    }
}