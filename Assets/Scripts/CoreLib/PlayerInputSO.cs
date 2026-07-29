using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace CoreLib
{
    [CreateAssetMenu(fileName = "PlayerInput", menuName = "SO/PlayerInput")]
    public class PlayerInputSO : ScriptableObject, Controls.IPlayerActions
    {
        private Controls _controls;

        public Vector2 MoveInput { get; private set; }
        public Vector2 AimInput { get; private set; }
        public bool AimRangeInput { get; private set; }

        private void OnEnable()
        {
            if (_controls == null)
            {
                _controls = new Controls();
                _controls.Player.SetCallbacks(this);
            }

            _controls.Enable();
        }

        private void OnDisable()
        {
            if (_controls != null)
                _controls.Disable();
        }
        
        public void OnMove(InputAction.CallbackContext context)
        {
            MoveInput = context.ReadValue<Vector2>();
        }

        public void OnAim(InputAction.CallbackContext context)
        {
            AimInput = context.ReadValue<Vector2>();
        }

        public void OnAimRange(InputAction.CallbackContext context)
        {
            if (context.performed)
                AimRangeInput = true;
            else
                AimRangeInput = false;
        }
    }
}