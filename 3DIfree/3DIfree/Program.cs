﻿using System;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Input;
using  System.Drawing;
using BeginMode = OpenTK.Graphics.OpenGL.BeginMode;
using ClearBufferMask = OpenTK.Graphics.OpenGL.ClearBufferMask;
using EnableCap = OpenTK.Graphics.OpenGL.EnableCap;
using GL = OpenTK.Graphics.OpenGL.GL;
using MatrixMode = OpenTK.Graphics.OpenGL.MatrixMode;

namespace _3DIfree
{
    class Program
    {
        static void Main(string[] args)
        {            
            using (Game game = new Game())
            {
                game.Run(30.0);
            }
        }
    }

    internal class Game : GameWindow
    {
        Vector3 vector1 = new Vector3(-1.0f, -1.0f, 4.0f);
        public Game() : base(800, 600, GraphicsMode.Default, "Копипаст")
        {
            VSync = VSyncMode.On;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            GL.ClearColor(0.1f, 0.2f, 0.5f, 0.0f);
            GL.Enable(EnableCap.DepthTest);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            GL.Viewport(ClientRectangle.X, ClientRectangle.Y, ClientRectangle.Width, ClientRectangle.Height);

            Matrix4 projection = Matrix4.CreatePerspectiveFieldOfView((float)Math.PI / 4, Width / (float)Height, 1.0f, 64.0f);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref projection);
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);

            if (Keyboard[Key.Up])
            {
                vector1 = Vector3.Add(vector1, new Vector3(0.1f, 0.1f, 0.1f));
            }
            if (Keyboard[Key.Down])
            {
                vector1 = Vector3.Add(vector1, new Vector3(-0.1f, -0.1f, -0.1f));
            }
            if (Keyboard[Key.Escape])
                Exit();
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            Matrix4 modelview = Matrix4.LookAt(Vector3.Zero, Vector3.UnitZ, Vector3.UnitY);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadMatrix(ref modelview);

            GL.Begin(BeginMode.Triangles);

            
            GL.Color3(1.0f, 1.0f, 0.0f);
            GL.Vertex3(vector1);

            GL.Color3(1.0f, 0.0f, 0.0f);
            GL.Vertex3(1.0f, -1.0f, 4.0f);

            GL.Color3(0.2f, 0.9f, 1.0f);
            GL.Vertex3(0.0f, 1.0f, 4.0f);

            GL.End();

            SwapBuffers();
        }
    }

}
