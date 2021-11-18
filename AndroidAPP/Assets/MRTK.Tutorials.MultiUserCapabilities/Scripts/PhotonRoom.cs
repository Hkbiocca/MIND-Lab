using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

namespace MRTK.Tutorials.MultiUserCapabilities
{
    public class PhotonRoom : MonoBehaviourPunCallbacks, IInRoomCallbacks
    {
        public static PhotonRoom Room;

        [SerializeField] private GameObject photonUserPrefab = default;
        [SerializeField] private GameObject roverExplorerPrefab = default;
        [SerializeField] private Transform roverExplorerLocation = default;

        private GameObject[] characterList;
        public GameObject SyncedCanvas;
        public GameObject destruct;
        destroyInstantiation destroyScript;

        // private PhotonView pv;
        private Player[] photonPlayers;
        private int playersInRoom;
        private int myNumberInRoom;
        private int index;

        // private GameObject module;
        // private Vector3 moduleLocation = Vector3.zero;


        public override void OnPlayerEnteredRoom(Player newPlayer)
        {
            base.OnPlayerEnteredRoom(newPlayer);
            photonPlayers = PhotonNetwork.PlayerList;
            playersInRoom++;
        }

        private void Awake()
        {
            if (Room == null)
            {
                Room = this;
            }
            else
            {
                if (Room != this)
                {
                    Destroy(Room.gameObject);
                    Room = this;
                }
            }
            destruct = GameObject.Find("Canvas");
            
        }

        public override void OnEnable()
        {
            base.OnEnable();
            PhotonNetwork.AddCallbackTarget(this);
        }

        public override void OnDisable()
        {
            base.OnDisable();
            PhotonNetwork.RemoveCallbackTarget(this);
        }

        private void Start()
        {
            // pv = GetComponent<PhotonView>();

            // Allow prefabs not in a Resources folder
            if (PhotonNetwork.PrefabPool is DefaultPool pool)
            {
                if (photonUserPrefab != null) pool.ResourceCache.Add(photonUserPrefab.name, photonUserPrefab);

                if (roverExplorerPrefab != null) pool.ResourceCache.Add(roverExplorerPrefab.name, roverExplorerPrefab);
            }

            StartGame();
        }

        public override void OnJoinedRoom()
        {
            base.OnJoinedRoom();

            photonPlayers = PhotonNetwork.PlayerList;
            playersInRoom = photonPlayers.Length;
            myNumberInRoom = playersInRoom;
            PhotonNetwork.NickName = myNumberInRoom.ToString();

            StartGame();
        }

        private void StartGame()
        {
            CreatePlayer();
            SpawnCanvas();
            if (!PhotonNetwork.IsMasterClient) return;

            //if (TableAnchor.Instance != null) CreateInteractableObjects();
        }

        private void CreatePlayer()
        {
            var player = PhotonNetwork.Instantiate(photonUserPrefab.name, Vector3.zero, Quaternion.identity);
        }

        private void SpawnCanvas()
        {
            if (GameObject.Find("Canvas(Clone)") == null)
            {
                var Canvas = Instantiate(SyncedCanvas, Vector3.zero, Quaternion.identity);                
            }
        }

        public void CreateInteractableObjects(GameObject DNAPrefab)
        {
            var position = roverExplorerLocation.position;
            var positionOnTopOfSurface = new Vector3(position.x, position.y + roverExplorerLocation.localScale.y / 2,
                position.z);

            var go = PhotonNetwork.Instantiate(DNAPrefab.name, positionOnTopOfSurface,
                roverExplorerLocation.rotation);
            LayerCreation.viewIDs = go.GetInstanceID();
            destroyInstantiation.SpawnedDNA.Add(go);

            foreach(Transform child in go.transform)
            {
                LayerCreation.Layers.Add(child.gameObject);
            }
          
        
        }


        // private void CreateMainLunarModule()
        // {
        //     module = PhotonNetwork.Instantiate(roverExplorerPrefab.name, Vector3.zero, Quaternion.identity);
        //     pv.RPC("Rpc_SetModuleParent", RpcTarget.AllBuffered);
        // }
        //
        // [PunRPC]
        // private void Rpc_SetModuleParent()
        // {
        //     Debug.Log("Rpc_SetModuleParent- RPC Called");
        //     module.transform.parent = TableAnchor.Instance.transform;
        //     module.transform.localPosition = moduleLocation;
        // }
    }
}
