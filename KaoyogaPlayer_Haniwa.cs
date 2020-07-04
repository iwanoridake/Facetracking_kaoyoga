using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.iOS;

public class KaoyogaPlayer : MonoBehaviour
{

	bool shapeEnabled = false;
	Dictionary<string, float> currentBlendShapes;
	float countup = 0.0f;
	int stage = 1;
	bool hugugao = false;
	bool keep = false;
	bool keep2 = false;
	//bool keep3 = false;
	bool hugugao_2 = false;
	//bool hugugao_3 = false;

	// Use this for initialization
	void Start()
	{
		UnityARSessionNativeInterface.ARFaceAnchorAddedEvent += FaceAdded;
		UnityARSessionNativeInterface.ARFaceAnchorUpdatedEvent += FaceUpdated;
		UnityARSessionNativeInterface.ARFaceAnchorRemovedEvent += FaceRemoved;
		

	}

	private IEnumerator DelayMethod()
    {
		yield return new WaitForSeconds(2.0f);
        
			stage++;
        
    }

	void OnGUI()
	{
		if (shapeEnabled)
		{

			string blendshapes = "";
			string shapeNames = "";
			string valueNames = "";
			/*foreach (KeyValuePair<string, float> kvp in currentBlendShapes)
			{
				blendshapes += " [";
				blendshapes += kvp.Key.ToString();
				blendshapes += ":";
				blendshapes += kvp.Value.ToString();
				blendshapes += "]\n";
				shapeNames += "\"";
				shapeNames += kvp.Key.ToString();
				shapeNames += "\",\n";
				valueNames += kvp.Value.ToString();
				valueNames += "\n";
			}*/

			if (stage == 1)
			{
				blendshapes += currentBlendShapes["mouthSmile_L"].ToString() + ("\n");
				blendshapes += currentBlendShapes["mouthSmile_R"].ToString() + ("\n");
				if (keep == false)
				{
					if (hugugao)
					{
						blendshapes = "2秒キープ！";
					}
					else
					{
						blendshapes = "口角を上げましょう！！";						
					}
				}
				else
				{
					blendshapes = "OK！";
				}
			}
			else if (stage == 2)
            {
				blendshapes += currentBlendShapes["eyeLookUp_R"].ToString() + ("\n");
				blendshapes += currentBlendShapes["eyeLookUp_L"].ToString() + ("\n");
				if (keep2 == false)
				{
					if (hugugao_2)
					{
						blendshapes = "10秒キープ！";
					}
					else
					{
						blendshapes = "目線を上に向けましょう！";
					}
				}
				else
				{
					blendshapes = "OK!";
				}

			}
			//else if (stage == 3)
   //         {
			//	if (keep3 == false)
			//	{
			//		if (hugugao_3)
			//		{
			//			blendshapes = "5秒キープ！";
			//		}
			//		else
			//		{
			//			blendshapes = "空気を口に含み、\n" + "空気を右に移動して、\n" + "右のほうれい線をのばしましょう";
			//			blendshapes += currentBlendShapes["mouthRight"].ToString() + ("\n");
			//			blendshapes += currentBlendShapes["mouthFrown_L"].ToString() + ("\n");

			//		}
			//	}
			//	else
			//	{
			//		blendshapes = "OK！";
			//	}
			//}
			else
            {
				blendshapes = "エクササイズ終了です";
			}
			

			//GUI.skin.box.fontSize = 22;
			GUI.skin.box.fontSize = 40;
			GUILayout.BeginHorizontal(GUILayout.ExpandHeight(true));
			GUILayout.Box(blendshapes);
			//GUILayout.Box(keep);
			GUILayout.EndHorizontal();


			Debug.Log(shapeNames);
			Debug.Log(valueNames);

		}


	}

	void FaceAdded(ARFaceAnchor anchorData)
	{
		shapeEnabled = true;
		currentBlendShapes = anchorData.blendShapes;
	}

	void FaceUpdated(ARFaceAnchor anchorData)
	{
		currentBlendShapes = anchorData.blendShapes;
	}

	void FaceRemoved(ARFaceAnchor anchorData)
	{
		shapeEnabled = false;
	}


	// Update is called once per frame
	void Update()
	{
		if(stage==1)
		{
            if (currentBlendShapes["mouthSmile_L"] >= 0.4 || currentBlendShapes["mouthSmile_L"] >= 0.4)
			{
				hugugao = true;
				countup += Time.deltaTime;
			}
			else
			{
				hugugao = false;
				countup = 0;
			}
            if (countup > 2.0f)
            {
				keep = true;
				countup = 0;
				StartCoroutine("DelayMethod");
            }
		}

		if (stage == 2)
		{
			if ((currentBlendShapes["mouthSmile_L"] >= 0.3 || currentBlendShapes["mouthSmile_L"] >= 0.3) && (currentBlendShapes["eyeLookUp_L"] >= 0.2 || currentBlendShapes["eyeLookUp_R"] >= 0.2))
			{
				hugugao_2 = true;
				countup += Time.deltaTime;
			}
			else
			{
				hugugao_2 = false;
				countup = 0;
			}
			if (countup > 10.0f)
			{
				keep2 = true;
				countup = 0;
				StartCoroutine("DelayMethod");
			}
		}

		//if (stage == 3)
		//{
		//	if (currentBlendShapes["cheekPuff"] >= 0.13 && (currentBlendShapes["mouthRight"] >= 0.04 || currentBlendShapes["mouthFrown_L"] >= 0.21))
		//	{
		//		hugugao_3 = true;
		//		countup += Time.deltaTime;
		//	}
		//	else
		//	{
		//		hugugao_3 = false;
		//		countup = 0;
		//	}
		//	if (countup > 5.0f)
		//	{
		//		keep3 = true;
		//		countup = 0;
		//		StartCoroutine("DelayMethod");
		//	}
		//}


	}
}
