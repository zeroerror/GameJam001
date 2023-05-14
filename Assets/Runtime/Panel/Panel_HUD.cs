using System;
using UnityEngine;

public class Panel_HUD : MonoBehaviour {

    [SerializeField] GameObject[] hpArr;

    public void SetHP(int hp){

        for (int i = 0; i < hpArr.Length; i++)
        {
            hpArr[i].SetActive(i < hp);
        }
        
    }

}