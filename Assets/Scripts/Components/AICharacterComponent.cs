using UnityEngine;

public struct AICharacterComponent
{
    public CharacterState State;
    public Vector3 Target;
}

public enum CharacterState
{
    Idle,
    Moving
}