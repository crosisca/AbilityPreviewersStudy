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

    [SerializeField, AbilityDatabaseValue]
    AreaMode areaScaleMode;

    [SerializeField, AbilityDatabaseValue, ShowIf("areaScaleMode", AreaMode.RADIUS)]
    string radiusVar;
    
    [SerializeField, AbilityDatabaseValue, ShowIf("areaScaleMode", AreaMode.VECTOR3)]
    string areaInVector3Var;

    [SerializeField, AbilityDatabaseValue, ShowIf("areaScaleMode", AreaMode.VECTOR2)]
    string areaInVector2Var;

    [SerializeField, AbilityDatabaseValue, ShowIf("areaScaleMode", AreaMode.XZ)]
    string areaWidthVar;

    [SerializeField, AbilityDatabaseValue, ShowIf("areaScaleMode", AreaMode.XZ)]
    string areaLengthVar;
    
    const float quadToRadiusScaleRatio = 2f;
    
    public override void Initialize ()
    {
        base.Initialize();

        scalableMesh.name = $"ScaleTo{areaScaleMode.ToString()}Quad";
    }

    public override void SetScale ()
    {
        switch (areaScaleMode)
        {
            case AreaMode.RADIUS:
                scalableMesh.localScale = Vector3.one * previewConfig.GetValue<float>(radiusVar) * quadToRadiusScaleRatio;
                break;
            case AreaMode.XZ:
                scalableMesh.localScale = new Vector3(previewConfig.GetValue<float>(areaWidthVar), previewConfig.GetValue<float>(areaLengthVar), 1);
                break;
            case AreaMode.VECTOR2:
                Vector2 vec2area = previewConfig.GetValue<Vector2>(areaInVector2Var);
                scalableMesh.localScale = new Vector3(vec2area.x, vec2area.y, 1);
                break;
            case AreaMode.VECTOR3:
                Vector3 vec3area = previewConfig.GetValue<Vector3>(areaInVector3Var);
                scalableMesh.localScale = new Vector3(vec3area.x, vec3area.z, vec3area.y);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
       
    }
}