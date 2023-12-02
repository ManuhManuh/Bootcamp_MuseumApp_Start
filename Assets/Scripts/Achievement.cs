using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace MuseumApp
{
    public class Achievement : MonoBehaviour
    {
        public TMP_Text beginMaster;
        public TMP_Text middleMaster;
        public TMP_Text endMaster;

        private void Awake()
        {
            PlayFabController.Instance.LoginWithPlayfab();
        }

    }
}

