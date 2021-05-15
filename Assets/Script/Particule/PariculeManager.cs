using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PariculeManager : MonoBehaviour
{
    public bool emissionModifEnable = false;
    public ParticleSystem jet;
    public ParticleSystem burst;
    public ParticleSystem rng;
    public AvatarMove avMove;
    public float jetSpeedRangeMin = 15f;
    public float jetSpeedRangeMax = 25f;
    public float jetSpeedRangeMod = 2f;
    public float jetRateOverTimeMin = 500f;
    public float jetRateOverTimeMax = 1000f;
    public float jetRateOverTimeMod = 4f;
    public float burstSpeedRangeMin = 15f;
    public float burstSpeedRangeMax = 25f;
    public float burstSpeedRangeMod = 2f;
    public float rngSpeedRangeMin = 8f;
    public float rngSpeedRangeMax = 25f;
    public float rngSpeedRangeMod = 2f;
    public float rngRateOverTimeMin = 10f;
    public float rngRateOverTimeMax = 30f;
    public float rngRateOverTimeMod = 2f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (avMove.inputJet != 0)
        {
            jet.Play();
            burst.Play();
            rng.Play();
        }
        else
        {
            jet.Stop();
            burst.Stop();
            rng.Stop();
        }
        jet.startSpeed = Random.Range(jetSpeedRangeMin, jetSpeedRangeMax) * Mathf.Abs(avMove.inputJet * jetSpeedRangeMod);
        burst.startSpeed = Random.Range(burstSpeedRangeMin, burstSpeedRangeMax) * Mathf.Abs(avMove.inputJet * burstSpeedRangeMod);
        rng.startSpeed = Random.Range(rngSpeedRangeMin, rngSpeedRangeMax) * Mathf.Abs(avMove.inputJet * rngSpeedRangeMod);

        if (emissionModifEnable)
        {
            float newJetRoT = Random.Range(jetRateOverTimeMin, jetRateOverTimeMax) * Mathf.Abs(avMove.inputJet * jetRateOverTimeMod);
            var jetEmission = jet.emission;
            jetEmission.rateOverTime = newJetRoT;

            float newRndRoT = Random.Range(rngRateOverTimeMin, rngRateOverTimeMax) * Mathf.Abs(avMove.inputJet * rngRateOverTimeMod);
            var rndEmission = jet.emission;
            rndEmission.rateOverTime = newRndRoT;
        }
    }
}
