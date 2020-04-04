using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

using System;
using System.Collections.Generic;

using UntitledGame.Animations;
using UntitledGame.Dynamics;
using UntitledGame.Input;

namespace UntitledGame.GameObjects.Sakazaki
{
    public class Sakazaki : GameObject
    {
        // Player physics engine params
        private readonly Point _initSize = new Point(24, 79);

        // Input manager, set to single input profile

        // Behavior libraries
        private Sakazaki_AnimationLibrary   _animationLibrary;
        private Sakazaki_BehaviorScript     _behaviorScript;

        // Behavior events delegate. 
        public delegate void BehaviorsDelegate();

        // Behavior events. Call this after appending all collisions and logic.
        public Action FirstBehaviorFunctions;
        public Action BehaviorFunctions;
        public Sakazaki_State State;

        public Sakazaki(WorldHandler setWorld, Vector2 setPosition, string key)
        {
            CurrentWorld    = setWorld;
            Key             = key;
            Drawable        = true;

            InitPosition    = setPosition;
            Position        = setPosition;
            Size            = _initSize;
        }

        public override void LoadContent()
        {
            Body = CurrentWorld.AddBody(this, InitPosition, Size);
            Body.ChildHitboxes[Key + "_body"] = new Hitbox(this, new Vector2(0, 0), Size, Key + "_body")
            {
                DebugSprite = Debug.Assets.BlueBox,
                Data = new CollisionPackage()
                {
                    Value = Key
                }
            };

            AnimationHandler    = new AnimationHandler(this);
            _animationLibrary   = new Sakazaki_AnimationLibrary();
            _animationLibrary.LoadAnimations(AnimationHandler);

            State = new Sakazaki_State();
            State.Facing = (Game.Rng.Next(0,2) > 0) ? Orientation.Left : Orientation.Right;
            _behaviorScript = new Sakazaki_BehaviorScript(this);
            _behaviorScript.InitBehaviors();

            AnimationHandler.ChangeAnimation((int)AnimationStates.Idle);
            AnimationHandler.Facing = Orientation.Right;
        }

        public override void Update()
        {
            if (CurrentWorld.State == WorldState.Update)
            {
                base.Update();
                FirstBehaviorFunctions?.Invoke();
                BehaviorFunctions?.Invoke();
                AnimationHandler.Facing = State.Facing;
                AnimationHandler.UpdateIndex();
            }
        }

        public override void Draw()
        {
            AnimationHandler.DrawFrame();
        }

        public override void DrawDebug()
        {
            Body.DrawDebug();
            AnimationHandler.DrawDebug();
        }
    }
}
