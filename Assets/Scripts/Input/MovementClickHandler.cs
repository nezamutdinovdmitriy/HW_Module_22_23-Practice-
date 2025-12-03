using UnityEngine;

public class MovementClickHandler
{
    private const int _leftMouseButtonKey = 0;
    
    public bool IsMoveClickPressed { get; private set; }

    public bool CheckForMovementClick() => IsMoveClickPressed = Input.GetMouseButtonDown(_leftMouseButtonKey);
}
