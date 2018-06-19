using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Click : MonoBehaviour
{
    public void LoadScrene(string scene)
    {
        SceneManager.LoadScene(scene, LoadSceneMode.Single);
    }
}