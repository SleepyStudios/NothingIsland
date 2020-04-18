using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PersistenceManager : MonoBehaviour {
    public static PersistenceManager instance;
    public bool inGame, skipMenu;
    private GameObject menu;

    void Awake() {
        if (instance == null) {
            instance = this;
        } else if (instance != this) {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    private void Update() {
        if (Input.GetKeyDown("r")) {
            skipMenu = true;
            GetComponent<MusicController>().inCave = false;
            SceneManager.LoadScene(0);
        }

        if (Input.GetKeyDown("space") && !inGame) {
            inGame = true;
            menu.GetComponent<Animator>().enabled = true;
            FindObjectOfType<BottleController>().StartMoving();
        }

        if (menu == null) {
            SetMenu(GameObject.Find("Menu"));
        }

        if (skipMenu && menu != null) menu.SetActive(false);
    }

    private void SetMenu(GameObject menu) {
        this.menu = menu;
        menu.GetComponent<Animator>().enabled = false;
        menu.transform.Find("Menu Overlay").GetComponent<Image>().enabled = true;
    }
}
