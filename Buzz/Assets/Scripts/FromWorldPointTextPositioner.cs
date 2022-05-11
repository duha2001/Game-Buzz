using UnityEngine;
using System.Collections;

public class FromWorldPointTextPositioner : iFloatingTextPositioner
{
    private readonly Camera _camera;
    private readonly Vector3 _worldPosition;
    private readonly float _speed;
    private float _timeToveLive;
    private float _yOffset;

    public FromWorldPointTextPositioner(Camera camera, Vector3 worldPosition, float timeToveLive, float speed)
    {
        _camera = camera;
        _worldPosition = worldPosition;
        _timeToveLive = timeToveLive;
        _speed = speed;
    }

    public bool GetPosition(ref Vector2 position, GUIContent content, Vector2 size)
    {
        if ((_timeToveLive -= Time.deltaTime) <= 0)
            return false;

        var screenPosition = _camera.WorldToScreenPoint(_worldPosition);
        position.x = screenPosition.x - (size.x / 2);
        position.y = Screen.height - screenPosition.y - _yOffset;

        _yOffset += Time.deltaTime * _speed;
        return true;
    }
}
