using System;
using UnityEngine;

[Serializable]
public class PreviewPositioner
{
    protected AbilityPreviewer previewer;

    protected PreviewConfig<object> previewConfig;

    protected Transform target;
    public Transform PositionerTransform { get; private set; }

    public virtual Vector3 TargetPosition => target.position;
    public virtual Quaternion TargetRotation => target.rotation;
    public virtual Vector3 Origin => previewer.Champion.position;


    public virtual void Initialize(AbilityPreviewer previewer, PreviewConfig<object> previewConfig)
    {
        this.previewer = previewer;
        this.previewConfig = previewConfig;

        PositionerTransform = new GameObject($"{GetType()}").transform;
        PositionerTransform.SetParent(previewer.transform);

        target = new GameObject("Target").transform;
        target.SetParent(PositionerTransform);
    }
           
    public virtual void CalculateTargetLocation () { }
    public virtual void CalculateTargetRotation () { }
           
    public virtual void SetPosition () { }
    public virtual void SetRotation () { }
}