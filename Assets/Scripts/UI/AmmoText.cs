using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Edit the text of a GUI via script
public class AmmoText : MonoBehaviour
{
    Text textField;

    void Awake()
    {
        textField = GetComponent<Text>();
    }   // Awake

    // Update is called once per frame
    void Update ()
    {
        textField.text = "AMMO: " + PlayerManager.current.GetAmmo();
	}
}
