using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class VersionText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private string versionPrefix;

    void Start()
    {
        text.text = $"${versionPrefix} ${Application.version}";
    }
}
