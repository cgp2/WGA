using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGameButton : MonoBehaviour {

	// Use this for initialization
	void Start () {
        //UnityEngine.SceneManagement.SceneManager.LoadScene("1");
        //var x = this.GetComponent<UnityEngine.UI.Button>();
        //x.onClick.AddListener(GameLevel);
        //UnityEngine.SceneManagement.SceneManager.LoadScene("Battle");
    }
	
	// Update is called once per frame
	void Update () {
        
    }
    public void GameLevel()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Battle");
    }

}
