using UnityEngine;
using UnityEngine.InputSystem;

namespace PlayerControl
{
    /// <summary>
    /// <see cref="InputAction.CallbackContext"/> wrapper for player control actions.
    /// </summary>
    public readonly ref struct CallbackContext
    {
        /// <summary>
        /// <see cref="InputAction.CallbackContext.action"/> name that triggered the callback.
        /// </summary>
        public readonly string ActionName;

        /// <summary>
        /// <see cref="InputAction.CallbackContext.phase"/> of the action when the callback was triggered.
        /// </summary>
        public readonly InputActionPhase Phase;

        /// <summary>
        /// <see cref="InputAction.CallbackContext.ReadValue{Vector2}()"/> value when the callback was triggered.
        /// </summary>
        public readonly Vector2 Value;

        public CallbackContext(in string actionName, in InputActionPhase phase, in Vector2 value = default)
        {
            ActionName = actionName;
            Phase = phase;
            Value = value;
        }

        public static implicit operator CallbackContext(InputAction.CallbackContext context) =>
            new(context.action.name, context.phase, context.valueType == typeof(Vector2) ? context.ReadValue<Vector2>() : default);
    }
}