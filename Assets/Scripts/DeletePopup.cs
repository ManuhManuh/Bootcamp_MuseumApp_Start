using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace MuseumApp
{
    public class DeletePopup : MonoBehaviour
    {
        private static readonly int ExitHash = Animator.StringToHash("Exit");

        public TMP_InputField passwordInput;
        public int minPasswordCharacters = 8;
        private Image passwordHolderImage;

        public Color wrongInputFieldColor = new Color(1, 0.82f, 0.82f);

        public Animator animator;

        public Button deleteButton;

        private void Awake()
        {
            passwordHolderImage = passwordInput.GetComponent<Image>();
        }

        public void OnDeleteClicked()
        {
            var passwordValid = IsInputValid(passwordInput, minPasswordCharacters);
            passwordHolderImage.color = passwordValid ? Color.white : wrongInputFieldColor;

            if (!passwordValid)
                return;

            var user = Database.GetUser(User.LoggedInUsername);

            if (user != null && user.Password != passwordInput.text)
            {
                passwordHolderImage.color = wrongInputFieldColor;
            }

            User.LogOff();
            Database.RemoveUser(User.LoggedInUsername);

            ClosePopup();
        }
        private bool IsInputValid(TMP_InputField inputField, int minCharacters)
        {
            return !string.IsNullOrEmpty(inputField.text) && inputField.text.Length >= minCharacters;
        }

        private void OnFinishedExitAnimation()
        {
            SceneManager.UnloadSceneAsync("DeletePopup");
        }

        public void ClosePopup()
        {
            animator.SetTrigger(ExitHash);
            FindObjectOfType<HomeScreen>().Refresh();
        }
    }

}
