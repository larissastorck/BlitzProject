using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTrap : MonoBehaviour {

    public GameObject fire;
    public bool ready = true;
    GameObject player;
    GameObject obj;
	// Use this for initialization
	void Start () {
        player = GameObject.Find("Blitz");
        obj = GameObject.Find("H6");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {

    }
    
    public void Kabum()
    {
        if (ready)
        {
            //if (player.GetComponent<Player>().skillEquiped == null)
            //{
            //    player.GetComponent<AudioSource>().GetComponent<AudioSource>().PlayOneShot(obj.GetComponent<H6>().skillEquiped.GetComponent<Skill>().soundEffect, musicControl.soundVolume);
            //}
            //else
            //{
            //    player.GetComponent<AudioSource>().GetComponent<AudioSource>().PlayOneShot(player.GetComponent<Player>().skillEquiped.GetComponent<Skill>().soundEffect, musicControl.soundVolume);
            //}

            Vector3 position;
            Instantiate(fire, transform.position, transform.rotation);

            position = new Vector3(transform.position.x + 0.5f, transform.position.y, transform.position.z);
            Instantiate(fire, position, transform.rotation);

            position = new Vector3(transform.position.x - 0.5f, transform.position.y, transform.position.z);
            Instantiate(fire, position, transform.rotation);

            position = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
            Instantiate(fire, position, transform.rotation);

            position = new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z);
            Instantiate(fire, position, transform.rotation);

            StartCoroutine(Timer());
            ready = false;
        }
    }

    IEnumerator Timer()
    {
        float time;
        if(player.GetComponent<Player>().skillEquiped == null)
        {
            time = obj.GetComponent<H6>().skillEquiped.GetComponent<Skill>().effectTime;
        }
        else
        {
            time = player.GetComponent<Player>().skillEquiped.GetComponent<Skill>().effectTime;
        }
        yield return new WaitForSeconds(time);
        foreach(GameObject fire in GameObject.FindGameObjectsWithTag("Fire"))
        {
            Destroy(fire.gameObject);
        }
        Destroy(this.gameObject);
    }

    private void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }
}
