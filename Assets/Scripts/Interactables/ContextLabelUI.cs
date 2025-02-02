﻿using System.Collections;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.Interactables
{
    public class ContextLabelUI : MonoBehaviour
    {
        public static ContextLabelUI Instance;

        public TMP_Text key, text;

        private void Awake()
        {
            Instance = this;
            gameObject.SetActive(false);
        }

        private void Update()
        {
            
        }

        public void ShowContextLabel(string key, string text)
        {
            this.gameObject.SetActive(true);
            this.key.text = key;
            this.text.text = text;

            this.key.ForceMeshUpdate(true, true);
            this.key.GetComponent<RectTransform>().sizeDelta = this.key.GetRenderedValues(true) + new Vector2(15, 15);
            this.text.GetComponent<RectTransform>().sizeDelta = this.text.GetRenderedValues(true) + new Vector2(15, 15);
            Vector2 bgSize = GameObject.Find("Key").GetComponent<RectTransform>().sizeDelta = this.key.GetRenderedValues(true) + new Vector2(15, 15);

            this.text.transform.localPosition = new Vector2(bgSize.x + 30, this.text.transform.localPosition.y);
        }

        public void HideContextLabel()
        {
            if (this == null) // quand l'objet joueur est apporté dans le monde du combat, il essaye d'appeler cette fonction,
                return;       // ce qui génère une erreure à chaque FixedUpdate. Ceci enlève cette erreure.

            gameObject.SetActive(false);
        }
    }
}