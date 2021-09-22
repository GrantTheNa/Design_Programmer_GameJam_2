using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Sandbox.Omar.SingletonGM
{
    public class GM_Test : MonoBehaviour
    {

        #region Singelton

        private static GM_Test _instance;

        public static GM_Test Instance
        {
            get
            {
                if (_instance == null)
                {
                    GameObject go = new GameObject("MASTER_GameManager");
                    go.AddComponent<GM_Test>();
                }
                return _instance;
            }
        }

        private void Awake()
        {
            _instance = this;
        }
        #endregion

        //// Start is called before the first frame update
        //void Awake()
        //{
        //    //instance = this;

        //    if (_instance != null && _instance != this)
        //    {
        //        Destroy(gameObject);
        //    }
        //    else
        //    {
        //        _instance = this;
        //    }
        //}

        /*
     private static SomeClass _instance;

public static SomeClass Instance { get { return _instance; } }


private void Awake()
{
if (_instance != null && _instance != this)
{
    Destroy(this.gameObject);
} else {
    _instance = this;
}
}
 */

        private int sceneIndex;
        private int nextSceneIndex;

        // Start is called before the first frame update
        void Start()
        {
            DontDestroyOnLoad(gameObject);

            sceneIndex = SceneManager.GetActiveScene().buildIndex;
            nextSceneIndex = sceneIndex + 1;
        }

        public void GoToNextScene()
        {
            //go to next scene
            SceneManager.LoadScene(nextSceneIndex);
            sceneIndex = nextSceneIndex;
            nextSceneIndex = sceneIndex + 1;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
                GoToNextScene();
        }

    }
}