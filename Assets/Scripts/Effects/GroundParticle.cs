using Players;
using UnityEngine;

public class GroundParticle : MonoBehaviour
{
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private GameObject particleObj;
        
    private void Update()
    {
        if (playerMovement.Velocity.Value != Vector2.zero)
        {
            particleObj.SetActive(true);
        }
        else
        {
            particleObj.SetActive(false);
        }
    }
}