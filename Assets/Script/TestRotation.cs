//using Unity.Mathematics;
using UnityEngine;

[ExecuteInEditMode]
public class TestRotation : MonoBehaviour
{
    [SerializeField] private Transform characterTarget;
    [SerializeField] private Transform propulseur;

    [SerializeField] private float verticalDeadAngle = 10f;

    private void Update()
    {
        Debug.DrawRay(characterTarget.position, characterTarget.forward, Color.cyan);
        Debug.DrawRay(characterTarget.position, propulseur.forward, Color.magenta);

        Vector3 localDir = (propulseur.position - characterTarget.position).normalized;

        float d = Vector3.Dot(localDir, characterTarget.forward);
        float facingRight = Vector3.Dot(localDir, characterTarget.right);
        float verticalAngle = Vector3.Angle(localDir, characterTarget.up);

        if (d > 0f)
        {
            Debug.DrawRay(characterTarget.position, localDir, Color.red);
            Vector3 perpendicular = Vector3.Cross(localDir, characterTarget.up);

            if (verticalAngle < verticalDeadAngle || verticalAngle > (180f - verticalDeadAngle))
            {
                //DeadZone
                Debug.DrawLine(characterTarget.position, propulseur.position, Color.green);
            }
            else if (facingRight < 0f)
            {
                Vector3 realTarget = Vector3.Lerp(-perpendicular, -characterTarget.forward, d);
                characterTarget.rotation = Quaternion.LookRotation(realTarget);
            }
            else if (facingRight > 0f)
            {
                Vector3 realTarget = Vector3.Lerp(perpendicular, -characterTarget.forward, d);
                characterTarget.rotation = Quaternion.LookRotation(realTarget);
            }

            Debug.DrawRay(characterTarget.position, perpendicular, Color.yellow);
        }
        else
        {
            //print("back prop");
            //do nothing ...
            Quaternion idleRot = new Quaternion(characterTarget.rotation.x, propulseur.rotation.y, characterTarget.rotation.z, propulseur.rotation.w) ;
            characterTarget.rotation = Quaternion.RotateTowards(characterTarget.rotation, idleRot, 0.08f   );
        }


    }
}