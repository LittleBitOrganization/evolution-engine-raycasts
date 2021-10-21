using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastService : IRaycastService
{
    private Camera _camera;

    private const float _maxRaycastDistance = 500;

    public RaycastService(Camera camera)
    {
        _camera = camera;
    }

    public void Raycast()
    {
        var raycastable = GetRaycastable();

        if (raycastable != null) raycastable.OnRaycastHit();
    }

    public void Raycast(int layerMask)
    {
        var raycastable = GetRaycastable(layerMask);

        if (raycastable != null) raycastable.OnRaycastHit();
    }
    private IRaycastable GetRaycastable()
    {
        RaycastHit hit;
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

        if (!Physics.Raycast(ray, out hit, _maxRaycastDistance)) return null;

        return hit.collider.GetComponent<IRaycastable>();
    }
    private IRaycastable GetRaycastable(int layerMask)
    {
        RaycastHit hit;
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

        if (!Physics.Raycast(ray, out hit, _maxRaycastDistance, layerMask)) return null;

        return hit.collider.GetComponent<IRaycastable>();
    }
}