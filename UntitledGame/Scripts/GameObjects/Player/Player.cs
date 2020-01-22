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

        // Debug fields and strings
        private string  _afterCollisionString;
        private string  _PositionDebugString;
        private string  _isFlooredString;
        private string  _isOverlappingOrangeString;
        private string  _isOverlappingPinkString;
        private string  _listOfCollisions;

        // Input manager, set to single input profile
        public InputManager Controller  { get; set; }

        // Behavior libraries
        private readonly Player_AnimationLibrary    _animationLibrary;
        private readonly Player_BehaviorFunctions   _behaviorFunctions;

        // Behavior events delegate. 
        public delegate void BehaviorsDelegate();

        // Behavior events. Call this after appending all collisions and logic.
        public BehaviorsDelegate        BehaviorFunctions;

        public Player(WorldHandler setWorld, Vector2 setPosition, InputManager controller, string key)
        {
            CurrentWorld    = setWorld;
            Key             = key;
            Drawable        = true;

            InitPosition = setPosition;
            Position = setPosition;
            Size = _size;

            Body                    = setWorld.AddBody(this, setPosition, _size);
            Body.ChildHitboxes[0]   = new Hitbox(this, new Vector2(0, 0), _size, "body");

            Controller          = controller;
            _animationLibrary   = new Player_AnimationLibrary();
            AnimationHandler    = new AnimationHandler(this);
            _behaviorFunctions  = new Player_BehaviorFunctions(this, Body, AnimationHandler, Controller);

            _behaviorFunctions.InitBehaviors();
            _animationLibrary.LoadAnimations(AnimationHandler);

            AnimationHandler.ChangeAnimation((int)AnimationStates.Idle); 
            AnimationHandler.Facing = Orientation.Right;
        }

        public override void SetController(InputManager controller)
        {
            Controller = controller;
        }

        public override void Update()
        {
            if(CurrentWorld.State == WorldState.Update)
            {
                AnimationHandler.UpdateIndex();
                BehaviorFunctions?.Invoke();
                //Controller.ParseInput();
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
