using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using System.Threading.Tasks;
using System;
using System.Linq;
using System.Text;




public class knnNew : MonoBehaviour {

    // Use this for initialization
    void Start()
    {
        //double[] unknown = new double[] { 45, 39, 12, 0, 0, 59, 3, 100 };
        //KNN(unknown, 3);
    }

    // Update is called once per frame
    void Update()
    {

    }

    /*
     
     10 > 0 - Sobrevivente
     20 > 1 - Explorador
     30 > 2 - Ta na Disney
     40 > 3 - Mexeu com a Mae
         
         
     */


    public int KNN(double[] unknown, int k)
    {
        //carrega a matriz de treinamento
        double[][] trainData = LoadData();
        int numClasses = 4;
        int predicted = Classify(unknown, trainData, numClasses, k);
       
        print("Predicted class = " + predicted);
        return predicted;//classificacao
    }
    static int Classify(double[] unknown, double[][] trainData, int numClasses, int k)
    {
        int n = trainData.Length;
        IndexAndDistance[] info = new IndexAndDistance[n];
        //coloca o index e a distancia do vetor desconhecido em relaçãoa todos os pontos de treinamento em um vetor auxiliar
        for (int i = 0; i < n; ++i)
        {
            IndexAndDistance curr = new IndexAndDistance();
            double dist = Distance(unknown, trainData[i]);
            curr.idx = i;
            curr.dist = dist;
            info[i] = curr;
        }
        Array.Sort(info);  // sort by distance crescent order
        print("Nearest / Distance / Class");
        print("==========================");
        for (int i = 0; i < k; ++i)
        {
            int c = (int)trainData[info[i].idx][8];
            string dist = info[i].dist.ToString("F3");
            print("( " + trainData[info[i].idx][0] +
              "," + trainData[info[i].idx][1] +
              "," + trainData[info[i].idx][2] +
              "," + trainData[info[i].idx][3] +
              "," + trainData[info[i].idx][4] +
              "," + trainData[info[i].idx][5] +
              "," + trainData[info[i].idx][6] +
              "," + trainData[info[i].idx][7] + ")  :  " +
              dist + "        " + c);
        }
        int result = Vote(info, trainData, numClasses, k);
        return result;
    } // Classify

    static int Vote(IndexAndDistance[] info, double[][] trainData, int numClasses, int k)
    {
        int[] votes = new int[numClasses];  // One cell per class
        for (int i = 0; i < k; ++i)
        {       // Just first k
            int idx = info[i].idx;            // Which train item
            int c = (int)trainData[idx][8];   // Class in last cell //estava 2 coloquei 8
                                              //c = 11;
            ++votes[c];//0 1 2 3 4 //
        }
        int mostVotes = 0;
        int classWithMostVotes = 0;
        for (int j = 0; j < numClasses; ++j)
        {
            if (votes[j] > mostVotes)
            {
                mostVotes = votes[j];
                classWithMostVotes = j;
            }
        }
        return classWithMostVotes;
    }

    static double Distance(double[] unknown, double[] data)
    {
        double sum = 0.0;
        for (int i = 0; i < unknown.Length; ++i)
            sum += (unknown[i] - data[i]) * (unknown[i] - data[i]);
        return Math.Sqrt(sum);
    }

    static double[][] LoadData()
    {
        double[][] data = new double[182][];
        data[0] = new double[] { 28, 18, 0, 2, 0, 59, 2, 85, 2 };
        data[1] = new double[] { 36, 21, 3, 1, 1, 60, 4, 100, 2 };
        data[2] = new double[] { 65, 86, 8, 3, 0, 78, 1, 100, 2 };
        data[3] = new double[] { 52, 78, 5, 2, 0, 54, 1, 100, 2 };
        data[4] = new double[] { 59, 61, 11, 2, 2, 55, 2, 100, 2 };
        data[5] = new double[] { 60, 90, 0, 4, 0, 61, 2, 100, 2 };
        data[6] = new double[] { 87, 45, 9, 10, 1, 205, 2, 100, 2 };
        data[7] = new double[] { 16, 14, 1, 0, 0, 43, 3, 90, 2 };
        data[8] = new double[] { 41, 9, 15, 1, 2, 45, 1, 100, 2 };
        data[9] = new double[] { 45, 57, 0, 5, 0, 44, 3, 94, 2 };
        data[10] = new double[] { 35, 22, 0, 0, 0, 66, 2, 100, 2 };
        data[11] = new double[] { 36, 26, 1, 3, 0, 91, 3, 100, 2 };
        data[12] = new double[] { 46, 31, 0, 2, 0, 67, 2, 96, 2 };
        data[13] = new double[] { 45, 66, 0, 2, 0, 66, 3, 100, 2 };
        data[14] = new double[] { 86, 18, 25, 5, 3, 101, 2, 100, 2 };
        data[15] = new double[] { 46, 17, 6, 8, 0, 40, 1, 100, 2 };
        data[16] = new double[] { 94, 41, 13, 8, 1, 137, 2, 100, 2 };
        data[17] = new double[] { 2, 3, 0, 4, 0, 5, 1, 0, 2 };
        data[18] = new double[] { 35, 24, 0, 10, 0, 41, 3, 67, 0 };
        data[19] = new double[] { 32, 19, 2, 3, 1, 33, 2, 100, 0 };
        data[20] = new double[] { 27, 11, 9, 4, 0, 48, 1, 100, 0 };
        data[21] = new double[] { 16, 11, 0, 4, 0, 52, 2, 100, 0 };
        data[22] = new double[] { 90, 45, 6, 2, 0, 158, 2, 100, 0 };
        data[23] = new double[] { 36, 21, 5, 0, 0, 47, 3, 100, 2 };
        data[24] = new double[] { 38, 19, 7, 0, 0, 73, 1, 100, 2 };
        data[25] = new double[] { 109, 86, 10, 2, 0, 156, 2, 100, 2 };
        data[26] = new double[] { 37, 18, 3, 3, 0, 49, 2, 100, 2 };
        data[27] = new double[] { 40, 19, 2, 10, 0, 90, 1, 100, 0 };
        data[28] = new double[] { 27, 17, 0, 11, 0, 59, 1, 100, 0 };
        data[29] = new double[] { 63, 28, 3, 2, 1, 89, 2, 100, 1 };
        data[30] = new double[] { 47, 38, 0, 8, 0, 78, 1, 100, 1 };
        data[31] = new double[] { 12, 10, 0, 1, 0, 18, 2, 100, 1 };
        data[32] = new double[] { 130, 50, 10, 7, 0, 305, 3, 100, 1 };
        data[33] = new double[] { 40, 55, 0, 4, 0, 54, 2, 100, 1 };
        data[34] = new double[] { 44, 10, 11, 0, 0, 43, 4, 100, 3 };
        data[35] = new double[] { 100, 17, 10, 4, 1, 95, 2, 100, 3 };
        data[36] = new double[] { 59, 35, 6, 5, 0, 99, 2, 100, 2 };
        data[37] = new double[] { 44, 14, 11, 0, 0, 59, 3, 100, 1 };
        data[38] = new double[] { 63, 39, 8, 0, 1, 130, 1, 100, 1 };
        data[39] = new double[] { 73, 20, 16, 0, 0, 29, 2, 100, 1 };
        data[40] = new double[] { 21, 13, 0, 2, 0, 34, 2, 73, 1 };
        data[41] = new double[] { 18, 11, 0, 1, 0, 31, 2, 100, 1 };
        data[42] = new double[] { 17, 10, 1, 5, 0, 16, 2, 0, 1 };
        data[43] = new double[] { 37, 20, 1, 1, 0, 39, 2, 100, 1 };
        data[44] = new double[] { 20, 14, 5, 2, 0, 35, 1, 90, 1 };
        data[45] = new double[] { 20, 17, 0, 3, 0, 25, 2, 100, 1 };
        data[46] = new double[] { 29, 9, 9, 2, 1, 28, 2, 100, 1 };
        data[47] = new double[] { 7, 5, 0, 0, 0, 12, 2, 100, 1 };
        data[48] = new double[] { 37, 21, 0, 0, 0, 23, 2, 100, 0 };
        data[49] = new double[] { 78, 75, 14, 1, 0, 35, 3, 100, 0 };
        data[50] = new double[] { 54, 71, 10, 3, 0, 27, 2, 100, 0 };
        data[51] = new double[] { 86, 109, 2, 0, 0, 59, 2, 100, 0 };
        data[52] = new double[] { 67, 69, 15, 2, 1, 86, 2, 100, 0 };
        data[53] = new double[] { 57, 49, 6, 2, 0, 30, 3, 100, 0 };
        data[54] = new double[] { 37, 9, 10, 0, 3, 54, 4, 100, 0 };
        data[55] = new double[] { 39, 18, 6, 1, 0, 47, 2, 100, 0 };
        data[56] = new double[] { 33, 21, 0, 3, 0, 55, 3, 100, 2 };
        data[57] = new double[] { 69, 19, 14, 1, 2, 102, 3, 100, 2 };
        data[58] = new double[] { 95, 43, 12, 2, 1, 78, 3, 100, 2 };
        data[59] = new double[] { 104, 102, 32, 2, 0, 52, 2, 97, 2 };
        data[60] = new double[] { 1, 0, 0, 3, 0, 3, 1, 0, 2 };
        data[61] = new double[] { 164, 22, 30, 1, 2, 135, 4, 100, 2 };
        data[62] = new double[] { 78, 19, 10, 2, 0, 55, 4, 100, 2 };
        data[63] = new double[] { 67, 29, 10, 4, 1, 141, 1, 100, 2 };
        data[64] = new double[] { 67, 31, 4, 6, 0, 99, 2, 100, 2 };
        data[65] = new double[] { 17, 9, 0, 0, 0, 26, 3, 100, 2 };
        data[66] = new double[] { 54, 28, 15, 3, 1, 31, 2, 74, 2 };
        data[67] = new double[] { 47, 69, 0, 5, 0, 61, 1, 100, 2 };
        data[68] = new double[] { 75, 99, 0, 1, 0, 52, 2, 86, 2 };
        data[69] = new double[] { 0, 0, 0, 12, 0, 66, 1, 0, 1 };
        data[70] = new double[] { 42, 14, 0, 4, 0, 66, 1, 100, 1 };
        data[71] = new double[] { 25, 23, 0, 1, 0, 40, 4, 100, 1 };
        data[72] = new double[] { 51, 13, 11, 0, 4, 20, 2, 100, 1 };
        data[73] = new double[] { 72, 36, 10, 7, 0, 71, 1, 100, 1 };
        data[74] = new double[] { 40, 44, 8, 1, 2, 17, 2, 100, 1 };
        data[75] = new double[] { 80, 32, 13, 6, 2, 97, 3, 100, 1 };
        data[76] = new double[] { 75, 24, 15, 4, 1, 67, 3, 92, 1 };
        data[77] = new double[] { 71, 38, 8, 8, 0, 153, 2, 89, 1 };
        data[78] = new double[] { 78, 39, 1, 4, 0, 144, 3, 95, 1 };
        data[79] = new double[] { 37, 37, 0, 2, 0, 42, 2, 100, 1 };
        data[80] = new double[] { 3, 1, 0, 1, 0, 3, 1, 0, 1 };
        data[81] = new double[] { 66, 17, 15, 3, 3, 76, 2, 100, 1 };
        data[82] = new double[] { 48, 13, 10, 0, 0, 30, 3, 100, 1 };
        data[83] = new double[] { 7, 7, 0, 6, 0, 12, 4, 0, 1 };
        data[84] = new double[] { 90, 48, 6, 9, 0, 137, 1, 93, 2 };
        data[85] = new double[] { 0, 0, 0, 1, 0, 2, 1, 0, 2 };
        data[86] = new double[] { 53, 38, 0, 3, 0, 67, 4, 100, 2 };
        data[87] = new double[] { 59, 36, 4, 3, 0, 78, 2, 100, 2 };
        data[88] = new double[] { 16, 15, 0, 4, 0, 17, 3, 100, 2 };
        data[89] = new double[] { 5, 0, 0, 10, 0, 26, 1, 0, 0 };
        data[90] = new double[] { 134, 38, 16, 0, 3, 163, 3, 100, 0 };
        data[91] = new double[] { 73, 16, 15, 1, 2, 77, 3, 100, 0 };
        data[92] = new double[] { 53, 12, 10, 0, 0, 72, 1, 83, 0 };
        data[93] = new double[] { 154, 116, 28, 3, 0, 65, 4, 100, 0 };
        data[94] = new double[] { 44, 13, 6, 2, 0, 37, 2, 100, 0 };
        data[95] = new double[] { 0, 0, 0, 10, 0, 58, 1, 0, 1 };
        data[96] = new double[] { 27, 23, 0, 5, 0, 40, 2, 100, 1 };
        data[97] = new double[] { 67, 23, 13, 2, 1, 110, 2, 75, 1 };
        data[98] = new double[] { 18, 12, 0, 5, 0, 39, 2, 100, 1 };
        data[99] = new double[] { 33, 15, 6, 10, 0, 55, 1, 100, 1 };
        data[100] = new double[] { 14, 9, 0, 10, 0, 15, 3, 0, 2 };
        data[101] = new double[] { 36, 13, 5, 3, 0, 45, 3, 76, 2 };
        data[102] = new double[] { 52, 73, 9, 0, 0, 52, 2, 100, 2 };
        data[103] = new double[] { 67, 69, 22, 0, 0, 53, 3, 88, 2 };
        data[104] = new double[] { 31, 19, 1, 0, 0, 37, 2, 100, 2 };
        data[105] = new double[] { 51, 63, 0, 1, 0, 54, 2, 100, 2 };
        data[106] = new double[] { 53, 91, 7, 1, 0, 52, 2, 97, 2 };
        data[107] = new double[] { 20, 13, 5, 3, 0, 16, 2, 100, 2 };
        data[108] = new double[] { 41, 22, 10, 0, 0, 65, 1, 100, 2 };
        data[109] = new double[] { 20, 11, 1, 10, 0, 87, 3, 100, 2 };
        data[110] = new double[] { 13, 1, 0, 10, 0, 25, 1, 0, 1 };
        data[111] = new double[] { 16, 7, 0, 11, 0, 29, 2, 100, 1 };
        data[112] = new double[] { 47, 27, 0, 10, 0, 140, 3, 100, 1 };
        data[113] = new double[] { 18, 17, 0, 10, 0, 22, 2, 75, 1 };
        data[114] = new double[] { 35, 16, 4, 2, 1, 44, 2, 100, 1 };
        data[115] = new double[] { 54, 33, 2, 5, 0, 59, 2, 100, 2 };
        data[116] = new double[] { 28, 30, 5, 5, 0, 28, 1, 100, 2 };
        data[117] = new double[] { 84, 43, 9, 4, 2, 94, 2, 100, 2 };
        data[118] = new double[] { 48, 9, 25, 0, 2, 41, 1, 100, 2 };
        data[119] = new double[] { 40, 27, 0, 5, 0, 82, 2, 100, 2 };
        data[120] = new double[] { 55, 35, 1, 2, 0, 64, 3, 703.704, 2 };
        data[121] = new double[] { 88, 106, 15, 3, 1, 63, 3, 787.879, 2 };
        data[122] = new double[] { 92, 46, 8, 7, 1, 161, 2, 100, 2 };
        data[123] = new double[] { 102, 26, 16, 1, 1, 92, 2, 962.963, 2 };
        data[124] = new double[] { 38, 11, 8, 0, 0, 55, 2, 100, 2 };
        data[125] = new double[] { 43, 5, 13, 1, 0, 45, 3, 100, 2 };
        data[126] = new double[] { 34, 12, 18, 2, 1, 41, 3, 100, 2 };
        data[127] = new double[] { 77, 25, 15, 0, 1, 53, 2, 100, 2 };
        data[128] = new double[] { 134, 109, 0, 3, 0, 57, 2, 100, 2 };
        data[129] = new double[] { 47, 25, 4, 10, 0, 87, 1, 100, 2 };
        data[130] = new double[] { 52, 33, 5, 1, 0, 62, 1, 100, 2 };
        data[131] = new double[] { 107, 37, 12, 6, 1, 208, 1, 944.444, 2 };
        data[132] = new double[] { 21, 11, 3, 2, 0, 47, 3, 100, 2 };
        data[133] = new double[] { 14, 11, 0, 3, 0, 26, 2, 100, 2 };
        data[134] = new double[] { 24, 11, 1, 4, 0, 33, 2, 70, 2 };
        data[135] = new double[] { 42, 17, 2, 1, 1, 65, 1, 100, 2 };
        data[136] = new double[] { 25, 18, 0, 1, 0, 38, 1, 100, 2 };
        data[137] = new double[] { 69, 76, 9, 1, 1, 43, 2, 100, 2 };
        data[138] = new double[] { 14, 9, 0, 1, 0, 25, 1, 100, 2 };
        data[139] = new double[] { 54, 29, 4, 10, 1, 71, 2, 666.667, 2 };
        data[140] = new double[] { 0, 0, 0, 0, 0, 0, 1, 0, 2 };
        data[141] = new double[] { 30, 10, 4, 2, 0, 17, 3, 100, 2 };
        data[142] = new double[] { 17, 18, 0, 1, 0, 29, 2, 100, 2 };
        data[143] = new double[] { 18, 12, 0, 2, 0, 40, 2, 100, 2 };
        data[144] = new double[] { 14, 9, 0, 3, 0, 33, 2, 100, 2 };
        data[145] = new double[] { 96, 31, 2, 9, 0, 86, 2, 100, 2 };
        data[146] = new double[] { 1, 0, 0, 1, 0, 2, 1, 0, 2 };
        data[147] = new double[] { 64, 23, 11, 1, 1, 115, 2, 100, 2 };
        data[148] = new double[] { 38, 26, 7, 0, 1, 89, 1, 100, 2 };
        data[149] = new double[] { 25, 18, 1, 0, 0, 47, 1, 100, 2 };
        data[150] = new double[] { 38, 33, 1, 0, 0, 56, 2, 100, 2 };
        data[151] = new double[] { 101, 60, 11, 1, 1, 200, 1, 100, 2 };
        data[152] = new double[] { 60, 68, 17, 0, 2, 49, 3, 555.556, 2 };
        data[153] = new double[] { 43, 68, 6, 2, 0, 27, 2, 100, 2 };
        data[154] = new double[] { 38, 19, 2, 10, 0, 149, 2, 62.5, 0 };
        data[155] = new double[] { 46, 16, 6, 2, 0, 32, 2, 100, 0 };
        data[156] = new double[] { 134, 44, 10, 8, 1, 150, 1, 100, 0 };
        data[157] = new double[] { 8, 9, 1, 2, 0, 7, 3, 0, 0 };
        data[158] = new double[] { 26, 17, 0, 10, 0, 78, 1, 100, 2 };
        data[159] = new double[] { 65, 12, 13, 11, 1, 112, 1, 100, 2 };
        data[160] = new double[] { 19, 12, 0, 10, 0, 26, 1, 100, 2 };
        data[161] = new double[] { 25, 16, 1, 11, 0, 40, 1, 100, 2 };
        data[162] = new double[] { 63, 19, 12, 12, 2, 201, 1, 100, 2 };
        data[163] = new double[] { 70, 21, 9, 12, 1, 127, 0, 100, 2 };
        data[164] = new double[] { 91, 50, 5, 5, 1, 195, 2, 888.889, 2 };
        data[165] = new double[] { 138, 56, 19, 4, 1, 242, 1, 904.762, 2 };
        data[166] = new double[] { 0, 0, 0, 3, 0, 6, 1, 0, 2 };
        data[167] = new double[] { 64, 93, 12, 3, 0, 36, 2, 100, 2 };
        data[168] = new double[] { 46, 34, 0, 1, 0, 85, 2, 87.5, 2 };
        data[169] = new double[] { 9, 4, 0, 9, 0, 15, 1, 0, 2 };
        data[170] = new double[] { 62, 29, 0, 5, 0, 102, 1, 100, 2 };
        data[171] = new double[] { 76, 28, 15, 4, 1, 139, 1, 100, 2 };
        data[172] = new double[] { 93, 56, 21, 0, 3, 59, 3, 100, 2 };
        data[173] = new double[] { 57, 20, 14, 1, 0, 133, 1, 84, 2 };
        data[174] = new double[] { 41, 28, 2, 0, 0, 136, 1, 100, 2 };
        data[175] = new double[] { 50, 11, 20, 1, 1, 73, 2, 100, 2 };
        data[176] = new double[] { 49, 34, 3, 4, 0, 93, 1, 100, 2 };
        data[177] = new double[] { 71, 23, 18, 2, 0, 66, 3, 100, 2 };
        data[178] = new double[] { 22, 8, 0, 8, 0, 58, 3, 100, 2 };
        data[179] = new double[] { 18, 4, 0, 10, 0, 62, 4, 100, 2 };
        data[180] = new double[] { 14, 7, 0, 10, 0, 32, 1, 100, 2 };
        data[181] = new double[] { 102, 52, 6, 4, 0, 185, 1, 100, 2 };
        return data;
    }
    // Program class
    public class IndexAndDistance : IComparable<IndexAndDistance>
    {
        public int idx;  // Index of a training item
        public double dist;  // To unknown
                             // Need to sort these to find k closest
        public int CompareTo(IndexAndDistance other)
        {
            if (this.dist < other.dist) return -1;
            else if (this.dist > other.dist) return +1;
            else return 0;
        }
    }
}
