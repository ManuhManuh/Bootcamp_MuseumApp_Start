using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace MuseumApp
{
    public class Tapping : MonoBehaviour
    {
        public int greenTaps = 0;
        public int yellowTaps = 0;
        public int blueTaps = 0;
        public int totalTaps = 0;

        public TMP_Text beginDispaly;
        public TMP_Text middleDisplay;
        public TMP_Text endDisplay;
        public TMP_Text totalDisplay;

        public void onGreenTapped()
        {
            greenTaps = System.Int32.Parse(beginDispaly.text);
            greenTaps++;
            beginDispaly.text = greenTaps.ToString();
            IncrementTotal();
            PlayFabController.Instance.RecordButtonPressed("Green");
        }

        public void onYellowTapped()
        {
            yellowTaps = System.Int32.Parse(middleDisplay.text);
            yellowTaps++;
            middleDisplay.text = yellowTaps.ToString();
            IncrementTotal();
            PlayFabController.Instance.RecordButtonPressed("Yellow");
        }

        public void onBlueTapped()
        {
            blueTaps = System.Int32.Parse(endDisplay.text);
            blueTaps++;
            endDisplay.text = blueTaps.ToString();
            IncrementTotal();
            PlayFabController.Instance.RecordButtonPressed("Blue");
        }

        public void IncrementTotal()
        {
            totalTaps = System.Int32.Parse(totalDisplay.text);
            totalTaps ++;
            totalDisplay.text = totalTaps.ToString();
        }
    }
}

