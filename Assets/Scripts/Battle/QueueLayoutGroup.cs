
using UnityEngine;
using UnityEngine.UI;

namespace Battle
{
    [ExecuteAlways]
    public class QueueLayoutGroup : MonoBehaviour
    {
        public float xOffset = 0.05f; // 5% of a 1000px width example
        public float yOffset = 0.05f; // 5% of a 1000px height example
        public float zOffset = -0.05f; // Push back in Z space

        public void ArrangeQueue()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                RectTransform child = transform.GetChild(i).transform.GetComponent<RectTransform>();

                Vector3 position = new Vector3(i * xOffset, i * yOffset, -i * zOffset);
                child.localPosition = position;
            }

            // TODO: Reverse ordering
            // for (int i = transform.childCount - 1; i <= 0; i++)
            // {
            //     RectTransform child = transform.GetChild(i).transform.GetComponent<RectTransform>();

            //     Vector3 position = new Vector3(i * xOffset, i * yOffset, -i * zOffset);
            //     child.localPosition = position;
            // }
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