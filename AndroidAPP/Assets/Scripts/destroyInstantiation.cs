using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class destroyInstantiation : MonoBehaviourPun
{
    public static List<GameObject> SpawnedDNA = new List<GameObject>();

    public void PunDestroyedSpawned()
    {
        photonView.RPC("DestroySpawned", RpcTarget.All);
    }


    private void Start()
    {

    }

    [PunRPC]
    public void DestroySpawned()
    {
        foreach (GameObject go in SpawnedDNA)
        {
            int ID = go.GetComponent<PhotonView>().ViewID;
            PhotonView.Find(ID).gameObject.SetActive(false);
        }
        SpawnedDNA.Clear();
        LayerCreation.Layers.Clear();
        LayerCreation.Buttons.Clear();

    }
}
