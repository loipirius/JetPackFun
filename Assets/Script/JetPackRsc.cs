using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JetPackRsc : MonoBehaviour
{
    public float energy = 100;
    public float maxEnergy = 100;
    public float energyRegen = 2;
    public float energyCost = 1;

    float inputJet;

    public EnergyBar energyBar;
    public AvatarMove spt_AvatarMove;

    void Update()
    {
        if (!spt_AvatarMove.OnMove && energy < maxEnergy && spt_AvatarMove.Grounded)
        {
            energy += energyRegen * Time.deltaTime;
            energyBar.SetEnergy(energy, maxEnergy);
        }

        inputJet = Input.GetAxis("Jet");
        if (inputJet != 0 && energy > 0 && !spt_AvatarMove.Grounded)
        {
            energy -= energyCost * inputJet * Time.deltaTime;
            energyBar.SetEnergy(energy, maxEnergy);
        }

        
    }
}
