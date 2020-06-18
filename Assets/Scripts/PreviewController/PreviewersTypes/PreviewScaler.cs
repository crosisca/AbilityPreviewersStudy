using System;

[Serializable]
public class PreviewScaler
{
    protected AbilityPreviewer previewer;
    protected PreviewPositioner positioner;
    protected PreviewConfig<object> previewConfig;
    
    public virtual void Initialize (AbilityPreviewer previewer, PreviewConfig<object> previewConfig)
    {
        this.previewer = previewer;
        this.previewConfig = previewConfig;
        this.positioner = previewConfig.positioner;
    }

    public virtual void SetScale () { }
}