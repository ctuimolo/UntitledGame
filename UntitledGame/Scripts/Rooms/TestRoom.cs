using Microsoft.Xna.Framework;
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
            CachedGameObjects = new Dictionary<string, GameObject>();
            ActiveGameObjects = new List<GameObject>();
        }

        public override void LoadContent()
        {
            LoadGameObject(new Wall(World, new Rectangle(0, 420, 800, 80),  "wall_01"));
            LoadGameObject(new Wall(World, new Rectangle(560, 302, 40, 20), "wall_02"));
            LoadGameObject(new Wall(World, new Rectangle(0, 0, 4, 480),     "wall_03"));
            LoadGameObject(new Wall(World, new Rectangle(0, 400, 5, 480),   "wall_04"));
            LoadGameObject(new Wall(World, new Rectangle(796, 0, 4, 480),   "wall_05"));
            LoadGameObject(new Wall(World, new Rectangle(200, 300, 70, 20), "wall_06"));
            LoadGameObject(new Wall(World, new Rectangle(220, 280, 70, 20), "wall_07"));
            LoadGameObject(new Wall(World, new Rectangle(0, 0, 800, 4),     "wall_08"));
            LoadGameObject(new Wall(World, new Rectangle(190, 400, 70, 20), "wall_09"));
            LoadGameObject(new Wall(World, new Rectangle(60, 325, 70, 20),  "wall_10"));
            LoadGameObject(new Wall(World, new Rectangle(390, 388, 40, 32), "wall_11"));
            LoadGameObject(new Wall(World, new Rectangle(432, 388, 40, 32), "wall_12"));
            LoadGameObject(new Wall(World, new Rectangle(474, 388, 40, 32), "wall_13"));
            LoadGameObject(new Wall(World, new Rectangle(516, 388, 40, 32), "wall_14"));
            LoadGameObject(new Wall(World, new Rectangle(558, 388, 40, 32), "wall_15"));
            LoadGameObject(new Wall(World, new Rectangle(600, 388, 40, 32), "wall_16"));
            LoadGameObject(new Wall(World, new Rectangle(642, 388, 40, 32), "wall_17"));
            LoadGameObject(new Player(World, new Vector2(250, 130), "player_1"));

            #region test misc. purple/orange hitboxes...
            World.AddHitbox(new Hitbox(null, new Vector2(420, 310), new Point(30, 60), "spuds")
            {
                DebugSprite = Debug.Assets.OrangeBox,
                Data = new CollisionPackage
                {
                    Value = "orange",
                }
            });;

            World.AddHitbox(new Hitbox(null, new Vector2(550, 290), new Point(20, 120), "cats")
            {
                DebugSprite = Debug.Assets.PurpleBox,
                Data = new CollisionPackage
                {
                    Value = "purple",
                }
            });

            World.AddHitbox(new Hitbox(null, new Vector2(60, 290), new Point(40, 40), "bruh")
            {
                DebugSprite = Debug.Assets.OrangeBox,
                Data = new CollisionPackage
                {
                    Value = "orange",
                }
            });

            World.AddHitbox(new Hitbox(null, new Vector2(440, 290), new Point(80, 40), "crunchy")
            {
                DebugSprite = Debug.Assets.PurpleBox,
                Data = new CollisionPackage
                {
                    Value = "purple",
                }
            });

            World.AddHitbox(new Hitbox(null, new Vector2(300, 250), new Point(40, 20), "trash")
            {
                DebugSprite = Debug.Assets.OrangeBox,
                Data = new CollisionPackage
                {
                    Value = "orange",
                }
            });

            World.AddHitbox(new Hitbox(null, new Vector2(280, 340), new Point(20, 50), "curly")
            {
                DebugSprite = Debug.Assets.PurpleBox,
                Data = new CollisionPackage
                {
                    Value = "purple",
                }
            });

            World.AddHitbox(new Hitbox(null, new Vector2(300, 340), new Point(20, 50), "d00d")
            {
                DebugSprite = Debug.Assets.OrangeBox,
                Data = new CollisionPackage
                {
                    Value = "orange",
                }
            });

            World.AddHitbox(new Hitbox(null, new Vector2(320, 340), new Point(20, 50), "very nice")
            {
                DebugSprite = Debug.Assets.PurpleBox,
                Data = new CollisionPackage
                {
                    Value = "purple",
                }
            });

            World.AddHitbox(new Hitbox(null, new Vector2(580, 120), new Point(66, 12), "cute funny")
            {
                DebugSprite = Debug.Assets.PurpleBox,
                Data = new CollisionPackage
                {
                    Value = "purple",
                }
            });

            World.AddHitbox(new Hitbox(null, new Vector2(400, 620), new Point(40, 40), "popcorn")
            {
                DebugSprite = Debug.Assets.OrangeBox,
                Data = new CollisionPackage
                {
                    Value = "orange",
                }
            });

            World.AddHitbox(new Hitbox(null, new Vector2(21, 400), new Point(24, 50), "bruh bruh bruh")
            {
                DebugSprite = Debug.Assets.PurpleBox,
                Data = new CollisionPackage
                {
                    Value = "purple",
                    String = "bruh bruh bruh",
                }
            });
            #endregion
        }

        public override void InitializeRoom()
        {
            Instantiate("wall_01");
            Instantiate("wall_02");
            Instantiate("wall_03");
            Instantiate("wall_04");
            Instantiate("wall_05");
            Instantiate("wall_06");
            Instantiate("wall_07");
            Instantiate("wall_08");
            Instantiate("wall_09");
            Instantiate("wall_10");
            Instantiate("wall_11");
            Instantiate("wall_12");
            Instantiate("wall_13");
            Instantiate("wall_14");
            Instantiate("wall_15");
            Instantiate("wall_16");
            Instantiate("wall_17");
            Instantiate("player_1");

            Remove("wall_12");
            Remove("wall_13");
            Remove("wall_14");
            Remove("wall_15");

            Instantiate("wall_13");
            Instantiate("wall_14");
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
            foreach (GameObject obj in ActiveGameObjects)
            {
                obj.ResolveCollisions();
            }

            // update every game object
            foreach (GameObject obj in ActiveGameObjects)
            {
                obj.Update();
            }
        }

        public override void Draw()
        {
            foreach (GameObject obj in ActiveGameObjects)
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
