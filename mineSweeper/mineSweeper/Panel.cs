using System;
using System.Collections.Generic;
using System.Text;

namespace project
{
    public class Panel
    {
        private int num;
        private bool isRevealed;
        private bool isFlagged;

        public Panel(int num)
        {
            this.num = num;
            this.isRevealed = false;
            this.isFlagged = false;
        }
        public void ResetPanel(int n)
        {
            this.num = n;
            this.isRevealed = false;
            this.isFlagged = false;
        }
        //
        public void setNum(int num)
        {
            this.num = num;
        }
        public int getNum()
        {
            return this.num;
        }

        //
        public void setIsRevealed(bool isRevealed)
        {
            this.isRevealed = isRevealed;
        }
        public bool getIsRevealed()
        {
            return this.isRevealed;
        }
        //
        public void setIsFlagged(bool isFlagged)
        {
            this.isFlagged = isFlagged;
        }
        public bool getIsFlagged()
        {
            return this.isFlagged;
        }
        //
    }
}
