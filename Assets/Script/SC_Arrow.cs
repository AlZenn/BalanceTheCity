using Unity.VisualScripting;
using UnityEngine;
using System.Collections;

public class SC_Arrow : MonoBehaviour
{
    public GameObject card; // Kart GameObject
    [SerializeField] private GameObject[] bugfix;

    public GameObject upArrow; // Yukarı ok
    public GameObject downArrow; // Aşağı ok
    public GameObject leftArrow; // Sol ok
    public GameObject rightArrow; // Sağ ok

    private bool isTouching = false;
    private float touchTimer = 0f;
    private float touchThreshold = 3f; // 3 saniye sınırı

    [SerializeField] private SC_Dice ScriptDice;

    private Vector3 originalUpPosition;
    private Vector3 originalDownPosition;
    private Vector3 originalLeftPosition;
    private Vector3 originalRightPosition;

    private void Awake()
    {
        // Ok işaretlerini başlangıçta pasif yap
        upArrow.SetActive(false);
        downArrow.SetActive(false);
        leftArrow.SetActive(false);
        rightArrow.SetActive(false);

        // Ok işaretlerinin orijinal pozisyonlarını sakla
        originalUpPosition = upArrow.transform.localPosition;
        originalDownPosition = downArrow.transform.localPosition;
        originalLeftPosition = leftArrow.transform.localPosition;
        originalRightPosition = rightArrow.transform.localPosition;
    }

    private void Update()
    {
        if (card.activeSelf && !bugfix[0].activeSelf && !bugfix[1].activeSelf && !bugfix[2].activeSelf)
        {
            if (Input.touchCount > 0 || Input.GetMouseButton(0)) // Ekrana dokunuyor mu?
            {
                isTouching = true;
                touchTimer = 0f; // Dokunuluyorsa süre sıfırla
                upArrow.SetActive(false);
                downArrow.SetActive(false);
                leftArrow.SetActive(false);
                rightArrow.SetActive(false);
            }
            else
            {
                isTouching = false;
                touchTimer += Time.deltaTime; // Dokunulmuyorsa süre artır
            }

            if (touchTimer >= touchThreshold) // 3 saniye boyunca dokunulmazsa
            {
                ActivateArrows(); // Okları aktif et
            }
        }

        // Ok işaretlerinin pozisyonlarını orijinal pozisyonlarına ayarla
        upArrow.transform.localPosition = originalUpPosition;
        downArrow.transform.localPosition = originalDownPosition;
        leftArrow.transform.localPosition = originalLeftPosition;
        rightArrow.transform.localPosition = originalRightPosition;
    }

    private void ActivateArrows()
    {
        if (!upArrow.activeSelf) // Eğer oklar zaten aktif değilse
        {
            if (ScriptDice.isSwitched == false)
            {
                leftArrow.SetActive(true);
                rightArrow.SetActive(true);
            }
            else if (ScriptDice.isSwitched == true)
            {
                upArrow.SetActive(true);
                downArrow.SetActive(true);
            }
            // Oklar için animasyonu başlat
            StartCoroutine(AnimateArrows());
        }
    }

    private IEnumerator AnimateArrows()
    {
        while (upArrow.activeSelf || leftArrow.activeSelf) // Oklar aktif olduğu sürece
        {
            // Okların x scale değerini 1 ile 1.05 arasında, y scale değerini 0.5 ile 0.7 arasında büyüt ve küçült
            float scaleX = Mathf.PingPong(Time.time * 0.5f, 0.05f) + 1f;
            float scaleY = Mathf.PingPong(Time.time * 0.5f, 0.2f) + 0.5f;

            upArrow.transform.localScale = new Vector3(scaleX, scaleY, 1);
            downArrow.transform.localScale = new Vector3(scaleX, scaleY, 1);
            leftArrow.transform.localScale = new Vector3(scaleX, scaleY, 1);
            rightArrow.transform.localScale = new Vector3(scaleX, scaleY, 1);

            yield return null;
        }
    }
}