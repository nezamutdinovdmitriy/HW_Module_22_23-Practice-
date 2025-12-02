using UnityEngine;

public class MousePositionReader
{
    private Camera _camera;

    public MousePositionReader(Camera camera) => _camera = camera;

    public Ray Ray => _camera.ScreenPointToRay(Input.mousePosition);
}
