using Godot;
using System;

public partial class NPCImage : MeshInstance3D
{
    [Export] float speed = 3.5f;

    [Export] Texture2D[] frames;

    float t = 0;

    public override void _Process(double delta)
    {
        AnimateKey(delta);
    }

    private void AnimateKey(double delta)
    {
        var n = frames.Length;
        t += (float)delta * speed;

        var frameIndex = (int)t % n;
        var frame = frames[frameIndex];

        var material = (ShaderMaterial)GetSurfaceOverrideMaterial(0);
        material.SetShaderParameter("color", frame);
    }
}
