using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManagerSound : MonoBehaviour
{
    [SerializeField] private GameObject OnSound;
    [SerializeField] private GameObject OffSound;
    void Start()
    {
        
    }

    public void Onsounds()
    {
        OnSound.SetActive(true);
        OffSound.SetActive(false);
    }
    public void Offsounds()
    {
        OnSound.SetActive(false);
        OffSound.SetActive(true);
    }
}
