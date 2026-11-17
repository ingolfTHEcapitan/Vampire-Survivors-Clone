using UnityEngine;

namespace _Project.Scripts.Services.Input
{
    public class DesktopInputService : IInputService
    {
        public Vector2 GetMovementAxisRaw() => 
            new Vector2(SimpleInput.GetAxisRaw("Horizontal"), SimpleInput.GetAxisRaw("Vertical"));
    }
}