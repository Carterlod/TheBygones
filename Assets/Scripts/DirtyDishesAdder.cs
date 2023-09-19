using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirtyDishesAdder : MonoBehaviour
{
    [SerializeField] Grabbable[] dirtyDishesArray;
    [SerializeField] List<GameObject> dirtyDishList;
    [SerializeField] bool hideDirtyDishesWhileEnabled = false;
    private void Start()
    {
        dirtyDishesArray = GetComponentsInChildren<Grabbable>();
        StartCoroutine(DumpNewDishes());
    }
    public void OnEnable()
    {
        if (hideDirtyDishesWhileEnabled)
        {
            DirtyDishes.i.dirtyDishAddSpot.gameObject.SetActive(false);
        }
    }
    public void OnDisable()
    {
        if (hideDirtyDishesWhileEnabled)
        {
            DirtyDishes.i.dirtyDishAddSpot.gameObject.SetActive(true);
        }
    }
    IEnumerator DumpNewDishes()
    {
        foreach (Grabbable dish in dirtyDishesArray)
        {
            
            dish.gameObject.transform.position = DirtyDishes.i.dirtyDishAddSpot.position;
            dish.gameObject.transform.parent = DirtyDishes.i.dirtyDishAddSpot;
            
            yield return null;
        }
        yield return null;
    }

}
