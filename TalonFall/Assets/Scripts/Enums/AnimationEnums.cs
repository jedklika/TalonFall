using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AnimationEnums
{
    public enum PlayerAnimationState : int
    {
        ANIMATION_NONE = 0,
		ANIMATION_STANDING,
		ANIMATION_WALKING,
		ANIMATION_RUNNING,
		ANIMATION_JUMPING,
		ANIMATION_TOTAL
    }
}
