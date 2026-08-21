using System;
using System.Threading;
using Agents;
using CoreLib;
using Cysharp.Threading.Tasks;
using Febucci.UI;
using Players;
using Systems.TurnSystem;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace Systems.Tutorial
    {
        public class TutorialSystem : MonoSingleton<TutorialSystem>
        {
            [Header("Objects For TutorialSystem")]
            [SerializeField] private Button fireButton;
            [SerializeField] private PlayerMovement moveModule;
            [SerializeField] private GameObject bounceOrb;
            [SerializeField] private GameObject cardContainer;
            [SerializeField] private GameObject[] arrowModule;
            [SerializeField] private GameObject enemy;
            
            private TextAnimator_TMP textAnimator;
            private TypewriterByCharacter tutorialText;
            private CancellationTokenSource tokenSource;
            private bool isTutorialStopped;
            
            protected override void Awake()
            {
                base.Awake();
                
                tokenSource = new CancellationTokenSource();
                
                textAnimator = GetComponent<TextAnimator_TMP>();
                tutorialText = GetComponent<TypewriterByCharacter>();
            }
            
            private void Start()
            {
                RunTutorial(tokenSource.Token).Forget();
            }

            private void Update()
            {
                if (!isTutorialStopped && TurnManager.Instance.CurrentState.Value == TurnManager.TurnState.Lose ||
                    enemy.GetComponentInChildren<HealthModule>().CurrentHealth.Value <= 0)
                {
                    StopTutorial();
                }
            }

            private void StopTutorial()
            {
                isTutorialStopped = true;
                tokenSource.Cancel();
                tutorialText.ShowText("");
                
                foreach(GameObject arrow  in arrowModule)
                    arrow.SetActive(false);
            }

            private async UniTaskVoid RunTutorial(CancellationToken token)
            {
                try
                {
                    textAnimator.DefaultAppearancesTags = Array.Empty<string>();
                    
                    await UniTask.Delay(1500, cancellationToken: token);
                    
                    tutorialText.ShowText("반갑습니다, 플레이어");
                    await WaitForClick(1000, token);

                    tutorialText.ShowText("전투에 들어가기 앞서,플레이어는 기초적인 훈련을 받아야 합니다.");
                    await WaitForClick(2500, token);

                    tutorialText.ShowText("플레이어는 <incr>턴</incr>을 전부 사용하기 전에 적들을 모두 처치해야 합니다.");
                    await WaitForClick(2250, token);

                    tutorialText.ShowText("턴은 왼쪽 상단에 확인할 수 있으며, 스테이지마다 사용할 수 있는 턴은 다릅니다.");
                    arrowModule[0].SetActive(true);
                    await WaitForClick(2750, token);

                    tutorialText.ShowText("턴은 플레이어 턴, 상대방 턴, 이렇게 있고 두 팀이 공격 시 턴 하나가 소모됩니다.");
                    arrowModule[0].SetActive(false);
                    await WaitForClick(2750, token);

                    tutorialText.ShowText("플레이어 턴일 때는 움직일 수가 있습니다.");
                    await WaitForClick(1500, token);

                    tutorialText.ShowText("이제 A와 D키를 활용해서 움직여 보세요.");
                    moveModule.enabled = true;
                    await WaitForClick(1500, token);

                    tutorialText.ShowText("하단 왼쪽에 한 턴에 움직일 수 있는 연료가 있습니다.");
                    arrowModule[1].SetActive(true);
                    await WaitForClick(2250, token);

                    tutorialText.ShowText("턴이 끝나면 연료가 자동으로 충전되고 연료가 부족하면 움직일 수 없습니다.");
                    await WaitForClick(2250, token);

                    tutorialText.ShowText("그리고 플레이어는 우클릭으로 발사 방향을 조절할 수 있으며");
                    arrowModule[1].SetActive(false);
                    await WaitForClick(2250, token);

                    tutorialText.ShowText("우클릭 상태에서 마우스를 플레이어 중심으로 이동하면 발사 힘을 조절할 수 있습니다.");
                    await WaitForClick(2250, token);

                    tutorialText.ShowText("오른쪽 하단에 있는 버튼을 누르면 공격할 수 있고 내 턴을 소모합니다.");
                    arrowModule[2].SetActive(true);
                    await WaitForClick(2000, token);
                    
                    tutorialText.ShowText("플레이어는 오른쪽에 있는 <rainb>파란 구슬</rainb>을 맞춰보세요.");
                    fireButton.interactable = true;
                    await WaitForDestroyOrb(token);

                    tutorialText.ShowText("이제 마우스를 카드에 대고 <rainb>Y</rainb>를 눌러 카드를 사용하세요.");
                    arrowModule[2].SetActive(false);
                    GameObject card = cardContainer.transform.GetChild(0).gameObject;
                    await WaitForUseCard(card, token);

                    tutorialText.ShowText("카드를 사용하면 총알의 능력이 생기며, 플레이어 턴일 때 한 번 사용할 수 있습니다.");
                    await WaitForClick(2500, token);

                    tutorialText.ShowText("적을 처치하세요.");
                    await WaitForEnemyDead(enemy.GetComponentInChildren<HealthModule>(), token);

                    tutorialText.ShowText("");
                    PlayerPrefs.SetInt("TutorialCleared", 1);
                    PlayerPrefs.Save();
                }
                catch (OperationCanceledException)
                {
                    
                }
            }

            private async UniTask WaitForClick(int waitTime, CancellationToken token)
            {
                await UniTask.Delay(waitTime, cancellationToken: token); // 이거 조심
                await UniTask.WaitUntil(() => Input.GetMouseButtonDown(0), cancellationToken: token);
            }

            private async UniTask WaitForDestroyOrb(CancellationToken token)
            {
                await UniTask.WaitUntil(() => bounceOrb.IsDestroyed(), cancellationToken: token);
            }

            private async UniTask WaitForUseCard(GameObject card, CancellationToken token)
            {
                await UniTask.WaitUntil(card.IsDestroyed, cancellationToken: token);
            }

            private async UniTask WaitForEnemyDead(HealthModule enemyHealth, CancellationToken token)
            {
                await UniTask.WaitUntil(() => enemyHealth.CurrentHealth.Value <= 0, cancellationToken: token);
            }
        }
    }