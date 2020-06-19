using System;
using UnityEngine;

[Serializable]
public class PreviewScaler
{
    protected AbilityPreviewer previewer;
    protected PreviewPositioner positioner;
    protected PreviewConfig previewConfig;

    [SerializeField]
    protected bool canScale;
    
    protected Transform scalableQuad;

    public void Setup(AbilityPreviewer previewer, PreviewConfig previewConfig)
    {
        this.previewer = previewer;
        this.previewConfig = previewConfig;
        this.positioner = previewConfig.positioner;

        Initialize();
        UpdateScale(true);
    }

    public virtual void Initialize ()
    {
        scalableQuad = GameObject.CreatePrimitive(PrimitiveType.Quad).transform;
        scalableQuad.name = $"ScalableQuad";
        scalableQuad.SetParent(positioner.PositionerTransform);
        scalableQuad.transform.localPosition = Vector3.zero;
        scalableQuad.transform.localRotation = Quaternion.Euler(90, 0, 0);

        scalableQuad.GetComponent<Renderer>().material = previewConfig.Material;
    }

    public void UpdateScale(bool ignoreCanScale = false)
    {
        if (!canScale && !ignoreCanScale)
            return;

        SetScale();
    }

    public virtual void SetScale () { }
}