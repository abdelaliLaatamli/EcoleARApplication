using System.Collections;
using System.Collections.Generic;
using UnityEngine;


using UnityEngine.XR.ARFoundation;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SceneManager : MonoBehaviour
{
    public Transform placementIndicator;
    public GameObject selectionUI;
    public GameObject fournitureUI;
    public GameObject multipleObjects;


    private GameObject ObjectSpawned;
    private GameObject selectedObject;

    


    private Transform tempTransform;

    private string tagSelected = "SelectionDetected";


    // Start is called before the first frame update
    void Start()
    {

        selectionUI.SetActive(false);

        ObjectSpawned = null;
        selectedObject = null;
        tempTransform = null;

    }



    public void PlaceObject(GameObject prefab)
    {


        if (ObjectSpawned != null) tempTransform = ObjectSpawned.transform;

        DeleteObject();


        ObjectsToChoose o = prefab.GetComponentInChildren<ObjectsToChoose>();
        if (o.objects.Length >= 1)
        {
            fournitureUI.SetActive(true);
            CanvaFornitureShower shower = fournitureUI.GetComponentInChildren<CanvaFornitureShower>();
            shower.setData(o.objects[0]);
        }
        
     
        
        GameObject obj;
        if (tempTransform == null)
        {
            obj = Instantiate(prefab, placementIndicator.position, Quaternion.identity);
        }
        else
        {
            obj = Instantiate(prefab, tempTransform.position, tempTransform.rotation);
            obj.transform.localScale = tempTransform.localScale;
            tempTransform = null;
        }


        ObjectSpawned = obj;
        selectionUI.SetActive(true);

    }


    /*
    public void PlaceObject(GameObject prefab)
    {


        if (ObjectSpawned != null) tempTransform = ObjectSpawned.transform;

        DeleteObject();


        GameObject obj;
        if (tempTransform == null)
        {
            obj = Instantiate(prefab, placementIndicator.position, Quaternion.identity);
        }
        else
        {
            obj = Instantiate(prefab, tempTransform.position, tempTransform.rotation);
            obj.transform.localScale = tempTransform.localScale;
            tempTransform = null;
        }


        ObjectSpawned = obj;
        selectionUI.SetActive(true);

    }
    */



    public void ScaleObject(float rate)
    {
        ObjectSpawned.transform.localScale += Vector3.one * rate;
    }

    public void RotateObject(float rate)
    {
        ObjectSpawned.transform.eulerAngles += Vector3.up * rate;
    }


    public void DeleteObject()
    {
        if (ObjectSpawned != null) Destroy(ObjectSpawned);
        ObjectSpawned = null;

    }

    public void openSelected ()
    {
        ObjectsToChoose o = selectedObject.GetComponentInChildren<ObjectsToChoose>();
        if( o.objects.Length == 1 )
        {
            fournitureUI.SetActive(true);
            CanvaFornitureShower shower = fournitureUI.GetComponentInChildren<CanvaFornitureShower>();
            shower.setData(o.objects[0]);

        } else if (o.objects.Length == 2)
        {
            MultipleObjectSelected shower = multipleObjects.GetComponentInChildren<MultipleObjectSelected>();
            shower.setData(o.objects);
            this.multipleObjects.SetActive(true);
        }

    }

    void Select(GameObject selected)
    {

        if (selectedObject != null)
            ToggleSelectionVisual(selectedObject, false);

        selectedObject = selected;
        ToggleSelectionVisual(selectedObject, true);

    }

    void ToggleSelectionVisual(GameObject obj, bool toggle)
    {
        obj.transform.Find("Selected").gameObject.SetActive(toggle);
    }

    // Update is called once per frame
    void Update()
    {



        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began && !EventSystem.current.IsPointerOverGameObject(Input.touches[0].fingerId))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.touches[0].position);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject != null && hit.collider.gameObject.tag == tagSelected)
                {
                    Select(hit.collider.gameObject);
                }
            }
        }

    }
}
