using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.HID;

public class BlueSchmove : MonoBehaviour
{
    [SerializeField] GameObject sticky;
    [SerializeField] Transform shootingPoint;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Activate()
    {
        GameObject stickyParent = Instantiate(sticky, shootingPoint.position, Quaternion.identity);
        stickyParent.transform.GetChild(0).GetComponent<StickyMechanics>().setActive(shootingPoint, stickyParent);
    }
}
