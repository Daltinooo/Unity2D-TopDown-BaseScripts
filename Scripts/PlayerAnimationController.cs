using System.Collections;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    [Header("Idle Sprites")]
    public Sprite[] idleSprites;
    [Header("Running Sprites")]
    public Sprite[] runningSprites;

    [Header("Animation Settings")]
    [Range(0f, 1f)]
    public float idleDelay = 0.5f; // Delay for idle animation
    [Range(0f, 1f)]
    public float runDelay = 0.2f; // Delay for running animation

    private bool isMoving = false;
    private SpriteRenderer spriteRenderer;
    private Coroutine currentCoroutine;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Start the idle animation
        currentCoroutine = StartCoroutine(PlayIdleAnimation());
    }

    private void Update()
    {
        bool wasMoving = isMoving;
        // Simulate player movement (replace with your actual movement logic)
        isMoving = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D);

        if (wasMoving != isMoving)
        {
            if(currentCoroutine != null)
            {
                StopCoroutine(currentCoroutine);
            }

            currentCoroutine = StartCoroutine(isMoving ? PlayRunningAnimation() : PlayIdleAnimation());
        }
    }
    private IEnumerator PlayIdleAnimation()
    {
        int currentFrame = 0;
        while (true)
        {
            yield return new WaitForSeconds(idleDelay);
            spriteRenderer.sprite = idleSprites[currentFrame];
            currentFrame = (currentFrame + 1) % idleSprites.Length;
        }
    }
    private IEnumerator PlayRunningAnimation()
    {
        int currentFrame = 0;
        while (true)
        {
            yield return new WaitForSeconds(runDelay);
            spriteRenderer.sprite = runningSprites[currentFrame];
            currentFrame = (currentFrame + 1) % runningSprites.Length;
        }
    }
}
