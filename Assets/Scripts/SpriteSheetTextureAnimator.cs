using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteSheetTextureAnimator : MonoBehaviour
{
    // Scroll the main texture based on time

    Renderer rend;
    [SerializeField] float fpsMax = 12;
    private float fpsCurrent = 0; 
    bool doneWaiting = true;
    [SerializeField] Vector2 RowsAndColumns;
    int currentColumn = 0;
    int currentRow = 0;
    float xOffset = 0;
    float yOffset = 0;
    [SerializeField] bool randomStartFrame = false;
    [SerializeField] AnimationCurve curve;
    private float t = 0;

    void Start()
    {
        rend = GetComponent<Renderer>();
        if (randomStartFrame)
        {
            currentColumn = Random.Range(0, (int)RowsAndColumns.x - 1);
            currentRow = Random.Range(0, (int)RowsAndColumns.y - 1);
        }
        else
        {
            currentRow = (int)RowsAndColumns.y;
        }
        t = 0;
    }

    void Update()
    {
        float curveProgress = curve.Evaluate(t);
        fpsCurrent = Mathf.Lerp(2, fpsMax, curveProgress);
        t += Time.deltaTime * 0.1f;
        if (t > 1) 
        { 
            t = 0; 
        }
        
        if (doneWaiting)
        {
            xOffset = currentColumn / RowsAndColumns.x;
            yOffset = currentRow / RowsAndColumns.y;
            rend.material.mainTextureOffset = new Vector2(xOffset, yOffset);

            //iterate columns
            currentColumn += 1;
            if (currentColumn > RowsAndColumns.x - 1)
            {
                currentColumn = 0;

                //iterate rows
                currentRow -= 1;
                if (currentRow == 0)
                {
                    currentRow = (int)RowsAndColumns.y;
                }
            }

            //start new countdown
            StartCoroutine(C_UpdateFrameTimer(fpsCurrent));
            doneWaiting = false;
        }
    }

    IEnumerator C_UpdateFrameTimer(float waitTime)
    {
        //Debug.Log("update frame wait time is " + waitTime);
        
        yield return new WaitForSeconds(1 / waitTime);
        doneWaiting = true;
        yield return null;
    }
}
