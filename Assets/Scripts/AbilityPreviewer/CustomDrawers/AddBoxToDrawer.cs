using System;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;
using UnityEngine;

public class AddBoxToDrawers<T> : OdinValueDrawer<T> where T : IBoxedDrawable
{
    protected override void DrawPropertyLayout(GUIContent label)
    {
        SirenixEditorGUI.BeginBox();
        this.CallNextDrawer(label);
        SirenixEditorGUI.EndBox();
    }
}

public interface IBoxedDrawable { }