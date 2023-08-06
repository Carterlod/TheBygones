
using UnityEngine;
using UnityEditor;

public class PopulateWithPrefab : EditorWindow
{
    [SerializeField] private GameObject obj;
    //[SerializeField] private bool noDupes;
    [MenuItem("Tools/Populate With Prefab")]

    static void CreateObjectInSelected()
    {
        EditorWindow.GetWindow<PopulateWithPrefab>();
    }

    private void OnGUI()
    {
        obj = (GameObject)EditorGUILayout.ObjectField("GameObject", obj, typeof(GameObject), true);
        
        if (GUILayout.Button("Populate"))
        {
            var selection = Selection.gameObjects;
            for(var i = selection.Length -1; i >= 0; --i)
            {
                var selected = selection[i];
                var prefabType = PrefabUtility.GetPrefabAssetType(obj);
                GameObject newObject;
                
                if(prefabType == PrefabAssetType.Regular || prefabType == PrefabAssetType.Variant)
                {
                    string assetpath = PrefabUtility.
                        GetPrefabAssetPathOfNearestInstanceRoot(obj);

                    newObject = 
                            AssetDatabase.LoadAssetAtPath(
                                assetpath, typeof(GameObject))
                                as GameObject;
                    newObject = PrefabUtility.InstantiatePrefab(newObject) as GameObject;
                    
                }
                else
                {
                    newObject = Instantiate(obj);
                    newObject.name = obj.name;
                }
               
                if(newObject == null)
                {
                    Debug.LogError("Error instantiating prefab");
                    break;
                }
                Undo.RegisterCreatedObjectUndo(newObject, "Populate With Prefabs");
                newObject.transform.parent = selected.transform;


            }
        }
    }

}
