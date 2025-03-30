using System;

class Ball
{
    public double X { get; set; }
    public double Y { get; set; }
    public double VelocityX { get; set; }
    public double VelocityY { get; set; }
    public double Radius { get; set; } = 2.85; // Tamaño estándar de una bola de billar
    public double Friction { get; set; } = 0.98; // Factor de fricción

    public Ball(double x, double y, double velocityX, double velocityY)
    {
        X = x;
        Y = y;
        VelocityX = velocityX;
        VelocityY = velocityY;
    }

    public void Update()
    {
        X += VelocityX;
        Y += VelocityY;
        
        // friccion
        VelocityX *= Friction;
        VelocityY *= Friction;
    }

    public void CheckWallCollision(double tableWidth, double tableHeight)
    {
        if (X - Radius < 0 || X + Radius > tableWidth)
        {
            VelocityX = -VelocityX; // Rebote en las paredes laterales
        }
        if (Y - Radius < 0 || Y + Radius > tableHeight)
        {
            VelocityY = -VelocityY; // Rebote en las paredes superior/inferior
        }
    }
}

class Program
{
    static void Main()
    {
        Ball ball = new Ball(50, 25, 5, 3);
        double tableWidth = 100;
        double tableHeight = 50;

        for (int i = 0; i < 100; i++) // Simulación de 100 frames
        {
            ball.Update();
            ball.CheckWallCollision(tableWidth, tableHeight);
            Console.WriteLine($"Frame {i + 1}: X={ball.X}, Y={ball.Y}, VX={ball.VelocityX}, VY={ball.VelocityY}");
        }
    }
}
