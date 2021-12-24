using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Scripting;

public class RaycastService : IRaycastService
{
    private Camera _camera;

    private const float _maxRaycastDistance = 1000;

    private UnityEvent<GameObject> onRaycastHit = new UnityEvent<GameObject>();
    private EventSystem _eventSystem;

    [Preserve]
    public RaycastService(Camera camera, EventSystem eventSystem)
    {
        _eventSystem = eventSystem;
        _camera = camera;
    }

    public void Raycast()
    {
        var gameObject = GetRaycastableGameObject();

        if (gameObject != null) onRaycastHit?.Invoke(gameObject);
    }

    public void Raycast(int layerMask)
    {
        var gameObject = GetRaycastableGameObject(layerMask);

        if (gameObject != null) onRaycastHit?.Invoke(gameObject);
    }

    public void AddOnRaycastHitListener(UnityAction<GameObject> action)
    {
        onRaycastHit.AddListener(action);
    }

    public void RemoveOnRaycastHitListener(UnityAction<GameObject> action)
    {
        onRaycastHit.RemoveListener(action);
    }

    private GameObject GetRaycastableGameObject()
    {
        RaycastHit hit;
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

        if (!Physics.Raycast(ray, out hit, _maxRaycastDistance)) return null;

        if (hit.collider.GetComponents<IRaycastable>() == null) return null;

        return hit.collider.gameObject;
    }
    

    private GameObject GetRaycastableGameObject(int layerMask)
    {
        RaycastHit hit;
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

        if (!Physics.Raycast(ray, out hit, _maxRaycastDistance, layerMask)) return null;

        if (hit.collider.GetComponents<IRaycastable>() == null) return null;

        return hit.collider.gameObject;
    }
}