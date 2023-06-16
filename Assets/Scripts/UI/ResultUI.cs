using System;
using SaveSystem;
using TMPro;
using UnityEngine;

namespace UI
{
    public class ResultUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI winsScore;
        [SerializeField] private TextMeshProUGUI loseScore;

        private void OnEnable()
        {
            var winsAndLoses = GameSavesManager.GetInstance().GetWinsAndLoses();
            winsScore.text = winsAndLoses.Wins.ToString();
            loseScore.text = winsAndLoses.Loses.ToString();
        }
    }
}