using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Fade : MonoBehaviour
{
	float a = 1;
	public Image image;
	private void Start()
	{
		StartCoroutine(FadeOut());
	}
   

    public IEnumerator FadeIn()
	{

		while (true)
		{
			a += 0.01f;
			image.color = new Color(0, 0, 0, a);
			Debug.Log(a);
			yield return new WaitForSeconds(0.01f);
			if (a >= 1)
				break;
		}
		//while (true)
		//{
		//	a -= 0.01f;
		//	image.color = new Color(0, 0, 0, a);
		//	yield return new WaitForSeconds(0.01f);
		//	if (a <= 0)
		//		break;
		//}

		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
		StartCoroutine(FadeOut());
	}
	public IEnumerator FadeOut()
	{
		while (true)
		{
			a -= 0.01f;
			image.color = new Color(0, 0, 0, a);
			yield return new WaitForSeconds(0.01f);
			if (a <= 0)
				break;
		}

	}
}
