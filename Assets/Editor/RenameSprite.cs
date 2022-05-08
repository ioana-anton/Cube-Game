using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public static class RenameSprite
{
    [MenuItem("GameObject/Rename/like Sprite", false, -1000)]
    public static void Execute()
    {
        var activeObject = Selection.activeGameObject;
        var meshes = activeObject.GetComponentsInChildren<Image>();
        foreach(var mesh in meshes)
        {
            var image = mesh.sprite;

            var obj = mesh.gameObject;
            while (obj != null)
            {
                obj.name = image.name;

                if (obj == activeObject)
                    break;

                obj = obj.transform.parent.gameObject;
            }
        }
    }
}
