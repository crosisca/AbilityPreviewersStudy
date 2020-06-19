using System;
using UnityEngine;

public class AimToMousePositioner : PreviewPositioner
{
    [SerializeField, AbilityDatabaseValue]
    string maxDistanceVar;

    public override void CalculateTargetLocation()
    {
        float maxRange = previewConfig.GetValue<float>(maxDistanceVar);

        if (!MathUtils.IsInsideCircle(Origin, maxRange, previewer.MouseHitPosition))
            Target.position = Origin + (previewer.MouseHitPosition - Origin).normalized * maxRange;
        else
            Target.position = previewer.MouseHitPosition;
    }

    public override void CalculateTargetRotation ()
    {
        if (Mathf.Approximately((Target.position - Origin).magnitude, 0))
            Target.rotation = Quaternion.identity;
        else
            Target.rotation = Quaternion.LookRotation(Target.position.FlattenY() - Origin.FlattenY(), Vector3.up);
    }

    public override void SetPosition ()
    {
        PositionerTransform.position = Origin;
    }

    public override void SetRotation ()
    {
        PositionerTransform.rotation = Target.rotation;
    }
}