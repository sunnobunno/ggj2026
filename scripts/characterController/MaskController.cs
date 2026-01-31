using Godot;
using System;

public partial class MaskController : MeshInstance3D
{
    [Export] Texture2D gasMaskOverlay;
    [Export] Texture2D birdMaskOverlay;
    [Export] Texture2D mustacheGlassesOverlay;

    Inventory.EquippedMask currentMask = Inventory.EquippedMask.None;
    Texture2D maskTexture;

    StandardMaterial3D maskMaterial;


    public override void _Ready()
    {
        maskMaterial = (StandardMaterial3D)GetSurfaceOverrideMaterial(0);
    }

    public override void _Process(double delta)
    {
        currentMask = Inventory.Instance.Mask;
        //DU.Log(currentMask);
        SwitchMaskOverlay();
    }

    private void SwitchMaskOverlay()
    {
        switch(currentMask)
        {
            case Inventory.EquippedMask.None:
                maskMaterial.AlbedoTexture = null;
                maskMaterial.AlbedoColor = new Color(1f, 1f, 1f, 0f);
                break;
            case Inventory.EquippedMask.GasMask:
                maskMaterial.AlbedoTexture = gasMaskOverlay;
                maskMaterial.AlbedoColor = new Color(1f, 1f, 1f, 1f);
                break;
            case Inventory.EquippedMask.MustacheGlasses:
                maskMaterial.AlbedoTexture = mustacheGlassesOverlay;
                maskMaterial.AlbedoColor = new Color(1f, 1f, 1f, 1f);
                break;
        }
    }
}
