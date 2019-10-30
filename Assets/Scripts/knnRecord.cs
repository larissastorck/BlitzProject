using System.Collections;
using System.Data;
using Mono.Data.Sqlite;
using System;
using System.Collections.Generic;
using UnityEngine;



public class knnRecord : MonoBehaviour {

    public int numberOfShoots; //player
    public int numberOfHits; //enemys
    public int numberOfBoxes;   //destructibles
    public int hpLost;      //player
    public int heal;        //player
    public int seconds;     //mapConfig
    public int distance; // enemys
    public int firstSkill;
    public int mostUsedSkill;
    public List<float> distanceOfEnemys = new List<float>();
    public int distanceInRoom;
    public float distanceAux;

    public bool knnAtivar = false;
    float timer = 0;
    public bool activeTimer = false;
    public bool blockKnn = false;

    //int numberOfCapture;
    float[] knn = new float[10];
    double[] unknown = new double[8];
    public double[] unknown_1 = new double[8];
    public double[] unknown_2 = new double[8];
    public double[] unknown_3 = new double[8];
    double[][] playerMedia = new double[2][];
    int index = 0;
    int mediaAux = 2;
    public int classify = 5;

    GameObject obj;
    GameObject gm;
    GameObject save;

    //FileMaker file = new FileMaker();
    MySqlDb insert = new MySqlDb();

    public int totalEnergy = 0;
    public int collectedEnergy = 0;

    //string conn = "URI=file:" + Application.dataPath + "/blitzDB.s3db"; //Path to database.



    // Use this for initialization
    void Start () {

        obj = GameObject.FindWithTag("Player");
        gm = GameObject.Find("Manager");
        save = GameObject.Find("Save");

        //for (int i = 0; i < unknown.Length; i++)
        //{
        //    for (int j = 0; j < mediaAux; j++)
        //    {
        //        playerMedia[j][i] = 0;
        //    }
        //}
        
    }

    // Update is called once per frame
    void Update () {

        knn[2] = numberOfBoxes;
        knn[9] = firstSkill;
        

        //salva os valores quando a porta da sala abre
        if (knnAtivar) //ligado no warp
        {
            knn[0] = numberOfShoots;
            knn[1] = numberOfHits;
            
            knn[3] = hpLost;
            knn[4] = heal;
            knn[5] = seconds;




            if (activeTimer && obj.GetComponent<Player>().currentMap.GetComponent<MapConfig>().clear == false)
            {
                timer += Time.deltaTime;
                seconds = Mathf.RoundToInt(timer);
                knn[8] = obj.GetComponent<Player>().shootID;
            }
        }
        else
        {
            if (obj.GetComponent<Player>().recordKnn && !blockKnn)
            {
                //dataBase.InsertDB("classe", numberOfShoots, numberOfHits, numberOfBoxes, hpLost, heal, seconds);

                foreach (float d in distanceOfEnemys)
                {
                    distanceAux += d;
                }
                float aux;
                aux = (distanceAux / distanceOfEnemys.Count);              
                knn[6] = Mathf.RoundToInt(aux);

                //calcula porcentagem de energia coletada
                aux = (collectedEnergy * 100f) / totalEnergy;
                if (aux > 100) aux = 100;
                knn[7] = aux;

                if (save.GetComponent<Save>().numberOfCapture >= 1)
                {
                    switch (save.GetComponent<Save>().numberOfCapture)
                    {
                        case 1:
                            unknown_1[0] = knn[0];
                            unknown_1[1] = knn[1];
                            unknown_1[2] = knn[2];
                            unknown_1[3] = knn[3];
                            unknown_1[4] = knn[4];
                            unknown_1[5] = knn[5];
                            unknown_1[6] = knn[6];
                            unknown_1[7] = knn[7];
                            obj.GetComponent<Player>().knnClass = KnnClassify();


                            break;
                        case 2:
                            unknown_2[0] = knn[0];
                            unknown_2[1] = knn[1];
                            unknown_2[2] = knn[2];
                            unknown_2[3] = knn[3];
                            unknown_2[4] = knn[4];
                            unknown_2[5] = knn[5];
                            unknown_2[6] = knn[6];
                            unknown_2[7] = knn[7];
                            break;
                        case 3:
                            unknown_3[0] = knn[0];
                            unknown_3[1] = knn[1];
                            unknown_3[2] = knn[2];
                            unknown_3[3] = knn[3];
                            unknown_3[4] = knn[4];
                            unknown_3[5] = knn[5];
                            unknown_3[6] = knn[6];
                            unknown_3[7] = knn[7];
                            break;
                    }
                    save.GetComponent<Save>().numberOfCapture--;
                }
                else
                {
                        
                }




                //if (obj.GetComponent<Player>().knnClass == -1)
                //{
                //    unknown[0] = knn[0];
                //    unknown[1] = knn[1];
                //    unknown[2] = knn[2];
                //    unknown[3] = knn[3];
                //    unknown[4] = knn[4];
                //    unknown[5] = knn[5];
                //    unknown[6] = knn[6];
                //    unknown[7] = knn[7];
                //    obj.GetComponent<Player>().knnClass = KnnClassify();
                //}
                //else
                //{

                //}




                //int skillAux = obj.GetComponent<Player>().arrayMostUsedSkill[0];
                //for (int i = 0 ; i< obj.GetComponent<Player>().arrayMostUsedSkill.Length ; i++)


                //file.WriteFile(knn);

                //gravaKnnData(knn);

                //unknown[0] = knn[0];
                //unknown[1] = knn[1];
                //unknown[2] = knn[2];
                //unknown[3] = knn[3];
                //unknown[4] = knn[4];
                //unknown[5] = knn[5];
                //unknown[6] = knn[6];
                //unknown[7] = knn[7];
                //classify = gm.GetComponent<knnNew>().KNN(unknown, 5);
                //obj.GetComponent<Player>().knnClass = classify;

                //if (index >= mediaAux)
                //{
                //    double sum = 0;
                //    for (int i = 0; i < unknown.Length; i++)
                //    {
                //        for(int j = 0; j < mediaAux; j++)
                //        {
                //            sum += playerMedia[j][i];
                //        }                       
                //        unknown[i] = sum / mediaAux;
                //        sum = 0;
                //        print(unknown[i]);
                //    }
                //    classify = gm.GetComponent<knnNew>().KNN(unknown, 5);
                //}
                //else
                //{
                //    print("to aqui no else");
                //    print(index.ToString());
                //    //for(int i = 0;i < unknown.Length; i++)
                //    //{
                //    //    playerMedia[index][i] = knn[i];
                //    //}
                //    playerMedia[0][0] = (double)knn[0];
                //    playerMedia[0][1] = (double)knn[1];
                //    playerMedia[0][2] = (double)knn[2];
                //    playerMedia[0][3] = (double)knn[3];
                //    playerMedia[0][4] = (double)knn[4];
                //    playerMedia[0][5] = (double)knn[5];
                //    playerMedia[0][6] = (double)knn[6];
                //    playerMedia[0][7] = (double)knn[7];
                //    index++;
                //}

                //reseta os valores para nova captura
                numberOfShoots = 0;
                numberOfHits = 0;
                numberOfBoxes = 0;
                hpLost = 0;
                heal = 0;
                seconds = 0;
                timer = 0;
                distance = 0;
                distanceOfEnemys.Clear();
                distanceAux = 0;
                collectedEnergy = 0;
                totalEnergy = 0;

                //insert.InsertKnnData(knn, distanceAux);
                

                obj.GetComponent<Player>().recordKnn = false;
                blockKnn = true;
            }

        }
    }

    public int KnnClassify()
    {
        int knnClass;
        for (int i = 0; i < unknown.Length; i++)
        {
            unknown[i] = (unknown_1[i] + unknown_2[i]) / 2;
        }

        knnClass = gm.GetComponent<knnNew>().KNN(unknown, 5);
        return knnClass;
    }

}
