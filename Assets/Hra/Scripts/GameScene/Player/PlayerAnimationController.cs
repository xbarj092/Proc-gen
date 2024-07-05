using AYellowpaper.SerializedCollections;
using System.Collections;
using UnityEngine;

public enum PlayerAnimation
{
    Idle = 0,
    Walk = 1,
    Run = 2, 
    FastRun = 3,
    Jump = 4,
    Roll = 5
}

public class PlayerAnimationController : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    // here are animation delay times
    //  - first number in array is for player movement restriction:
    // higher number, so that player does not rotate in some weird directions during animation playtime
    //  - second number in array is for letting the animation have some time to transition:
    // lower number, animation needs some time to get into "normal" position
    // player can already rotate the character, but animations still cannot be played
    [SerializeField] private SerializedDictionary<PlayerAnimation, float[]> _animationTimes;

    private PlayerAnimation _currentlyPlaying;
    private bool _canPlayAnim = true;

    public bool CanMove = true;

    private const string PLAYER_ANIM_NAME_IDLE = "Idle";
    private const string PLAYER_ANIM_NAME_WALK = "Walk";
    private const string PLAYER_ANIM_NAME_RUN = "Run";
    private const string PLAYER_ANIM_NAME_RUN_FAST = "FastRun";
    private const string PLAYER_ANIM_NAME_JUMP = "Jump";
    private const string PLAYER_ANIM_NAME_ROLL = "Roll";

    public void PlayAnimation(PlayerAnimation playerAnim)
    {
        if (playerAnim == _currentlyPlaying || !_canPlayAnim)
        {
            return;
        }

        _animator.ResetTrigger(PLAYER_ANIM_NAME_IDLE);
        _animator.ResetTrigger(PLAYER_ANIM_NAME_WALK);
        _animator.ResetTrigger(PLAYER_ANIM_NAME_RUN);
        _animator.ResetTrigger(PLAYER_ANIM_NAME_RUN_FAST);
        _animator.ResetTrigger(PLAYER_ANIM_NAME_JUMP);
        _animator.ResetTrigger(PLAYER_ANIM_NAME_ROLL);

        _currentlyPlaying = playerAnim;

        switch (playerAnim)
        {
            case PlayerAnimation.Idle:
                PlayIdle();
                break;
            case PlayerAnimation.Walk:
                PlayWalk();
                break;
            case PlayerAnimation.Run:
                PlayRun();
                break;
            case PlayerAnimation.FastRun:
                PlayFastRun();
                break;
            case PlayerAnimation.Jump:
                PlayJump();
                break;
            case PlayerAnimation.Roll:
                PlayRoll();
                break;
        }
    }

    private void PlayIdle()
    {
        _animator.SetTrigger(PLAYER_ANIM_NAME_IDLE);
    }

    private void PlayWalk()
    {
        _animator.SetTrigger(PLAYER_ANIM_NAME_WALK);
    }

    private void PlayRun()
    {
        _animator.SetTrigger(PLAYER_ANIM_NAME_RUN);
    }

    private void PlayFastRun()
    {
        _animator.SetTrigger(PLAYER_ANIM_NAME_RUN_FAST);
    }

    private void PlayJump()
    {
        _animator.SetTrigger(PLAYER_ANIM_NAME_JUMP);
        StartCoroutine(SetParams(PlayerAnimation.Jump));
    }

    private void PlayRoll()
    {
        // TODO - import some rotate anims and blend them at the end with whatever animation will be playing
        // will get rid of ugly snapping
        _animator.SetTrigger(PLAYER_ANIM_NAME_ROLL);
        StartCoroutine(SetParams(PlayerAnimation.Roll));
    }

    private IEnumerator SetParams(PlayerAnimation playerAnim)
    {
        CanMove = false;
        _canPlayAnim = false;
        yield return new WaitForSeconds(_animationTimes[playerAnim][0]);
        _canPlayAnim = true;
        yield return new WaitForSeconds(_animationTimes[playerAnim][1]);
        CanMove = true;
    }
}
