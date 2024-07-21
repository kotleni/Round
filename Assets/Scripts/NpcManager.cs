using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NpcManager : MonoBehaviour
{
	public static NpcManager instance;

	private List<Npc> npcs = new List<Npc>();

	public NpcManager() { instance = this; }

	public void RegisterNpc(Npc npc)
	{
		npcs.Add(npc);
	}

	public List<Npc> GetNpcs()
	{
		return npcs;
	}

	public Npc? FindNpc(string id)
	{
		foreach(Npc npc in npcs)
		{
			if (npc.GetId() == id)
				return npc;
		}
		return null;
	}
}

