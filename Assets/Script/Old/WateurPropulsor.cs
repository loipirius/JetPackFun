using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WateurPropulsor : MonoBehaviour
{
    public float power = 0.1f;
    public float maxScaleX = 5f;
    public float maxScaleY = 5f;
    public float maxScaleZ = 100f;
    public float propPower = 10f;
    TrailRenderer trail;
    Collider col;
    // Start is called before the first frame update
    void Start()
    {
        trail = GetComponent<TrailRenderer>();
        col = GetComponent<BoxCollider>();
        power += 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Jump"))
        {
            trail.enabled = true;
            if(transform.localScale.x < maxScaleX)
            {
                transform.localScale *= power;
            }
            if(transform.localScale.x < maxScaleX)
            {
                //print("Z up");
                Vector3 desiredScale = transform.localScale * power;
                desiredScale.z = maxScaleZ;
                desiredScale.y = maxScaleY;
                transform.localScale = desiredScale;
            }
            Vector3 desiedPos = transform.localScale / 2;
            desiedPos.z = 0;
            desiedPos.y = 0; 
            transform.localPosition = desiedPos;
        }
        else
        {
            trail.enabled = false;
            if (transform.localScale.x > 1)
            {
                transform.localScale /= power;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        other.gameObject.TryGetComponent<Rigidbody>(out Rigidbody otherRb);
        otherRb.AddForce(-Vector3.forward * power, ForceMode.Impulse);
    }
}
