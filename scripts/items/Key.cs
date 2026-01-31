using Clickables;
using Godot;
using System;

public partial class Key : Node3D, IClickable
{
    public void LeftClick(Vector3? position)
    {
        DU.Log("Clicked");
    }

    public void LeftRelease(Vector3? position)
    {
        
    }

    public void RightClick(Vector3? position)
    {
        
    }

    public void RightRelease(Vector3? position)
    {
        
    }
}
