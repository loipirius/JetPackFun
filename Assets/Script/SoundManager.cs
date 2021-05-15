using System;
using System.Collections;
using System.Collections.Generic;
using FMOD;
using UnityEngine;
using FMOD.Studio;
using FMODUnity;
using Debug = UnityEngine.Debug;

public class SoundManager : MonoBehaviour
{
    public AvatarMove movement;
    public float PlayerEnergy;
    private FMOD.Studio.System Base;
    
    private FMOD.Studio.EventInstance eventAmbiance2D;
    [FMODUnity.EventRef] 
    public string ambiance2D;

    private FMOD.Studio.EventInstance eventJPChain;
    [FMODUnity.EventRef] 
    public string JP_Ensemble;
    
    private FMOD.Studio.EventInstance eventVOheight;
    [FMODUnity.EventRef] 
    public string VO_Height;
    
    private FMOD.Studio.EventInstance eventVOSpeed;
    [FMODUnity.EventRef] 
    public string VO_Speed;
    
    private FMOD.Studio.EventInstance eventJingleSimple;
    [FMODUnity.EventRef] 
    public string Collect_jingle_simple;
    
    private FMOD.Studio.EventInstance eventJingleUnlock;
    [FMODUnity.EventRef] 
    public string Collect_jingle_unlock;
    
    private FMOD.Studio.EventInstance eventLand;
    [FMODUnity.EventRef] 
    public string CL_land;
    
    private FMOD.Studio.EventInstance eventLiftUp;
    [FMODUnity.EventRef] 
    public string CL_LiftUp;

    private FMOD.Studio.EventInstance eventOutdoorsTempleMusic;
    [FMODUnity.EventRef] public string Outdoors_Temple;

    [SerializeField] private float voicelineCD = 20f;
    [SerializeField] private float voice2Timer = 20f;
    [SerializeField] private float voice1Timer = 20f;


    [SerializeField]private GameObject player;
    public Rigidbody playerRB;
    public float playerSpeed;
    public float currentFuel;
    
    void Start()
    {
        playerRB = player.GetComponent<Rigidbody>();
        Base.initialize(256, FMOD.Studio.INITFLAGS.NORMAL, FMOD.INITFLAGS.NORMAL, IntPtr.Zero);
        eventAmbiance2D = FMODUnity.RuntimeManager.CreateInstance(ambiance2D);
        eventAmbiance2D.start();
        eventOutdoorsTempleMusic = FMODUnity.RuntimeManager.CreateInstance(Outdoors_Temple);
        eventOutdoorsTempleMusic.start();
        eventVOheight = FMODUnity.RuntimeManager.CreateInstance(VO_Height);
        eventVOSpeed= FMODUnity.RuntimeManager.CreateInstance(VO_Speed);
        eventLand= FMODUnity.RuntimeManager.CreateInstance(CL_land);
        eventJingleSimple = FMODUnity.RuntimeManager.CreateInstance(Collect_jingle_simple);
        eventLiftUp = FMODUnity.RuntimeManager.CreateInstance(CL_LiftUp);

        eventJPChain = FMODUnity.RuntimeManager.CreateInstance(JP_Ensemble);
    }

    void Update()
    {
        playerSpeed = playerRB.velocity.magnitude;
        voice1Timer += Time.deltaTime;
        voice2Timer += Time.deltaTime;
        currentFuel = player.GetComponent<JetPackRsc>().energy;
        eventJPChain.setParameterByName("Fuel", currentFuel, false);
        
        if (Input.GetKeyDown(KeyCode.Space) && player.GetComponent<JetPackRsc>().energy >= 5)
        {
            eventJPChain.setParameterByName("Active", 0, false);
            eventJPChain.start();
        }

        if (Input.GetKeyUp(KeyCode.Space) || player.GetComponent<JetPackRsc>().energy <= 0)
        {
            eventJPChain.setParameterByName("Active", 1, false);
        }

        if (player.transform.position.y <= -180 && voice1Timer >= voicelineCD)
        {
            voice1Timer = 0;
            eventVOheight.start();
        }

        if (playerSpeed > movement.maxVelocity && voice2Timer > voicelineCD)
        {
            voice2Timer = 0;
            eventVOSpeed.start();
        }
        
        if (!movement.Grounded)
        {
            eventLand.start();
        }

        if (movement.Grounded)
        {
            eventLiftUp.start();
        }

        if (player.GetComponent<Collect>().touchedBlock == true)
        {
            Debug.Log("Jiggie!");
            eventJingleSimple.start();
        }
    }
}
