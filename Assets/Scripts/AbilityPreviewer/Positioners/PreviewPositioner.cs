using System;
using UnityEngine;

[Serializable]
public abstract class PreviewPositioner : IBoxedDrawable
{
    protected AbilityPreviewer previewer;

    protected PreviewConfig previewConfig;

    public Transform Target { get; protected set; }
    public Transform PositionerTransform { get; private set; }

    public virtual Vector3 TargetPosition => Target.position;
    public virtual Quaternion TargetRotation => Target.rotation;
    public virtual Vector3 OriginPosition => previewer.Champion.position;
    public virtual Transform Origin => previewer.Champion;


    public virtual void Setup(AbilityPreviewer previewer, PreviewConfig previewConfig)
    {
        this.previewer = previewer;
        this.previewConfig = previewConfig;

        PositionerTransform = new GameObject($"{GetType()}").transform;
        PositionerTransform.SetParent(previewer.transform);

        Target = new GameObject("Target").transform;
        Target.SetParent(PositionerTransform);
    }
           
    public virtual void CalculateTargetLocation () { }
    public virtual void CalculateTargetRotation () { }
           
    public virtual void SetPosition () { }
    public virtual void SetRotation () { }
}