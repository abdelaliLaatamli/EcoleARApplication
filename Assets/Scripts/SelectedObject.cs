using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[CreateAssetMenu(fileName = "new Fourniture", menuName = "Fournitures/Fourniture", order = 1)]    
public class SelectedObject : ScriptableObject
{

    public Sprite imageObject;
    // public string title;
    public langues title;
    public langues [] sentence;
    public string vedioUrl;
    public AudioSource audioData;


    [Serializable]
    public class langues
    {


        [SerializeField]
        private string fr;
        [SerializeField]
        private string en;
        [SerializeField]
        private string es;
        [SerializeField]


        public string getString( string lang )
        {
            switch( lang )
            {
                case "fr":  return this.fr;
                
                case "en": return this.en; 

                case "es": return this.es;
            
                default:  return this.fr;
            }
        }
        
    };

}
