using UnityEngine;
using InputContext = UnityEngine.InputSystem.InputAction.CallbackContext;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerMovement MovementBehaviour = null;

    private bool LSpressed = false;
    private Vector2 moveDirection = Vector2.zero;
    private Vector2 turnDirection = Vector2.zero;

    private void Update()
    {
        UpdateMoveDirection();
        UpdateTurnDirection();
    }

    public void OnMoveDirection(InputContext action)
    {
        moveDirection = action.ReadValue<Vector2>();
    }

    public void OnTurnDirection(InputContext action)
    {
        turnDirection = action.ReadValue<Vector2>();
    }

    public void OnPressLeftShift(InputContext action)
    {
        if (action.started)
            LSpressed = true;
        else if (action.canceled)
            LSpressed = false;

        MovementBehaviour.UpdateBoostState(LSpressed);
    }

    private void UpdateMoveDirection()
    {
        MovementBehaviour.UpdateMoveDirection(moveDirection);
    }

    private void UpdateTurnDirection()
    {
        MovementBehaviour.UpdateTurnDirection(turnDirection);
    }
}
