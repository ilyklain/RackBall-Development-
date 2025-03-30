using System;
using System.Collections.Generic;

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
        
        // fricción
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

    public static void CheckBallCollision(Ball b1, Ball b2)
    {
        double dx = b2.X - b1.X;
        double dy = b2.Y - b1.Y;
        double distance = Math.Sqrt(dx * dx + dy * dy);
        double minDistance = b1.Radius + b2.Radius;

        if (distance < minDistance)
        {
            // Calcular nueva dirección usando conservación del momento
            double angle = Math.Atan2(dy, dx);
            double speed1 = Math.Sqrt(b1.VelocityX * b1.VelocityX + b1.VelocityY * b1.VelocityY);
            double speed2 = Math.Sqrt(b2.VelocityX * b2.VelocityX + b2.VelocityY * b2.VelocityY);
            
            b1.VelocityX = speed2 * Math.Cos(angle);
            b1.VelocityY = speed2 * Math.Sin(angle);
            b2.VelocityX = speed1 * Math.Cos(angle + Math.PI);
            b2.VelocityY = speed1 * Math.Sin(angle + Math.PI);
        }
    }
}

class Program
{
    static void Main()
    {
        List<Ball> balls = new List<Ball>
        {
            new Ball(50, 25, 5, 3),
            new Ball(60, 25, -4, -2)
        };
        double tableWidth = 100;
        double tableHeight = 50;

        for (int i = 0; i < 100; i++) // Simulación de 100 frames
        {
            foreach (var ball in balls)
            {
                ball.Update();
                ball.CheckWallCollision(tableWidth, tableHeight);
            }
            
            Ball.CheckBallCollision(balls[0], balls[1]);
            
            Console.WriteLine($"Frame {i + 1}: Ball 1 -> X={balls[0].X}, Y={balls[0].Y}, VX={balls[0].VelocityX}, VY={balls[0].VelocityY}");
            Console.WriteLine($"Frame {i + 1}: Ball 2 -> X={balls[1].X}, Y={balls[1].Y}, VX={balls[1].VelocityX}, VY={balls[1].VelocityY}");
        }
    }
}
