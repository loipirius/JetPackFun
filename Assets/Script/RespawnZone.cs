using UnityEngine;

public class RespawnZone : MonoBehaviour
{
    public Transform spawnPoint;
    public AvatarRespawn avatar;


    private void OnTriggerEnter(Collider other)
    {
        avatar.spawnPoint = spawnPoint;
    }
}
