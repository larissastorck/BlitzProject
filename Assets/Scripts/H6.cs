using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class H6 : MonoBehaviour {

    Transform target;
    Vector2 velocity;
    public float smoothTime;
    float attackRadius = 3;
    public GameObject shoot;
    float enemyDistance;
    bool isAttacking = false;
    float attackDelay = 1.5f;
    List<GameObject> shoots = new List<GameObject>();
    public List<GameObject> skillList = new List<GameObject>();
    public GameObject skillEquiped;
    GameObject player;
    public string status = null;

    float skillTimer = 0;
    float auxTimer;

    bool selectionBlock = false;

	// Use this for initialization
	void Start () {
        //target = GameObject.FindGameObjectWithTag("Player").transform;
        target = GameObject.Find("Blitz").transform;
        player = GameObject.Find("Blitz");

        //skillEquiped = skillList[3];
        //skillEquiped.GetComponent<Skill>().ready = true;



    }
	
	// Update is called once per frame
	void Update () {

        float distance = Vector3.Distance(target.position, transform.position);

        if (distance > 1.5f)
        {
            float posX = Mathf.Round(Mathf.SmoothDamp(transform.position.x, target.position.x, ref velocity.x, smoothTime) * 100) / 100;
            float posY = Mathf.Round(Mathf.SmoothDamp(transform.position.y, target.position.y, ref velocity.y, smoothTime) * 100) / 100;
            transform.position = new Vector3(posX, posY, transform.position.z);
        }




        //atribui cor e skill ao H6
        if (target.GetComponent<Player>().knnClass != -1)
        {
            if (!selectionBlock)
            {
                switch (target.GetComponent<Player>().knnClass)
                {
                    //blue sobrevivente
                    case 0:
                        GetComponent<SpriteRenderer>().color = new Color32(13, 18, 246, 255);
                        skillEquiped = skillList[0];
                        skillEquiped.GetComponent<Skill>().ready = true;
                        print("sobrevivente");
                        break;
                    //green explorador
                    case 1:
                        skillEquiped = skillList[1];
                        GetComponent<SpriteRenderer>().color = new Color32(0, 250, 6, 255);
                        skillEquiped.GetComponent<Skill>().ready = true;
                        print("explorador");
                        break;
                    //amarelo ta na disney
                    case 2:
                        skillEquiped = skillList[3];
                        GetComponent<SpriteRenderer>().color = new Color32(245, 253, 14, 255);
                        skillEquiped.GetComponent<Skill>().ready = true;
                        print("ta na disney");
                        break;
                    case 3:
                        // red mexeu com a mae
                        skillEquiped = skillList[2];
                        GetComponent<SpriteRenderer>().color = new Color32(245, 14, 32, 255);
                        skillEquiped.GetComponent<Skill>().ready = true;
                        print("mexeu com a mae");
                        break;
                }
                selectionBlock = true;
            }

            

            //contagem de tempo para recarga da skill
            if (skillEquiped.GetComponent<Skill>().ready == false)
            {
                skillTimer += Time.deltaTime;
                auxTimer = skillTimer / skillEquiped.GetComponent<Skill>().rechargeTime;
                if (skillTimer >= skillEquiped.GetComponent<Skill>().rechargeTime)
                {
                    skillTimer = 0;
                    skillEquiped.GetComponent<Skill>().ready = true;
                }
            }

            //realiza cura no player
            if (player.GetComponent<Player>().hp <= 3)
            {
                CastSkill(player.GetComponent<Player>().knnClass);
            }

            //foreach(GameObject obj in GameObject.FindGameObjectsWithTag("Enemy"))
            //{
            //    enemyDistance = Vector3.Distance(transform.position, obj.transform.position);
            //    if(enemyDistance <= attackRadius)
            //    {
            //        if (!isAttacking)
            //        {
            //            Instantiate(shoot, transform.position, transform.rotation);

            //        }
            //    }
            //}
        }
    }

    IEnumerator Delay()
    {
        isAttacking = true;
        yield return new WaitForSeconds(attackDelay);
        isAttacking = false;
    }

    IEnumerator skillTime()
    {
        player.GetComponent<Player>().skillActiveInfo.GetComponent<Image>().sprite = skillEquiped.GetComponent<Skill>().icon;
        yield return new WaitForSeconds(skillEquiped.GetComponent<Skill>().effectTime);
        if (skillEquiped.GetComponent<Skill>().effect.Equals("Clone"))
        {
            player.GetComponent<Transform>().tag = "Player";
            Destroy(GameObject.Find("Clone(Clone)").gameObject);
        }
        skillEquiped.GetComponent<Skill>().active = false;
        player.GetComponent<Player>().skillActiveInfo.SetActive(false);

    }

    public void CastSkill(int classify)
    {
        switch (classify)
        {
            //armadilha fogo
            case 0:
                if (skillEquiped.GetComponent<Skill>().ready)
                {
                    print("fire trap");
                    skillEquiped.GetComponent<Skill>().ready = false;
                    skillEquiped.GetComponent<Skill>().active = true;
                    StartCoroutine(skillTime());
                    Instantiate(skillEquiped.GetComponent<Skill>().fire, player.transform.position, player.transform.rotation);
                }
                break;
            case 1:
                //clone
                if (skillEquiped.GetComponent<Skill>().ready)
                {
                    print("clone");
                    Instantiate(player.GetComponent<Player>().clone, player.transform.position, player.transform.rotation);
                    player.GetComponent<Transform>().tag = "Clone";
                    skillEquiped.GetComponent<Skill>().ready = false;
                    StartCoroutine(skillTime());
                    player.GetComponent<AudioSource>().PlayOneShot(skillEquiped.GetComponent<Skill>().soundEffect, musicControl.soundVolume);
                    player.GetComponent<Player>().skillActiveInfo.SetActive(true);
                }
                break;
            case 2:
                //escudo
                if (skillEquiped.GetComponent<Skill>().ready)
                {
                    print("escudo");
                    skillEquiped.GetComponent<Skill>().ready = false;
                    skillEquiped.GetComponent<Skill>().active = true;
                    status = "escudo";
                    Instantiate(skillEquiped.GetComponent<Skill>().visualEffect, player.transform.position, player.transform.rotation);
                    player.GetComponent<AudioSource>().PlayOneShot(skillEquiped.GetComponent<Skill>().soundEffect, musicControl.soundVolume);
                }
                break;
            case 3:
                //restaurar
                if (skillEquiped.GetComponent<Skill>().ready)
                {
                    print("cura");
                    player.GetComponent<Player>().hp += (skillEquiped.GetComponent<Skill>().effectPower);
                    skillEquiped.GetComponent<Skill>().ready = false;
                    Instantiate(skillEquiped.GetComponent<Skill>().visualEffect, player.transform.position, player.transform.rotation);
                    player.GetComponent<AudioSource>().PlayOneShot(skillEquiped.GetComponent<Skill>().soundEffect, musicControl.soundVolume);
                }
                break;
        }
    }
}
