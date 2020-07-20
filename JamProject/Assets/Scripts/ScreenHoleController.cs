using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenHoleController : MonoBehaviour
{
    [SerializeField] Image energyBar;
    [SerializeField] Image mask;
    [SerializeField] KeyCode firstPlayerKey;
    [SerializeField] KeyCode secondPlayerKey;
    [SerializeField] float timeToCloseHole;
    [SerializeField] float energyConsumedByPlayers;
    [SerializeField] float maxEnergyAmount;
    [SerializeField] [Range(0f, 1f)] float holeMinRadiusPercentage;
    [SerializeField] float timeBeforeHoleCloses;
    [SerializeField] float speedIncrementAmount;
    [SerializeField] float timeToRestartShrinking;
    [SerializeField] TileSetManager tsManager;

    float timer = 0f;
    float counter = 1f;
    float maskSize = 1024f;
    static float staticEnergy;
    static float currentEnergy;
    bool changeSpeed = false;
    static bool isShrinking = true;

    [HideInInspector] public bool CloseHole = true;

    private void Start()
    {
        staticEnergy = maxEnergyAmount;
        currentEnergy = maxEnergyAmount;
    }

    public static bool AddEnergy(float amount)
    {
        if (!isShrinking) return false;
        currentEnergy += amount;
        if (currentEnergy >= staticEnergy)
        {
            currentEnergy = staticEnergy;
        }
        return true;
    }
    
    void Update()
    {
        if (!CloseHole) return;
        if (timer < timeBeforeHoleCloses)
        {
            timer += Time.deltaTime;
            return;
        }

        staticEnergy = maxEnergyAmount;
        float ratio = currentEnergy / maxEnergyAmount;
        float holeRatio = 1 - holeMinRadiusPercentage;

        if (Input.GetKey(firstPlayerKey) && Input.GetKey(secondPlayerKey) && ratio >= 1f)
        {
            isShrinking = false;
        }

        if (isShrinking)
        {
            counter -= Time.deltaTime / timeToCloseHole * holeRatio;
        }
        else
        {
            currentEnergy -= Time.deltaTime * energyConsumedByPlayers;
            counter += Time.deltaTime / timeToCloseHole * holeRatio;

            if (currentEnergy <= 0f)
            {
                isShrinking = true;
                timer = 0f;
                timeBeforeHoleCloses = timeToRestartShrinking;
                tsManager.IncreaseSpeed(speedIncrementAmount);
            }
        }

        // mask
        counter = Mathf.Clamp(counter, holeMinRadiusPercentage, 1);
        mask.rectTransform.sizeDelta = Vector2.one * (maskSize * counter);

        // energy bar
        currentEnergy = Mathf.Clamp(currentEnergy, 0, maxEnergyAmount);
        Vector3 size = energyBar.rectTransform.localScale;
        size.y = Mathf.Lerp(0, 1, ratio);
        energyBar.rectTransform.localScale = size;
    }
}
