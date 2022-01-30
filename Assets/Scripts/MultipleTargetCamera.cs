using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MultipleTargetCamera : MonoBehaviour
{
    private Vector3 velocity;
    private float zoomVelocity;
    private List<Transform> targets;
    public float smoothTime = .5f;
    public float minZoom = 10f;
    public float maxZoom = 20.5f;

    void LateUpdate()
    {
        targets = GameObject.FindGameObjectsWithTag("Player").Select(obj => obj.transform).ToList();
        Zoom();
        Move();
    }

    void Zoom()
    {
        var targetZoom = maxZoom;
        if (targets.Count > 0)
        {
            var distance = GetGreatestDistance();
            targetZoom = Mathf.Max(Mathf.Min(distance / 35, 1) * maxZoom, minZoom);
        }
        var camera = GetComponent<Camera>();
        camera.orthographicSize = Mathf.SmoothDamp(camera.orthographicSize, targetZoom, ref zoomVelocity, smoothTime);
    }

    void Move()
    {
        Vector3 centerPoint = GetCenterPoint();
        var pos = transform.position;
        pos.x = centerPoint.x;
        pos.y = centerPoint.y;
        transform.position = Vector3.SmoothDamp(transform.position, pos, ref velocity, smoothTime);
    }

    float GetGreatestDistance()
    {
        var bounds = new Bounds();
        foreach (var target in targets)
        {
            bounds.Encapsulate(target.position);
        }
        return Mathf.Max(bounds.size.x, bounds.size.y);
    }

    Vector3 GetCenterPoint()
    {
        if (targets.Count == 0)
        {
            return new Vector3(0, 0, 0);
        }
        if (targets.Count == 1)
        {
            return targets[0].position;
        }
        else
        {
            var bounds = new Bounds();
            foreach (var target in targets)
            {
                bounds.Encapsulate(target.position);
            }
            return bounds.center;
        }
    }
}
