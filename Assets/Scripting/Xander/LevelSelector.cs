using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 
using System.Threading.Tasks; 

public class LevelSelector : MonoBehaviour
{
    public async void LoadScene1Async()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(1);

        while (!asyncLoad.isDone)
        {
            await Task.Yield(); 
        }
    }
    public void OnButtonPress()
    {
        LoadScene1Async();
    }
}