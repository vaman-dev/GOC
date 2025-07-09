using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

namespace Endless2DFe.Data.Scripts
{
    public class CoinAnimator : MonoBehaviour
    {
        private Animator animator;
        private ScorBoard scoreBoard;   


        void Start()
        {
            animator = GetComponent<Animator>();
            animator.SetBool("CoinSpin",true);
            if(scoreBoard==null)
            {
                scoreBoard = FindFirstObjectByType<ScorBoard>();
            }
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                scoreBoard.AddCoin();
                Destroy(gameObject);
            }
        }
    }
}