using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButtonHandler : MonoBehaviour
{
    public void PlayGame()
    {
        Debug.Log("PlayGame method called");
        SceneManager.LoadScene("MainScene");
    }
}
