using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class prefabList : MonoBehaviour
{
    public GameObject DNA;
    private GameObject[] characterList;
    private int index;

    // Start is called before the first frame update
    void Start()
    {
        //fill arrray with models
        for(int i = 0; i < transform.childCount; i++)
        {
            characterList[i] = DNA.transform.GetChild(i).gameObject;
        }

        //toggle renderer
        foreach(GameObject go in characterList)
        {
            go.SetActive(false);
        }

        //toggle first index
        if(characterList[0])
        {
            characterList[0].SetActive(true);
        }
    }


    public void toggleLeft()
    {
        //toggle off current model
        characterList[index].SetActive(false);
        index--;
        if(index < 0)
        {
            index = characterList.Length - 1;
        }

        //toggle on new model
        characterList[index].SetActive(true);

        Debug.Log("toggle left");
    }

    public void toggleRight()
    {
        //toggle off current model
        characterList[index].SetActive(false);
        index++;
        if (index == characterList.Length)
        {
            index = 0;
        }

        //toggle on new model
        characterList[index].SetActive(true);

        Debug.Log("toggle right");
    }


}
