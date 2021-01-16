using Photon.Pun;
using UnityEngine;

public class BaseBulletVariables : MonoBehaviour, IPunInstantiateMagicCallback
{
    public int ownerPlayerId;

    void IPunInstantiateMagicCallback.OnPhotonInstantiate(PhotonMessageInfo info)
    {
        object[] instantiationData = info.photonView.InstantiationData;
        ownerPlayerId = (int) instantiationData[0];

        if (ownerPlayerId == 1)
        {
            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.color = new Color(0, 235, 255);
        }
    }
}
