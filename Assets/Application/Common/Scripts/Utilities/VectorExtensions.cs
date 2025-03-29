using UnityEngine;

public static class VectorExtensions
{
    // Vector2 extensions
    public static Vector2 SetX(this Vector2 v, float x)
    {
        v.x = x;
        return v;
    }

    public static Vector2 SetY(this Vector2 v, float y)
    {
        v.y = y;
        return v;
    }

    public static Vector2 WithX(this Vector2 v, float x)
    {
        return new Vector2(x, v.y);
    }

    public static Vector2 WithY(this Vector2 v, float y)
    {
        return new Vector2(v.x, y);
    }

    // Vector2Int extensions
    public static Vector2Int SetX(this Vector2Int v, int x)
    {
        v.x = x;
        return v;
    }

    public static Vector2Int SetY(this Vector2Int v, int y)
    {
        v.y = y;
        return v;
    }

    public static Vector2Int WithX(this Vector2Int v, int x)
    {
        return new Vector2Int(x, v.y);
    }

    public static Vector2Int WithY(this Vector2Int v, int y)
    {
        return new Vector2Int(v.x, y);
    }

    // Vector3 extensions
    public static Vector3 SetX(this Vector3 v, float x)
    {
        v.x = x;
        return v;
    }

    public static Vector3 SetY(this Vector3 v, float y)
    {
        v.y = y;
        return v;
    }

    public static Vector3 SetZ(this Vector3 v, float z)
    {
        v.z = z;
        return v;
    }

    public static Vector3 WithX(this Vector3 v, float x)
    {
        return new Vector3(x, v.y, v.z);
    }

    public static Vector3 WithY(this Vector3 v, float y)
    {
        return new Vector3(v.x, y, v.z);
    }

    public static Vector3 WithZ(this Vector3 v, float z)
    {
        return new Vector3(v.x, v.y, z);
    }

    // Vector4 extensions
    public static Vector4 SetX(this Vector4 v, float x)
    {
        v.x = x;
        return v;
    }

    public static Vector4 SetY(this Vector4 v, float y)
    {
        v.y = y;
        return v;
    }

    public static Vector4 SetZ(this Vector4 v, float z)
    {
        v.z = z;
        return v;
    }

    public static Vector4 SetW(this Vector4 v, float w)
    {
        v.w = w;
        return v;
    }

    public static Vector4 WithX(this Vector4 v, float x)
    {
        return new Vector4(x, v.y, v.z, v.w);
    }

    public static Vector4 WithY(this Vector4 v, float y)
    {
        return new Vector4(v.x, y, v.z, v.w);
    }

    public static Vector4 WithZ(this Vector4 v, float z)
    {
        return new Vector4(v.x, v.y, z, v.w);
    }

    public static Vector4 WithW(this Vector4 v, float w)
    {
        return new Vector4(v.x, v.y, v.z, w);
    }
}
