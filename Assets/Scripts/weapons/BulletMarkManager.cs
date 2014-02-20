using UnityEngine;
using System.Collections;

public class BulletMarkManager : MonoBehaviour {

	static private BulletMarkManager instance;

	public int maxMarks;
	public ArrayList marks;
    public ArrayList pushDistances;
	
	void Start()
	{
		if(instance == null)
		{
			instance = this;
		}
	}
	
	static public float Add(GameObject go)
	{
		if(instance == null)
		{
			GameObject aux = new GameObject("BulletMarkManager");
			instance = aux.AddComponent("BulletMarkManager") as BulletMarkManager;
			instance.marks = new ArrayList();
            instance.pushDistances = new ArrayList();
			instance.maxMarks = 60;
		}
		
		GameObject auxGO;
		Transform auxT;
		Transform currentT = go.transform;
		Vector3 currentPos = currentT.position;
		float radius = (currentT.localScale.x * currentT.localScale.x * 0.25f) + (currentT.localScale.y * currentT.localScale.y * 0.25f) + (currentT.localScale.z * currentT.localScale.z * 0.25f);
		radius = Mathf.Sqrt(radius);
		float realRadius = radius * 2;
		radius *= 0.2f;
		
		float distance;
		
		if(instance.marks.Count == instance.maxMarks)
		{
			auxGO = instance.marks[0] as GameObject;
			Destroy(auxGO);
			instance.marks.RemoveAt(0);
			instance.pushDistances.RemoveAt(0);
		}
		
		float pushDistance = 0.0001f;
		int length = instance.marks.Count;
		//int sideMarks = 0;
		for(int i = 0; i < length; i++)
		{
			auxGO = instance.marks[i] as GameObject;
			
			if(auxGO != null)
			{
				auxT = auxGO.transform;
				distance = (auxT.position - currentPos).magnitude;
				if(distance < radius)
				{
					Destroy(auxGO);
					instance.marks.RemoveAt(i);
					instance.pushDistances.RemoveAt(i);
					i--;
					length--;
				}
				else if(distance < realRadius)
				{
					float cDist = (float)instance.pushDistances[i];
					
					pushDistance = Mathf.Max(pushDistance, cDist);
				}
			}
			else
			{
				instance.marks.RemoveAt(i);
				instance.pushDistances.RemoveAt(i);
				i--;
				length--;
			}
		}
		pushDistance += 0.0005f;
		
		instance.marks.Add(go);
		instance.pushDistances.Add(pushDistance);
		
		return pushDistance;
	}
	
	static void ClearMarks()
	{
		GameObject go;
		
		if(instance.marks.Count > 0)
		{
			for(int i = 0; i < instance.marks.Count; i++)
			{
				go = instance.marks[i] as GameObject;
				
				Destroy(go);
			}	
			
			instance.marks.Clear();
		}
	}
}