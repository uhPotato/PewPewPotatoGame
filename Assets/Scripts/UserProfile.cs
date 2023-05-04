using System.Collections;
using System.Collections.Generic;
using PlayFab.ClientModels;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

[DefaultExecutionOrder(-1)]

public class UserProfile : MonoBehaviour {

    /*
        Central processor of user data 
    */
    
    public static UserProfile Instance;

    public UnityEvent<ProfileData> OnProfileDataUpdated = new UnityEvent<ProfileData> ();
    public UnityEvent<List<PlayerLeaderboardEntry>> OnLeaderboardHighscoreUpdated = new UnityEvent<List<PlayerLeaderboardEntry>> ();

    [Header ("Data")]
    [SerializeField] ProfileData profileData;
    [SerializeField] List<PlayerLeaderboardEntry> leaderboardHighscore = new List<PlayerLeaderboardEntry> ();
    public int highscore = 0;

    [Header ("Settings")]
    public float levelCap = 1000;

    

    void Awake () {
        Instance = this;
    }

    void OnEnable () {
        UserAccountManager.OnSignInSuccess.AddListener (SignInSuccess);
        UserAccountManager.OnUserDataRetrieved.AddListener (UserDataRetrieved);
        UserAccountManager.OnLeaderboardRetrieved.AddListener (LeaderboardRetrieved);
        UserAccountManager.OnStatisticRetrieved.AddListener (StatisticRetrieved);
    }

    void OnDisable () {
        UserAccountManager.OnSignInSuccess.RemoveListener (SignInSuccess);
        UserAccountManager.OnUserDataRetrieved.RemoveListener (UserDataRetrieved);
        UserAccountManager.OnLeaderboardRetrieved.RemoveListener (LeaderboardRetrieved);
        UserAccountManager.OnStatisticRetrieved.RemoveListener (StatisticRetrieved);
    }

    void SignInSuccess () {
        GetUserData ();
        GetHighscoreStatistic ();
        GetLeaderboardHighscore ();
    }

    /* 
        USERDATA
    */

    void UserDataRetrieved (string key, string value) {
        if (key == "ProfileData") {
            if (value != null) {
                profileData = JsonUtility.FromJson<ProfileData> (value);
            } else {
                profileData = new ProfileData ();
            }
            profileData.playerName = UserAccountManager.userAccountInfo.TitleInfo.DisplayName;

            OnProfileDataUpdated.Invoke (profileData);
        }
    }

    [ContextMenu ("Get UserData")]
    void GetUserData () {
        UserAccountManager.Instance.GetUserData ("ProfileData");
    }

    [ContextMenu ("Set UserData")]
    void SetUserData () {
        UserAccountManager.Instance.SetUserData ("ProfileData", JsonUtility.ToJson (profileData));

        OnProfileDataUpdated.Invoke (profileData);
    }

    void GetHighscoreStatistic () {
        UserAccountManager.Instance.GetStatistic ("HighScore");
    }

    void StatisticRetrieved (string statistic, StatisticValue statisticValue) {
        if (statistic == "HighScore") {
            highscore = statisticValue.Value;
        }
    }

    [ContextMenu ("Update Display Name")]
    public void UpdateDisplayName () {
        UserAccountManager.Instance.SetDisplayName (profileData.playerName);
    }

    public void SetPlayerName (string playerName) {
        profileData.playerName = playerName;
        // profileData.score = Level.score;
    }

    /* 
        XP
    */

    [ContextMenu ("Add Random XP")]
    public void AddRandomXP () {
        profileData.level++;
        // profileData.xp = (int) ((profileData.level - (Mathf.Floor (profileData.level))) * levelCap);
        // if(Level.score > profileData.score) {
        // profileData.score = Level.score;
        // }

        SetUserData ();
        SceneManager.LoadScene("Level1");
        Level.score = 0;

    }

    /* 
        LEADERBOARDS
    */

    [ContextMenu ("Get Highscore Leaderboard")]
    void GetLeaderboardHighscore () {
        UserAccountManager.Instance.GetLeaderboard ("HighScore");
    }

    [ContextMenu ("Increase Highscore")]
    public void IncreaseHighscore () {
        int score = Level.score;
         if(score > 1 && score > profileData.score) {
             profileData.score = score;
             SetUserData();
              highscore = score;
        }
        // highscore += 1;
        UserAccountManager.Instance.SetStatistic ("HighScore", highscore);
    }

    void LeaderboardRetrieved (string statistic, List<PlayerLeaderboardEntry> leaderboard) {
        if (statistic == "HighScore") {
            leaderboardHighscore = leaderboard;
            OnLeaderboardHighscoreUpdated.Invoke (leaderboardHighscore);
        }
    }

}

[System.Serializable]
public class ProfileData {
    public string playerName;
    public float level;
    public int xp;

    public int score;
}