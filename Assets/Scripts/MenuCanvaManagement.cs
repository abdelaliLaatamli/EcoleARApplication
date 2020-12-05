using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;


public class MenuCanvaManagement : MonoBehaviour
{

    public TextMeshProUGUI langage;
    public GameManagement gameManagement;

    void Start()
    {
        
        this.onLangaugeChange("fr");
        //Debug.Log(this.gameManagement.langage);
    }

    public void onLangaugeChange(string langage)
    {
        this.langage.text = "Current Languege : " + langage.ToUpper();
        this.gameManagement.langage = langage;
    }


    public void onStartGame()
    {
        //SceneManager.LoadScene(1);
        //SceneManager.LoadScene(1);
        //SceneManager.loadScene(1);
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }
    
}
