using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

using System;
using System.Collections.Generic;

using UntitledGame.Animations;
using UntitledGame.Dynamics;
using UntitledGame.Input;

namespace UntitledGame.GameObjects.Player
{
    public class Player : GameObject
    {
        // Player physics engine params
        private readonly Point      _initSize = new Point(20,64);

        // Debug fields and strings
        private string  _afterCollisionString;
        private string  _PositionDebugString;
        private string  _isFlooredString;
        private string  _isOverlappingOrangeString;
        private string  _isOverlappingPinkString;
        private string  _listOfCollisions;

        // Input manager, set to single input profile

        // Behavior libraries
        private Player_AnimationLibrary _animationLibrary;
        private Player_BehaviorScript   _behaviorScript;
        private InputManager    _controller;

        // Behavior events delegate. 
        public delegate void BehaviorsDelegate();

        // Behavior events. Call this after appending all collisions and logic.
        public Action FirstBehaviorFunctions;
        public Action BehaviorFunctions;
        public Player_State State;

        public Player(Vector2 setPosition, string key)
        {
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
            //CurrentWorld.AddHitbox(Body.ChildHitboxes[Key + "_body"]);

            AnimationHandler = new AnimationHandler(this);
            _animationLibrary   = new Player_AnimationLibrary();
            _animationLibrary.LoadAnimations(AnimationHandler);

            _controller         = Game.GlobalKeyboard;
            _controller.History = new InputRecord(_controller);
            _controller.History.InitKBRecord();

            State = new Player_State();
            _behaviorScript = new Player_BehaviorScript(this);
            _behaviorScript.SetController(ref _controller);
            _behaviorScript.InitBehaviors();

            AnimationHandler.ChangeAnimation((int)AnimationStates.Idle);
            AnimationHandler.Facing = Orientation.Right;
        }

        public override void Update()
        {
            if(CurrentWorld.State == WorldState.Update)
            {
                base.Update();
                FirstBehaviorFunctions?.Invoke();
                BehaviorFunctions?.Invoke();
                _controller.History.UpdateInputRecordIndex();
                AnimationHandler.Facing = State.Facing;
                AnimationHandler.UpdateIndex();
            }
        }

        public override void Draw()
        {
            AnimationHandler.DrawFrame();
            _controller.History.DrawHistory();
        }

        public override void DrawDebug()
        {
            _PositionDebugString = "Position: \n" +
                      "X: " + (int)(Body.BoxCollider.X - Size.X / 2) + "\n" +
                      "Y: " + (int)(Body.BoxCollider.Y - Size.Y / 2) + "\n";

            _isFlooredString            = "Grounded:            " + (Body.IsFloored ? "true" : "false");
            _isOverlappingOrangeString  = "Hitbox Collisions:   " + (_behaviorScript.isOverlappingOrange ? "true" : "false");
            _isOverlappingPinkString    = "Hitbox Collisions:   " + (_behaviorScript.isOverlappingPink ? "true" : "false");
            _afterCollisionString       = "Collisions present:  " + (Body.CurrentCollisions.Count > 0 ? "true" : "false");

            Game.SpriteBatch.DrawString(
                Debug.Assets.DebugFont,
                "collision packages: " + Body.CurrentCollisions.Count,
                new Vector2(10, 140),
                Color.GreenYellow);

            _listOfCollisions = "";
            foreach (Hitbox collision in Body.CurrentCollisions)
            {
                _listOfCollisions += "<" + collision.Position.X + "," + collision.Position.Y + " : " + collision.Key + ">\n";
            }

            if (Body.CurrentCollisions.Count > 0)
            {
                Game.SpriteBatch.DrawString(
                    Debug.Assets.DebugFont,
                    _listOfCollisions,
                    new Vector2(10, 152),
                    Color.GreenYellow);
            }

            Game.SpriteBatch.DrawString(
                Debug.Assets.DebugFont,
                _PositionDebugString,
                new Vector2(Body.BoxCollider.X, Body.BoxCollider.Y) + new Vector2(-60, -88),
                Color.CornflowerBlue);

            Game.SpriteBatch.DrawString(
                Debug.Assets.DebugFont,
                _isFlooredString,
                new Vector2(Body.BoxCollider.X, Body.BoxCollider.Y) + new Vector2(-60, -102),
                Color.Gray);

            Game.SpriteBatch.DrawString(
                Debug.Assets.DebugFont,
                _afterCollisionString,
                new Vector2(Body.BoxCollider.X, Body.BoxCollider.Y) + new Vector2(-60, -116),
                Color.Gray);

            Game.SpriteBatch.DrawString(
                Debug.Assets.DebugFont,
                "A " + _isOverlappingPinkString,
                new Vector2(Body.BoxCollider.X, Body.BoxCollider.Y) + new Vector2(20, -64),
                Color.Violet);

            Game.SpriteBatch.DrawString(
                Debug.Assets.DebugFont,
                "B " + _isOverlappingOrangeString,
                new Vector2(Body.BoxCollider.X, Body.BoxCollider.Y) + new Vector2(20, -50),
                Color.Orange);
            Body.DrawDebug();
            AnimationHandler.DrawDebug();
        }
    }
}
