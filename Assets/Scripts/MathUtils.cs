using UnityEngine;

public class MathUtils : MonoBehaviour
{
    public static bool IsInsideCircle (Vector3 centerOfCircle, float radius, Vector3 positionToCheck)
    {
        return (centerOfCircle - positionToCheck).sqrMagnitude <= radius * radius;
    }
}
