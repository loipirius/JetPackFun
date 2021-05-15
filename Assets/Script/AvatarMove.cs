using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarMove : MonoBehaviour
{
    float inputX;   //valeur de l'input sur l'axe Y World (-1; 1); influe sur le propulseur
    float inputY;   //valeur de l'input sur l'axe X World (-1; 1); influe sur le propulseur
     public float inputJet; //valeur de l'input qui commande la propulsion du jet (0; 1)
    float horizontal;   //valeur réel de l'axe X
    float vertical; //valeur réel de l'axe Y
    [SerializeField] bool grounded = false;  //est ce que le joueur est sur le sol
    bool onMove = false;    //est ce que le joueur est en mouvement
    

    public float veloDebug; //valeur de vélocité actuel de l'avatar (ne sert qu'au debug)
    public float maxY = 10000;  //hauteur maximal que le joueur peut atteindre;
    public float groundTreshHold = 0.5f;    //valeur de normal à laquelle la surface est considéré comme du sol
    public float maxVelocity = 75f; //vitesse max que l'avatar peut atteindre
    public float moveSpeed = 1000f;   //vitesse de déplacement de l'avatar
    public float propRotateSpeed = 150f;    //modificateur de la vitesse de rotation du propulseur
    public float fallSpeedMod = 200; //vitesse qui pousse l'avatar vers le bas lorsqu'il ne se propulse pas (gravity)
    public float maxFriction = 1.2f;

    Quaternion rotation;    //rotation du pivot du propulseur
    Vector3 desiredMove = new Vector3();    //vecteur vers lequel le joueur est propulsé
    GameObject curGround;   //objet servant actuellement de sol
    Rigidbody rb;   //rigidbody de l'avatar
    PhysicMaterial phyMat;  //physicMaterial appliquer à l'avatar

    public Transform pivot; //pivot du propulseur
    public Transform propuplseur;   //propulseur
    public JetPackRsc spt_jetPackRsc;
    public bool Grounded { get => grounded; set => grounded = value; }
    public bool OnMove { get => onMove; set => onMove = value; }

    public float frc;

    public float rotationReduc;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        phyMat = GetComponent<Collider>().material;
    }

    void Update()
    {
        ToMaxHeight();
        if(grounded && phyMat.staticFriction < maxFriction)
        {
            phyMat.staticFriction += Time.deltaTime;
            phyMat.dynamicFriction += Time.deltaTime;
        }
        frc = phyMat.staticFriction;
    }

    private void FixedUpdate()
    {
        JetPropulsion();
        MovePropulsor();
        CustomGravity();
        ClampMagnitude();
        DebugDirection();
        ChangeDirection();
    }

    void ToMaxHeight()
    {
        if (Mathf.Clamp(transform.position.y, 0f, maxY) >= maxY)
        {
            float dif = transform.position.y - maxY;
            Vector3 skyLimit = transform.position;
            skyLimit.y -= dif;
            transform.position = skyLimit;
        }
    }
    void MovePropulsor()
    {
        inputX = Input.GetAxis("Horizontal");
        inputY = Input.GetAxis("Vertical");
        if (inputX != 0 || inputY != 0)
        {
            horizontal -= inputY;
            vertical -= inputX;
            rotation = Quaternion.Euler(horizontal * propRotateSpeed  * Time.deltaTime, vertical * propRotateSpeed * Time.deltaTime, 0);
            pivot.rotation = rotation;
        } 
    }
    void JetPropulsion()
    {
        desiredMove = propuplseur.position - transform.position;
        Vector3.Normalize(desiredMove);

        inputJet = Input.GetAxis("Jet");
        if (inputJet != 0 && spt_jetPackRsc.energy > 0)
        {
            onMove = true;
            phyMat.staticFriction = 0;
            phyMat.dynamicFriction = 0;
            rb.AddForce(-desiredMove * moveSpeed * inputJet * Time.deltaTime);
        }
        else
        {
            onMove = false;
        }
    }
    void CustomGravity()
    {
        if (!onMove && !grounded)
        {

            rb.AddForce(-Vector3.up * fallSpeedMod * Time.deltaTime, ForceMode.Impulse);
        }
    }
    void ClampMagnitude()
    {
        var vel = rb.velocity;
        if (vel.sqrMagnitude > maxVelocity * maxVelocity)
            rb.velocity = vel.normalized * maxVelocity;
    }
    void DebugDirection()
    {
        veloDebug = rb.velocity.magnitude;
        Debug.DrawRay(transform.position, rb.velocity);
        Debug.DrawRay(transform.position, desiredMove);
    }

    void ChangeDirection()
    {
        
        if(((desiredMove.z > 0 && rb.velocity.z > 0) || (desiredMove.z < 0 && rb.velocity.z < 0)) && inputJet != 0)
        {
            //Debug.Log("a");
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, -rb.velocity.z/rotationReduc);
        }

        if (((desiredMove.x > 0 && rb.velocity.x > 0) || (desiredMove.x < 0 && rb.velocity.x < 0)) && inputJet != 0)
        {
            //Debug.Log("o");
            rb.velocity = new Vector3(-rb.velocity.x/rotationReduc, rb.velocity.y, rb.velocity.z);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        ContactPoint contact = collision.GetContact(0);
        if (contact.normal.y >= groundTreshHold)
        {
            grounded = true;
            curGround = collision.gameObject;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (curGround == collision.gameObject)
        {
            grounded = false;
            curGround = null;
        }
    }
}
