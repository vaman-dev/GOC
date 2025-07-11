using UnityEngine;

namespace Endless2DFe.Data.Scripts
{
    public class ObjectInstantiate : MonoBehaviour
    {
        [SerializeField] private Transform spawnPoint;
        [SerializeField] private GameObject objectPrefab;
        [SerializeField] private float PoolDelay = 2f;

        private bool hasSpawned = false;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.CompareTag("Player") || hasSpawned) return;

            hasSpawned = true;

            if (LevelPooling.Instance != null && spawnPoint != null)
            {
                LevelPooling.Instance.GetObject(spawnPoint.position, Quaternion.identity);
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (!other.CompareTag("Player")) return;

            if (LevelPooling.Instance != null)
            {
                StartCoroutine(ReturnCoroutine());
            }

            hasSpawned = false;
        }

        private System.Collections.IEnumerator ReturnCoroutine()
        {
            yield return new WaitForSeconds(PoolDelay);
            LevelPooling.Instance.ReturnObject(objectPrefab);
        }
    }
}
