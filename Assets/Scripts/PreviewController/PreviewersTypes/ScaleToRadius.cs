using UnityEngine;

public class ScaleToRadius : PreviewScaler
{
    [SerializeField]
    string radiusVar;

    Transform scalableQuad;

    const float radiusScaleRatio = 2f;

    public override void Initialize (AbilityPreviewer previewer, PreviewConfig<object> previewConfig)
    {
        base.Initialize(previewer, previewConfig);

        scalableQuad = GameObject.CreatePrimitive(PrimitiveType.Quad).transform;
        scalableQuad.name = "ScaleToRadiusQuad";
        scalableQuad.SetParent(positioner.PositionerTransform);
        scalableQuad.transform.localPosition = Vector3.zero;
        scalableQuad.transform.localRotation = Quaternion.Euler(90, 0, 0);
        scalableQuad.transform.localScale = Vector3.one * radiusScaleRatio;

        scalableQuad.GetComponent<Renderer>().material = previewConfig.Material;
    }

    public override void SetScale ()
    {
        scalableQuad.localScale = Vector3.one * (float)previewConfig.Variables[radiusVar] * radiusScaleRatio;
    }
}