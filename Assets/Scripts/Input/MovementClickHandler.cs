using UnityEngine;

public class MovementClickHandler
{
    private const int _leftMouseButtonKey = 0;
    
    public bool IsMoveClickPressed => Input.GetMouseButtonDown(_leftMouseButtonKey);
}
