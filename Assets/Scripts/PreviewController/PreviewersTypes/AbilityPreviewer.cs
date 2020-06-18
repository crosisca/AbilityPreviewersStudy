using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

public class AbilityPreviewer : SerializedMonoBehaviour
{
    //[SerializeField]
    //PositionerConfig positionerConfig;

    public Ability Ability { get; private set; }

    [SerializeField]
    public Transform Champion { get; private set; }

    //[ReadOnly]
    public List<PreviewConfig<object>> PreviewConfigs = new List<PreviewConfig<object>>();

    public Vector3 MouseHitPosition { get; private set; }

    [ReadOnly]
    public List<PreviewPositioner> positioners = new List<PreviewPositioner>();

    void Awake()
    {
        Ability = new Ability();

        CacheAbilityValues();
    }

    void CacheAbilityValues()
    {
        foreach (PreviewConfig<object> previewConfig in PreviewConfigs)
        {
            foreach (FieldInfo fieldInfo in Ability.GetType().GetFields(BindingFlags.Public | BindingFlags.Instance))
            {
                if (previewConfig.Variables.ContainsKey(fieldInfo.Name))
                    previewConfig.Variables[fieldInfo.Name] = fieldInfo.GetValue(Ability);
            }

            previewConfig.positioner.Initialize(this, previewConfig);
            previewConfig.scaler.Initialize(this, previewConfig);
        }
    }

    void Update()
    {
        MouseHitPosition = Camera.main.ScreenPointToRay(Input.mousePosition).GetIntersectionPoint();

        foreach (PreviewConfig<object> previewConfig in PreviewConfigs)
        {
            previewConfig.positioner.CalculateTargetLocation();
            previewConfig.positioner.CalculateTargetRotation();

            bool IsValid = !Ability.Previewable || Ability.IsPreviewPositionValid(previewConfig.positioner.TargetPosition);

            if (!IsValid)
                continue;

            previewConfig.positioner.SetPosition();
            previewConfig.positioner.SetRotation();

            previewConfig.scaler.SetScale();
        }
    }
}