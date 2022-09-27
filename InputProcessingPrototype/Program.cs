using System;
using System.Collections.Generic;

namespace InputProcessingPrototype
{
    class Program
    {
        public enum Input
        {
            // keyboard
            F1, F2, F3, F4, F5, F6, F7, F8, F9, F10, F11, F12,
            N0, N1, N2, N3, N4, N5, N6, N7, N8, N9,
            A, B, C, D, E, F, G, H, I, J, K, L, M, N, O, P, Q, R, S, T, U, V, W, X, Y, Z,
            Space, Enter, Escape, CapsLock, Shift, Tab, Ctrl, BackSpace,
            // numpad
            P0, P1, P2, P3, P4, P5, P6, P7, P8, P9,
            Up, Down, Left, Right,
            // mouse
            LeftClick, RightClick, ScrollWheel, ScrollUp, ScrollDown,
            Count,
        }

        // Input --> process as key bind --> event

        private static EventHandler[] _keyPressed = new EventHandler[(int)Input.Count];

        public event EventHandler
            AKeyPressed,
            BKeyPressed,
            CKeyPressed,
            DKeyPressed
            ;

        static void Main(string[] args)
        {

        }

        private static void InitializeKeyBinds()
        {
            SetKeyBind(Input.W, OnPlayerJumps);
            SetKeyBind(Input.W, OnWKeyPressed);
            SetKeyBind(Input.RightClick, PlayerDestroysBlock);
            SetKeyBind(Input.E, MenuIsOpened);
        }
        private static void SetKeyBind(Input input, params EventHandler[] subscribers)
        {
            int i = (int)input;
            if (subscribers is null)
            {
                return;
            }

            if (_eyPressed[i] is null)
            {
                _keyPressed[i] = subscribers[0];
            }
            else
            {
                _keyPressed[i] += subscribers[0];
            }

            for (int j = 1; j < subscribers.Length; j++)
            {
                _keyPressed[i] += subscribers[j];
            }
        }

        private static void PlayerDestroysBlock(object? source, EventArgs eventArgs)
        {

        }

        private static void OnPlayerJumps(object? source, EventArgs eventArgs)
        {
            Console.WriteLine("Player jumps.");
        }

        private static void OnWKeyPressed(object? source, EventArgs eventArgs)
        {
            Console.WriteLine("[W] key was pressed.");
        }

        private static void MenuIsOpened(object? source, EventArgs eventArgs)
        {
            Console.WriteLine("Menu");
            Console.WriteLine("1. Option A");
            Console.WriteLine("2. Option B");
        }
    }
}
