using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using System;

namespace AngularDynamicsExercise;

/// <summary>
/// A class representing the players' ship
/// </summary>
public class ShipSprite
{
    const float LINEAR_ACC = 10;
    const float ANGULAR_ACC = 5;
    Game game;
    Texture2D texture;
    Vector2 position;
    Vector2 velocity;

    Vector2 direction;

    float angle; 
    float angVelo;

    

    /// <summary>
    /// Creates the ship sprite
    /// </summary>
    public ShipSprite(Game game)
    {
        this.game = game;
        this.position = new Vector2(375, 250);
        this.direction = -Vector2.UnitY;
    }

    /// <summary>
    /// Loads the sprite texture
    /// </summary>
    /// <param name="content">The content manager to load with</param>
    public void LoadContent(ContentManager content)
    {
        texture = content.Load<Texture2D>("ship");
    }

    /// <summary>
    /// Updates the ship sprite
    /// </summary>
    /// <param name="gameTime">An object representing time in the game</param>
    public void Update(GameTime gameTime)
    {
        KeyboardState keyboardState = Keyboard.GetState();
        float t = (float)gameTime.ElapsedGameTime.TotalSeconds;

        Vector2 acc = new Vector2(0, 0);
        float angularAcc = 0;
        if (keyboardState.IsKeyDown(Keys.Left))
        {
            acc += direction * LINEAR_ACC;
            angularAcc += ANGULAR_ACC;
            
        }
        if (keyboardState.IsKeyDown(Keys.Right))
        {
            acc += direction * LINEAR_ACC;
            angularAcc -= ANGULAR_ACC;
            
        }

        angVelo += angularAcc * t;
        angle += angVelo * t;
        direction.X = (float)Math.Sin(angle);
        direction.Y = (float)-Math.Cos(angle);

        velocity += acc * t;
        position += velocity * t;

        // Wrap the ship to keep it on-screen
        var viewport = game.GraphicsDevice.Viewport;
        if (position.Y < 0) position.Y = viewport.Height;
        if (position.Y > viewport.Height) position.Y = 0;
        if (position.X < 0) position.X = viewport.Width;
        if (position.X > viewport.Width) position.X = 0;
    }

    /// <summary>
    /// Draws the sprite
    /// </summary>
    /// <param name="gameTime">An object representing time in the game</param>
    /// <param name="spriteBatch">The SpriteBatch to draw with</param>
    public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(texture, position, null, Color.White, angle, new Vector2(30, 39), 1f, SpriteEffects.None, 0);
    }
}

