using UnityEngine;
using System.Collections;
using ArabicSupport;
using TMPro;

public class FixGUITextCS : MonoBehaviour {
	
	public string text;
	public bool tashkeel = true;
	public bool hinduNumbers = true;
	TextMeshProUGUI textMesh;
	
	// Use this for initialization
	void Start () {
		textMesh = GetComponent<TextMeshProUGUI>();
		textMesh.text = ArabicFixer.Fix(text, tashkeel, hinduNumbers);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
