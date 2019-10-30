using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Save : MonoBehaviour {

    public int floor = 1;
    public int xp = 0;
    public GameObject skill = null;
    public GameObject shoot = null;
    public int classify = -1;
    public List<GameObject> skillList;
    public int numberOfCapture;

    public static Save instance = null;

    private void Awake()
    {
        if (instance == null) instance = this;
        else if (instance != this) Destroy(gameObject);
        DontDestroyOnLoad(this.gameObject);
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
