using Photon.Pun;
using UnityEngine;

namespace MRTK.Tutorials.MultiUserCapabilities
{
    public class GenericNetSync : MonoBehaviourPun, IPunObservable
    {
        [SerializeField] private bool isUser = default;

        private Camera mainCamera;

        private Vector3 networkLocalPosition;
        private Quaternion networkLocalRotation;
        private Vector3 networkScale;

        private Vector3 startingLocalPosition;
        private Quaternion startingLocalRotation;
        private Vector3 startingScale;

        void IPunObservable.OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.IsWriting)
            {
                stream.SendNext(transform.localPosition);
                stream.SendNext(transform.localRotation);
                stream.SendNext(transform.localScale);
            }
            else
            {
                networkLocalPosition = (Vector3) stream.ReceiveNext();
                networkLocalRotation = (Quaternion) stream.ReceiveNext();
                networkScale = (Vector3) stream.ReceiveNext();
            }
        }

        private void Start()
        {
            mainCamera = Camera.main;

            if (isUser)
            {
                if (TableAnchor.Instance != null) transform.parent = FindObjectOfType<TableAnchor>().transform;

                if (photonView.IsMine) GenericNetworkManager.Instance.localUser = photonView;
            }

            var trans = transform;
            startingLocalPosition = trans.localPosition;
            startingLocalRotation = trans.localRotation;
            startingScale = trans.localScale;

            networkLocalPosition = startingLocalPosition;
            networkLocalRotation = startingLocalRotation;
            networkScale = startingScale;
        }

        // private void FixedUpdate()
        private void Update()
        {
            if (!photonView.IsMine)
            {
                var trans = transform;
                trans.localPosition = networkLocalPosition;
                trans.localRotation = networkLocalRotation;
                trans.localScale = networkScale;
            }

            if (photonView.IsMine && isUser)
            {
                var trans = transform;
                var mainCameraTransform = mainCamera.transform;
                trans.position = mainCameraTransform.position;
                trans.rotation = mainCameraTransform.rotation;
                trans.localScale = mainCameraTransform.localScale;
            }
        }
    }
}
