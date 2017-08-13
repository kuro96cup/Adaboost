using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adaboost
{
    class Program
    {

        static void Main(string[] args)
        {
          getCorrectParcent(3, 200, 1500);
        }
        static float getCorrectParcent(int k, int p, int trainingDataNum)
        {
            /*
            int k = 3;  //分類器の数
            int p = 10; //分割数
            int trainingDataNum = 1500; //学習データ数
            */
            string path = "csvファイルのパス";
            float[][] data = ReadCsv(path, "データのcsvファイル名");
            float[][] trainingData = new float[trainingDataNum][];
            int[] classElement = ReadCsvInt(path, "クラス属性値のcsvファイル名")[0];
            int[] trainingClassElement = new int[trainingDataNum];
            //float[][] testdata = ReadCsv(path, "winequality-red_test.csv");
            for (int j = 0; j < trainingDataNum; j++)
            {
                trainingData[j] = data[j];
                trainingClassElement[j] = classElement[j];
            }
            float[][] testdata = new float[data.GetLength(0) - trainingDataNum][];
            int[] testClassElement = new int[data.GetLength(0) - trainingDataNum];

            for (int j = 0; j < data.GetLength(0) - trainingDataNum; j++)
            {
                testdata[j] = data[j + trainingDataNum];
                testClassElement[j] = classElement[j + trainingDataNum];
            }
            
            Adaboost adaboost = new Adaboost(data, k, classElement, p);
            DecisionTreeClassifyer[] dtcf = adaboost.boosting();
            Console.WriteLine();
            for (int i = 0; i < k; i++)
            {
                Console.WriteLine("v=" + dtcf[i].v + ",element_num=" + dtcf[i].elementType);
            }

            int correct = 0;
            //分類器を用いた分類の処理
            for (int j = 0; j < testdata.GetLength(0); j++)
            {
                float[] v = new float[2] { 0, 0 };
                for (int i = 0; i < k; i++)
                {
                    float w = (1 - dtcf[i].error) / dtcf[i].error;
                    int c = dtcf[i].classify(testdata[j]);
                    v[c] = v[c] + w;
                }
                //System.Console.WriteLine( j + " = " + (v[1]>v[0]?1:0));
                if (testClassElement[j] == (v[1] > v[0] ? 1 : 0))
                {
                    correct += 1;
                }
            }
            //System.Console.WriteLine(k+","+p+","+trainingDataNum+","+((float)correct / testdata.GetLength(0)) * 100);
            StreamWriter writer =
            new StreamWriter(@"C:/Users/ohmachi/Documents/Visual Studio 2017/Projects/Adaboost/Adaboost/result.csv",true);
            writer.WriteLine(k + "," + p + "," + trainingDataNum + "," + ((float)correct / testdata.GetLength(0)) * 100);
            writer.Close();
            return ((float)correct / testdata.GetLength(0)) * 100;
    }
        static float[][] ReadCsv(string path,string filename)

        {
            List<string[]> stArrayData = new List<string[]>();
            try
            {
                // csvファイルを開く
                System.IO.DirectoryInfo dirPath =
                    new System.IO.DirectoryInfo(@path);
                System.IO.FileInfo[] files =
                     dirPath.GetFiles(filename, System.IO.SearchOption.AllDirectories);

                // 指定フォルダからCSVを取得し、配列に格納する。
                foreach (System.IO.FileInfo filePath in files)
                {
                    using (var readCsv = new System.IO.StreamReader(filePath.FullName))
                    {
                        //ヘッダを読み捨てる。
                        //readCsv.ReadLine();
                        // ストリームの末尾まで繰り返す
                        while (!readCsv.EndOfStream)
                        {
                            // ファイルから一行読み込む
                            string line = readCsv.ReadLine();
                            // カンマ区切りで分割して配列に格納する
                            string[] record = line.Split(',');  // ★
                            stArrayData.Add(record);            // ★
                        }
                    }
                }
                float[][] outArray = new float[stArrayData.Count][];
                for(int j=0;j< stArrayData.Count; j++)
                {
                    outArray[j] = new float[stArrayData[0].Length];
                    for (int i=0;i<stArrayData[0].Length;i++)
                    {
                        outArray[j][i] =float.Parse(stArrayData[j][i]);
                    }
                }
                return outArray;
            }
            catch (System.Exception e)
            {
                // ファイルを開くのに失敗したとき
                System.Console.WriteLine(e.Message);
                return null;
            }
        }
        static int[][] ReadCsvInt(string path, string filename)

        {
            List<string[]> stArrayData = new List<string[]>();
            try
            {
                // csvファイルを開く
                System.IO.DirectoryInfo dirPath =
                    new System.IO.DirectoryInfo(@path);
                System.IO.FileInfo[] files =
                     dirPath.GetFiles(filename, System.IO.SearchOption.AllDirectories);

                // 指定フォルダからCSVを取得し、配列に格納する。
                foreach (System.IO.FileInfo filePath in files)
                {
                    using (var readCsv = new System.IO.StreamReader(filePath.FullName))
                    {
                        //ヘッダを読み捨てる。
                        //readCsv.ReadLine();
                        // ストリームの末尾まで繰り返す
                        while (!readCsv.EndOfStream)
                        {
                            // ファイルから一行読み込む
                            string line = readCsv.ReadLine();
                            // カンマ区切りで分割して配列に格納する
                            string[] record = line.Split(',');  // ★
                            stArrayData.Add(record);            // ★
                        }
                    }
                }
                int[][] outArray = new int[stArrayData.Count][];
                for (int j = 0; j < stArrayData.Count; j++)
                {
                    outArray[j] = new int[stArrayData[0].Length];
                    for (int i = 0; i < stArrayData[0].Length; i++)
                    {
                        outArray[j][i] = int.Parse(stArrayData[j][i]);
                    }
                }
                return outArray;
            }
            catch (System.Exception e)
            {
                // ファイルを開くのに失敗したとき
                System.Console.WriteLine(e.Message);
                return null;
            }
        }
    }
}
