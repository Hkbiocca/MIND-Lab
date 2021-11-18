using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DNAFilter : MonoBehaviour
{
    public InputField searchBar = null;
    public Transform searchableObjects;
    public List<GameObject> searchedObjects;

    void Start()
    {
        searchBar.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
        searchedObjects = new List<GameObject>();
        populateList();
    }

    public void ValueChangeCheck()
    {
        string DNA = searchBar.text;
        
        if (DNA != null)
        {
            foreach (GameObject obj in searchedObjects)
            {
                if (obj.CompareTag("Button") == true)
                {
                    if (obj.name.Contains(DNA))
                    {
                        obj.SetActive(true);
                    }
                    else
                    {
                        obj.SetActive(false);
                    }
                }
            }
        }
        else
        {
            foreach(GameObject obj in searchedObjects)
            {
                obj.SetActive(true);
            }
        }
        
    }


    private void populateList()
    {
        foreach(Transform child in searchableObjects)
        {
            if(child.gameObject.CompareTag("Button") == true)
            {
                searchedObjects.Add(child.gameObject);
            }
        }
    }
}
