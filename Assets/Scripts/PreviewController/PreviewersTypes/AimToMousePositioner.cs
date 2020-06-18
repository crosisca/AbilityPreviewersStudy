﻿using System;
using UnityEngine;

public class AimToMousePositioner : PreviewPositioner
{
    [SerializeField]
    string maxDistanceVar;

    public override void CalculateTargetLocation()
    {
        float maxRange = (float)previewConfig.Variables[maxDistanceVar];

        if (!MathUtils.IsInsideCircle(Origin, maxRange, previewer.MouseHitPosition))
            target.position = Origin + (previewer.MouseHitPosition - Origin).normalized * maxRange;
        else
            target.position = previewer.MouseHitPosition;
    }

    public override void CalculateTargetRotation ()
    {
        if (Mathf.Approximately((target.position - Origin).magnitude, 0))
            target.rotation = Quaternion.identity;
        else
            target.rotation = Quaternion.LookRotation(target.position.FlattenY() - Origin.FlattenY(), Vector3.up);
    }

    public override void SetPosition ()
    {
        PositionerTransform.position = Origin;
    }

    public override void SetRotation ()
    {
        PositionerTransform.rotation = target.rotation;
    }
}