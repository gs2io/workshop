# マネージメントコンソールで行った操作を自動化する

## 再現性のあるデプロイ

ここまで実装する過程で何度もマネージメントコンソールで操作を行いました。これらの操作を自動化することで、再現性のあるデプロイを実現します。
デプロイに再現性がないと、開発環境と本番環境で異なる設定になってしまう可能性があります。

GS2 では、このような問題に対応するために GS2-Deploy というサービスを提供しています。

GS2-Deploy を利用する前に、まずはこれまで作成してきたリソースを削除しましょう。

## リソースの削除

### GS2-Account のリソースを削除

![img_1.png](img/img_1.png)

![img.png](img/img.png)

![img_2.png](img/img_2.png)

### GS2-Inventory のリソースを削除

![img_3.png](img/img_3.png)

![img_4.png](img/img_4.png)

![img_5.png](img/img_5.png)

### GS2-Exchange のリソースを削除

![img_6.png](img/img_6.png)

![img_7.png](img/img_7.png)

![img_8.png](img/img_8.png)

### GS2-Dictionary のリソースを削除

![img_9.png](img/img_9.png)

![img_10.png](img/img_10.png)

![img_11.png](img/img_11.png)

### GS2-Lottery のリソースを削除

![img_12.png](img/img_12.png)

![img_13.png](img/img_13.png)

![img_14.png](img/img_14.png)

### GS2-Showcase のリソースを削除

![img_15.png](img/img_15.png)

![img_16.png](img/img_16.png)

![img_17.png](img/img_17.png)

## GS2-Deploy を利用して GS2-Account のネームスペースを作成する

GS2-Deploy ではテンプレートファイルというファイルを作成し、そのファイルをアップロードすることで環境を構築します。

GS2-Account のネームスペースを作成するテンプレートファイルは以下です。

```yaml
GS2TemplateFormatVersion: "2019-05-01"
Resources:
  AccountNamespace:
    Type: GS2::Account::Namespace
    Properties:
      Name: default
```
[ダウンロード](template/step1.yaml)

このファイルをマネージメントコンソールから 「Deploy > Stacks」 を選択します。

![img_18.png](img/img_18.png)

「スタックの新規作成」を選択し、テンプレートファイルをアップロードします。

![img_19.png](img/img_19.png)

![img_20.png](img/img_20.png)

ネームスペースの作成が進行します。

![img_21.png](img/img_21.png)

GS2-Account のネームスペース一覧で作成されたことを確認します。

![img_22.png](img/img_22.png)

無事作成されています。

続けて、GS2-Inventory のネームスペースを作成します。
GS2-Inventory では、マスターデータもアップロードする必要がありました。
マスターデータのアップロードも GS2-Deploy から行うことが可能です。

```yaml
GS2TemplateFormatVersion: "2019-05-01"
Resources:
  AccountNamespace:
    Type: GS2::Account::Namespace
    Properties:
      Name: default

  InventoryNamespace:
    Type: GS2::Inventory::Namespace
    Properties:
      Name: LotteryTicket

  InventoryMasterData:
    Type: GS2::Inventory::CurrentItemModelMaster
    Properties:
      NamespaceName: LotteryTicket
      Settings:
        {
          "version": "2019-02-05",
          "inventoryModels": [
            {
              "name": "Bag",
              "initialCapacity": 10,
              "maxCapacity": 10,
              "protectReferencedItem": false,
              "itemModels": [
                {
                  "name": "Ticket-1",
                  "stackingLimit": 99,
                  "allowMultipleStacks": false,
                  "sortValue": 0
                },
                {
                  "name": "Ticket-10",
                  "stackingLimit": 99,
                  "allowMultipleStacks": false,
                  "sortValue": 0
                }
              ]
            }
          ],
          "simpleInventoryModels": [],
          "bigInventoryModels": []
        }
    DependsOn:
      - InventoryNamespace
```
[ダウンロード](template/step2.yaml)

マスターデータの設定はネームスペースの作成後に行って欲しいです。
そのような場合は DependsOn を使って依存関係を設定することができます。

このファイルを先ほど作成したスタックを更新する形でアップロードします。

![img_23.png](img/img_23.png)

![img_24.png](img/img_24.png)

前回適用したテンプレートとの差分を計算し、変更があったリソースを表示します。

![img_25.png](img/img_25.png)

問題がなさそうなので「更新」を選択します。

![img_26.png](img/img_26.png)

「InventoryNamespace」 を作成した後で 「InventoryMasterData」 を作成しています。

この調子で他のリソースも作成しましょう。

```yaml
GS2TemplateFormatVersion: "2019-05-01"
Resources:
  AccountNamespace:
    Type: GS2::Account::Namespace
    Properties:
      Name: default

  InventoryNamespace:
    Type: GS2::Inventory::Namespace
    Properties:
      Name: LotteryTicket

  InventoryMasterData:
    Type: GS2::Inventory::CurrentItemModelMaster
    Properties:
      NamespaceName: LotteryTicket
      Settings:
        {
          "version": "2019-02-05",
          "inventoryModels": [
            {
              "name": "Bag",
              "initialCapacity": 10,
              "maxCapacity": 10,
              "protectReferencedItem": false,
              "itemModels": [
                {
                  "name": "Ticket-1",
                  "stackingLimit": 99,
                  "allowMultipleStacks": false,
                  "sortValue": 0
                },
                {
                  "name": "Ticket-10",
                  "stackingLimit": 99,
                  "allowMultipleStacks": false,
                  "sortValue": 0
                }
              ]
            }
          ],
          "simpleInventoryModels": [],
          "bigInventoryModels": []
        }
    DependsOn:
      - InventoryNamespace

  ExchangeNamespace:
    Type: GS2::Exchange::Namespace
    Properties:
      Name: AcquireLotteryTicket
      TransactionSetting:
        EnableAutoRun: true

  ExchangeMasterData:
    Type: GS2::Exchange::CurrentRateMaster
    Properties:
      NamespaceName: AcquireLotteryTicket
      Settings:
        {
          "version": "2019-08-19",
          "rateModels": [
            {
              "name": "Ticket-1",
              "consumeActions": [],
              "timingType": "immediate",
              "lockTime": 0,
              "acquireActions": [
                {
                  "action": "Gs2Inventory:AcquireItemSetByUserId",
                  "request": {
                    "namespaceName": "LotteryTicket",
                    "inventoryName": "Bag",
                    "itemName": "Ticket-1",
                    "userId": "#{userId}",
                    "acquireCount": 1
                  }
                }
              ]
            },
            {
              "name": "Ticket-10",
              "consumeActions": [],
              "timingType": "immediate",
              "lockTime": 0,
              "acquireActions": [
                {
                  "action": "Gs2Inventory:AcquireItemSetByUserId",
                  "request": {
                    "namespaceName": "LotteryTicket",
                    "inventoryName": "Bag",
                    "itemName": "Ticket-10",
                    "userId": "#{userId}",
                    "acquireCount": 1
                  }
                }
              ]
            }
          ],
          "incrementalRateModels": []
        }
    DependsOn:
      - ExchangeNamespace

  DictionaryNamespace:
    Type: GS2::Dictionary::Namespace
    Properties:
      Name: Panel

  DictionaryMasterData:
    Type: GS2::Dictionary::CurrentEntryMaster
    Properties:
      NamespaceName: Panel
      Settings:
        {
          "version": "2020-04-30",
          "entryModels": [
            {
              "name": "Panel-x1y1"
            },
            {
              "name": "Panel-x1y2"
            },
            {
              "name": "Panel-x1y3"
            },
            {
              "name": "Panel-x2y1"
            },
            {
              "name": "Panel-x2y2"
            },
            {
              "name": "Panel-x2y3"
            },
            {
              "name": "Panel-x3y1"
            },
            {
              "name": "Panel-x3y2"
            },
            {
              "name": "Panel-x3y3"
            }
          ]
        }
    DependsOn:
      - DictionaryNamespace

  LotteryNamespace:
    Type: GS2::Lottery::Namespace
    Properties:
      Name: Lottery

  LotteryMasterData:
    Type: GS2::Lottery::CurrentLotteryMaster
    Properties:
      NamespaceName: Lottery
      Settings:
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
                      "request": {
                        "namespaceName": "Panel",
                        "userId": "#{userId}",
                        "entryModelNames": [
                          "Panel-x1y1"
                        ]
                      }
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
                      "request": {
                        "namespaceName": "Panel",
                        "userId": "#{userId}",
                        "entryModelNames": [
                          "Panel-x1y2"
                        ]
                      }
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
                      "request": {
                        "namespaceName": "Panel",
                        "userId": "#{userId}",
                        "entryModelNames": [
                          "Panel-x1y3"
                        ]
                      }
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
                      "request": {
                        "namespaceName": "Panel",
                        "userId": "#{userId}",
                        "entryModelNames": [
                          "Panel-x2y1"
                        ]
                      }
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
                      "request": {
                        "namespaceName": "Panel",
                        "userId": "#{userId}",
                        "entryModelNames": [
                          "Panel-x2y2"
                        ]
                      }
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
                      "request": {
                        "namespaceName": "Panel",
                        "userId": "#{userId}",
                        "entryModelNames": [
                          "Panel-x2y3"
                        ]
                      }
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
                      "request": {
                        "namespaceName": "Panel",
                        "userId": "#{userId}",
                        "entryModelNames": [
                          "Panel-x3y1"
                        ]
                      }
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
                      "request": {
                        "namespaceName": "Panel",
                        "userId": "#{userId}",
                        "entryModelNames": [
                          "Panel-x3y2"
                        ]
                      }
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
                      "request": {
                        "namespaceName": "Panel",
                        "userId": "#{userId}",
                        "entryModelNames": [
                          "Panel-x3y3"
                        ]
                      }
                    }
                  ],
                  "weight": 1
                }
              ]
            }
          ]
        }
    DependsOn:
      - LotteryNamespace

  ShowcaseNamespace:
    Type: GS2::Showcase::Namespace
    Properties:
      Name: LotteryShowcase
      TransactionSetting:
        EnableAutoRun: true

  ShowcaseMasterData:
    Type: GS2::Showcase::CurrentShowcaseMaster
    Properties:
      NamespaceName: LotteryShowcase
      Settings:
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
                        "request": {
                          "namespaceName": "LotteryTicket",
                          "inventoryName": "Bag",
                          "userId": "#{userId}",
                          "itemName": "Ticket-1",
                          "consumeCount": 1
                        }
                      }
                    ],
                    "acquireActions": [
                      {
                        "action": "Gs2Lottery:DrawByUserId",
                        "request": {
                          "namespaceName": "Lottery",
                          "lotteryName": "Panel",
                          "userId": "#{userId}",
                          "count": 1,
                          "config": []
                        }
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
                        "request": {
                          "namespaceName": "LotteryTicket",
                          "inventoryName": "Bag",
                          "userId": "#{userId}",
                          "itemName": "Ticket-10",
                          "consumeCount": 1
                        }
                      }
                    ],
                    "acquireActions": [
                      {
                        "action": "Gs2Lottery:DrawByUserId",
                        "request": {
                          "namespaceName": "Lottery",
                          "lotteryName": "Panel",
                          "userId": "#{userId}",
                          "count": 10,
                          "config": []
                        }
                      }
                    ]
                  }
                }
              ]
            }
          ],
          "randomShowcases": []
        }
    DependsOn:
      - ShowcaseNamespace
```
[ダウンロード](template/step3.yaml)

こちらのテンプレートは、今回のワークショップを通して手作業で作成したリソースが全て含まれています。

![img_27.png](img/img_27.png)

手作業で行った操作を自動化することができました。
これで、いつ「本番環境を作って」と言われても怖くありませんね。

ちなみに、step2.yaml をアップロードすると、変更を巻き戻すことも可能です。

![img_28.png](img/img_28.png)

GS2-Deploy のテンプレートは git などのバージョン管理システムで管理するといいでしょう。

## 最後に

今回はシンプルなゲームサイクルを実装しながら GS2 の基本的な使い方を学びました。
しかし、ワークショップを通して使用したマイクロサービスは GS2 が提供する機能の中でもごくごく一部です。

ぜひ、さまざまな機能を試して、さまざまなゲームの機能を実装してみてください。
そして、いつかは製品での活用もご検討ください。