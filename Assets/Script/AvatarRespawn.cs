using UnityEngine;

public class AvatarRespawn : MonoBehaviour
{
    public Transform spawnPoint;
    private Transform avatarPos;

    public float respawnHeight = -300;

    private void Start()
    {
        avatarPos = GetComponent<Transform>();
        spawnPoint = avatarPos;
    }

    private void Update()
    {
        if (avatarPos.position.y < respawnHeight)
        {
            avatarPos.position = spawnPoint.position;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("SavePoint"))
        {
            spawnPoint = other.transform;
        }
    }
}
