using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class GeneralAnimationControl : BaseAnimationControl
{
    PlayerMovementTutorial movement;

    protected override void Start()
    {
        base.Start();
        movement = GetComponent<PlayerMovementTutorial>();
    }

    protected override Vector3 GetVelocity()
    {
        return movement.MovementInputs;
    }
}
