using System;
using Sirenix.OdinInspector;
using UnityEngine;

public class FollowMousePositioner : PreviewPositioner
{
    [SerializeField]
    bool canRotate = true;

    [SerializeField]
    bool useMaxRange;

    [SerializeField, ShowIf("useMaxRange")]
    VariableType maxDistanceType;

    [SerializeField, AbilityDatabaseValue, ShowIf("useMaxRange")]
    string maxDistanceVar;

    [SerializeField]
    bool useMinRange;

    [SerializeField, ShowIf("useMinRange")]
    VariableType minDistanceType;

    [SerializeField, AbilityDatabaseValue, ShowIf("useMinRange")]
    string minDistanceVar;
    
    Vector3 targetDirection;

    //TODO add offset ??
    //[SerializeField]
    //string offsetVar;

    public override void CalculateTargetLocation ()
    {
        Target.position = previewer.MouseHitPosition;

        if (useMaxRange)
        {
            float maxRange = previewConfig.GetFloat(maxDistanceVar, maxDistanceType);

            if (!MathUtils.IsInsideCircle(OriginPosition, maxRange, previewer.MouseHitPosition))
                Target.position = OriginPosition + (previewer.MouseHitPosition - OriginPosition).normalized * maxRange;
            else if (!useMinRange)
                Target.position = previewer.MouseHitPosition;
        }
        if (useMinRange)
        {
            float minRange = previewConfig.GetFloat(minDistanceVar, minDistanceType);

            if (MathUtils.IsInsideCircle(OriginPosition, minRange, previewer.MouseHitPosition))
                Target.position = OriginPosition + (previewer.MouseHitPosition - OriginPosition).normalized * minRange;
            else if(!useMaxRange)
                Target.position = previewer.MouseHitPosition;
        }

        //add offset?
        //Target.position += (previewer.Champion.rotation * previewConfig.GetValue<Vector3>(offsetVar));
    }

    public override void CalculateTargetRotation ()
    {
        targetDirection = Target.position.FlattenY() - OriginPosition.FlattenY();

        Target.rotation = targetDirection != Vector3.zero ? Quaternion.LookRotation(targetDirection, Vector3.up) : Quaternion.identity;
    }

    public override void SetPosition ()
    {
        PositionerTransform.position = Target.position;
    }

    public override void SetRotation ()
    {
        PositionerTransform.rotation = canRotate ? Target.rotation : Quaternion.identity;
    }
}