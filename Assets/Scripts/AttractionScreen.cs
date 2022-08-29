using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace MuseumApp
{
    public class AttractionScreen : MonoBehaviour
    {
        [Serializable]
        public class WeatherIconEquivalency
        {
            public Sprite icon;
            public string iconId;
        }

        public Image cover;

        public TMP_Text attractionTitle;
        public TMP_Text attractionLocation;
        public TMP_Text attractionAuthor;
        public TMP_Text attractionDescription;
        public Image weatherIconImage;

        public List<WeatherIconEquivalency> weatherIcons;

        public Image[] stars;

        private AttractionScreenParameters attractionParameters;
        private AttractionConfig attractionConfig;

        private static string weatherAPIEndpoint = "https://api.openweathermap.org/data/2.5/weather?lat={0}&lon={1}&appid={2}";
        private static string weatherAPIKey = "c4a796ecd1359f03623849bac490d137";

        public void OnClickBack()
        {
            Destroy(attractionParameters.gameObject);

            SceneManager.LoadScene("HomeScreen", LoadSceneMode.Single);
        }

        public void OnClickStar(int index)
        {
            if (!User.IsLoggedIn)
            {
                return;
            }

            // TODO: Create save user rating
            var attractionID = attractionConfig.id;
            Database.Rate(attractionID, index);

            StarsRatingLib.SetupStars(stars, attractionID);
        }

        private void Start()
        {
            weatherIconImage.gameObject.SetActive(false);

            attractionParameters = FindObjectOfType<AttractionScreenParameters>();
            attractionConfig = attractionParameters.attractionConfig;

            attractionTitle.text = attractionConfig.title;
            attractionLocation.text = attractionConfig.location;
            attractionAuthor.text = attractionConfig.author;
            attractionDescription.text = attractionConfig.description;

            SetupCover();

            // TODO: StarsRatingLib.SetupStars
            StarsRatingLib.SetupStars(stars, attractionConfig.id);

            StartCoroutine(SetWeatherIcon());

        }

        private void SetupCover()
        {
            cover.sprite = attractionConfig.image;

            var rectTransform = cover.GetComponent<RectTransform>();
            rectTransform.anchoredPosition3D = attractionConfig.headerImagePosition;
            rectTransform.sizeDelta = attractionConfig.headerImageSize;
        }

        private IEnumerator SetWeatherIcon()
        {
            string url = string.Format(weatherAPIEndpoint, attractionConfig.latitude, attractionConfig.longitude, weatherAPIKey);

            UnityWebRequest webRequest = UnityWebRequest.Get(url);
            yield return webRequest.SendWebRequest();
            if(webRequest.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Weather request network error");
                yield break;
            }

            WeatherData data = JsonUtility.FromJson<WeatherData>(webRequest.downloadHandler.text);

            weatherIconImage.sprite = weatherIcons.Find(entry => entry.iconId == data.weather[0].icon).icon;
            weatherIconImage.gameObject.SetActive(true);

        }

    }
}