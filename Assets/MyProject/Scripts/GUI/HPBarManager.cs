using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
class HPBarManager : MonoBehaviour
{
    private AllyShip playerShip;
    private Image hpBar;

    private void Start()
    {
        hpBar = GetComponent<Image>();
        playerShip = GameObject.Find("AllyShip").GetComponent<AllyShip>();
    }

    private void Update()
    {
        hpBar.fillAmount = (float)playerShip.HP / (float)playerShip.MaxHP;
    }
}
