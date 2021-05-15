using UnityEngine;
using UnityEngine.UI;

public class EnergyBar : MonoBehaviour
{
    public Image sliderCircle;
    Color baseColor;
    Color idleColor;

    float idle = 0f;
    private void Start()
    {
        baseColor = sliderCircle.color;
        idleColor = baseColor;
        idleColor.a = 0;
    }

    private void Update()
    {
        idle += Time.deltaTime;
        if (idle > 2 && sliderCircle.fillAmount == 1)
        {
            sliderCircle.color = Color.Lerp(baseColor, idleColor, idle-2);
        }
    }

    public void SetEnergy(float energy, float maxEnergy)
    {
        sliderCircle.fillAmount = energy / maxEnergy;
        sliderCircle.color = baseColor;
        idle = 0f;
    }

}
