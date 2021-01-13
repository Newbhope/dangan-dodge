using Photon.Pun;
using UnityEngine;

public class BulletDespawner : MonoBehaviour
{
    void OnTriggerExit2D(Collider2D other)
    {
        PhotonView bulletPhotonView = other.gameObject.GetPhotonView();

        if (other != null && other.tag == "Bullet" && PhotonNetwork.IsMasterClient && bulletPhotonView.IsMine)
        {
            PhotonNetwork.Destroy(other.gameObject);
        }
    }
}
