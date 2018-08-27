using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TutTrig : MonoBehaviour
{
	[SerializeField] private UnityEvent _tuttrig;
	
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("Player") && !other.isTrigger && PlayerPrefs.GetInt("TutorFinished") == 0)
		{
			_tuttrig.Invoke();
			
		}
			
	}
}
