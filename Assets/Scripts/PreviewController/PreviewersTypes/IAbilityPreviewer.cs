using UnityEngine;

public interface IAbilityPreviewer
{
    Vector3 TargetPosition { get; }
    Quaternion TargetRotation { get; }

    void Initialize();

    void CalculateTargetLocation();
    void CalculateTargetRotation();

    void SetPosition ();
    void SetRotation ();
    void SetScale();
}