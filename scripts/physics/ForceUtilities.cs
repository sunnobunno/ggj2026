using Godot;
using System;

public static class ForceUtilities
{
    public static Vector3 CalculateImmediateForce(
        Vector3 currentPosition,
        Vector3? _targetPosition,
        Vector3 linearVelocity,
        double delta,
        float moveStr,
        float mass)
    {
        var targetPosition = currentPosition;
        if (_targetPosition != null) targetPosition = (Vector3)_targetPosition;

        var pointA = currentPosition;
        var pointB = targetPosition;

        var targetVelocity = (pointB - pointA) / (float)delta;
        var velocityAdjustment = targetVelocity - linearVelocity;
        var accel = velocityAdjustment / (float)delta;

        var force = mass * accel * (float)delta;

        return force;
    }

    public static Vector3 CalculateSpringForce(
        Vector3 currentPosition,
        Vector3? _targetPosition,
        Vector3 linearVelocity,
        double delta,
        float springStr,
        float dampStr,
        float mass)
    {
        var targetPosition = currentPosition;
        if (_targetPosition != null) targetPosition = (Vector3)_targetPosition;
        
        var pointA = currentPosition;
        var pointB = targetPosition;

        var offset = (pointB - pointA);
        var force = ((offset * springStr) - (linearVelocity * dampStr));

        //var targetVelocity = (pointB - pointA) / (float)delta;
        //var velocityAdjustment = targetVelocity - linearVelocity;
        //var accel = velocityAdjustment / (float)delta;
        //var force = ((accel * springStr) - (linearVelocity * dampStr)) * mass * (float)delta;





        return force;
    }
}
