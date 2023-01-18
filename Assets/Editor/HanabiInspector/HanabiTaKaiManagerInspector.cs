using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

[CustomEditor(typeof(HanabiTakaiManager))]
public class HanabiTaKaiManagerInspector : Editor
{
    public override VisualElement CreateInspectorGUI()
    {
        var root = new VisualElement();
        var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Editor/HanabiMaker/HanabiMaker.uxml");
        visualTree.CloneTree(root);
        return root;
    }
}
