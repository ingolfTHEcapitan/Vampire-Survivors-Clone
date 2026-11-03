using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.Scripts.UI.Common.WindowAnimations
{
    public abstract class WindowAnimationBase : MonoBehaviour
    {
        public abstract UniTask AnimateOpenAsync();
        public abstract UniTask AnimateCloseAsync();
    }
}