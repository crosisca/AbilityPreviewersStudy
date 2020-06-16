using UnityEngine;

public static class VectorExtensions
{
    /// <summary>
    /// return v3.x, v3.z
    /// </summary>
    public static Vector2 ToXZLine (this Vector3 vector2)
    {
        return new Vector2(vector2.x, vector2.z);
    }

    /// <summary>
    /// return v2.x, 0, v2.y
    /// </summary>
    public static Vector3 ToXZPlane (this Vector2 vector2)
    {
        return new Vector3(vector2.x, 0, vector2.y);
    }

    public static Vector3 FlattenY (this Vector3 vector3)
    {
        return new Vector3(vector3.x, 0, vector3.z);
    }

    public static Vector2 FlipScaleX (this Vector2 vector2)
    {
        return new Vector2(vector2.x * -1, vector2.y);
    }

    public static Vector2 FlipScaleY (this Vector2 vector2)
    {
        return new Vector2(vector2.x, vector2.y * -1);
    }

    public static Vector3 FlipScaleX (this Vector3 vector3)
    {
        return new Vector3(vector3.x * -1, vector3.y, vector3.z);
    }

    public static Vector3 FlipScaleY (this Vector3 vector3)
    {
        return new Vector3(vector3.x, vector3.y * -1, vector3.z);
    }

    public static Vector3 FlipScaleZ (this Vector3 vector3)
    {
        return new Vector3(vector3.x, vector3.y, vector3.z * -1);
    }

    public static Vector3 Absolute (this Vector3 vector3)
    {
        return new Vector3(Mathf.Abs(vector3.x), Mathf.Abs(vector3.y), Mathf.Abs(vector3.z));
    }

    public static Vector2 Rotate (this Vector2 v, float degrees)
    {
        float sin = Mathf.Sin(degrees * Mathf.Deg2Rad);
        float cos = Mathf.Cos(degrees * Mathf.Deg2Rad);

        float tx = v.x;
        float ty = v.y;
        v.x = (cos * tx) - (sin * ty);
        v.y = (sin * tx) + (cos * ty);
        return v;
    }
}