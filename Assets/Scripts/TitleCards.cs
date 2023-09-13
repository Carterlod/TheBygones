using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Audio;
using UnityEngine.Events;

public class TitleCards : MonoBehaviour
{
    public Image m_Image;

    public TMP_Text m_Text;

    //public TMP_Text m_Text2;

    public PlayerSettings m_player;

    [System.Serializable]
    public class TitleCard
    {
        public string text;
        public float durationOverride;
    }
    public TitleCard[] titleCards;
    //public AudioListener listener;

    [SerializeField] private float textOnDelay = 0.5f;
    [SerializeField] private float cardDuration = 1f;
    [SerializeField] private float lingerOnBlack = 0f;
    [SerializeField] private float soundFadeDelay = 0f;
    [SerializeField] private float soundFadeDuration = 1f;

    [SerializeField] UnityEvent onEndEvent;

    
    private void Start()
    {
        
        Color col = m_Image.color;
        m_Image.color = new Color(col.r, col.b, col.g, 1);
        m_Image.gameObject.SetActive(true);
        m_Text.gameObject.SetActive(false);
        //m_Text1[2].gameObject.SetActive(false);
        m_player.PausePlayer();

        if(soundFadeDuration > 0)
        {
            StartCoroutine(IntroCardSoundFade());
        }

        StartCoroutine(IntroCardVisuals());
    }

    IEnumerator IntroCardSoundFade()
    {
        AudioListener.volume = 0;
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

        foreach(TitleCard card in titleCards)
        {
            m_Text.text = card.text;
            m_Text.gameObject.SetActive(true);
            yield return new WaitForSeconds(cardDuration + card.durationOverride);
            m_Text.gameObject.SetActive(false);
            yield return new WaitForSeconds(lingerOnBlack);
            yield return null;
        }
        m_player.UnpausePlayer();
        m_Image.gameObject.SetActive(false);
        if(onEndEvent != null)
        {
            onEndEvent.Invoke();
        }

        yield return null;
    }

   
}


