using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

public class AbilityPreviewer : SerializedMonoBehaviour
{
    public Ability Ability { get; private set; }

    [SerializeField]
    public Transform Champion { get; private set; }

    public List<PreviewConfig> PreviewConfigs = new List<PreviewConfig>();

    public Vector3 MouseHitPosition { get; private set; }
    
    void Awake()
    {
        if (Champion == null)
            Champion = GameObject.FindWithTag("Player").transform;

        Ability = Champion.GetComponent<Champion>().ability;

        CacheAbilityValues();
    }
    
    void CacheAbilityValues()
    {
        foreach (PreviewConfig previewConfig in PreviewConfigs)
            previewConfig.Setup(this);
    }

    [ContextMenu("ResyncDBVariables")]
    void DebugResyncAbilityValues()
    {
        foreach (PreviewConfig previewConfig in PreviewConfigs)
            previewConfig.CacheUsedVariables();
    }

    void Update()
    {
        MouseHitPosition = Camera.main.ScreenPointToRay(Input.mousePosition).GetIntersectionPoint();

        foreach (PreviewConfig previewConfig in PreviewConfigs)
        {
            previewConfig.positioner.CalculateTargetLocation();
            previewConfig.positioner.CalculateTargetRotation();

            bool IsValid = !Ability.Previewable || Ability.IsPreviewPositionValid(previewConfig.positioner.TargetPosition);

            if (!IsValid)
                continue;

            previewConfig.positioner.SetPosition();
            previewConfig.positioner.SetRotation();

            previewConfig.scaler.UpdateScale();
            previewConfig.scaler.SetMaterialProperties();
        }
    }
}