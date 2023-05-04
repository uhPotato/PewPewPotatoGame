using System;
using System.Collections;
using System.Collections.Generic;
using PlayFab.ClientModels;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UILobby : MonoBehaviour {

    [SerializeField] CanvasGroup canvasGroup;

    [Header ("Profile")]
    [SerializeField] InputField nameText;
    [SerializeField] Text levelText;
    [SerializeField] Text xpText;
    [SerializeField] Image xpFill;

    public TextMeshProUGUI scoreText;

    [Header ("Highscore Leaderboard")]
    [SerializeField] PoolUILeaderboardEntry poolUILeaderboardEntry;
    List<UILeaderboardEntry> entries = new List<UILeaderboardEntry> ();


    private void Update()
        {   

             Scene currentScene = SceneManager.GetActiveScene();
            
            if(currentScene.name != "StartGame"){
            scoreText = GameObject.Find("ScoreText").GetComponent<TextMeshProUGUI>();
            scoreText.text = Level.score.ToString();
            }
                
        }
    void OnEnable () {
        UserAccountManager.OnSignInSuccess.AddListener (SignInSuccess);

        UserProfile.Instance.OnProfileDataUpdated.AddListener (ProfileDataUpdated);
        UserProfile.Instance.OnLeaderboardHighscoreUpdated.AddListener (LeaderboardHighscoreUpdated);
    }

    void OnDisable () {
        UserAccountManager.OnSignInSuccess.RemoveListener (SignInSuccess);

        UserProfile.Instance.OnProfileDataUpdated.RemoveListener (ProfileDataUpdated);
        UserProfile.Instance.OnLeaderboardHighscoreUpdated.RemoveListener (LeaderboardHighscoreUpdated);
    }

    void SignInSuccess () {
        StartCoroutine (FadeIn ());
    }

    IEnumerator FadeIn () {
        canvasGroup.blocksRaycasts = false;
        canvasGroup.interactable = false;
        canvasGroup.alpha = 0;
        while (canvasGroup.alpha < 1) {
            canvasGroup.alpha += Time.deltaTime;
            yield return null;
        }
        canvasGroup.blocksRaycasts = true;
        canvasGroup.interactable = true;
    }

    public void LeaderboardHighscoreUpdated (List<PlayerLeaderboardEntry> leaderboard) {
        if (entries.Count > 0) {
            for (var i = entries.Count - 1; i >= 0; i--) {
                poolUILeaderboardEntry.ReturnToObjectPool (entries[i]);
            }
        }
        entries.Clear ();
        for (var i = 0; i < leaderboard.Count; i++) {
            UILeaderboardEntry entry = poolUILeaderboardEntry.GetFromObjectPool ();
            entry.SetLeaderboardEntry (leaderboard[i]);
            entries.Add (entry);
            entry.transform.SetAsLastSibling ();

            //Check for leaderboard delay
            if (leaderboard[i].PlayFabId == UserAccountManager.playfabID) {
                if (leaderboard[i].StatValue != UserProfile.Instance.highscore) UserAccountManager.Instance.GetLeaderboardDelayed ("HighScore");
            }
        }
    }

    public void ProfileDataUpdated (ProfileData profileData) {
        nameText.text = profileData.playerName;
        levelText.text = (Mathf.Floor (profileData.level)).ToString ();
        xpText.text = $"{profileData.score}";
        xpFill.fillAmount = ((int) 100);
    }

}