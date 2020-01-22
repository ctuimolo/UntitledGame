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
        private readonly Point      _size = new Point(20,64);
        private readonly float      _walkSpeed    = 3;
        private readonly float      _jumpStrength = 8;

        // Debug fields and strings
        private string  _afterCollisionString;
        private string  _PositionDebugString;
        private string  _isFlooredString;
        private string  _isOverlappingOrangeString;
        private string  _isOverlappingPinkString;
        private string  _listOfCollisions;

        // Behavior libraries
        private readonly InputManager               _controller;
        private readonly Player_AnimationLibrary    _animationLibrary;
        private readonly Player_BehaviorFunctions   _behaviorFunctions;

        // Behavior events delegate. 
        public delegate void BehaviorsDelegate();

        // Behavior events. Call this after appending all collisions and logic.
        public BehaviorsDelegate        BehaviorFunctions;

        public Player(WorldHandler setWorld, Vector2 setPosition, string key)
        {
            CurrentWorld    = setWorld;
            Key             = key;
            Drawable        = true;

            Body                    = setWorld.AddBody(this, setPosition, _size);
            Body.ChildHitboxes[0]   = new Hitbox(this, new Vector2(0, 0), _size, "body");

            _controller         = new InputManager();
            _animationLibrary   = new Player_AnimationLibrary();
            AnimationHandler    = new AnimationHandler(this);

            InitPosition            = setPosition;
            Position                = setPosition;
            Size                    = _size;

            _behaviorFunctions  = new Player_BehaviorFunctions(this, Body, AnimationHandler);

            _behaviorFunctions.InitBehaviors();
            _animationLibrary.LoadAnimations(AnimationHandler);

            AnimationHandler.ChangeAnimation((int)AnimationStates.Idle); 
            AnimationHandler.Facing = Orientation.Right;
        } 

        private void HandleKeyboard()
        {
            if (_controller.InputDown(InputFlags.Left) && !_controller.InputDown(InputFlags.Right))
            {
                Body.Velocity.X = -_walkSpeed;
                AnimationHandler.Facing = Orientation.Left;
                if(Body.IsFloored)
                {
                    AnimationHandler.ChangeAnimation((int)AnimationStates.Walking);
                }
            }

            if (_controller.InputDown(InputFlags.Right) && !_controller.InputDown(InputFlags.Left))
            {
                Body.Velocity.X = _walkSpeed;
                AnimationHandler.Facing = Orientation.Right;
                if (Body.IsFloored)
                {
                    AnimationHandler.ChangeAnimation((int)AnimationStates.Walking);
                }
            }

            if (!_controller.InputDown(InputFlags.Left) && !_controller.InputDown(InputFlags.Right))
            {
                Body.Velocity.X = 0;
                if (Body.IsFloored)
                {
                    AnimationHandler.ChangeAnimation((int)AnimationStates.Idle);
                }
            }

            if (_controller.InputDown(InputFlags.Left) && _controller.InputDown(InputFlags.Right))
            {
                Body.Velocity.X = 0;
                if (Body.IsFloored)
                {
                    AnimationHandler.ChangeAnimation((int)AnimationStates.Idle);
                }
            }

            if (_controller.InputPressed(InputFlags.Button1))
            {
                Body.Velocity.Y = -_jumpStrength;
            }
        }

        public override void Update()
        {
            if(CurrentWorld.State == WorldState.Update)
            {
                AnimationHandler.UpdateIndex();
                BehaviorFunctions?.Invoke();
                HandleKeyboard();
                _controller.ParseInput();
            }
        }

        public override void Draw()
        {
            AnimationHandler.DrawFrame();
        }

        public override void DrawDebug()
        {
            _PositionDebugString = "Position: \n" +
                      "X: " + (int)(Body.BoxCollider.X - _size.X / 2) + "\n" +
                      "Y: " + (int)(Body.BoxCollider.Y - _size.Y / 2) + "\n";

            _isFlooredString = "Grounded:            " + (Body.IsFloored ? "true" : "false");
            _isOverlappingOrangeString = "Hitbox Collisions:   " + (_behaviorFunctions.isOverlappingOrange ? "true" : "false");
            _isOverlappingPinkString = "Hitbox Collisions:   " + (_behaviorFunctions.isOverlappingPink ? "true" : "false");
            _afterCollisionString = "Collisions present:  " + (Body.CurrentCollisions.Count > 0 ? "true" : "false");

            Game.SpriteBatch.DrawString(
                Debug.Assets.DebugFont,
                "collision packages: " + Body.CurrentCollisions.Count,
                new Vector2(10, 124),
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
                    new Vector2(10, 136),
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
