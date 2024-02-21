using System;

public class TileHealth
{
    public int indexX;
    public int indexY;
    public float health;

    public TileHealth(int x, int y)
    {
        indexX = x;
        indexY = y;
    }
    
    public TileHealth(int x, int y, float health): this(x, y)
    {
        this.health = health;
    }
    
    public TileHealth WithHealth(float health)
    {
        this.health = health;
        return this;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
    }
}