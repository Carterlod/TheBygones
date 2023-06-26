using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueBoxes : MonoBehaviour
{
    [Header("Box")]
    private Camera cam;
    [SerializeField] private Canvas canvas;
    [SerializeField] private float margin = 10f;
    [SerializeField] private Image box;
    private Vector3 liftedPos;


    [Header("Pointer")]
    [SerializeField] private Image point;

    [Header("NameSlot")]
    public NPC npc;
    public TMP_Text nameSlot;

    [Header("DialogueSlot")]
    public TMP_Text dialogueSlot;

    private void Start()
    {
        cam = Camera.main;
        canvas = canvas.rootCanvas;
        
        //nameSlot.text = npc.characterName;


    }

    private void Update()
    {
        if(npc == null)
        {
            return;
        }
        RectTransform this_rect = gameObject.GetComponent<RectTransform>();
        //RectTransform bg_rect = box.GetComponent<RectTransform>();
        this_rect.position = cam.WorldToScreenPoint(npc.characterHead.position);
        liftedPos = this_rect.position;
        liftedPos.y += 300;
        ClampToWindow(liftedPos, this_rect, canvas.GetComponent<RectTransform>());

    }
    void ClampToWindow(Vector3 m_dialogueBoxPos, RectTransform panelRectTransform, RectTransform parentRectTransform)
    {

        panelRectTransform.transform.position = m_dialogueBoxPos;

        Vector3 pos = panelRectTransform.localPosition;

        Vector3 minPosition = parentRectTransform.rect.min - panelRectTransform.rect.min;
        //Debug.Log("canvas rect min = " + parentRectTransform.rect.min + "max = " + parentRectTransform.rect.max);

        Vector3 maxPosition = parentRectTransform.rect.max - panelRectTransform.rect.max;


        if(liftedPos.z > 0)
        {
            pos.x = Mathf.Clamp(panelRectTransform.localPosition.x, minPosition.x + margin, maxPosition.x - margin) ;
            pos.y = Mathf.Clamp(panelRectTransform.localPosition.y, minPosition.y + margin, maxPosition.y - margin) ;

        }
        else
        {
            pos.x = Mathf.Clamp(panelRectTransform.localPosition.x, -minPosition.x, -maxPosition.x);
            pos.y = Mathf.Clamp(panelRectTransform.localPosition.y, minPosition.y, maxPosition.y);
        }
        

        panelRectTransform.localPosition = pos;

        Vector3 newPointerScale = point.transform.localScale;
        if (panelRectTransform.anchoredPosition.x < 0)
        {
            newPointerScale.x = -1;
            point.transform.localScale = newPointerScale;
        }
        else
        {
            newPointerScale.x = 1;
            point.transform.localScale = newPointerScale;
        }
    }
}