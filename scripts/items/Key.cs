using Clickables;
using Godot;
using System;

public partial class Key : Node3D, IClickable
{
	[Export] AudioStreamPlayer3D player;
	
	bool isActive = true;
	public bool IsActive { get => isActive; set => isActive = value; }
	
	public void LeftClick(Vector3? position)
	{
		DU.Log("Clicked");
		PickUpKey();
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

	private void PickUpKey()
	{
		Inventory.Instance.HasKey = true;
		player.Play();
		QueueFree();
	}
}
