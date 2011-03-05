using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Media;
using wg7;


namespace wg7
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        //SpriteFont Font1;
        Texture2D ch1, ch2, s, c1, c2, c3, c4, c5;
        Texture2D selectCh1, selectCh2, bg;
        Random rand = new Random();

        #region position
        Vector2 posSCh1 = new Vector2(150, 150);
        Vector2 posSCh2 = new Vector2(160, 400);
        Vector2 posCh1 = new Vector2(240, 520);
        Vector2 posCh2 = new Vector2(240, 520);
        Vector2 tp = new Vector2(240, 648);

        Vector2 posS = new Vector2(0, 730);
        Vector2 posBG = new Vector2(0, 0);

        Vector2 pos1 = new Vector2(0, 0);
        Vector2 pos2 = new Vector2(0, 0);
        Vector2 pos3 = new Vector2(0, 0);
        Vector2 pos4 = new Vector2(0, 0);
        Vector2 pos5 = new Vector2(0, 0);
        #endregion

        int sm = 0, sm1 = 1, smY = 0, point = 0;
        int s1 = 2, s2 = 2, s3 = 2, s4 = 2, s5 = 2;
        byte stage = 1, character = 0;
        int frame, fps;
        float te, tpf;

        bool touching = false;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            // Frame rate is 30 fps by default for Windows Phone.
            //TargetElapsedTime = TimeSpan.FromTicks(333333);
            TargetElapsedTime = TimeSpan.FromTicks(220000);
            graphics.PreferredBackBufferWidth = 480;
            graphics.PreferredBackBufferHeight = 728;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            pos1.X = rand.Next(0, 440);
            pos1.Y = -1 * (rand.Next(70, 150));

            pos2.X = rand.Next(0, 440);
            pos2.Y = -1 * (rand.Next(70, 150));

            pos3.X = rand.Next(0, 440);
            pos3.Y = -1 * (rand.Next(70, 150));

            pos4.X = rand.Next(0, 440);
            pos4.Y = -1 * (rand.Next(70, 150));

            pos5.X = rand.Next(0, 440);
            pos5.Y = -1 * (rand.Next(70, 150));

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            selectCh1 = this.Content.Load<Texture2D>("character_Boy1");
            selectCh2 = this.Content.Load<Texture2D>("character_Girl1");
            ch1 = this.Content.Load<Texture2D>("character3");
            ch2 = this.Content.Load<Texture2D>("character_Girl2");
            c1 = this.Content.Load<Texture2D>("circle");
            c2 = this.Content.Load<Texture2D>("circle");
            c3 = this.Content.Load<Texture2D>("circle");
            c4 = this.Content.Load<Texture2D>("circle");
            c5 = this.Content.Load<Texture2D>("circle");
            bg = this.Content.Load<Texture2D>("ghost_bg");
            s = this.Content.Load<Texture2D>("smoke");
            //circle  = this.Content.Load<Texture2D>("sprite");
            fps = 4;
            tpf = 1 / (float)fps;
            frame = 0;
            te = 0;
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        void UpdateFrame(float tElapse)
        {
            te += tElapse;
            if (te > tpf)
            {
                frame = (frame + 1) % 4;
                te -= tpf;
            }
        }

        private void HandleTouches()
        {
            TouchCollection touches = TouchPanel.GetState();

            if (!touching && touches.Count > 0)
            {
                touching = true;

                foreach (TouchLocation t in touches)
                {

                    if ((t.State == TouchLocationState.Pressed) ||
                        (t.State == TouchLocationState.Moved))
                    {
                        if (stage == 1)
                        {
                            if ((t.Position.X >= posSCh1.X && t.Position.X <= (posSCh1.X + 150)) && (t.Position.Y >= posSCh1.Y && t.Position.Y <= (posSCh1.Y + 120)))
                            {
                                character = 1;
                            }
                            else if ((t.Position.X >= posSCh2.X && t.Position.X <= (posSCh2.X + 150)) && (t.Position.Y >= posSCh2.Y && t.Position.Y <= (posSCh2.Y + 143)))
                            {
                                character = 2;
                            }
                            stage = 2;
                        }
                        else
                        {
                            tp = t.Position;
                        }

                    }
                }
            }
            else if (touches.Count == 0)
                touching = false;
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here
            if (stage == 1)
            {
                HandleTouches();
            }
            else if (stage == 2)
            {
                HandleTouches();
                rndDown();
                if (character == 1)
                {
                    go1();
                    check1();
                }
                else if (character == 2)
                {
                    go2();
                    check2();
                }

            }
            UpdateFrame((float)gameTime.ElapsedGameTime.TotalSeconds);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            // TODO: Add your drawing code here
            spriteBatch.Begin();
            spriteBatch.Draw(bg, posBG, Color.CornflowerBlue);

            if (stage == 1)
            {
                spriteBatch.Draw(selectCh1, posSCh1, Color.AliceBlue);
                spriteBatch.Draw(selectCh2, posSCh2, Color.AliceBlue);
            }
            else if (stage == 2)
            {
                spriteBatch.Draw(c1, pos1, Color.AliceBlue);
                spriteBatch.Draw(c2, pos2, Color.DarkGoldenrod);
                spriteBatch.Draw(c3, pos3, Color.BlueViolet);
                spriteBatch.Draw(c4, pos4, Color.YellowGreen);
                spriteBatch.Draw(c5, pos5, Color.Peru);
                if (character == 1)
                {
                    spriteBatch.Draw(ch1, posCh1, Color.White);
                }
                else if (character == 2)
                {
                    spriteBatch.Draw(ch2, posCh2, Color.White);
                }
                spriteBatch.Draw(s, posS, new Rectangle(sm * 128, smY, 110, smY + 128), Color.White);
            }
            //spriteBatch.Draw(circle, pos, new Rectangle(frame * 50, 0, 50, 50), Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }

        #region Move
        protected void go1()
        {
            if (tp.X > posCh1.X + 30) posCh1.X += 5;
            if (tp.X < posCh1.X + 30) posCh1.X -= 5;
        }
        protected void go2()
        {
            if (tp.X > posCh2.X + 30) posCh2.X += 5;
            if (tp.X < posCh2.X + 30) posCh2.X -= 5;
        }
        protected void rndDown()
        {
            if (pos1.Y >= 728)
            {
                pos1.Y = -1 * (rand.Next(70, 150));
                pos1.X = rand.Next(0, 440);
            }
            if (pos2.Y >= 728)
            {
                pos2.Y = -1 * (rand.Next(70, 150));
                pos2.X = rand.Next(0, 440);
            }
            if (pos3.Y >= 728)
            {
                pos3.Y = -1 * (rand.Next(70, 150));
                pos3.X = rand.Next(0, 440);
            }
            if (pos4.Y >= 728)
            {
                pos4.Y = -1 * (rand.Next(70, 150));
                pos4.X = rand.Next(0, 440);
            }
            if (pos5.Y >= 728)
            {
                pos5.Y = -1 * (rand.Next(70, 150));
                pos5.X = rand.Next(0, 440);
            }
            pos1.Y += s1;
            pos2.Y += s2;
            pos3.Y += s3;
            pos4.Y += s4;
            pos5.Y += s5;
            if (sm1 == 11)
            {
                effect();
            }

        }
        protected void effect()
        {
            sm = (sm + 1) % sm1;
            if (sm == 10 && smY == 0)
            {
                smY = 128;
            }
            else if (sm == 10 && smY == 128)
            {
                smY = 0;
                sm1 = 1;
                posS.Y = 730;
            }
        }
        #endregion

        #region Check ball
        protected void check1()
        {
            if ((pos1.X - posCh1.X >= -20 && pos1.X - posCh1.X <= 40) && (pos1.Y - posCh1.Y >= -65 && pos1.Y - posCh1.Y <= 10))
            {
                point += 10;
                sm1 = 11;
                posS.X = (posCh1.X + 47) - 55;
                posS.Y = posCh1.Y - 100;
                pos1.X = rand.Next(0, 440);
                pos1.Y = -1 * (rand.Next(70, 150));
            }
            else if ((pos2.X - posCh1.X >= -20 && pos2.X - posCh1.X <= 40) && (pos2.Y - posCh1.Y >= -65 && pos2.Y - posCh1.Y <= 10))
            {
                point += 10;
                sm1 = 11;
                posS.X = (posCh1.X + 47) - 55;
                posS.Y = posCh1.Y - 100;
                pos2.X = rand.Next(0, 440);
                pos2.Y = -1 * (rand.Next(70, 150));
            }
            else if ((pos3.X - posCh1.X >= -20 && pos3.X - posCh1.X <= 40) && (pos3.Y - posCh1.Y >= -65 && pos3.Y - posCh1.Y <= 10))
            {
                point += 10;
                sm1 = 11;
                posS.X = (posCh1.X + 47) - 55;
                posS.Y = posCh1.Y - 100;
                pos3.X = rand.Next(0, 440);
                pos3.Y = -1 * (rand.Next(70, 150));
            }
            else if ((pos4.X - posCh1.X >= -20 && pos4.X - posCh1.X <= 40) && (pos4.Y - posCh1.Y >= -65 && pos4.Y - posCh1.Y <= 10))
            {
                point += 10;
                sm1 = 11;
                posS.X = (posCh1.X + 47) - 55;
                posS.Y = posCh1.Y - 100;
                pos4.X = rand.Next(0, 440);
                pos4.Y = -1 * (rand.Next(70, 150));
            }
            else if ((pos5.X - posCh1.X >= -20 && pos5.X - posCh1.X <= 40) && (pos5.Y - posCh1.Y >= -65 && pos5.Y - posCh1.Y <= 10))
            {
                point += 10;
                sm1 = 11;
                posS.X = (posCh1.X + 47) - 55;
                posS.Y = posCh1.Y - 100;
                pos5.X = rand.Next(0, 440);
                pos5.Y = -1 * (rand.Next(70, 150));
            }
        }
        protected void check2()
        {
            if ((pos1.X - posCh2.X >= -20 && pos1.X - posCh2.X <= 40) && (pos1.Y - posCh2.Y >= -65 && pos1.Y - posCh2.Y <= 10))
            {
                point += 10;
                sm1 = 11;
                posS.X = (posCh2.X + 47) - 55;
                posS.Y = posCh2.Y - 100;
                pos1.X = rand.Next(0, 440);
                pos1.Y = -1 * (rand.Next(70, 150));
            }
            else if ((pos2.X - posCh2.X >= -20 && pos2.X - posCh2.X <= 40) && (pos2.Y - posCh2.Y >= -65 && pos2.Y - posCh2.Y <= 10))
            {
                point += 10;
                sm1 = 11;
                posS.X = (posCh2.X + 47) - 55;
                posS.Y = posCh2.Y - 100;
                pos2.X = rand.Next(0, 440);
                pos2.Y = -1 * (rand.Next(70, 150));
            }
            else if ((pos3.X - posCh2.X >= -20 && pos3.X - posCh2.X <= 40) && (pos3.Y - posCh2.Y >= -65 && pos3.Y - posCh2.Y <= 10))
            {
                point += 10;
                sm1 = 11;
                posS.X = (posCh2.X + 47) - 55;
                posS.Y = posCh2.Y - 100;
                pos3.X = rand.Next(0, 440);
                pos3.Y = -1 * (rand.Next(70, 150));
            }
            else if ((pos4.X - posCh2.X >= -20 && pos4.X - posCh2.X <= 40) && (pos4.Y - posCh2.Y >= -65 && pos4.Y - posCh2.Y <= 10))
            {
                point += 10;
                sm1 = 11;
                posS.X = (posCh2.X + 47) - 55;
                posS.Y = posCh2.Y - 100;
                pos4.X = rand.Next(0, 440);
                pos4.Y = -1 * (rand.Next(70, 150));
            }
            else if ((pos5.X - posCh2.X >= -20 && pos5.X - posCh2.X <= 40) && (pos5.Y - posCh2.Y >= -65 && pos5.Y - posCh2.Y <= 10))
            {
                point += 10;
                sm1 = 11;
                posS.X = (posCh2.X + 47) - 55;
                posS.Y = posCh2.Y - 100;
                pos5.X = rand.Next(0, 440);
                pos5.Y = -1 * (rand.Next(70, 150));
            }
        }
        #endregion




    }
}
