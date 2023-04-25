using UnityEngine;

// (IBN) Made with refernce to Unity's StarterAssets [ThirdPersonController.cs]
public class MPAnimation : MonoBehaviour
{
	// Animation IDs
	private int _animIDSpeed;
	private int _animIDGrounded;
	private int _animIDJump;
	private int _animIDFreeFall;
	private int _animIDMotionSpeed;

	private bool _hasAnimator;
	private Animator _animator;

	private MPNormalMovement _normalMovement;

	private void Start()
	{
		_hasAnimator = TryGetComponent(out _animator);
		// Assign animation IDs
		_animIDSpeed = Animator.StringToHash("Speed");
		_animIDGrounded = Animator.StringToHash("Grounded");
		_animIDJump = Animator.StringToHash("Jump");
		_animIDFreeFall = Animator.StringToHash("FreeFall");
		_animIDMotionSpeed = Animator.StringToHash("MotionSpeed");

		// Normal movement events
		_normalMovement = GetComponent<MPNormalMovement>();
		_normalMovement.OnGroundedCheck += NormalMovement_OnGroundedCheck;
		_normalMovement.OnMoveAnimation += NormalMovement_OnMoveAnimation;
		_normalMovement.OnGrounded += NormalMovement_OnGrounded;
		_normalMovement.OnJump += NormalMovement_OnJump;
		_normalMovement.OnFall += NormalMovement_OnFall;
	}
	private void Update()
	{
		_hasAnimator = TryGetComponent(out _animator);
	}

	private void NormalMovement_OnGroundedCheck(object sender, bool e)
	{
		_animator.SetBool(_animIDGrounded, e);
	}
	private void NormalMovement_OnMoveAnimation(object sender, float[] e)
	{
		_animator.SetFloat(_animIDSpeed, e[0]);
		_animator.SetFloat(_animIDMotionSpeed, e[1]);
	}
	private void NormalMovement_OnGrounded(object sender, System.EventArgs e)
	{
		_animator.SetBool(_animIDJump, false);
		_animator.SetBool(_animIDFreeFall, false);
	}

	private void NormalMovement_OnJump(object sender, System.EventArgs e)
	{
		_animator.SetBool(_animIDJump, true);
	}
	private void NormalMovement_OnFall(object sender, System.EventArgs e)
	{
		_animator.SetBool(_animIDFreeFall, true);
	}
}
