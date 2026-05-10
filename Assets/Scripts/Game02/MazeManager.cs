using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeManager : MonoBehaviour
{
    void Start()
    {
        RandomizeMaze();
    }

    void RandomizeMaze()
    {
        int childCount = transform.childCount;

        if (childCount == 0) return;

        int randomIndex = Random.Range(0, childCount);

        for (int i = 0; i < childCount; i++)
        {
            GameObject child = transform.GetChild(i).gameObject;

            if (i == randomIndex)
            {
                child.SetActive(true);
            }
            else
            {
                child.SetActive(false);
            }
        }
    }
}