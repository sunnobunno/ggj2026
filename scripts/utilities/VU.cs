using Godot;
using System;

public static class VU
{
    public static Vector3 ProjectVectorToNormalPlane(Vector3 vector, Vector3 planeNormal, bool debug = false)
    {
        var right = vector.Cross(planeNormal).Normalized();
        var forward = planeNormal.Cross(right).Normalized();
        var projectedVector = vector.Dot(forward) * forward;

        if (debug)
        {
            Color green = new Color(0f, 1f, 0f);
            Color red = new Color(1f, 0f, 0f);
            Color blue = new Color(0f, 0f, 1f);
            Color yellow = new Color(1f, 1f, 0f);
            Color cyan = new Color(0f, 1f, 1f);
        }

        

        return projectedVector;
    }
}
