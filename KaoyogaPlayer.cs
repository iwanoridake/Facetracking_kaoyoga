using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.iOS;

public class KaoyogaPlayer : MonoBehaviour
{

bool shapeEnabled = false;
Dictionary<string, float> currentBlendShapes;
float countup = 0.0f;
float countup2 = 0.0f;
int stage = 1;
bool hugugao = false;
bool keep = false;
bool keep2 = false;
bool keep3 = false;
bool hugugao_2 = false;
bool hugugao_3 = false;

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
			if (keep == false)
			{
				if (hugugao)
				{
					blendshapes = "5秒キープ！" + ("\n"); ;
					//blendshapes += ((int)(5 + 1 - countup)).ToString();
				}
				else
				{
					blendshapes = "ほうれい線を伸ばすように\n" + "口に空気を含みましょう";
						

				}
			}
			else
			{
				blendshapes = "OK！";
			}
		}else if (stage == 2)
        {
			if (keep2 == false)
			{
				if (hugugao_2)
				{
					blendshapes = "5秒キープ！" + ("\n"); ;
					blendshapes += ((int)(5 + 1 - countup)).ToString();
				}
				else
				{
					blendshapes = "空気を口に含み、\n"+"空気を左に移動して、\n" + "左のほうれい線をのばしましょう" + ("\n"); ;
					//blendshapes += currentBlendShapes["mouthLeft"].ToString()+("\n");
					//blendshapes += currentBlendShapes["mouthFrown_R"].ToString() + ("\n");

				}
			}
			else
			{
				blendshapes = "OK！";
			}

		}
		else if (stage == 3)
        {
			if (keep3 == false)
			{
				if (hugugao_3)
				{
					blendshapes = "5秒キープ！" + ("\n"); ;
					blendshapes += ((int)(5 + 1 - countup)).ToString();
				}
				else
				{
					blendshapes = "空気を口に含み、\n" + "空気を右に移動して、\n" + "右のほうれい線をのばしましょう" + ("\n"); ;
					//blendshapes += currentBlendShapes["mouthRight"].ToString() + ("\n");
					//blendshapes += currentBlendShapes["mouthFrown_L"].ToString() + ("\n");

				}
			}
			else
			{
				blendshapes = "OK！";
			}
		} else
        {
			blendshapes = "エクササイズ終了です";
		}


			//GUI.skin.box.fontSize = 22;
			GUI.skin.box.fontSize = 50;/*
			GUILayout.BeginHorizontal(GUILayout.ExpandHeight(true));
			GUILayout.Box(blendshapes);
			GUILayout.EndHorizontal();
			*/
			Rect windowRect = new Rect(0, 0, UnityEngine.Screen.width, UnityEngine.Screen.height); 
			GUILayout.BeginArea(windowRect);
			GUILayout.FlexibleSpace();
			GUILayout.BeginHorizontal();
			GUILayout.FlexibleSpace();

			GUILayout.Box(blendshapes);

			GUILayout.FlexibleSpace();
			GUILayout.EndHorizontal();
			GUILayout.FlexibleSpace();
			GUILayout.EndArea();


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

	[System.Runtime.InteropServices.DllImport("__Internal")]
	private static extern void playSystemSound(int n);

	// Update is called once per frame
	void Update()
	{

		
		
		if(stage==1)
		{
			if (currentBlendShapes["cheekPuff"] >= 0.18)
			{
				hugugao = true;
				if (!keep)
				{
					countup += Time.deltaTime;
					#if UNITY_EDITOR
				Debug.Log("Play system sound or vibration on real devices");
				#else
				playSystemSound(1013);
				#endif
				}
			}
			else
			{
				hugugao = false;
				countup = 0;
			}
			if (countup > 5.0f)
			{
				keep = true;
				countup = 0;

				#if UNITY_EDITOR
				Debug.Log("Play system sound or vibration on real devices");
				#else
				playSystemSound(1022);
				#endif

			}
			else if (keep)
			{
				countup2 += Time.deltaTime;
				if (countup2 > 2.0f)
				{
					countup2 = 0;
					stage++;
				}
			}





		}

		if (stage == 2)
		{
			if (currentBlendShapes["cheekPuff"] >= 0.13 && (currentBlendShapes["mouthLeft"] >= 0.04 || currentBlendShapes["mouthFrown_R"] >= 0.3))
			{
				hugugao_2 = true;
				if (!keep2)
				{
					countup += Time.deltaTime;
					Debug.Log("Play system sound or vibration on real devices");
#if UNITY_EDITOR
				Debug.Log("Play system sound or vibration on real devices");
				#else
				playSystemSound(1001);
				#endif
				}
			}
			else
			{
				hugugao_2 = false;
				countup = 0;
			}
			if (countup > 5.0f)
			{
				keep2 = true;
				countup = 0;
				//StartCoroutine("DelayMethod");

				#if UNITY_EDITOR
				Debug.Log("Play system sound or vibration on real devices");
				#else
				playSystemSound(1001);
				#endif

			}
			else if (keep2)
			{
				countup2 += Time.deltaTime;
				if (countup2 > 2.0f)
				{
					countup2 = 0;
					stage++;
				}
			}
	}

		if (stage == 3)
		{
			if (currentBlendShapes["cheekPuff"] >= 0.13 && (currentBlendShapes["mouthRight"] >= 0.02 || currentBlendShapes["mouthFrown_L"] >= 0.21))
			{
				hugugao_3 = true;
				if (!keep3)
				{
					countup += Time.deltaTime;
					#if UNITY_EDITOR
				Debug.Log("Play system sound or vibration on real devices");
				#else
				playSystemSound(1001);
				#endif
				}
			}
			else
			{
				hugugao_3 = false;
				countup = 0;
			}
			if (countup > 5.0f)
			{
				keep3 = true;
				countup = 0;
				StartCoroutine("DelayMethod");

				#if UNITY_EDITOR
				Debug.Log("Play system sound or vibration on real devices");
				#else
				playSystemSound(1001);
				#endif

			}
		}


	}
}
