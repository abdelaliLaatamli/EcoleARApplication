using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Text.RegularExpressions;
using UnityEngine.Networking;

public class CanvaFornitureShower : MonoBehaviour
{

    // private SelectedObject data;

    private SelectedObject dataObject;

    public Image image;
    public TextMeshProUGUI title;
    public TextMeshProUGUI sentence;

    public GameObject nextButton;
    public GameObject prevButton;

    public int currentSentence = 0;
    public string langage = "fr";
    // public Image image;
    // public Image image

    public AudioSource audioPlys;
    AudioClip myClip;

    // Start is called before the first frame update
    public void setData( SelectedObject data )
    {
        this.dataObject = data;
        this.remplaire();

    }

    void Start()
    {
        audioPlys = GetComponent<AudioSource>();
    }

    public void remplaire()
    {

        image.sprite  = this.dataObject.imageObject;
        title.text = this.dataObject.title.getString(this.langage);
        sentence.text = this.dataObject.sentence[currentSentence].getString(this.langage);

        this.prevButton.SetActive(false);
        if(this.dataObject.sentence.Length <= 1 ) this.nextButton.SetActive(false);

    }


    public void changeCurrentSentence(int value)
    {
        currentSentence = currentSentence + value;
        sentence.text = this.dataObject.sentence[currentSentence].getString(this.langage);

        this.prevButton.SetActive(this.currentSentence != 0);
        this.nextButton.SetActive((this.dataObject.sentence.Length - 1 != this.currentSentence && this.dataObject.sentence.Length > 1 ));
    }

    public void speelName()
    {

        StartCoroutine(GetAudioClip( this.dataObject.title.getString(this.langage) , this.langage));
        //StartCoroutine(GetAudioClip());

    }


    IEnumerator GetAudioClip( string sentenceToAudio , string langage )
    {

        Regex rgx = new Regex("\\s+");

        string sentenceToAudioEncode = rgx.Replace(sentenceToAudio, "%20");

        // string url = "https://translate.google.com/translate_tts?ie=UTF-8&client=tw-ob&tl=es&q=holla";

        string url = "https://translate.google.com/translate_tts?ie=UTF-8&client=tw-ob&tl="+
                        langage+"&q="+ sentenceToAudioEncode;

        using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip( url , AudioType.MPEG))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError)
            {
                Debug.Log(www.error);
                Debug.Log("network Error");
            }
            else
            {
                myClip = DownloadHandlerAudioClip.GetContent(www);
                audioPlys.clip = myClip;
                audioPlys.Play();
        
            }
        }
    }
    /*
    IEnumerator GetAudioClip()
    {
        string url = "https://translate.google.com/translate_tts?ie=UTF-8&client=tw-ob&tl=es&q=holla";

        using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip(url, AudioType.MPEG))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError)
            {
                Debug.Log(www.error);
                Debug.Log("network Error");
            }
            else
            {
                myClip = DownloadHandlerAudioClip.GetContent(www);
                audioPlys.clip = myClip;
                audioPlys.Play();

            }
        }
    }
    */

    /*
    IEnumerator GetAudioClip2()
    {

        string url = "https://translate.google.com/translate_tts?ie=UTF-8&client=tw-ob&tl=es&q=holla";

        using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip(url, AudioType.MPEG))
        {
            yield return www.SendWebRequest();
            /*
            if (www.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.Log(www.error);
            }
            else
            {
                AudioClip myClip = DownloadHandlerAudioClip.GetContent(www);
            }
            
            AudioClip myClip = DownloadHandlerAudioClip.GetContent(www);
            this.audioPlys.clip = myClip;
            this.audioPlys.Play();

        }
    }
*/

    public void speelSentence()
    {

        // StartCoroutine(GetAudioClip());
        // StartCoroutine(GetAudioClip2());
        StartCoroutine(GetAudioClip(this.dataObject.sentence[currentSentence].getString(this.langage), this.langage));
    }

  

    // Update is called once per frame


    public void closeUI()
    {
        gameObject.SetActive(false);
    }
}
