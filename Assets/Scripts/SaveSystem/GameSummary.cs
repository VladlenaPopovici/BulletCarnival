using System;
using System.Collections.Generic;
using UnityEngine;

namespace SaveSystem
{
    [Serializable]
    public class GameSummary
    {
        public string date;
        public bool isWin;

        public GameSummary()
        {
            
        }
        
        public GameSummary(bool isWin)
        {
            this.isWin = isWin;
            date = DateTime.UtcNow.ToLocalTime().ToString("dd-MM-yyyy   HH:mm:ss");
        }
    }

    [Serializable]
    public class ListOfGameSummary
    {
        public List<GameSummary> data = new();
    }

    public struct Totals
    {
        public int Loses;
        public int Wins;
    }
}