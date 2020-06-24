using System;
using Sirenix.OdinInspector;
using UnityEngine;
using Object = UnityEngine.Object;

[Serializable]
public abstract class PreviewScaler
{
    [SerializeField]
    protected MeshMode meshMode = MeshMode.QUAD;

    [SerializeField, ShowIf("meshMode", MeshMode.CUSTOM)]
    protected GameObject CustomMesh;
    
    [SerializeField, HideIf("meshMode", MeshMode.CUSTOM), Required]
    protected Material Material;

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

    protected Renderer scalableRenderer;

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
                scalableMesh.transform.localRotation = Quaternion.Euler(90, 0, 0);
                break;
            case MeshMode.CIRCLE:
                scalableMesh = Object.Instantiate(Resources.Load<GameObject>("CircularMesh")).transform;
                scalableMesh.transform.localRotation = Quaternion.Euler(90, 0, 0);
                break;
            case MeshMode.CUSTOM:
                scalableMesh = Object.Instantiate(CustomMesh).transform;
                scalableMesh.transform.localRotation = Quaternion.identity;
                break;
                default:
                    throw new ArgumentOutOfRangeException();
        }
        scalableMesh.name = $"ScalableMesh";
        scalableMesh.SetParent(positioner.PositionerTransform); 
        scalableMesh.transform.localPosition = Vector3.zero;


        if (meshMode != MeshMode.CUSTOM)
        {
            scalableRenderer = scalableMesh.GetComponent<Renderer>();
            scalableRenderer.material = Material;
        }
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
            scalableRenderer.material.SetFloat("_MaskAngle", previewConfig.GetValue<float>(angleVar));
    }

    public virtual void SetScale () { }
}