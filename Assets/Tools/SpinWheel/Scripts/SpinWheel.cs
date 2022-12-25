using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using TMPro;

public class SpinWheel : MonoBehaviour
{
    [Header("Referance")]
    public List<GameObject> Lights;
    public AudioSource spinAudio;
    public AudioSource winnerAudio;
    public AudioSource claimAudio;
    public GameObject coinAnimator;
    public RectTransform wheelRect;
    public GameObject claimPanel;
    public Button spinBtn;
    public Text winText;
    public TextMeshProUGUI spinWheelCoinText;
    
    [Header("Settings")]
    public float spinTourne = 3f;
    [Range(0f, 20f)] public float smoothTime = 8f;
    [Range(0f, 100f)] public float smoothToComplet = 45f;


    [Header("Items")]
    public List<spinrItem> items;


    [Header("Animations")]
    public bool showClaimPanel;

    float prevDeg;
    float nextdeg = 30f;

    float randomDegre;
    float rotation;
    float getSmooth;

    bool continuew;
    bool stopRotate;
    bool startShow;
    bool canTurn;
    bool stopTurn;

    int coinWinner;


    IEnumerator TurnLights()
    {
        canTurn = true;
        yield return new WaitForSeconds(0.5f);
        Lights.ForEach(l => l.SetActive(false));
        for (int i = 0; i < Random.Range(0,16); i++)
        {
            Lights[Random.Range(0, Lights.Count)].SetActive(true);
        }
        canTurn = false;
    }

    public void Spin()
    {
        if(wheelRect.localEulerAngles.z >= randomDegre - 0.1f && wheelRect.localEulerAngles.z <= randomDegre)
        {
            stopTurn = true;
            spinBtn.interactable = false;
            continuew = true;
            prevDeg = 0;
            nextdeg = (360f / items.Count);

            getSmooth = smoothTime;

            rotation = 0;

            int p = Random.Range(0, 100);

            for (int i = 0; i < items.Count; i++)
            {
                if (p >= items[i].minPercentage && p <= items[i].maxPercentage)
                {
                    coinWinner = items[i].ammount;
                    winText.text = "You Won " + items[i].ammount.ToString(); ;
                    float degre = (360f / items.Count) * i;
                    randomDegre = degre;
                }
            }
        }
    }

    private void Update()
    {
        if (!canTurn && !stopTurn) { StartCoroutine(TurnLights()); }
        if(startShow) { claimPanel.GetComponent<CanvasGroup>().alpha = Mathf.Lerp(claimPanel.GetComponent<CanvasGroup>().alpha, 1, Mathf.Sin(0.1f)); }
        if (!stopRotate) { rotation = Mathf.Lerp(rotation, ((360f * spinTourne) + (randomDegre + smoothToComplet)), Mathf.Sin(getSmooth * 0.001f)); }
        if (rotation > ((360f * spinTourne) + randomDegre)) { stopRotate = true; }
        
        if (rotation <= nextdeg && rotation > prevDeg)
        {
            nextdeg = nextdeg + 22f;
            prevDeg = prevDeg + 22f;

            spinAudio.Play();
        }
        if (stopTurn) { Lights.ForEach(l => l.SetActive(true)); }

        wheelRect.localEulerAngles = new Vector3(0, 0, rotation);

        if (rotation < ((360f * spinTourne) + randomDegre) && rotation > ((360f * spinTourne) + (randomDegre - 1)) && continuew)
        {
            PlayerWin();
            continuew = false;
        }
    }

    void PlayerWin()
    {
        winnerAudio.Play();
        if (showClaimPanel)
        {
            claimPanel.SetActive(true);
            claimPanel.GetComponent<CanvasGroup>().alpha = 0f;
            startShow = true;
        }
    }

   IEnumerator Claimed()
    {
        yield return new WaitForSeconds(1.6f);
        coinAnimator.GetComponent<Animator>().SetBool("Add", false);
        coinAnimator.SetActive(false);
    }

    public void ClaimCoin()
    {
        claimPanel.SetActive(false);
        coinAnimator.SetActive(true);
        coinAnimator.GetComponent<Animator>().SetBool("Add", true);
        CoinManager.instance.AddCoins(coinWinner);
        Lights.ForEach(l => l.SetActive(false));
        stopTurn = false;
        claimAudio.Play();
        StartCoroutine(Claimed());
    }

    [System.Serializable]
    public class spinrItem
    {
        public int ammount;
        [Range(0f, 100f)] public int minPercentage;
        [Range(0f, 100f)] public int maxPercentage;
    }

}
