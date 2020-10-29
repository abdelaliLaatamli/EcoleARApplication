using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CanvaFornitureShower : MonoBehaviour
{

    // private SelectedObject data;


    public Image image;
    public TextMeshProUGUI title;
    public TextMeshProUGUI sentence;
    


    // Start is called before the first frame update
    public void setData( SelectedObject data )
    {
        
        image.sprite  = data.imageObject;
        title.text    = data.title.getString("fr");
        sentence.text = data.sentence[0].getString("fr");

    }

    // Update is called once per frame
    
   
    public void closeUI()
    {
        gameObject.SetActive(false);
    }
}
