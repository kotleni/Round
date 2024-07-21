using UnityEngine;
using System.Collections;

public class Npc : MonoBehaviour
{
	[SerializeField] private string id;

	private void Start()
	{
		NpcManager.instance.RegisterNpc(this);
	}

	public string GetId()
	{
		return id;
	}
}

