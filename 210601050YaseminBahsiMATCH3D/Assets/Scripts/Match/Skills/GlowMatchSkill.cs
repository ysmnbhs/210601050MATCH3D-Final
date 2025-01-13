using UnityEngine;
using UnityEngine.UI;

namespace Match
{
    public class GlowMatchSkill : MonoBehaviour
    {
        [SerializeField] private float highlightScaleFactor = 1.2f; // Boyut artýrma faktörü
        [SerializeField] private float highlightDuration = 2f; // Highlight süresi

 
        public void MergeObjects()
        {
            // Eþleþme kontrolünü baþlat
            FindAndHighlightMatchingObjects();
        }

        private void FindAndHighlightMatchingObjects()
        {
            GameObject[] objects = GameObject.FindGameObjectsWithTag("Moveable");

            if (objects.Length < 2)
            {
                Debug.LogWarning("Eþleþme yapmak için yeterli nesne bulunamadý!");
                return;
            }

            for (int i = 0; i < objects.Length; i++)
            {
                for (int j = i + 1; j < objects.Length; j++)
                {
                    var item1 = objects[i].GetComponent<Item>();
                    var item2 = objects[j].GetComponent<Item>();

                    if (item1 != null && item2 != null && item1.IsMatching(item2))
                    {
                        Debug.Log($"Eþleþen nesneler bulundu: {item1.name} ve {item2.name}");

                        // Nesnelerin boyutunu büyüt
                        HighlightObject(item1.gameObject);
                        HighlightObject(item2.gameObject);

                        // Bir kez eþleþme bulunduktan sonra çýk
                        return;
                    }
                }
            }

            Debug.LogWarning("Eþleþen nesne bulunamadý!");
        }

        private void HighlightObject(GameObject targetObject)
        {
            // Orijinal boyutu kaydet
            Vector3 originalScale = targetObject.transform.localScale;

            // Boyutu büyüt
            targetObject.transform.localScale = originalScale * highlightScaleFactor;

            // Belirli bir süre sonra boyutu eski haline döndür
            StartCoroutine(ResetScaleAfterDelay(targetObject, originalScale));
        }

        private System.Collections.IEnumerator ResetScaleAfterDelay(GameObject targetObject, Vector3 originalScale)
        {
            yield return new WaitForSeconds(highlightDuration);

            // Boyutu eski haline getir
            targetObject.transform.localScale = originalScale;
        }
    }
}