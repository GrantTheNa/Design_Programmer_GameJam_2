using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SandBox.Staging.OS_StateTesting
{
    public class DestroyLevelManager : LevelManager
    {

        // Update is called once per frame
        void Update()
        {
            
            
            if (Input.GetKeyDown(KeyCode.Return)) GameManager_v02.instance.GoToNextScene();
        }

        public override void OnLevelLoad()
        {
            // to stuff
            base.OnLevelLoad();
            // do stuff
        }

        public override void OnLevelUnload()
        {
            base.OnLevelUnload();
        }
    }
}