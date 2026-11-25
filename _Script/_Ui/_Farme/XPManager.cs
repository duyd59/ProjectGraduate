using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class XPManager : MyObj
{
    [SerializeField]protected FarmeManager farmeManager;
    [Header("UI References")]
    [SerializeField] private Image fillImage;
    [SerializeField] private TextMeshProUGUI levelText;


    [Header("Animation")]
    [SerializeField] private float fillSpeed = 5f;
    private float targetFillAmount;

    public override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadComponent();
        this.LoadcurrentFarme();
    }

    protected virtual void LoadComponent()
    {
        if(this.farmeManager == null) this.farmeManager = transform.GetComponentInParent<FarmeManager>();
        if (this.fillImage == null)
        {
            Image[] images = GetComponentsInChildren<Image>(true);
            foreach (Image img in images)
            {
                if (img.gameObject.name == "Fillimage")
                {
                    this.fillImage = img;
                    break;
                }
            }
        }

        if (this.levelText == null)
        {
            TextMeshProUGUI[] texts = GetComponentsInChildren<TextMeshProUGUI>(true);
            foreach (TextMeshProUGUI tmp in texts)
            {
                if (tmp.gameObject.name == "LevelText")
                {
                    this.levelText = tmp;
                    break;
                }
            }
        }
    }
    private void LoadcurrentFarme()
    {
        targetFillAmount = this.farmeManager.farme_ScipTble.CurrentXP / this.farmeManager.farme_ScipTble.MaxXp;
        fillImage.fillAmount = targetFillAmount;
        UpdateUI();
    }

    private void Update()
    {
        if (Mathf.Abs(fillImage.fillAmount - targetFillAmount) > 0.001f)
        {
            fillImage.fillAmount = targetFillAmount;
        }

        UpdateUI();
    }

    private void LevelUp()
    {
        fillImage.fillAmount = 0f;
    }

    private void UpdateUI()

    {
        targetFillAmount = this.farmeManager.farme_ScipTble.CurrentXP / this.farmeManager.farme_ScipTble.MaxXp;
        if (levelText != null) levelText.text = $"Lv. {this.farmeManager.farme_ScipTble.FarmeLv}";
    }
}

