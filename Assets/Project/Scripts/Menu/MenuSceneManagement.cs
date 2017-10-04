using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuSceneManagement : MonoBehaviour
{

	public void OnStart ()
	{
		SceneManager.LoadScene ("Level1");
	}
}
