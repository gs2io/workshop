using System;
using System.Collections;
using Cysharp.Threading.Tasks;
using Gs2.Core.Exception;
using Gs2.Unity.Util;
using UnityEngine;
using UnityEngine.Events;

public class AcquireTicket : MonoBehaviour
{
    public SuccessEvent onSuccess;
    public ErrorEvent onFailure;
    
    public void Exchange(string exchangeRateName)
    {
        async UniTask ExchangeAsync(string exchangeRateName)
        {
            try
            {
                // 交換処理を実行
                var transaction = await Gs2ClientHolder.Instance.Gs2.Exchange.Namespace(
                    namespaceName: "AcquireLotteryTicket"
                ).Me(
                    gameSession: Gs2GameSessionHolder.Instance.GameSession
                ).Exchange(
                ).ExchangeAsync(
                    rateName: exchangeRateName,
                    count: 1
                );

                // 交換処理の完了を待つ
                await transaction.WaitAsync(true);
                
                this.onSuccess.Invoke();
            }
            catch (Gs2Exception e)
            {
                this.onFailure.Invoke(e, () => ExchangeAsync(exchangeRateName).ToCoroutine());
            }
        }
        
        StartCoroutine(ExchangeAsync(exchangeRateName).ToCoroutine());
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
