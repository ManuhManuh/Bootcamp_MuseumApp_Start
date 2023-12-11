using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using PlayFab;
using PlayFab.ClientModels;

namespace MuseumApp
{
    public class Achievement : MonoBehaviour
    {
        public TMP_Text beginMaster;
        public TMP_Text middleMaster;
        public TMP_Text endMaster;
        public TMP_Text displayTotal;

        private void Awake()
        {
            PlayFabController.Instance.LoginWithPlayfab("brain@gmail.com", "other1234", OnPlayFabLogin);
            //PlayFabController.Instance.LoginWithPlayfab("brian@gmail.com", "other1234", OnPlayFabLogin);
        }


        private void OnPlayFabLogin()
        {
            GetStatistics();

        }

        public void GetStatistics()
        {
            //Debug.Log("Getting player statistics...");
            PlayFabClientAPI.GetPlayerStatistics(
                new GetPlayerStatisticsRequest(),
                OnGetStatistics,
                error => Debug.LogError(error.GenerateErrorReport())
            );
        }

        void OnGetStatistics(GetPlayerStatisticsResult result)
        {
            //Debug.Log("Received the following Statistics:");
            foreach (var eachStat in result.Statistics)
            {
                // Debug.Log("Statistic (" + eachStat.StatisticName + "): " + eachStat.Value);
                switch (eachStat.StatisticName)
                {
                    case "Green Button Pressed":
                        {
                            beginMaster.text = eachStat.Value.ToString();
                            break;
                        }
                    case "Yellow Button Pressed":
                        {
                            middleMaster.text = eachStat.Value.ToString();
                            break;
                        }
                    case "Blue Button Pressed":
                        {
                            endMaster.text = eachStat.Value.ToString();
                            break;
                        }
                    case "Any button pressed":
                        {
                            displayTotal.text = eachStat.Value.ToString();
                            break;
                        }
                }
            }
                

        }
    }
}

