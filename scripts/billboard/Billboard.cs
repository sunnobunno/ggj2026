using Godot;
using PlayerControllers.FirstPerson;
using System;


public partial class Billboard : Node3D
{
    [Export] bool pitchTowardPlayer = false;
    
    private Node3D playerPOV;

    public override void _Ready()
    {
        playerPOV = FirstPersonController.Instance as Node3D;
    }

    public override void _Process(double delta)
    {
        var playerPosition = playerPOV.Position;
        if (!pitchTowardPlayer)
        {
            playerPosition.Y = GlobalPosition.Y;
        }
        
        LookAt(playerPosition, null, true);

        //var direction = GetDirectionToPlayer();
        //TurnTowardDirection(direction);
    }

    private Vector3 GetDirectionToPlayer()
    {
        var objectPosition = GlobalPosition;
        var playerPosition = playerPOV.GlobalPosition;
        var directionToPlayer = objectPosition.DirectionTo(playerPosition);
        var angleToPlayer = objectPosition.SignedAngleTo(playerPosition, Vector3.Up);

        if (!pitchTowardPlayer)
        {
            directionToPlayer = new Vector3(directionToPlayer.X, 0f, directionToPlayer.Z);
        }

        

        DU.Log(directionToPlayer);
        return directionToPlayer;
    }

    private void TurnTowardDirection(Vector3 direction)
    {
        var Yangle = Vector3.Forward.SignedAngleTo(direction, Vector3.Up);
        var Xangle = Vector3.Forward.SignedAngleTo(direction, Vector3.Right);

        var newDirection = new Vector3(Xangle, Yangle, 0f);
        DU.Log(direction);
        DU.Log(newDirection);

        GlobalRotation = newDirection;
    }
}
