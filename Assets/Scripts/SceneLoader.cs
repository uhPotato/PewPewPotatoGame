using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneLoader : MonoBehaviour
{
     public GameObject explosion;
    // Set the next scene to load in the inspector
    public int nextSceneIndex;
    public float moveSpeed = 1;

     private int currentSceneIndex;

    // Update is called once per frame

    void Start(){
         currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    }
    void Update()
    {
   
        if (Input.GetKeyDown(KeyCode.P))
        {

    
       StartCoroutine(LoadNextScene());

    
        }
    }
     private void FixedUpdate() {
          
    }


IEnumerator MoveGameObjectOffScreen()
{
    // Move the gameObject off-screen
    while (transform.position.y < 20)
    {
         Vector2 pos = transform.position;

        pos.y += moveSpeed * Time.fixedDeltaTime;

        if (pos.y < 7){
            moveSpeed = 0;
        }

        transform.position = pos;
        yield return null;
    }
}
  IEnumerator LoadNextScene()
    {
          yield return MoveGameObjectOffScreen();
        yield return new WaitForSeconds(2.0f); // Wait for 5 seconds
        
        SceneManager.LoadScene(currentSceneIndex + 1);
    }

}
