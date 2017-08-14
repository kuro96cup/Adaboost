using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adaboost
{
    public class DecisionTreeClassifyer
    {
        //判定条件(op,v)
        //op is operater ,op={0:<,1:>}
        public int op;
        public float v;
        public int elementType;
        //分類器の誤り率
        public float error;
        //分類に必要な要素を初期化
        //判定条件の書き換えと属性番号（属性のタイプ）の書き換えは禁止。
        public DecisionTreeClassifyer(float v, int elementType,int op)
        {
            this.v = v;
            this.elementType = elementType;
            this.op = op;
        }
        public DecisionTreeClassifyer()
        {

        }
        //与えられたデータのelementTypeの値をvと比較しクラスわけする。
        public int classify(float[] data)
        {
            if(op==0){
            return data[elementType] > v ? 1 : 0;
            }else if(op==1){
            return data[elementType] < v ? 1 : 0;
            }else{
}                return -1;
        }
    }
}
