using UnityEngine;

public class SnapRotation : MonoBehaviour
{
    public float matchSpeed = 10f;
    public Transform target;
    public float snapAngle = 10f;

    private void Update()
    {
        Quaternion targetRot = Quaternion.LookRotation(target.forward, target.up);

        if (Vector3.Angle(target.forward, Vector3.up) < snapAngle)
        {
            targetRot = Quaternion.LookRotation(Vector3.up, target.up);
            //Debug.Log("a");
        }

        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRot, matchSpeed * Time.deltaTime);
    }
}
