using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class LayerCreation : MonoBehaviourPun
{
    public GameObject LayerSystem;
    private GameObject Canvas;
    public static GameObject Button;
    public static List<GameObject> Layers = new List<GameObject>();
    public static List<GameObject> Buttons = new List<GameObject>();
    public GameObject destruct;
    public static int viewIDs;
    public int viewIDTest = 0;

    private void Start()
    {
        
        Canvas = GameObject.Find("Canvas(Clone)");      
        var go = GameObject.Find("updatedDNA(Clone)");
        


        if (go != null)
        {
            foreach (Transform child in go.transform)
            {
                Layers.Add(child.gameObject);
            }
        }

    }




    public void PunHideLayer(int child)
    {
        photonView.RPC("HideLayer", RpcTarget.All, child);
    }

    public void PunShowLayer(int child)
    {
        photonView.RPC("ShowLayer", RpcTarget.All, child);
    }


    [PunRPC]
    public void HideLayer(int child)
    {

        if (Layers != null)
        {
            int ID = Layers[child].GetComponent<PhotonView>().ViewID;
            PhotonView.Find(ID).gameObject.SetActive(false);
        }

    }

    [PunRPC]
    public void ShowLayer(int child)
    {
        if (Layers != null)
        {
            int ID = Layers[child].GetComponent<PhotonView>().ViewID;
            PhotonView.Find(ID).gameObject.SetActive(true);
        }

    }


    public void PunCreateLayerCanvas(string button)
    {
        photonView.RPC("CreateLayerCanvas", RpcTarget.AllBuffered, button);
    }

    public void PunShowButton(int ButtonToShow)
    {
        photonView.RPC("ShowButton", RpcTarget.AllBuffered, ButtonToShow);
    }

    public void PunHideButton(int ButtonToHide)
    {
        photonView.RPC("HideButton", RpcTarget.AllBuffered, ButtonToHide);
    }


    [PunRPC]
    public void CreateLayerCanvas(string button)
    {
        var go = PhotonNetwork.Instantiate(button, Canvas.transform.position, Canvas.transform.rotation);
        go.transform.SetParent(Canvas.transform);
        go.transform.localScale = new Vector3(1, 1, 1);
        destroyInstantiation.SpawnedDNA.Add(go);
        destroyInstantiation.SpawnedDNA.Add(GameObject.Find("updatedDNA(Clone)"));
        GameObject panel = go.transform.Find("Panel").gameObject;
        foreach(Transform child in panel.transform)
        {
            Buttons.Add(child.gameObject);
        }
    }


    [PunRPC]
    public void ShowButton(int ButtonToShow)
    {
        int ID = Buttons[ButtonToShow].GetComponent<PhotonView>().ViewID;
        PhotonView.Find(ID).gameObject.SetActive(true);
    }


    [PunRPC]
    public void HideButton(int ButtonToHide)
    {        
        int ID = Buttons[ButtonToHide].GetComponent<PhotonView>().ViewID;
        PhotonView.Find(ID).gameObject.SetActive(false);
    }




}
