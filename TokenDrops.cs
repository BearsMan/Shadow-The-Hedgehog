using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TokenDrops : MonoBehaviour
{
    public GameObject token;
    public Material redCircleCoin;
    public Material blackCircleCoin;
    GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindFirstObjectByType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SpawnToken()
    {
        // Note for sub: Make the coin spawn on the cube that player clicks on.
        Instantiate(token);
        MeshRenderer tokenMaterialRenderer = token.GetComponent<MeshRenderer>();
        if (gameManager.redTurn)
        {
            tokenMaterialRenderer.material = redCircleCoin;
        }
        else
        {
            tokenMaterialRenderer.material = blackCircleCoin;
        }
        gameManager.redTurn = !gameManager.redTurn;
    }
}
