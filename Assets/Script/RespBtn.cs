using UnityEngine;

public class RespBtn : MonoBehaviour
{
    private AvatarRespawn ar;
    private Transform avatar;

    public Transform demoSpawn;

    private void Start()
    {
        ar = GetComponent<AvatarRespawn>();
        avatar = GetComponent<Transform>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            //avatar.position = ar.spawnPoint.position;
            avatar.position = demoSpawn.position;
        }
    }
}
