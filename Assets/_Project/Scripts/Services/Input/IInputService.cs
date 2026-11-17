using UnityEngine;

namespace _Project.Scripts.Services.Input
{
    public interface IInputService
    {
        Vector2 GetMovementAxisRaw();
    }
}