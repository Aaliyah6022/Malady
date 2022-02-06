using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.Networking;
using UnityEngine.Playables;
using UnityEngine.UI;

struct UpdateSystem
{
    public string Description;
    public string Version;
    public string Url;
}


public class MainMenu : MonoBehaviour
{
    [Header ("## UI References:")]
    public GameObject Maimenu;
    public GameObject PressAnyButton;
    [SerializeField] TMP_Text text;
    [Header("## Update References:")]
    [SerializeField] GameObject updatePop;
    [SerializeField] TextMeshProUGUI uiDescriptionText;
    [SerializeField] Button uiNotNowButton;
    [SerializeField] Button uiUpdateButton;

    [Space(20f)]
    [Header("## Settings:")]
    [SerializeField] [TextArea(1, 5)] string jsonDataUrl;

    static bool isAlreadyCheckedForUpdates = false;

    UpdateSystem latestGameData;

    void ShowPopup()
    {
        uiNotNowButton.onClick.AddListener(() =>
        {
            HidePopup();
        });

        uiUpdateButton.onClick.AddListener(() =>
        {
            Application.OpenURL(latestGameData.Url);
        });

        updatePop.SetActive(true);
    }

    void HidePopup()
    {
        updatePop.SetActive(false);

        uiNotNowButton.onClick.RemoveAllListeners();
        uiUpdateButton.onClick.RemoveAllListeners();
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }

    // Start is called before the first frame update
    [System.Obsolete]
    void Start()
    {
        if(!isAlreadyCheckedForUpdates)
        {
            StopAllCoroutines();
            StartCoroutine(CheckForUpdates());
        }
        
        Maimenu.SetActive(false);
        PressAnyButton.SetActive(true);
    }

    void Update()
    {
        if (Input.anyKey)
        {
            Maimenu.SetActive(true);
            text.text = "Logging In....";

            StartCoroutine(Magic());
        }
    }

    [System.Obsolete]
    IEnumerator CheckForUpdates()
    {
        UnityWebRequest request = UnityWebRequest.Get(jsonDataUrl);
        request.chunkedTransfer = false;
        request.disposeDownloadHandlerOnDispose = true;
        request.timeout = 60;

        yield return request.Send();

        if (request.isDone)
        {
            isAlreadyCheckedForUpdates = true;

            if(!request.isNetworkError)
            {
                latestGameData = JsonUtility.FromJson<UpdateSystem>(request.downloadHandler.text);
                if (!string.IsNullOrEmpty(latestGameData.Version) && !Application.version.Equals(latestGameData.Version))
                {
                    uiDescriptionText.text = latestGameData.Description;
                    ShowPopup();
                }
            }
        }
    }

    private IEnumerator Magic()
    {
        //wait 1.4 for unity to post call the video from internet
        yield return new WaitForSeconds(1.4f);
        PressAnyButton.SetActive(false);
    }

    void OnDestroy()
    {
        StopAllCoroutines();
    }

}
