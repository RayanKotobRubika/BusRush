using System;
using UnityEngine;

public class TestThings : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(CoroutineUtils.FloatObject(transform, 0.5f, 2f, 0.1f));
    }

    
}
