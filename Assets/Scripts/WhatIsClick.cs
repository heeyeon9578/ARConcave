using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class WhatIsClick : MonoBehaviour
{
    [SerializeField] List<Button> list = new List<Button>();        
    [SerializeField] GameObject black;
    [SerializeField] GameObject white;
    bool whatColor=false;

    private void Update()
    {
        
    }
   /* private void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            int temp = i;
            transform.GetChild(temp).GetComponent<Button>().onClick.AddListener(() => ClickBtn(temp));
            Debug.Log(temp);
        }

    }*/
    // ��ư�� ������ �� ȣ��� �Լ�
    public void ClickBtn(int i)
    {

        if (whatColor)
        {
            Instantiate(black, list[i].transform);
            whatColor = false;
        }
        else
        {
            Instantiate(white, list[i].transform);
            whatColor = true;
        }
    }
}
