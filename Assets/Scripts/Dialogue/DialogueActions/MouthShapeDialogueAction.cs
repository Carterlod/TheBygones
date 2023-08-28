using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouthShapeDialogueAction : BaseDialogueAction
{
    private DialogueTester dialogue;
    public enum MouthOwner { Stu, Ted, Mick, Nigel };
    public MouthOwner mouthOwner;
    [SerializeField] float startValue = 0;
    [SerializeField] float endValue = 100;

    private SkinnedMeshRenderer mouth;
    private void Awake()
    {
        dialogue = GetComponentInParent<DialogueTester>();
        if (mouthOwner == MouthOwner.Stu)
        {
            mouth = dialogue.stu.mouth;
        }
        if (mouthOwner == MouthOwner.Mick)
        {
            mouth = dialogue.mick.mouth;
        }
        if (mouthOwner == MouthOwner.Nigel)
        {
            mouth = dialogue.nigel.mouth;
        }
        if (mouthOwner == MouthOwner.Ted)
        {
            mouth = dialogue.ted.mouth;
        }
    }
    public override void OnDialogueStart()
    {
        base.OnDialogueStart();
        StartCoroutine(LerpMouthRoutine());
    }

    IEnumerator LerpMouthRoutine()
    {
        float t = 0;
        float d = 0.5f;

        while (t < d)
        {
            t += Time.deltaTime;
            if (t > d)
            {
                t = d;
            }

            mouth.SetBlendShapeWeight(0, Mathf.Lerp(startValue, endValue, t / d));

            yield return null;
        }
        yield return null;
    }
}
