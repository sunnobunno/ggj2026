using Clickables;
using Godot;
using System;
using System.Collections.Generic;

public partial class MaskItem : Area3D, IClickable
{
    [Export] Inventory.EquippedMask maskType;

    static List<MaskItem> allMasks;

    bool isActive = true;
    public bool IsActive { get => isActive; set => isActive = value; }

    public Inventory.EquippedMask MaskType { get => maskType; }

    public override void _Ready()
    {
        if (allMasks == null)
        {
            allMasks = new List<MaskItem>();
        }
        allMasks.Add(this);
    }

    private void PickUpMask()
    {
        Inventory.Instance.Mask = maskType;
        ToggleMaskVisibility(maskType);
    }

    private void ToggleMaskVisibility(Inventory.EquippedMask currentmask)
    {
        Visible = false;
        Monitoring = false;
        Monitorable = false;
        IsActive = false;

        foreach (var mask in allMasks)
        {
            DU.Log(mask.MaskType);
            
            if (mask.MaskType != currentmask)
            {
                mask.Visible = true;
                Monitoring = true;
                Monitorable = true;
                IsActive = true;
            }
        }
    }

    public void LeftClick(Vector3? position)
    {
        DU.Log("Clicked");
        PickUpMask();
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
