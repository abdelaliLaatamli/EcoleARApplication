using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MultipleObjectSelected : MonoBehaviour
{

    public GameObject fournitureUI;

    public Image image1;
    public TextMeshProUGUI title1;

    public Image image2;
    public TextMeshProUGUI title2;

    SelectedObject[] data;


    public void setData(SelectedObject [] data)
    {
        this.data = data;
        image1.sprite = this.data[0].imageObject;
        title1.text = this.data[0].title.getString("fr");

        image2.sprite = this.data[1].imageObject;
        title2.text = this.data[1].title.getString("fr");

    }


    public void showSelected(int index)
    {
        
        CanvaFornitureShower shower = fournitureUI.GetComponentInChildren<CanvaFornitureShower>();
        shower.setData(this.data[index]);
        gameObject.SetActive(false);
        fournitureUI.SetActive(true);
    }

    /*
    public void closeUI()
    {
        gameObject.SetActive(false);
    }
    */
}
