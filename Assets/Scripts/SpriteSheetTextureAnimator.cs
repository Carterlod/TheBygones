using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteSheetTextureAnimator : MonoBehaviour
{
    // Scroll the main texture based on time

    Renderer rend;
    [SerializeField] float fps = 12;
    bool doneWaiting = true;
    [SerializeField] Vector2 RowsAndColumns;
    int currentColumn = 0;
    int currentRow = 0;
    float xOffset = 0;
    float yOffset = 0;
    [SerializeField] bool randomStartFrame = false;

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

    }

    void Update()
    {
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
            StartCoroutine(UpdateFrameRoutine());
            doneWaiting = false;
        }
    }

    IEnumerator UpdateFrameRoutine()
    {
        yield return new WaitForSeconds(1 / fps);
        doneWaiting = true;
        yield return null;
    }
}
