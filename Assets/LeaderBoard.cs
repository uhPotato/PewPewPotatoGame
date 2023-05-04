// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using TMPro;

// using Dan.Main;

// public class LeaderBoard : MonoBehaviour
// {


//     [SerializeField]
//     public List<TextMeshProGUI> names;


//     [SerializeField]
//     public List<TextMeshProGUI> scores;
    

//     private string publicLeaderboardKey = "c31bf9970e2ac752bdf357a71973844ee6767c2ef95b6d2f6c506e0238a281c3";

//         public void GetLeaderBoard() {
//             LeaderBoardCreator.GetLeaderBoard(publicLeaderboardKey, ((msg) => {
//                 for (int i ; i < names.Count(); i++) {
//                     names[i].txt = msg[i].Username;
//                     score[i].txt = msg[i].Score.ToString();
//                 }
            
//             }));
//             // LeaderboardCreator.GetLeaderboard(publicLeaderboardKey, Action<Entry[]> callback);
//     }

//     public void SetLeaderboardEntry(string username, int score) {
//         LeaderBoardCreator.UploadNewEntry(publicLeaderboardKey, username, score, ((msg) => {
            
    
//             GetLeaderBoard();

//         }));
//     }
//     // Start is called before the first frame update
//     void Start()
//     {
        
//     }

//     // Update is called once per frame
//     void Update()
//     {
        
//     }
// }
