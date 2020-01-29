using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class SetGfx : MonoBehaviour
{
    [SerializeField] private Text txtGFX;
    [SerializeField] private string nameParam;
    private string[] gfxNames;

    public void SetGFX(float vol)
    {
        Slider slide = GetComponent<Slider>();
        int v = (int)Mathf.Floor(vol);
        QualitySettings.SetQualityLevel(v, true);
        txtGFX.text = gfxNames[v];
        PlayerPrefs.SetFloat(nameParam, vol);
        PlayerPrefs.Save();
        slide.value = vol;
    }

    // Use this for initialization
    void Start()
    {
        gfxNames = QualitySettings.names;
        Slider slide = GetComponent<Slider>();
        float v = PlayerPrefs.GetFloat(nameParam, 0);
        slide.value = v;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
