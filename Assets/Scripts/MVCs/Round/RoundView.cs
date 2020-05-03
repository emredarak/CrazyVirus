using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoundView : MonoBehaviour
{
    [SerializeField] private Text _roundText;
    public RoundModel Model;

    internal void OnRoundFinish()
    {
        _roundText.text = "Round " + Model.CurrentRound;
    }
}
