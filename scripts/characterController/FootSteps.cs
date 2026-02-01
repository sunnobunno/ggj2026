using Godot;
using InputControllers.firstPerson;
using System;

public partial class FootSteps : Node3D
{
    [Export] AudioStreamPlayer3D player;
    
    bool isWalking = false;
    bool latch = false;
    bool latch2 = false;

    public override void _Ready()
    {
        
    }

    public override void _Process(double delta)
    {
        CheckIfWalking();
    }

    private void CheckIfWalking()
    {
        var localDirection = PlayerInputProcessor.LocalDirection;
        if (localDirection != Vector3.Zero)
        {
            isWalking = true;
            PlayFootsteps();

        }
        else
        {
            isWalking = false;
            StopFootsteps();
        }
    }

    private void PlayFootsteps()
    {
        if (latch) return;
        player.Play();
        latch = true;
        latch2 = false;
    }

    private void StopFootsteps()
    {
        if (latch2) return;
        player.Stop();
        latch2 = true;
        latch = false;
    }
}
