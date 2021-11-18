using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PhotonSpawner : MonoBehaviourPun
{
    [SerializeField] private Transform roverExplorerLocation = default;
    private GameObject destruct;

    private GameObject SpawnedDNA = null;
    public List<GameObject> Layers = new List<GameObject>();

    private void Awake()
    {
        roverExplorerLocation = GameObject.Find("Table").transform;
        destruct = GameObject.Find("Canvas(Clone)");


    }

    
    public void CreateInteractableObjects(string DnaToSpawn)
    {
        var position = roverExplorerLocation.position;
        var positionOnTopOfSurface = new Vector3(position.x, position.y + roverExplorerLocation.localScale.y / 2,
            position.z);
        var go = PhotonNetwork.Instantiate(DnaToSpawn, positionOnTopOfSurface,
            roverExplorerLocation.rotation);
        destroyInstantiation.SpawnedDNA.Add(go);

        foreach (Transform child in go.transform)
        {
            Layers.Add(child.gameObject);
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
}
