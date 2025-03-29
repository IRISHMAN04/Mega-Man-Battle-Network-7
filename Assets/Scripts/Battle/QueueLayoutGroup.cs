
using UnityEngine;
using Utility;

namespace Battle
{
    [ExecuteAlways]
    public class QueueLayoutGroup : MonoBehaviour
    {
        public float xOffset;
        public float yOffset;

        public void ArrangeQueue()
        {
            transform.ReverseChildren();
            for (int i = 0; i < transform.childCount; i++)
            {
                RectTransform child = transform.GetChild(i).transform.GetComponent<RectTransform>();

                // Push the children in the x and y directions
                Vector3 position = new(i * xOffset, i * yOffset, 0);
                child.localPosition = position;
            }
            // Reverse the order of the children so that they render in the correct order
            transform.ReverseChildren();
        }

        public void AddItem(GameObject item)
        {
            item.transform.SetParent(transform, false);
            item.transform.SetAsFirstSibling();
            ArrangeQueue();
        }

        public void RemoveFirstItem()
        {
            if (transform.childCount > 0)
            {
                DestroyImmediate(transform.GetChild(transform.childCount - 1).gameObject);
                ArrangeQueue();
            }
        }
    }
}