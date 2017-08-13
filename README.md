# Adaboost
決定株によるAdaboostアルゴリズムの実装
## 動作環境
Visual Studio 2017 community のコンソールプロジェクトで作成
## 使用方法
```
 string path = "csvファイルのパス";
            float[][] data = ReadCsv(path, "データのcsvファイル名");
            float[][] trainingData = new float[trainingDataNum][];
            int[] classElement = ReadCsvInt(path, "クラス属性値のcsvファイル")[0];
```
Program.cの上記のコードの
- csvファイルのパスをAdaboostプロジェクトのパス
- データのcsvファイル名をwinequality-red.csv
- クラス属性値のcsvファイル名をwinequality-red_class_element.csv

に設定し、ソリューションをビルドし、実行できます。

生成されるデータは

```
            writer.WriteLine(k + "," + p + "," + trainingDataNum + "," + ((float)correct / testdata.GetLength(0)) * 100);

```
によって

分類器の数,分割数,分類器の属性番号,正解率

を持つresult.csvがプロジェクト内に吐き出されます。

*テスト用のデータはhttp://archive.ics.uci.edu/ml/datasets/Wine　を用いています。
