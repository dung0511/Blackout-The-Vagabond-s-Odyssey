using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Script.UI.Inventory
{
    public class DraggableItem : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
    {
        public Image image;

        [HideInInspector] public Transform parentAfterDrag;

        public void OnBeginDrag(PointerEventData eventData)
        {
            parentAfterDrag = transform.parent;
            transform.SetParent(transform.root);
            transform.SetAsLastSibling();
            image.raycastTarget = false;
        }

        public void OnDrag(PointerEventData eventData)
        {
            transform.position = Input.mousePosition;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            transform.SetParent(parentAfterDrag);
            transform.localPosition = Vector3.zero; 
            image.raycastTarget = true;
        }
    }
}