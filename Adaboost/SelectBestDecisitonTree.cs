using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adaboost
{
    public class SelectBestDecisitonTree
    {
        float[][] data;              //データ集合
        int[] classElement;       //クラス属性 0 or 1
        //float[] w;                 //重み
        float[] max;                //各属性の最大値
        float[] min;                //各属性の最小値
        float[] step;               //刻み幅
        int k;                  //分割数
        DecisionTreeClassifyer best = null;
        public SelectBestDecisitonTree(float[][] data,int[] classElement,int k)
        {
            this.data = data;
            this.classElement = classElement;
            this.k = k;
            this.min = new float[data[0].GetLength(0)];
            this.max = new float[data[0].GetLength(0)];
            this.step = new float[data[0].GetLength(0)];
            calculateStep();
        }
        private void calculateStep()
        {
            for (int r = 0; r < data[0].GetLength(0); r++)
            {
                this.min[r] = 10000.0f;
                this.max[r] = 0.0f;
                //X1からXdの各属性xrの値のmin,maxを求める
                for (int j = 0; j < data.GetLength(0); j++)
                {
                    if (data[j][r] < this.min[r]) this.min[r] = data[j][r];
                    else if (data[j][r] > this.max[r]) this.max[r] = data[j][r];
                }
                //刻み幅の計算
                this.step[r] = (this.max[r] - this.min[r]) / (float)k;
            }

        }
        public DecisionTreeClassifyer getBestDecisionTreeClassifyer(float[] w)
        {
            //最小誤り率を無限大に初期化
            float errMin = 10000;
            for (int r = 0; r < data[0].GetLength(0); r++)
            {
                for(int op=0;op<=1;op++){
                for (float v = this.min[r] - this.step[r]; v < this.min[r] + ((float)k + 1.0f) * this.step[r]; v += this.step[r])
                {
                    float werr = 0.0f;
                    DecisionTreeClassifyer dtcf = new DecisionTreeClassifyer(v, r,op);
                    for (int j = 0; j < data.GetLength(0); j++)
                    {
                        werr += w[j]*(dtcf.classify(data[j]) == classElement[j] ? 0.0f : 1.0f);
                    }

                    if (errMin > werr)
                    {
                        best = dtcf;
                        errMin = werr;
                    }
                }
                }
            }
            
            return best;
        }
    }
}
