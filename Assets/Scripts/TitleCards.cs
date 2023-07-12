using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Audio;

public class TitleCards : MonoBehaviour
{
    public Image m_Image;

    public TMP_Text[] m_Text1;

    //public TMP_Text m_Text2;

    public player m_player;


    //public AudioListener listener;

    [SerializeField] private float textOnDelay = 0.5f;
    [SerializeField] private float cardDuration = 1f;
    [SerializeField] private float soundFadeDelay = 0f;
    [SerializeField] private float soundFadeDuration = 1f;
    
    

    private void OnEnable()
    {
        AudioListener.volume = 0;
    }
    private void Start()
    {
        Color col = m_Image.color;
        m_Image.color = new Color(col.r, col.b, col.g, 1);
        m_Image.gameObject.SetActive(true);
        m_Text1[0].gameObject.SetActive(false);
        //m_Text1[2].gameObject.SetActive(false);
        m_player.PausePlayer();
        

        StartCoroutine(IntroCardSoundFade());
        StartCoroutine(IntroCardVisuals());
    }

    IEnumerator IntroCardSoundFade()
    {
        yield return new WaitForSeconds(soundFadeDelay);

        float t = 0;
        while (t < soundFadeDuration)
        {
            t += Time.deltaTime;
            AudioListener.volume = Mathf.Lerp(0, 1, t / soundFadeDuration);
            yield return null;
        }

        yield return null;
    }

    IEnumerator IntroCardVisuals()
    {   
        yield return new WaitForSeconds(textOnDelay);
        
        m_Text1[0].gameObject.SetActive(true);

        yield return new WaitForSeconds(cardDuration);
        m_Text1[0].gameObject.SetActive(false);
        m_Text1[1].gameObject.SetActive(true);
        
        yield return new WaitForSeconds(2);
        m_Text1[1].gameObject.SetActive(false);
        m_Image.gameObject.SetActive(false);
        m_player.UnpausePlayer();
       
        yield return null;
    }

   
}


