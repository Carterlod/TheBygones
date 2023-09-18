using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirtyDishes : MonoBehaviour
{
    public static DirtyDishes i;
    [SerializeField] Grabbable[] dirtyDishesArray;
    public List<Transform> dirtyDishList;
    [SerializeField] List<Transform> cleanDishList;
    [SerializeField] Transform cleanDishDumpSpot;
    public Transform dirtyDishAddSpot;
    [SerializeField] Vessel sinkVessel;
    

    private void Awake()
    {
        i = this;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Grabbable>() && !dirtyDishList.Contains(other.gameObject.transform))
        {
            dirtyDishList.Add(other.gameObject.transform);
            sinkVessel.ResetFill();
        }
    }

    public void CleanOneDish()
    {
        if (dirtyDishList.Count < 1)
        {
            return;
        }
        Transform dish = dirtyDishList[0];
        dish.position = cleanDishDumpSpot.position;
        dish.parent = cleanDishDumpSpot;
        dish.GetComponent<Grabbable>().Resettle();
        if (!cleanDishList.Contains(dish))
        {
            cleanDishList.Add(dish);
        }
        dirtyDishList.Remove(dish);
        sinkVessel.ResetFill();
        
    }

    public void ClearCleanDishes()
    {
        if (cleanDishList.Count > 0)
        {
            foreach(Transform cleanDish in cleanDishList)
            {
                cleanDish.gameObject.SetActive(false);
            }
            cleanDishList.Clear() ;
        }
    }
}
