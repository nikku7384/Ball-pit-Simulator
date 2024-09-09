using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class MenuController : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField ballCountInput;
    private int ballCount;
    [SerializeField]
    private GameObject warningPopup;

    public void OnButtonPressed()
    {

        if (int.TryParse(ballCountInput.text, out ballCount) && ballCount >= 1 && ballCount <= 200)
        {
            PlayerPrefs.SetInt("BallCount", ballCount); 
            SceneManager.LoadScene("GameScene"); 
        }
        else
        {
            Debug.Log("Please enter a number between 1 and 200");
            StartCoroutine(WarningPopup());
        }
    }

    //For showing warning popup if value is not in range 
    public IEnumerator WarningPopup()
    {
        warningPopup.gameObject.SetActive(true);
        yield return new WaitForSeconds(5f);
        warningPopup.gameObject.SetActive(false);
       
    }
}
