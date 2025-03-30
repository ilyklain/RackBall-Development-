using System;
using System.Collections.Generic;

class Ball
{
    public double X { get; set; }
    public double Y { get; set; }
    public double VelocityX { get; set; }
    public double VelocityY { get; set; }
    public double Spin { get; set; } = 0; // Efecto de giro
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
        
        // efecto de giro
        VelocityY += Spin;
        Spin *= 0.95; // Disminuye con el tiempo
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

class CueStick
{
    public double Angle { get; set; } // Ángulo en radianes
    public double Power { get; set; } // Potencia del golpe
    public double Spin { get; set; } // Efecto de giro aplicado

    public CueStick(double angle, double power, double spin)
    {
        Angle = angle;
        Power = power;
        Spin = spin;
    }

    public void HitBall(Ball cueBall)
    {
        cueBall.VelocityX = Power * Math.Cos(Angle);
        cueBall.VelocityY = Power * Math.Sin(Angle);
        cueBall.Spin = Spin; // Aplica el efecto de giro
    }
}

class Program
{
    static void Main()
    {
        Ball cueBall = new Ball(50, 25, 0, 0);
        double tableWidth = 100;
        double tableHeight = 50;

        // Simulación de un golpe con efecto de giro
        CueStick cue = new CueStick(angle: Math.PI / 4, power: 10, spin: -1.5); // 45° con fuerza 10 y efecto de retroceso
        cue.HitBall(cueBall);
        
        for (int i = 0; i < 100; i++) // Simulación de 100 frames
        {
            cueBall.Update();
            cueBall.CheckWallCollision(tableWidth, tableHeight);
            
            Console.WriteLine($"Frame {i + 1}: Cue Ball -> X={cueBall.X}, Y={cueBall.Y}, VX={cueBall.VelocityX}, VY={cueBall.VelocityY}, Spin={cueBall.Spin}");
        }
    }
}
