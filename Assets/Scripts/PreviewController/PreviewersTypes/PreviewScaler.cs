using System;
using Sirenix.OdinInspector;
using UnityEngine;
using Object = UnityEngine.Object;

[Serializable]
public class PreviewScaler
{
    [SerializeField]
    protected MeshMode meshMode = MeshMode.QUAD;

    [SerializeField]
    protected bool canScale;

    [SerializeField]
    bool useAngleMask;

    [SerializeField, AbilityDatabaseValue, ShowIf("useAngleMask")]
    protected string angleVar;

    protected Transform scalableMesh;
    protected AbilityPreviewer previewer;
    protected PreviewPositioner positioner;
    protected PreviewConfig previewConfig;


    protected Renderer quadRenderer;

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
        switch (meshMode)
        {
            case MeshMode.QUAD:
                scalableMesh = GameObject.CreatePrimitive(PrimitiveType.Quad).transform;
                break;
            case MeshMode.CIRCLE:
                scalableMesh = Object.Instantiate(Resources.Load<GameObject>("CircularMesh")).transform;
                break;
        }
        scalableMesh.name = $"ScalableMesh";
        scalableMesh.SetParent(positioner.PositionerTransform);
        scalableMesh.transform.localPosition = Vector3.zero;
        scalableMesh.transform.localRotation = Quaternion.Euler(90, 0, 0);

        quadRenderer = scalableMesh.GetComponent<Renderer>();

        quadRenderer.material = previewConfig.Material;
    }

    public void UpdateScale(bool ignoreCanScale = false)
    {
        if (!canScale && !ignoreCanScale)
            return;

        SetScale();
    }

    public virtual void SetMaterialProperties()
    {
        if (useAngleMask)
            quadRenderer.material.SetFloat("_MaskAngle", previewConfig.GetValue<float>(angleVar));
    }

    public virtual void SetScale () { }
}