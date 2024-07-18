using System;
using System.Collections;
using System.Linq;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using Gs2.Core.Exception;
using Gs2.Unity.Gs2Inventory.Model;
using Gs2.Unity.Util;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Inventory : MonoBehaviour
{
    [SerializeField] private TMP_Text inventory;
    
    public SuccessEvent onReload;
    public ErrorEvent onFailure;

    private void OnEnable() {
        async UniTask SubscribeAsync()
        {
            void Reload(EzItemSet[] items) {
                if (items.Length == 0) {
                    // チケットを一枚も持っていない
                    this.inventory.SetText("Do not have a ticket");
                }
                else {
                    // 何らかのチケットを持っている
                    this.inventory.SetText(string.Join("\n", items.Select(item => $"{item.ItemName}: x{item.Count}")));
                }
                
                this.onReload.Invoke();
            }
            
            try
            {
                var domain = Gs2ClientHolder.Instance.Gs2.Inventory.Namespace(
                    namespaceName: "LotteryTicket"
                ).Me(
                    gameSession: Gs2GameSessionHolder.Instance.GameSession
                ).Inventory(
                    inventoryName: "Bag"
                );
                
                // 所持しているアイテムの一覧に変化があった時に呼び出されるコールバックを追加
                domain.SubscribeItemSets(Reload);
                
                // 初期値を設定
                Reload(await domain.ItemSetsAsync().ToArrayAsync());
            }
            catch (Gs2Exception e)
            {
                this.onFailure.Invoke(e, null);
            }
        }
        StartCoroutine(SubscribeAsync().ToCoroutine());
    }

    [Serializable]
    public class SuccessEvent : UnityEvent
    {
    }
    
    [Serializable]
    public class ErrorEvent : UnityEvent<Gs2Exception, Func<IEnumerator>>
    {
    }
}
