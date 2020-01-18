﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

using System.Collections.Generic;

using UntitledGame.Dynamics;
using UntitledGame.GameObjects;
using UntitledGame.GameObjects.Player;
using UntitledGame.GameObjects.Wall;

namespace UntitledGame.Rooms.TestRoom
{
    public class TestRoom : Room
    {
        private bool                _drawDebug = false;
        private KeyboardState       _oldKeyState;

        public TestRoom(Point worldSize, string setKey) : base(worldSize, setKey)
        {
            ActiveGameObjects = new Dictionary<string, GameObject>();
        }

        public override void LoadContent()
        {
            LoadGameObject("w1", new Wall(new Rectangle(0, 420, 800, 80)));
            LoadGameObject("w2", new Wall(new Rectangle(560, 302, 40, 20)));
            LoadGameObject("w3", new Wall(new Rectangle(0, 0, 4, 480)));
            LoadGameObject("w4", new Wall(new Rectangle(0, 400, 5, 480)));
            LoadGameObject("w5", new Wall(new Rectangle(796, 0, 4, 480)));
            LoadGameObject("w6", new Wall(new Rectangle(200, 300, 70, 20)));
            LoadGameObject("w7", new Wall(new Rectangle(220, 280, 70, 20)));
            LoadGameObject("w8", new Wall(new Rectangle(0, 0, 800, 4)));
            LoadGameObject("w9", new Wall(new Rectangle(190, 400, 70, 20)));
            LoadGameObject("w10", new Wall(new Rectangle(60, 325, 70, 20)));
            LoadGameObject("w11", new Wall(new Rectangle(390, 388, 40, 32)));
            LoadGameObject("w12", new Wall(new Rectangle(432, 388, 40, 32)));
            LoadGameObject("w13", new Wall(new Rectangle(474, 388, 40, 32)));
            LoadGameObject("w14", new Wall(new Rectangle(516, 388, 40, 32)));
            LoadGameObject("w15", new Wall(new Rectangle(558, 388, 40, 32)));
            LoadGameObject("w16", new Wall(new Rectangle(600, 388, 40, 32)));
            LoadGameObject("w17", new Wall(new Rectangle(642, 388, 40, 32)));
            LoadGameObject("player1", new Player(World, new Vector2(250, 230)));

            World.AddHitbox(new Hitbox(null, new Vector2(420, 310), new Point(30, 60))
            {
                DebugSprite = UntitledGame.Debug.Assets.OrangeBox,
                Data = new CollisionPackage
                {
                    Value = "orange",
                    String = "spuds",
                }
            });;

            World.AddHitbox(new Hitbox(null, new Vector2(550, 290), new Point(20, 120))
            {
                DebugSprite = UntitledGame.Debug.Assets.PurpleBox,
                Data = new CollisionPackage
                {
                    Value = "purple",
                    String = "cats",
                }
            });

            World.AddHitbox(new Hitbox(null, new Vector2(60, 290), new Point(40, 40))
            {
                DebugSprite = UntitledGame.Debug.Assets.OrangeBox,
                Data = new CollisionPackage
                {
                    Value = "orange",
                    String = "bruh",
                }
            });

            World.AddHitbox(new Hitbox(null, new Vector2(440, 290), new Point(80, 40))
            {
                DebugSprite = UntitledGame.Debug.Assets.PurpleBox,
                Data = new CollisionPackage
                {
                    Value = "purple",
                    String = "crunchy",
                }
            });

            World.AddHitbox(new Hitbox(null, new Vector2(300, 250), new Point(40, 20))
            {
                DebugSprite = UntitledGame.Debug.Assets.OrangeBox,
                Data = new CollisionPackage
                {
                    Value = "orange",
                    String = "trash",
                }
            });

            World.AddHitbox(new Hitbox(null, new Vector2(280, 340), new Point(20, 50))
            {
                DebugSprite = UntitledGame.Debug.Assets.PurpleBox,
                Data = new CollisionPackage
                {
                    Value = "purple",
                    String = "curly",
                }
            });

            World.AddHitbox(new Hitbox(null, new Vector2(300, 340), new Point(20, 50))
            {
                DebugSprite = UntitledGame.Debug.Assets.OrangeBox,
                Data = new CollisionPackage
                {
                    Value = "orange",
                    String = "d00d",
                }
            });

            World.AddHitbox(new Hitbox(null, new Vector2(320, 340), new Point(20, 50))
            {
                DebugSprite = UntitledGame.Debug.Assets.PurpleBox,
                Data = new CollisionPackage
                {
                    Value = "purple",
                    String = "very nice",
                }
            });

            World.AddHitbox(new Hitbox(null, new Vector2(580, 120), new Point(66, 12))
            {
                DebugSprite = UntitledGame.Debug.Assets.PurpleBox,
                Data = new CollisionPackage
                {
                    Value = "purple",
                    String = "cute funny",
                }
            });

            World.AddHitbox(new Hitbox(null, new Vector2(400, 620), new Point(40, 40))
            {
                DebugSprite = UntitledGame.Debug.Assets.OrangeBox,
                Data = new CollisionPackage
                {
                    Value = "orange",
                    String = "popcorn",
                }
            });

            World.AddHitbox(new Hitbox(null, new Vector2(21, 400), new Point(24, 50))
            {
                DebugSprite = UntitledGame.Debug.Assets.PurpleBox,
                Data = new CollisionPackage
                {
                    Value = "purple",
                    String = "bruh bruh bruh",
                }
            });
        }

        private void HandleKeyboard()
        {
            KeyboardState state = Keyboard.GetState();

            if (state.IsKeyDown(Keys.F1) && _oldKeyState.IsKeyUp(Keys.F1))
                _drawDebug = !_drawDebug;

            _oldKeyState = state;
        }

        public override void Update()
        {
            // Physics world step, and then resolve collisions
            // Send to collisions, interacting objects
            World.PhysicsStep();
            HandleKeyboard();

            // update every game object
            foreach (GameObject obj in ActiveGameObjects.Values)
            {
                obj.ResolveCollisions();
            }

            // update every game object
            foreach (GameObject obj in ActiveGameObjects.Values)
            {
                obj.Update();
            }
        }

        public override void Draw()
        {
            foreach (GameObject obj in ActiveGameObjects.Values)
            {
                obj.Draw();
                if (_drawDebug)
                {
                    obj.DrawDebug();
                }
            }

            if (_drawDebug)
            {
                World.DrawDebug();
            }
        }
    }
}
