using System;
using System.Collections.Generic;
using System.Reflection;
using Sirenix.OdinInspector;
using UnityEngine;

public class PreviewConfig
{
    [SerializeField]
    public PreviewPositioner positioner;

    [SerializeField]
    public PreviewScaler scaler;

    [SerializeField]
    public Material Material { get; set; }

    [SerializeField, ShowIf("@UnityEngine.Application.isPlaying")]
    Dictionary<string, object> _variables;
    public Dictionary<string, object> Variables => _variables ?? (_variables = new Dictionary<string, object>());

    AbilityPreviewer previewer;

    public void Setup(AbilityPreviewer previewer)
    {
        this.previewer = previewer;
        CacheUsedVariables();
        
        positioner.Setup(previewer, this);
        scaler.Setup(previewer, this);
    }

    public void CacheUsedVariables ()
    {
        AddAbilityDatabaseVariableNameToDictionary(positioner);
        AddAbilityDatabaseVariableNameToDictionary(scaler);

        foreach (FieldInfo fieldInfo in previewer.Ability.GetType().GetFields(BindingFlags.Public | BindingFlags.Instance))
        {
            if (Variables.ContainsKey(fieldInfo.Name))
                Variables[fieldInfo.Name] = fieldInfo.GetValue(previewer.Ability);
        }
    }


    void AddAbilityDatabaseVariableNameToDictionary(object objToLookForAttribute)
    {
        foreach (FieldInfo positionerField in objToLookForAttribute.GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance))
        {
            AbilityDatabaseValueAttribute abilityDbValue = Attribute.GetCustomAttribute(positionerField, typeof(AbilityDatabaseValueAttribute)) as AbilityDatabaseValueAttribute;

            if (abilityDbValue != null)
                Variables.Add((string)positionerField.GetValue(objToLookForAttribute), null);
        }
    }

    public T GetValue<T> (string variableName)
    {
        return (T)Variables[variableName];
    }
}