using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MuseumApp
{
    public class HomeScreen : MonoBehaviour
    {
        public GameObject loginButton;
        public GameObject deleteProfileButton;
        public TMP_Text username;

        public RectTransform attractionEntriesParent;

        public AttractionEntryGraphics attractionPrefab;
        public List<AttractionConfig> attractions;
        public List<AttractionEntryGraphics> attractionEntries;

        public void Signup()
        {
            SceneManager.LoadScene("SignupPopup", LoadSceneMode.Additive);
        }

        public void LogOff()
        {
            // TODO(DONE): LogOff
            User.LogOff();

            Refresh();
        }

        public void DeleteProfile()
        {
            SceneManager.LoadScene("DeletePopup", LoadSceneMode.Additive);
        }

        public void Refresh()
        {
            SetupUsername();

            foreach (var attraction in attractionEntries)
                attraction.Refresh();
        }

        private void Awake()
        {
            attractionEntries = new List<AttractionEntryGraphics>(attractions.Count);
            foreach (var attraction in attractions)
            {
                var newAttraction = Instantiate(attractionPrefab, attractionEntriesParent);
                newAttraction.Setup(attraction);
                attractionEntries.Add(newAttraction);
            }

            SetupUsername();
        }

        private void SetupUsername()
        {
            // TODO
            bool isLoggedIn = User.IsLoggedIn;

            if (!isLoggedIn)
            {
                loginButton.SetActive(true);
                username.gameObject.SetActive(false);
                deleteProfileButton.SetActive(false);
                return;
            }

            loginButton.SetActive(false);
            deleteProfileButton.SetActive(true);
            username.gameObject.SetActive(true);

            // TODO: username.text = <NAME>;
            username.text = User.LoggedInUsername;
        }
    }
}