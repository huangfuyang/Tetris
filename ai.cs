using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading;
using System.Windows;

namespace Tetris
{
    public class AI
    {
        private double speed = 5;
        private Thread aiThread;
        private BlockMaster bm;
        private bool isEnd;
        private Window1 win;
        public AI(Window1 win)
        {
            bm = win.blockMaster;
            this.win = win;
            aiThread = new Thread(AIControl);
        }

        public void Start()
        {
            isEnd = false;
            aiThread.Start();
        }

        private void AIControl()
        {
            while (!isEnd)
            {
                if (bm.master == null || bm.block == null)
                {
                    continue;
                }
                double bestValue = double.MinValue;
                Block bestBlock = bm.block;
                int bestPos = 0;
                BlockMock bmock = new BlockMock(bm.rows,bm.columns,bm.master);
                for (int i = 0; i < bmock.columns; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        Block b = new Block(bm.block.type, (bm.block.direction + j)%4);
                        b.x = i;
                        b.y = 0;
                        var v = bmock.AddBlock(b);
                        bmock.SetData(bm.master);
                        if (v > bestValue)
                        {
                            bestBlock = b;
                            bestValue = v;
                            bestPos = i;
                        }
                    }
                }
                Console.WriteLine(bestValue);
                while (bm.block.direction != bestBlock.direction)
                {
                    Application.Current.Dispatcher.Invoke(new Action(() => win.BlockChange()));
                    Thread.Sleep(Convert.ToInt32(200/speed));
                }
                while (bm.block.x < bestPos)
                {
                    Application.Current.Dispatcher.Invoke(new Action(() => win.BlockMoveRight()));
                    Thread.Sleep(Convert.ToInt32(200 / speed));
                }
                while (bm.block.x > bestPos)
                {
                    Application.Current.Dispatcher.Invoke(new Action(() => win.BlockMoveLeft()));
                    Thread.Sleep(Convert.ToInt32(200 / speed));
                }
                while (bm.canMovedown)
                {
                    Application.Current.Dispatcher.Invoke(new Action(() => win.BlockMoveDown()));
                    Thread.Sleep(Convert.ToInt32(100 / speed));
                }
                Application.Current.Dispatcher.Invoke(new Action(() => win.BlockMoveDown()));


            }
        }

        public void End()
        {
            isEnd = true;
            aiThread.Abort();
        }
    }
}
