using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dothan.ExportCmd
{
    public class MyProgressBar
    {
        #region Property

        public int TotalCount
        {
            get;
            set;
        }

        public int CurrentIndex
        {
            get { return this._CurrentIndex; }
            set
            {
                if (value < 0 || value > this.TotalCount)
                    return;

                if (this._CurrentIndex != value)
                {
                    this._CurrentIndex = value;

                    if (value != 0)
                    {
                        this.PbIndex = pbLength * value / TotalCount;
                        this.PbRate = value * 100 / this.TotalCount;
                        this.updateRateView();
                    }
                }

                if (value >= this.TotalCount)
                    this.Close();
            }
        }
        private int _CurrentIndex;

        #endregion

        #region View

        public bool IsShowing
        {
            get;
            protected set;
        }

        private ConsoleColor backColor;
        private ConsoleColor foreColor;
        private int pbLeft;
        private int pbTop;
        private const int pbLength = 25;
        public int PbRate
        {
            get { return this._PbRate; }
            protected set
            {
                if (this._PbRate != value)
                {
                    this._PbRate = value;
                }
            }
        }
        private int _PbRate;

        protected int PbIndex
        {
            get { return this._PbIndex; }
            set
            {
                if (value > pbLength || value < 0)
                    return;

                if (this._PbIndex != value)
                {
                    this.pbIndexBack = this._PbIndex;
                    this._PbIndex = value;

                    this.updateProgressBar();
                }
            }
        }
        private int _PbIndex;
        private int pbIndexBack;

        public void Show(int totalCount)
        {
            this.TotalCount = totalCount;

            this.initView();
        }

        private void initView()
        {
            // 新起一行
            Console.WriteLine();
            this.backColor = Console.BackgroundColor;
            this.foreColor = Console.ForegroundColor;
            this.pbLeft = Console.CursorLeft;
            this.pbTop = Console.CursorTop;
            this.pbIndexBack = this._PbIndex = 0;
            this._CurrentIndex = 0;
            this.PbRate = 0;
            this.IsShowing = true;

            // 初始化进度条背景色
            Console.BackgroundColor = ConsoleColor.DarkCyan;
            for (int i = 0; i < pbLength; i++)
            {
                Console.Write(" ");
            }
            Console.BackgroundColor = backColor;
            // 初始化进度
            this.updateRateView();
            Console.CursorVisible = false;
        }

        /// <summary>
        /// 将进度条移动到指定的位置。
        /// </summary>
        /// <param name="pbPos">目标位置，介于0~pbLength之间。</param>
        private void updateProgressBar()
        {
            if (!this.IsShowing)
                return;

            // 向右移动
            Console.BackgroundColor = ConsoleColor.Yellow;
            Console.SetCursorPosition(pbLeft + pbIndexBack, pbTop);
            for (int i = pbIndexBack; i < PbIndex; i++)
            {
                Console.Write(" ");
            }
            Console.BackgroundColor = backColor;
            Console.SetCursorPosition(this.pbLeft, this.pbTop + 2);
        }

        private void updateRateView()
        {
            if (!this.IsShowing)
                return;

            Console.ForegroundColor = ConsoleColor.Green;
            Console.SetCursorPosition(pbLeft + pbLength + 1, pbTop);
            Console.WriteLine(string.Format("{0, 3}% ({1}/{2})", this.PbRate, this.CurrentIndex, this.TotalCount));
            Console.ForegroundColor = this.foreColor;
            Console.SetCursorPosition(this.pbLeft, this.pbTop + 2);
        }

        public void Close()
        {
            if (!this.IsShowing)
                return;

            this.IsShowing = false;
            Console.BackgroundColor = this.backColor;
            Console.ForegroundColor = this.foreColor;
            Console.SetCursorPosition(this.pbLeft, this.pbTop + 2);
            Console.CursorVisible = true;
        }

        #endregion

    }
}
