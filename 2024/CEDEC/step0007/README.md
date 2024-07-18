# GS2-Lottery にパネルを排出するガチャを登録

## GS2-Lottery の設定

### ネームスペースの作成

まずは、GS2-Lottery にガチャの排出するコンテンツや確率を管理するネームスペースを作成します。

マネージメントコンソールのサイドメニューから「Lottery > Namespaces」を選択します。

![img.png](img/img.png)

次に「ネームスペースの新規作成」を選択します。

![img_1.png](img/img_1.png)

ネームスペースの設定項目を入力して「作成」ボタンを押下します。

![img_2.png](img/img_2.png)

![img_3.png](img/img_3.png)

### マスターデータの作成

#### 排出確率テーブルの登録

これまでと同様にマスターデータエディタを選択します。

![img_4.png](img/img_4.png)

「排出確率テーブルマスターの新規作成」を選択します。

![img_5.png](img/img_5.png)

排出確率テーブル名に「Panel」と入力し、景品リストの「＋」を選択します。

![img_6.png](img/img_6.png)

景品IDに「Panel-x1y1」と入力し、景品の種類に「景品の入手アクション」を選択し、排出重みに「1」を設定します。
つづけて、景品の入手アクションリストの「＋」を選択します。

![img_7.png](img/img_7.png)

スタンプシートを使用して実行するアクションの種類 に「GS2-Dictionary: ユーザIDを指定してエントリーを入手済みとして登録」を選択します。

![img_8.png](img/img_8.png)

ネームスペース名に「Panel」、エントリーモデル名に「Panel-x1y1」を設定します。

![img_9.png](img/img_9.png)

景品リストの「＋」を選択して、同様の手順で以下の景品を登録します。
時間が勿体無いと感じた場合はこの先にエクスポート結果の JSON ファイルがありますので、それをテキストファイルとして保存して利用してください。

- Panel-x1y2
- Panel-x1y3
- Panel-x2y1
- Panel-x2y2
- Panel-x2y3
- Panel-x3y1
- Panel-x3y2
- Panel-x3y3

非常に入力フォームが長くなりますが、以下のようになります。

![img_10.png](img/img_10.png)

「作成」ボタンを押下して登録します。

![img_11.png](img/img_11.png)

#### 抽選モデルの登録

一旦ネームスペースの詳細ページに戻ります。

続けて「抽選モデルマスターの新規作成」を選択します。

![img_12.png](img/img_12.png)

各項目を入力して「作成」ボタンを押下します。

![img_13.png](img/img_13.png)

### マスターデータをエクスポート

「マスターデータのエクスポート」を選択して、マスターデータをエクスポートします。

![img_14.png](img/img_14.png)

```json
{
  "version": "2019-02-21",
  "lotteryModels": [
    {
      "name": "Panel",
      "mode": "normal",
      "method": "prize_table",
      "prizeTableName": "Panel"
    }
  ],
  "prizeTables": [
    {
      "name": "Panel",
      "prizes": [
        {
          "prizeId": "Panel-x1y1",
          "type": "action",
          "acquireActions": [
            {
              "action": "Gs2Dictionary:AddEntriesByUserId",
              "request": "{\n  \"namespaceName\": \"Panel\",\n  \"userId\": \"#{userId}\",\n  \"entryModelNames\": [\n    \"Panel-x1y1\"\n  ]\n}"
            }
          ],
          "weight": 1
        },
        {
          "prizeId": "Panel-x1y2",
          "type": "action",
          "acquireActions": [
            {
              "action": "Gs2Dictionary:AddEntriesByUserId",
              "request": "{\n  \"namespaceName\": \"Panel\",\n  \"userId\": \"#{userId}\",\n  \"entryModelNames\": [\n    \"Panel-x1y2\"\n  ]\n}"
            }
          ],
          "weight": 1
        },
        {
          "prizeId": "Panel-x1y3",
          "type": "action",
          "acquireActions": [
            {
              "action": "Gs2Dictionary:AddEntriesByUserId",
              "request": "{\n  \"namespaceName\": \"Panel\",\n  \"userId\": \"#{userId}\",\n  \"entryModelNames\": [\n    \"Panel-x1y3\"\n  ]\n}"
            }
          ],
          "weight": 1
        },
        {
          "prizeId": "Panel-x2y1",
          "type": "action",
          "acquireActions": [
            {
              "action": "Gs2Dictionary:AddEntriesByUserId",
              "request": "{\n  \"namespaceName\": \"Panel\",\n  \"userId\": \"#{userId}\",\n  \"entryModelNames\": [\n    \"Panel-x2y1\"\n  ]\n}"
            }
          ],
          "weight": 1
        },
        {
          "prizeId": "Panel-x2y2",
          "type": "action",
          "acquireActions": [
            {
              "action": "Gs2Dictionary:AddEntriesByUserId",
              "request": "{\n  \"namespaceName\": \"Panel\",\n  \"userId\": \"#{userId}\",\n  \"entryModelNames\": [\n    \"Panel-x2y2\"\n  ]\n}"
            }
          ],
          "weight": 1
        },
        {
          "prizeId": "Panel-x2y3",
          "type": "action",
          "acquireActions": [
            {
              "action": "Gs2Dictionary:AddEntriesByUserId",
              "request": "{\n  \"namespaceName\": \"Panel\",\n  \"userId\": \"#{userId}\",\n  \"entryModelNames\": [\n    \"Panel-x2y3\"\n  ]\n}"
            }
          ],
          "weight": 1
        },
        {
          "prizeId": "Panel-x3y1",
          "type": "action",
          "acquireActions": [
            {
              "action": "Gs2Dictionary:AddEntriesByUserId",
              "request": "{\n  \"namespaceName\": \"Panel\",\n  \"userId\": \"#{userId}\",\n  \"entryModelNames\": [\n    \"Panel-x3y1\"\n  ]\n}"
            }
          ],
          "weight": 1
        },
        {
          "prizeId": "Panel-x3y2",
          "type": "action",
          "acquireActions": [
            {
              "action": "Gs2Dictionary:AddEntriesByUserId",
              "request": "{\n  \"namespaceName\": \"Panel\",\n  \"userId\": \"#{userId}\",\n  \"entryModelNames\": [\n    \"Panel-x3y2\"\n  ]\n}"
            }
          ],
          "weight": 1
        },
        {
          "prizeId": "Panel-x3y3",
          "type": "action",
          "acquireActions": [
            {
              "action": "Gs2Dictionary:AddEntriesByUserId",
              "request": "{\n  \"namespaceName\": \"Panel\",\n  \"userId\": \"#{userId}\",\n  \"entryModelNames\": [\n    \"Panel-x3y3\"\n  ]\n}"
            }
          ],
          "weight": 1
        }
      ]
    }
  ]
}
```

### マスターデータのインポート

エクスポートしたマスターデータをインポートします。

![img_15.png](img/img_15.png)

![img_16.png](img/img_16.png)

エクスポートした JSON ファイルを選択して「更新」ボタンを押下します。

![img_17.png](img/img_17.png)

以上で GS2-Lottery の設定は完了です。

## 次のステップへ

[GS2-Showcase にガチャチケットを消費してガチャを引く商品を登録](../step0007)