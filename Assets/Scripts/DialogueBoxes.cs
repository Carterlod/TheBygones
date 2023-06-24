using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueBoxes : MonoBehaviour
{
    [Header("Box")]
    public Camera cam;
    public Transform character;
    public Canvas canvas;
    public float margin = 10f;
    public Image box;
    public Vector3 liftedPos;

    [Header("Pointer")]
    public Image point;

    [Header("NameSlot")]
    private NPC npc;
    [SerializeField] TMP_Text nameSlot;

    private void Start()
    {
        npc = character.GetComponentInParent<NPC>();
        nameSlot.text = npc.characterName;
    }

    private void Update()
    {
        RectTransform m_rect = box.GetComponent<RectTransform>();
        m_rect.position = cam.WorldToScreenPoint(character.position);
        liftedPos = box.rectTransform.position;
        liftedPos.y += 300;
        ClampToWindow(liftedPos, m_rect, canvas.GetComponent<RectTransform>());

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