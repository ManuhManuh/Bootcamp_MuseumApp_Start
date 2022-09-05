using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace MuseumApp
{
    public class DeletePopup : MonoBehaviour
    {
        private static readonly int ExitHash = Animator.StringToHash("Exit");

        public TMP_InputField passwordInput;
        public int minPasswordCharacters = 8;
        private Image passwordHolderImage;

        public TMP_Text warningText;

        public Color wrongInputFieldColor = new Color(1, 0.82f, 0.82f);

        public Animator animator;
        public void OnDeleteClicked()
        {
            var user = Database.GetUser(User.LoggedInUsername);
            if (user == null)
            {
                warningText.text = "Cannot delete - not logged in";
                return;
            }
            else if (user.Password != passwordInput.text)
            {
                passwordHolderImage.color = wrongInputFieldColor;
                return;
            }

            RemoveProfile();
            
        }

        public void ClosePopup()
        {
            animator.SetTrigger(ExitHash);
            FindObjectOfType<HomeScreen>().Refresh();
        }

        public void RemoveProfile()
        {
            Database.DeleteUser();
            User.LogOff();
            ClosePopup();
        }
        private void OnFinishedExitAnimation()
        {
            SceneManager.UnloadSceneAsync("DeletePopup");
        }

    }
}

