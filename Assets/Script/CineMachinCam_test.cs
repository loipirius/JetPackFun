using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


public class CineMachinCam_test : MonoBehaviour
{
    public GameObject avatar;
    Rigidbody avRB;
    public float maxVelo = 60;
    public float minVelo = 20;
    public float maxFov = 70;
    public float minFov = 40;

    CinemachineFreeLook fov;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        avRB = avatar.GetComponent<Rigidbody>();
        fov = GetComponent<CinemachineFreeLook>();
        fov.m_CommonLens = true;
    }

    // Update is called once per frame
    void Update()
    {    
        if (avRB.velocity.magnitude >= minVelo && avRB.velocity.magnitude <maxVelo)
        {
            //print("velocity: " + avRB.velocity.magnitude);
            float fovMod = (avRB.velocity.magnitude - minVelo)/(maxVelo - minVelo);
            Mathf.Clamp(fovMod, 0, 1);
            float res = Mathf.Lerp(minFov, maxFov, fovMod);
            //print("fov: " + res);
            fov.m_Lens.FieldOfView = res;
        }

        /*print("RS X: " + Input.GetAxis("Right Stick X"));
        print("RS Y: " + Input.GetAxis("Right Stick Y"));
        fov.m_XAxis.Value = Input.GetAxis("Right Stick X");
        fov.m_YAxis.Value = Input.GetAxis("Right Stick Y");*/
    }
}
