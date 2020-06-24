using System;
using Sirenix.OdinInspector;
using UnityEngine;

public class AimToMousePositioner : PreviewPositioner
{
    [SerializeField]
    bool useOffset;

    [SerializeField, ShowIf("useOffset")]
    VariableType offsetVarType;

    [SerializeField, AbilityDatabaseValue, ShowIf("useOffset")]
    string offsetVar;

    Vector3 targetDirection;
    
    public override void CalculateTargetLocation()
    {
        if (useOffset)
        {
            switch (offsetVarType)
            {
                case VariableType.FLOAT:
                    Target.position = Origin.TransformPoint(Vector3.forward * previewConfig.GetValue<float>(offsetVar));
                    break;
                case VariableType.INT:
                    Target.position = Origin.TransformPoint(Vector3.forward * previewConfig.GetValue<int>(offsetVar));
                    break;
                case VariableType.VECTOR3:
                    Target.position = Origin.TransformPoint(previewConfig.GetValue<Vector3>(offsetVar));
                    break;
                case VariableType.VECTOR2:
                    Target.position = Origin.TransformPoint(previewConfig.GetValue<Vector2>(offsetVar).ToXZPlane());
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        else
            Target.position = OriginPosition;
    }

    public override void CalculateTargetRotation ()
    {
        targetDirection = (previewer.MouseHitPosition - OriginPosition).normalized;
        Target.rotation = Quaternion.LookRotation(targetDirection, Vector3.up);
    }

    public override void SetPosition ()
    {
        PositionerTransform.position = TargetPosition;
    }

    public override void SetRotation ()
    {
        PositionerTransform.rotation = TargetRotation;
    }
}