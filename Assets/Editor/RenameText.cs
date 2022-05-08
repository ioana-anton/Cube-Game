using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public static class RenameText
{
    [MenuItem("GameObject/Rename/like Text", false, -1001)]
    public static void Execute()
    {
        var activeObject = Selection.activeGameObject;
        var meshes = activeObject.GetComponentsInChildren<Text>();
        var meshes1 = activeObject.GetComponentsInChildren<TextMeshProUGUI>();

        foreach (var mesh in meshes)
        {
            var text = mesh.text;

            var obj = mesh.gameObject;
            while (obj != null)
            {
                obj.name = text;

                if (obj == activeObject)
                    break;

                obj = obj.transform.parent.gameObject;
            }
        }

        foreach (var mesh in meshes1)
        {
            var text = mesh.text;

            var obj = mesh.gameObject;
            while (obj != null)
            {
                obj.name = text;

                if (obj == activeObject)
                    break;

                obj = obj.transform.parent.gameObject;
            }
        }
    }
}
