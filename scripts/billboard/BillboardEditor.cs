using Godot;
using System;
using static Godot.RenderingServer;


public partial class BillboardEditor : Node3D
{
    private Texture2D _texture;

    [Export] MeshInstance3D meshInstance;
    [Export] Shader shaderCode;
    [Export]
    Texture2D texture
    {
        get => _texture;
        set
        {
            _texture = value;
            UpdateTexture(value);
        }
    }

    public override void _Ready()
    {
        if (texture != null) UpdateTexture(texture);
    }

    private void UpdateTexture(Texture2D texture)
    {
        if (texture == null) return;
        DU.Log("Texture changed");

        var shaderMaterial = new ShaderMaterial();
        shaderMaterial.Shader = shaderCode;
        shaderMaterial.SetShaderParameter("color", texture);

        meshInstance.SetSurfaceOverrideMaterial(0, shaderMaterial);
    }
}
