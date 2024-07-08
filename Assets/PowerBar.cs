using UnityEngine;
using UnityEngine.UI;

public class PowerBar : MonoBehaviour
{
    public Player player;

    private Slider bar;

    private void Start()
    {
        bar = GetComponent<Slider>();
    }

    void Update()
    {
        bar.value = player.CurrentPower;
        bar.maxValue = player.maxPower;
    }
}
