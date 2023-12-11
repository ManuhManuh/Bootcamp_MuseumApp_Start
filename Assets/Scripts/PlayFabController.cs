using PlayFab;
using PlayFab.ClientModels;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MuseumApp
{
    public class PlayFabController
    {
        private static PlayFabController instance;
        public static PlayFabController Instance 
        {
            get
            {
                if(instance == null)
                {
                    instance = new PlayFabController();
                }
                return instance;
            }
            private set => instance = value;
        
        }

        public bool successfulRegistration;
        /// <summary>
        /// Log into PreFab using CustomIDRequest
        /// </summary>
        public void LoginWithPlayfab(Action callback = null)
        {
            var request = new LoginWithCustomIDRequest { CustomId = "GettingStartedGuide", CreateAccount = true };
            PlayFabClientAPI.LoginWithCustomID(request, result => OnLoginSuccessful(result, callback), error => OnPlayfabFailure(error, "LoginWithCustomID"));
        }

        /// <summary>
        /// Log into PreFab using EmailRequest
        /// <paramref name="email"/>
        /// <paramref name="password"/>
        /// <paramref name="callback"/>
        /// </summary>

        public void LoginWithPlayfab(string email, string password, Action callback = null)
        {
            var request = new LoginWithEmailAddressRequest { Email = email, Password = password };
            PlayFabClientAPI.LoginWithEmailAddress(request, result => OnLoginSuccessful(result, callback), error => OnPlayfabFailure(error, "LoginWithEmailAddress"));
        }

        /// <summary>
        /// Check if login was successful
        /// </summary>
        private void OnLoginSuccessful(LoginResult result, Action callback)
        {
            Debug.Log($"Successfully logged in with : {result.PlayFabId}");
            callback?.Invoke();

        }

        /// <summary>
        /// Handle login failure
        /// </summary>
        private void OnPlayfabFailure(PlayFabError error, string requestName)
        {
            Debug.LogWarning($"Something fishy is going on with the request {requestName}");
            Debug.LogError(error.GenerateErrorReport());
        }

        public void RegisterPlayFabUser(string email, string password, Action callback = null)
        {
            var request = new RegisterPlayFabUserRequest { RequireBothUsernameAndEmail = false, Email = email, Password = password };
            PlayFabClientAPI.RegisterPlayFabUser(request, result => OnRegistrationSuccessful(result, callback), error => OnRegistrationFailed(error, "RegisterPlayGabUser"));
        }

        private void OnRegistrationSuccessful(RegisterPlayFabUserResult result, Action callback)
        {
            Debug.Log($"Successfully registered : {result.PlayFabId}");
            callback?.Invoke();
            successfulRegistration = true;
        }

        private void OnRegistrationFailed(PlayFabError error, string requestName)
        {
            Debug.LogWarning($"Registration {requestName} failed");
            Debug.LogError(error.GenerateErrorReport());
            successfulRegistration = false;
        }

        public void SubmitAcceptedTerms(int value)
        {
            
        }

        public void OnStatisticsUpdated(UpdatePlayerStatisticsResult updateResult)
        {
            Debug.Log("Successfully submitted time to PlayFab");
        }

        public void RecordButtonPressed(string pressType)
        {
            //PlayFabClientAPI.UpdatePlayerStatistics(new UpdatePlayerStatisticsRequest
            //{
            //    Statistics = new List<StatisticUpdate>
            //{
            //    new StatisticUpdate
            //    {
            //        StatisticName = pressType,
            //         Value = pressCount
            //    }
            //}
            //}, result => OnStatisticsUpdated(result), FailureCallback); ; ;

            PlayFabClientAPI.WritePlayerEvent(new WriteClientPlayerEventRequest
            {
                EventName = "ButtonPress",
                Body = new Dictionary<string, object>
            {
                { "PressType", pressType },
        
            }
            },
            (result) => Debug.Log("Press event recorded"),
            (error) => Debug.LogError(error.GenerateErrorReport()));

        }

        private void FailureCallback(PlayFabError error)
        {
            Debug.LogWarning("Something went wrong with your API call. Here's some debug information:");
            Debug.LogError(error.GenerateErrorReport());
        }

        

    }
}

