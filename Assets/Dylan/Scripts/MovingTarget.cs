using UnityEngine;
using System.Collections;

public class MovingTarget : MonoBehaviour
{
	//point to move object
	public Vector3 pointB;
	public float speed = 2.0f;

	public GameObject vParticle;
	IEnumerator Start()
	{
		//move object back and forth
		var pointA = transform.position;
		while(true)
		{
			yield return StartCoroutine(MoveObject(transform, pointA, pointB, 3.0f));
			yield return StartCoroutine(MoveObject(transform, pointB, pointA, 3.0f));
		}
	}

	IEnumerator MoveObject(Transform thisTransform, Vector3 startPos, Vector3 endPos, float time)
	{
		var i= 0.0f;
		var rate= speed/time;
		while(i < 1.0f)
		{
			i += Time.deltaTime * rate;
			thisTransform.position = Vector3.Lerp(startPos, endPos, i);
			yield return null;
		}
	}
	void OnTriggerEnter (Collider other){
		//Destroy (gameObject);
		GameObject tObj = Instantiate(vParticle);
		tObj.transform.position = this.transform.position;
		this.gameObject.SetActive(false);
	}
}
