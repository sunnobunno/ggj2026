using Godot;
using System;

public partial class ItemHover : MeshInstance3D
{
    [Export] float scale = 1f;
    [Export] float speed = 1f;
    [Export] Texture2D[] frames;
    
    float t = 0;
    float p = 0;
    
    public override void _Process(double delta)
    {
        var Yoffset = Mathf.Sin(t) * scale;
        t += (float)delta * speed;

        Position = new Vector3(0f, Yoffset, 0f);

        AnimateKey(delta);
    }

    private void AnimateKey(double delta)
    {
        t += (float)delta * 5f;

        var frameIndex = (int)t % 2;
        var frame = frames[frameIndex];

        var material = (ShaderMaterial)GetSurfaceOverrideMaterial(0);
        material.SetShaderParameter("color", frame);
    }
}
