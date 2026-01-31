using Godot;
using System;

public partial class ItemHover : Node3D
{
    [Export] float scale = 1f;
    [Export] float speed = 1f;
    
    float t = 0;
    
    public override void _Process(double delta)
    {
        var Yoffset = Mathf.Sin(t) * scale;
        t += (float)delta * speed;

        Position = new Vector3(0f, Yoffset, 0f);
    }
}
