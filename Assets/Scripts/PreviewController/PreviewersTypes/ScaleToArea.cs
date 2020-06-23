using System;
using Sirenix.OdinInspector;
using UnityEngine;

public class ScaleToArea : PreviewScaler
{
    enum AreaMode
    {
        RADIUS,
        XZ,
        VECTOR2,
        VECTOR3
    }

    [SerializeField, AbilityDatabaseValue, ShowIf("canScale")]
    AreaMode areaScaleMode;

    [SerializeField, AbilityDatabaseValue, ShowIf("@canScale && areaScaleMode == AreaMode.RADIUS")]
    string radiusVar;
    
    [SerializeField, AbilityDatabaseValue, ShowIf("@canScale && areaScaleMode == AreaMode.VECTOR3")]
    string areaInVector3Var;

    [SerializeField, AbilityDatabaseValue, ShowIf("@canScale && areaScaleMode == AreaMode.VECTOR2")]
    string areaInVector2Var;

    [SerializeField, AbilityDatabaseValue, ShowIf("@canScale && areaScaleMode == AreaMode.XZ")]
    string areaWidthVar;

    [SerializeField, AbilityDatabaseValue, ShowIf("@canScale && areaScaleMode == AreaMode.XZ")]
    string areaLengthVar;

    [SerializeField]
    protected PivotMode pivot;

    const float quadToRadiusScaleRatio = 2f;

    Transform scalableMeshPivot;

    public override void Initialize ()
    {
        base.Initialize();
        
        scalableMesh.name = $"ScaleTo{areaScaleMode.ToString()}Quad";

        if (pivot == PivotMode.EDGE)
        {
            scalableMeshPivot = new GameObject("Pivot").transform;
            scalableMeshPivot.SetParent(positioner.PositionerTransform); ;
            scalableMeshPivot.localPosition = Vector3.zero;
            scalableMeshPivot.localRotation = Quaternion.identity;

            scalableMesh.SetParent(scalableMeshPivot, false);
            scalableMesh.localPosition = new Vector3(0, 0, 0.5f);
        }
    }

    public override void SetScale ()
    {
        if (!canScale)
        {
            scalableMesh.localScale = Vector3.one;
            return;
        }

        Vector3 newScale = Vector3.one;

        switch (areaScaleMode)
        {
            case AreaMode.RADIUS:
                newScale = Vector3.one * previewConfig.GetValue<float>(radiusVar) * quadToRadiusScaleRatio;
                //scalableMesh.localScale = Vector3.one * previewConfig.GetValue<float>(radiusVar) * quadToRadiusScaleRatio;
                break;
            case AreaMode.XZ:
                newScale = new Vector3(previewConfig.GetValue<float>(areaWidthVar), previewConfig.GetValue<float>(areaLengthVar), 1);
                //scalableMesh.localScale = new Vector3(previewConfig.GetValue<float>(areaWidthVar), previewConfig.GetValue<float>(areaLengthVar), 1);
                break;
            case AreaMode.VECTOR2:
                Vector2 vec2area = previewConfig.GetValue<Vector2>(areaInVector2Var);
                newScale = new Vector3(vec2area.x, vec2area.y, 1);
                //scalableMesh.localScale = new Vector3(vec2area.x, vec2area.y, 1);
                break;
            case AreaMode.VECTOR3:
                Vector3 vec3area = previewConfig.GetValue<Vector3>(areaInVector3Var);
                newScale = new Vector3(vec3area.x, vec3area.z, vec3area.y);
                //scalableMesh.localScale = new Vector3(vec3area.x, vec3area.z, vec3area.y);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        switch (pivot)
        {
            case PivotMode.CENTER:
                scalableMesh.localScale = newScale;
                break;
            case PivotMode.EDGE:
                scalableMeshPivot.localScale = new Vector3(newScale.x, newScale.z, newScale.y);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}