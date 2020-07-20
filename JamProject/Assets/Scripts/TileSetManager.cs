using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TileSetManager : MonoBehaviour
{
    public GameObject[] prefabs;
    public float movementSpeed;
    public GameObject LastObj => lastObj;
    public float SpeedIncrementTime;
    public TextMeshProUGUI ScoreText;
    int level;
    List<GameObject> activeObjs;
    GameObject lastObj;
    Vector3 offset;
    float speedIncrement;
    float newSpeed;

    float speedIncrementCounter;
    bool canIncreaseSpeed = false;

    [HideInInspector] public bool SpawnStraightTiles;
    int straightTilesCounter = 0;

    float score;

    // Start is called before the first frame update
    void Start()
    {
        activeObjs = new List<GameObject>();
        for (int i = 0; i < 5; i++)
        {
            lastObj = Instantiate(prefabs[0], Vector3.right * 16 * i, Quaternion.identity);
            activeObjs.Add(lastObj);
        }
        level = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        foreach (GameObject obj in activeObjs)
        {
            obj.transform.Translate(new Vector3(-movementSpeed * Time.fixedDeltaTime, 0, 0));
        }
    }

    private void Update()
    {
        if (canIncreaseSpeed)
        {
            speedIncrementCounter += Time.deltaTime;

            movementSpeed = Mathf.Lerp(movementSpeed, newSpeed, speedIncrementCounter);

            if (speedIncrementCounter >= SpeedIncrementTime)
            {
                speedIncrementCounter = 0f;
                canIncreaseSpeed = false;
            }
        }
        score += movementSpeed * Time.deltaTime;
        ScoreText.text = string.Format("{0:0}", score);
    }

    public void RemoveObjFromList(GameObject obj)
    {
        activeObjs.Remove(obj);
    }

    public void IncreaseSpeed(float amount)
    {
        canIncreaseSpeed = true;
        newSpeed = movementSpeed + amount;
    }

    public void SpawnObject()
    {
        int index = 0;
        Vector3 position = lastObj.transform.position + Vector3.right * 16;

        if (SpawnStraightTiles && straightTilesCounter < 5)
        {
            lastObj = Instantiate(prefabs[index], position + offset, Quaternion.identity);
            activeObjs.Add(lastObj);
            offset = Vector3.zero;
            straightTilesCounter++;
            return;
        }
        else if (straightTilesCounter >= 5)
        {
            SpawnStraightTiles = false;
            straightTilesCounter = 0;
        }

        int[] prefabIndeces;

        switch (level)
        {
            case -2:
                prefabIndeces = new int[] { 0, 1, 3 };
                index = prefabIndeces[Random.Range(0, 3)];
                break;
            case 2:
                prefabIndeces = new int[] { 0, 2, 4 };
                index = prefabIndeces[Random.Range(0, 3)];
                break;
            default:
                index = Random.Range(0, 5);
                break;
        }

        lastObj = Instantiate(prefabs[index], position + offset, Quaternion.identity);
        activeObjs.Add(lastObj);

        switch (index)
        {
            case 1:
                level++;
                offset = Vector3.up * 4f;
                break;
            case 2:
                level--;
                offset = Vector3.down * 4f;
                break;
            default:
                offset = Vector3.zero;
                break;
        }
    }
}
