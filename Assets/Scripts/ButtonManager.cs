using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public GameObject SceneSelect;
    public GameObject MainMenu;
    public GameObject Options;
    public GameObject Darken;

    // Start is called before the first frame update
    public void ToSceneSelect()
    {
        SceneSelect.gameObject.SetActive(true);
        MainMenu.gameObject.SetActive(false);
        Options.gameObject.SetActive(false);
        Darken.gameObject.SetActive(true);
    }

    public void ToMainMenu()
    {
        MainMenu.gameObject.SetActive(true);
        SceneSelect.gameObject.SetActive(false);
        Options.gameObject.SetActive(false);
        Darken.gameObject.SetActive(false);
    }

    public void ToOptions()
    {
        Options.gameObject.SetActive(true);
        SceneSelect.gameObject.SetActive(false);
        MainMenu.gameObject.SetActive(false);
        Darken.gameObject.SetActive(true);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void City()
    {
        SceneManager.LoadScene("CityScene");
    }

    public void Apartment()
    {
        SceneManager.LoadScene("ApartmentScene");
    }


}
