using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public TMP_Text bossName;
    public Image bossImage;

    public GameObject startPanel;
    public GameObject lobbyPanel;

    public Button startBtn;
    public Button exitBtn;
    public Button nextBtn;
    public Button prevBtn;
    public Button fightBtn;
    public Button closeBtn;

    static bool isLobby = false;

    void Start()
    {
        PanelSetUp();

        startBtn.onClick.AddListener(JoinLobby);
        exitBtn.onClick.AddListener(ExitGame);
        nextBtn.onClick.AddListener(() => UpdateBossIdx(1));
        prevBtn.onClick.AddListener(() => UpdateBossIdx(-1));
        fightBtn.onClick.AddListener(StartGame);
        closeBtn.onClick.AddListener(LeaveLobby);
    }

    void PanelSetUp()
    {
        startPanel.SetActive(!isLobby);
        lobbyPanel.SetActive(isLobby);
    }

    void JoinLobby()
    {
        isLobby = true;
        PanelSetUp();
    }

    void LeaveLobby()
    {
        isLobby = false;
        PanelSetUp();
    }

    void UpdateBossIdx(int num)
    {
        BossList.instance.bossIdx += num + BossList.instance.bossNames.Length;
        BossList.instance.bossIdx %= BossList.instance.bossNames.Length;

        fightBtn.interactable = BossList.instance.bossIdx != BossList.instance.bossImages.Length - 1;

        bossImage.sprite = BossList.instance.bossImages[BossList.instance.bossIdx];
        bossName.text = BossList.instance.bossNames[BossList.instance.bossIdx];
    }
    void StartGame()
    {
        SceneManager.LoadScene("Game");
    }

    void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
