using _Project.Scripts.Services.GamePause;
using _Project.Scripts.Services.PlayerInput;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Logic.Player.Components
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private CharacterController _characterController;
        [Header("Settings")]
        [SerializeField] private float _speed = 5f;

        private Vector3 _movementDirection;
        private IGamePauseService _pauseService;
        private IInputService _inputService;
        
        [Inject]
        private void Construct(IGamePauseService pauseService, IInputService inputService)
        {
            _pauseService = pauseService;
            _inputService = inputService;
        }

        private void Start() => 
            _pauseService.OnPausableUpdate += PausableUpdate;

        private void OnDestroy()
        {
            _pauseService.OnPausableUpdate -= PausableUpdate;
        }

        private void PausableUpdate()
        {
            Vector2 movementInput = _inputService.GetMovementAxis();
            Move(movementInput);
        }

        private void Move(Vector2 axis)
        {
            _movementDirection = new Vector3(axis.x * _speed, _movementDirection.y, axis.y * _speed);
            _movementDirection = transform.TransformDirection(_movementDirection);
            
            _characterController.Move(_movementDirection * Time.deltaTime);
        }
    }
}