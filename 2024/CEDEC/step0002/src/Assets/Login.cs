using System;
using System.Collections;
using System.Linq;
using Cysharp.Threading.Tasks;
using Gs2.Core.Exception;
using Gs2.Unity.Util;
using Gs2.Util.WebSocketSharp;
using UnityEngine;
using UnityEngine.Events;

public class Login : MonoBehaviour
{
    public string accountNamespaceName;
    
    public SuccessEvent onSuccess;
    public ErrorEvent onFailure;

    [SerializeField] public string userId;
    [SerializeField] public string password;
    
    public void OnLogin()
    {
        async UniTask LoginAsync()
        {
            try {
                if (this.userId.IsNullOrEmpty() && this.password.IsNullOrEmpty())
                {
                    // Create an anonymous _ezAccount
                    var result = await Gs2ClientHolder.Instance.Gs2.Account.Namespace(
                        namespaceName: this.accountNamespaceName
                    ).CreateAsync();
                    var account = await result.ModelAsync();
                    this.userId = account.UserId;
                    this.password = account.Password;
                }
                
                Debug.Log($"UserId: {this.userId}");
                Debug.Log($"Password: {this.password}");
    
                // Log-in created anonymous _ezAccount
                var gameSession = await Gs2ClientHolder.Instance.Gs2.LoginAsync(
                    new Gs2AccountAuthenticator(
                        accountSetting: new AccountSetting
                        {
                            accountNamespaceName = this.accountNamespaceName,
                        }
                    ),
                    this.userId,
                    this.password
                );
                
                this.onSuccess.Invoke(gameSession);
            }
            catch (Gs2Exception e)
            {
                this.onFailure.Invoke(e, () => LoginAsync().ToCoroutine());
            }
        }
        StartCoroutine(LoginAsync().ToCoroutine());
    }

    [Serializable]
    public class SuccessEvent : UnityEvent<GameSession>
    {
    }
    
    [Serializable]
    public class ErrorEvent : UnityEvent<Gs2Exception, Func<IEnumerator>>
    {
    }
}