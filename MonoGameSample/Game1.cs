using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
namespace MonoGameSample
{
    public class Game1 : Game
    {
        public class rockets
        {
           public Texture2D tex;
           public Vector2 pos;
        }
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteFont Font;
        List<rockets> roke = new List<rockets>();
        Texture2D texture, explode, rocket, back;
        int waiting = 0;
        Vector2 bpos = new Vector2(0, 0);
        Vector2 ship_dir = new Vector2(-3, 0);
        Vector2 ship_pos = new Vector2(0, 80);
        Vector2 explode_pos;
        int width, height;
        int explosion = 0;
        int life = 3, score = 0, highscore = 0, kill = 0;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            rocket = Content.Load<Texture2D>("Rocket");
            texture = Content.Load<Texture2D>("Ship");
            explode = Content.Load<Texture2D>("Explode");
            back = Content.Load<Texture2D>("back");
            Font = Content.Load<SpriteFont>("font");
            base.LoadContent();
        }

        protected override void UnloadContent()
        {
            base.UnloadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            width = GraphicsDevice.Viewport.Width;
            height = GraphicsDevice.Viewport.Height;

            if (explosion > 0)
            {
                explosion--;
                if (explosion == 0)
                {
                    ship_pos.X = new Random().Next(1, 300);
                    if (ship_dir.X < 0)
                        ship_dir.X = ship_dir.X - 1;
                    else
                        ship_dir.X = -ship_dir.X - 1;
                }
                base.Update(gameTime);
                return;
            }

            ship_pos -= ship_dir;
            if (ship_pos.X <= 0 || ship_pos.X > width - (texture.Width*0.25))
            {
                ship_dir = -ship_dir;
            }
            
            var tc = TouchPanel.GetState();
            if (waiting > 0)
                {
                    waiting--;
                }
            else
                if (tc.Count() > 0)
                    {
                        rockets g = new rockets();
                        g.tex = rocket;
                        g.pos.X = tc[0].Position.X;
                        g.pos.Y = height - 10;
                        roke.Add(g);
                        waiting = 30;
                    }

                foreach (rockets ro in roke)
                {
                    ro.pos += new Vector2(0, -7);
                    if (ro.pos.Y <= 80 + (texture.Height*0.25) && ro.pos.Y >= texture.Height*0.25 && ro.pos.X >= ship_pos.X && ro.pos.X <= ship_pos.X + (texture.Width*0.25))
                    {
                        explosion = 30;

                        kill++;
                        explode_pos = new Vector2(ro.pos.X - (explode.Width) / 2, ship_pos.Y + 25);
                        score += kill;
                        roke.Clear();
                        break;
                    }
                    if (ro.pos.Y <= 0)
                    {
                        roke.Remove(ro);
                        life -= 1;
                        if (life == 0)
                        {
                            if (score > highscore)
                                highscore = score;
                            kill = 0;
                            score = 0;
                            ship_dir.X = -3;
                            life = 3;
                        }
                            
                        break;
                    }
                        
                }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            spriteBatch.Begin();
            spriteBatch.Draw(back, Vector2.Zero, Color.White);
            spriteBatch.DrawString(Font, "Score:" + score.ToString(), Vector2.Zero, Color.Black);
            spriteBatch.DrawString(Font, "High score:" + highscore.ToString(), new Vector2(0,30), Color.Black);
            spriteBatch.DrawString(Font, "Life:" + life.ToString(), new Vector2(420,0), Color.Black);
            if (explosion == 0)
            {
                spriteBatch.Draw(texture, ship_pos, null, Color.White, 0f, Vector2.Zero, 0.25f, ship_dir.X > 0 ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0f);
            }
            else spriteBatch.Draw(explode, explode_pos, Color.White);

            if (roke.Count >= 0) 
            foreach (rockets ro in roke)
            {
                spriteBatch.Draw(ro.tex, ro.pos, null, Color.White, 0f, Vector2.Zero, 0.5f, SpriteEffects.None, 0f);
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}