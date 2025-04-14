using System.Collections.Generic;
using TowerDefence.Enemies;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace TowerDefence.Towers.Grid
{
    public class TowerCardHolder : MonoBehaviour
    {
        [SerializeField] private GridController gridController;
        public GridController GridController => gridController;

        [SerializeField] private Tilemap tilemap;

        [SerializeField] private NavMeshUpdater navMeshUpdater;

        [SerializeField] private GameObject cardPrefab;
        [SerializeField] private List<TowerCard> cardObjects;

        [SerializeField] private List<GameObject> placedCards;
        private string _formattedCost;
        private Sprite _icon;

        private void Start()
        {
            foreach (var cardObj in cardObjects)
            {
                CreateCard(cardObj);
            }
        }
        private void CreateCard(TowerCard cardObject)
        {
            var card = Instantiate(cardPrefab, transform);
            var cardManager = card.GetComponent<CardManager>();
            cardManager.CardObject = cardObject;
            cardManager.ParentHolder = this;
            cardManager.tilemap = tilemap;
            cardManager.navMeshUpdater = navMeshUpdater;
            
            placedCards.Add(card);
            _icon = cardObject.Icon;
            _formattedCost = cardObject.FormatCost();
            card.GetComponent<TowerCardPrefab>().SetupValues(_icon, _formattedCost);
        }
    }
}