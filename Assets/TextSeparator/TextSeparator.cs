using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextSeparator : MonoBehaviour {

    public string text = "YO CUANDO QUIROZ NO VA A LA GRADUACION";
    public Font font;
    public Material material;

    void Start()
    {
        for (int i = 0; i < text.Length; i++){
            GameObject newGO = new GameObject(text[i].ToString());
            newGO.transform.SetParent(this.transform);

            Text myText = newGO.AddComponent<Text>();

            RectTransform parentTransform = GetComponentInParent<RectTransform>();
            myText.text = text[i].ToString();
            myText.alignment = TextAnchor.LowerCenter;
            myText.font = font;
            myText.material = material;
            myText.GetComponent<RectTransform>().localPosition = new Vector3(parentTransform.localPosition.x , parentTransform.localPosition.y, myText.rectTransform.localPosition.z);
            myText.fontSize = 40;
            myText.color = new Color(1f, 0.0f, 0.0f,1.0f);

            newGO.AddComponent<ShakeText>();
            //newGO.AddComponent<WaveText>();
            //newGO.GetComponent<WaveText>().Offset = .15f * i;
        }
       
    }
}
