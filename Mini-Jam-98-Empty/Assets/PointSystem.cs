using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PointSystem : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI text;

    private int currentPoints = 0;

    [SerializeField]
    private float changeInterval;

    public static PointSystem instance;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        text.text = "0";
    }

 

    public void AddPoints(int value)
    {
        StartCoroutine("PointAnimation", value);
    }

    private IEnumerator PointAnimation(int addValue)
    {
        int oldPoints = currentPoints;
        int newPoints = oldPoints + addValue;

        for (int i = oldPoints; i < newPoints; i++)
        {
            currentPoints++;

            text.text = "" + currentPoints;

            yield return new WaitForSeconds(changeInterval);
        }
    }

}
