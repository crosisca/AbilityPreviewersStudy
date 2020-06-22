using System;
using System.Collections.Generic;
using System.Linq;
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
        Variables.Clear();
        
        AddAbilityDatabaseVariableNameToDictionary(positioner);
        AddAbilityDatabaseVariableNameToDictionary(scaler);
        FillDictionaryWithAbilityValues();
    }

    void AddAbilityDatabaseVariableNameToDictionary (object objToLookForAttribute)
    {
        foreach (FieldInfo fieldInfo in objToLookForAttribute.GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.FlattenHierarchy))
        {
            AbilityDatabaseValueAttribute abilityDbValue = Attribute.GetCustomAttribute(fieldInfo, typeof(AbilityDatabaseValueAttribute)) as AbilityDatabaseValueAttribute;

            //Debug.Log($"{fieldInfo.Name} is atribute {abilityDbValue != null}");

            if (abilityDbValue != null)
            {
                string key = fieldInfo.GetValue(objToLookForAttribute) as string;

                if (!string.IsNullOrEmpty(key))
                    Variables.Add(key, null);
            }
        }
    }

    void FillDictionaryWithAbilityValues()
    {
        //foreach (FieldInfo fieldInfo in previewer.Ability.GetType().GetFields(BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy))
        //{
        //    if (Variables.ContainsKey(fieldInfo.Name))
        //        Variables[fieldInfo.Name] = fieldInfo.GetValue(previewer.Ability);
        //}

        //foreach (KeyValuePair<string, object> kvp in Variables)
        //{
        //    FieldInfo fieldInfo = previewer.Ability.GetType().GetField(kvp.Key);
        //    if (fieldInfo != null)
        //        Variables[kvp.Key] = fieldInfo.GetValue(previewer.Ability);
        //    else
        //    {
        //        PropertyInfo propertyInfo = previewer.Ability.GetType().GetProperty(kvp.Key);
        //        if (propertyInfo != null)
        //            Variables[kvp.Key] = propertyInfo.GetValue(previewer.Ability);
        //    }
        //}

        foreach (string variableName in Variables.Keys.ToArray())
        {
            FieldInfo fieldInfo = previewer.Ability.GetType().GetField(variableName);
            if (fieldInfo != null)
                Variables[variableName] = fieldInfo.GetValue(previewer.Ability);
            else
            {
                PropertyInfo propertyInfo = previewer.Ability.GetType().GetProperty(variableName);
                if (propertyInfo != null)
                    Variables[variableName] = propertyInfo.GetValue(previewer.Ability);
            }
        }
    }

    void CacheAbilityValues()
    {
        float range = (float)previewer.Ability.GetType().GetProperty("Range", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.FlattenHierarchy).GetValue(previewer.Ability);
        Debug.Log(range);
    }
    
    

    public T GetValue<T> (string variableName)
    {
        return (T)Variables[variableName];
    }
}