# GS2-Showcase にガチャチケットを消費してガチャを引く商品を登録

## GS2-Showcase の設定

### ネームスペースの作成

まずは、GS2-Showcase にガチャを引くために購入する商品を管理するネームスペースを作成します。
今回のサンプルは商品の陳列棚を利用しないため GS2-Exchange でも代用可能なのですが、少しでも多くのマイクロサービスを利用することで理解を深めていただきたく GS2-Showcase を選択しています。

マネージメントコンソールのサイドメニューから「Showcase > Namespaces」を選択します。

![img.png](img/img.png)

次に「ネームスペースの新規作成」を選択します。

![img_1.png](img/img_1.png)

ネームスペースの設定項目を入力して「作成」ボタンを押下します。

![img_2.png](img/img_2.png)

![img_3.png](img/img_3.png)

### マスターデータの作成

#### 商品を登録

これまでと同様にマスターデータエディタを選択します。

![img_4.png](img/img_4.png)

「商品の新規作成」を選択します。

![img_5.png](img/img_5.png)

商品名に「Lottery-1」を指定し、消費アクションリストの「＋」を選択します。

![img_6.png](img/img_6.png)![img_6.png](img_6.png

続けて、「スタンプシートを使用して実行するアクションの種類」に「GS2-Inventory: ユーザーIDを指定してインベントリのアイテムを消費」を選択します。

![img_7.png](img/img_7.png)

消費するアイテムの情報を入力します。

![img_8.png](img/img_8.png)

続けて、「入手アクションリスト」の「＋」を選択します。

![img_9.png](img/img_9.png)

「スタンプシートを使用して実行するアクションの種類」に「GS2-Lottery: ユーザーIDを指定して抽選を実行」を選択します。

![img_10.png](img/img_10.png)

抽選を実行する抽選モデルの情報を選択します。

![img_11.png](img/img_11.png)

作成を選択します。

続けて、10連ガチャの商品を登録します。

![img_12.png](img/img_12.png)

商品名に「Lottery-10」を指定し、消費アクションリストのでは「Ticket-10」を「1」個消費するように設定します。
入手アクションリストでは、抽選回数に「10」を設定します。

![img_13.png](img/img_13.png)

#### 陳列棚を登録

「陳列棚マスターの新規作成」を選択します。

![img_15.png](img/img_15.png)

陳列棚の名前に「Showcase」を指定し、陳列された商品リストに「Lottery-1」と「Lottery-10」を登録します。

![img_16.png](img/img_16.png)

### マスターデータをエクスポート

次に、マスターデータをエクスポートします。手順は GS2-Inventory や GS2-Exchange の時と同じです。

![img_14.png](img/img_14.png)

```json
{
  "version": "2019-04-04",
  "showcases": [
    {
      "name": "Showcase",
      "displayItems": [
        {
          "displayItemId": "Lottery-1",
          "type": "salesItem",
          "salesItem": {
            "name": "Lottery-1",
            "consumeActions": [
              {
                "action": "Gs2Inventory:ConsumeItemSetByUserId",
                "request": "{\n  \"namespaceName\": \"LotteryTicket\",\n  \"inventoryName\": \"Bag\",\n  \"userId\": \"#{userId}\",\n  \"itemName\": \"Ticket-1\",\n  \"consumeCount\": 1\n}"
              }
            ],
            "acquireActions": [
              {
                "action": "Gs2Lottery:DrawByUserId",
                "request": "{\n  \"namespaceName\": \"Lottery\",\n  \"lotteryName\": \"Panel\",\n  \"userId\": \"#{userId}\",\n  \"count\": 1,\n  \"config\": []\n}"
              }
            ]
          }
        },
        {
          "displayItemId": "Lottery-10",
          "type": "salesItem",
          "salesItem": {
            "name": "Lottery-10",
            "consumeActions": [
              {
                "action": "Gs2Inventory:ConsumeItemSetByUserId",
                "request": "{\n  \"namespaceName\": \"LotteryTicket\",\n  \"inventoryName\": \"Bag\",\n  \"userId\": \"#{userId}\",\n  \"itemName\": \"Ticket-10\",\n  \"consumeCount\": 1\n}"
              }
            ],
            "acquireActions": [
              {
                "action": "Gs2Lottery:DrawByUserId",
                "request": "{\n  \"namespaceName\": \"Lottery\",\n  \"lotteryName\": \"Panel\",\n  \"userId\": \"#{userId}\",\n  \"count\": 10,\n  \"config\": []\n}"
              }
            ]
          }
        }
      ]
    }
  ],
  "randomShowcases": []
}
```

### マスターデータのインポート

エクスポートしたマスターデータをインポートします。

![img_17.png](img/img_17.png)

エクスポートした JSON ファイルを選択して「更新」ボタンを押下します。

![img_18.png](img/img_18.png)

以上で GS2-Showcase の設定は完了です。

![img_19.png](img/img_19.png)

## 次のステップへ

[GS2-Showcase にガチャチケットを消費してガチャを引く商品を登録](../step0007)