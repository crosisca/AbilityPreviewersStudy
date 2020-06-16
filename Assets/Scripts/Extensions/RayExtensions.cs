using UnityEngine;

public static class RayExtensions
{
    public static Vector3 GetIntersectionPoint (this Ray ray, float intersectionPointHeight = 0)
    {
        float a = Vector3.Dot(ray.direction, Vector3.up);
        float num = -Vector3.Dot(ray.origin, Vector3.up) - intersectionPointHeight;

        if (Mathf.Approximately(a, 0.0f))
            return Vector3.zero;

        float dist = num / a;

        if ((double)dist > 0.0)
            return ray.GetPoint(dist);

        return Vector3.zero;
    }
}