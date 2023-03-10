using System.Collections;
using System.Collections.Generic;
using Intelmatix.Settings;
using UnityEngine;
using static Intelmatix.Data.StoreData;

namespace Intelmatix.Examples.Templates
{
    public class NotificationManager : MonoBehaviour
    {
        private Notification notification;


        [Header("References")]
        [SerializeField] private FloatingCard floatingCard;
        [SerializeField] private RectTransform parentOfCards;

        private bool isOnAnimation = false;
        public void Awake()
        {
            parentOfCards.DestroyChildren();
        }

        // public void Start()
        // {
        //     var notify = new Notification("position1", new List<Card>
        //     {
        //         new Card(0, "description1", "supplier"),
        //         new Card(2, "description2", "inventory"),
        //         new Card(3, "description3", "managment"),
        //         new Card(3, "description4", "starff"),
        //         new Card(0, "description5", "inventory"),
        //         new Card(0, "description6", "managment"),
        //         new Card(0, "description7", "managment"),
        //         new Card(0, "description8", "inventory"),
        //         new Card(0, "description9", "managment"),
        //         new Card(0, "description10", "supplier"),
        //         new Card(0, "description11", "managment"),
        //         new Card(0, "description12", "inventory"),
        //         new Card(0, "description13", "supplier")
        //     });
        //     SetData(notify);
        //     Show();
        // }

        private List<FloatingCard> floatingCards = new List<FloatingCard>();
        public void SetData(Notification notification)
        {
            this.notification = notification;
        }
        public void Show()
        {
            floatingCards.Clear();
            isOnAnimation = true;
            RecursivelyShowCards(0);
        }


        private void RecursivelyShowCards(int index)
        {
            if (isOnAnimation == false)
            {
                return;
            }
            var instance = Instantiate(floatingCard, parentOfCards);
            instance.SetData(notification.Cards[index % notification.Cards.Count]);
            instance.Show();
            floatingCards.Add(instance);
            LeanTween.delayedCall(this.gameObject, SidebarAnimationSettings.NotificationAppearDelay, () =>
            {
                floatingCards.Remove(instance);
                RecursivelyShowCards(index + 1);
            });
        }

        public void Hide(bool destroy = false)
        {
            isOnAnimation = false;
            LeanTween.cancel(this.gameObject);
            foreach (var card in floatingCards)
            {
                card.Hide();
            }
            if (destroy)
            {
                Destroy(this.gameObject, 6f);
            }
        }

    }
}
