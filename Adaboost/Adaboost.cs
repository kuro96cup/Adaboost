using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adaboost
{
    public class Adaboost
    {
        float[][] data;               //データ集合
        int[] classElemnt;            //クラス属性 0 or 1
        int k;                        //分類器の数
        float[][] w;                  //重み　i行j列  
        float[] errorM;
        int p;                         //分割数
        public Adaboost(float[][] data,int k,int[] classElement,int p)
        {
            this.classElemnt = classElement;
            this.data = data;
            this.k = k;
            this.errorM = new float[k];
            this.p = p;
            initWeight();
        }

        //重みの初期化
        public void initWeight()
        {
            w = new float[k][];

            for (int i = 0; i < k; i++)
            {
                w[i] = new float[data.GetLength(0)];
                for (int j = 0; j < data.GetLength(0); j++)

                {
                    w[i][j] = 1.0f / data.GetLength(0);
                }
            }
        }
        public DecisionTreeClassifyer[] boosting()
        {
            DecisionTreeClassifyer[] dtcf = new DecisionTreeClassifyer[k];
            for (int i=0; i < k; i++) {
                SelectBestDecisitonTree sbdt = new SelectBestDecisitonTree(data, classElemnt, p);
                dtcf[i] = sbdt.getBestDecisionTreeClassifyer(w[i]);
                //Miの誤り率の計算
                errorM[i] = 0;
                for (int j = 0; j < data.GetLength(0); j++)
                {
                    errorM[i] += w[i][j] * (dtcf[i].classify(data[j])==classElemnt[j]? 0.0f:1.0f);
                }
                dtcf[i].error = errorM[i];
                if (i < k - 1)
                {
                    for (int j = 0; j < data.GetLength(0); j++)
                    {
                        if (dtcf[i].classify(data[j]) == classElemnt[j])
                        {
                            w[i + 1][j] = w[i][j] * (errorM[i] / (1.0f - errorM[i]));
                        }
                    }
                    float wsum = 0;
                    for (int j = 0; j < data.GetLength(0); j++)
                    {
                        wsum += w[i + 1][j];
                    }
                    for (int j = 0; j < data.GetLength(0); j++)
                    {
                        w[i + 1][j] = w[i + 1][j] / wsum;
                    }
                }
            }
            return dtcf;
        }

    }
}
