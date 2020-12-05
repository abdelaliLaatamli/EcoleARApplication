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


    public AudioSource audioPlys;
    AudioClip myClip;

    /*
    private GameObject gameManagement ;
    private GameManagement gameManagementScript;
    */


    public void setData( SelectedObject data )
    {
        this.dataObject = data;
        this.remplaire();

    }

    void Start()
    {
        audioPlys = GetComponent<AudioSource>();
        // Debug.Log("aaaaa");
        GameObject gameManagement = GameObject.Find("GameManagement");


        //Debug.Log(gameManagement );
        if(gameManagement != null)
        {
            GameManagement gameManagementScript = gameManagement.GetComponent<GameManagement>();
            this.langage = gameManagementScript.langage;
        }
        // gameManagementScript = gameManagement.GetComponent<GameManagement>();
        //gameManagementScript = gameManagement.GetComponent<GameManagement>();


        //Debug.Log(gameManagementScript.langage);
        //CanvaFornitureShower shower = fournitureUI.GetComponentInChildren<CanvaFornitureShower>();
        //scriptComponent = gameobject.GetComponent.< LevelController > ();
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


    public void speelSentence()
    {

        StartCoroutine(GetAudioClip(this.dataObject.sentence[currentSentence].getString(this.langage), this.langage));
    }

  

    // Update is called once per frame


    public void closeUI()
    {
        gameObject.SetActive(false);
    }
}
