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

        public TMP_Text beginDispaly;
        public TMP_Text middleDisplay;
        public TMP_Text endDisplay;

        public void onGreenTapped()
        {
            greenTaps++;
            beginDispaly.text = greenTaps.ToString();
            PlayFabController.Instance.RecordButtonPressed("Green");
        }

        public void onYellowTapped()
        {
            yellowTaps++;
            middleDisplay.text = yellowTaps.ToString();
            PlayFabController.Instance.RecordButtonPressed("Yellow");
        }

        public void onBlueTapped()
        {
            blueTaps++;
            endDisplay.text = blueTaps.ToString();
            PlayFabController.Instance.RecordButtonPressed("Blue");
        }
    }
}

