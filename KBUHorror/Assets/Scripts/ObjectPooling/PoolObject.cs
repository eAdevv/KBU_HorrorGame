using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolObject : MonoBehaviour
{
	public string poolKey;

	public virtual void OnAwake()
	{
		Debug.Log("PoolObject Awake");
	}

	protected void Done(float time)
	{
		Invoke("Done", time);
	}

	protected void Done()
	{
		PoolManager.instance.ReturnObjectToQueue(gameObject);
	}
}
