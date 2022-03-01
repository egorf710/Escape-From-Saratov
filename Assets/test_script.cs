using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class test_script : MonoBehaviour
{
    [SerializeField] private GameObject Image;

    public void boobs()
    {
        Image.SetActive(true);
        StartCoroutine(boobsCoroutine());
    }

    IEnumerator boobsCoroutine()
    {
        yield return new WaitForSeconds(3);
        Image.SetActive(false);
    }
}
