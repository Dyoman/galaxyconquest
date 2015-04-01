using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gwen.Control {
	public class ClickedEventArgs : EventArgs {
		public int X { get; private set; }
		public int Y { get; private set; }
		public bool MouseDown { get; private set; }

		internal ClickedEventArgs(int x, int y, bool down) {
			this.X = x;
			this.Y = y;
			this.MouseDown = down;
		}
	}

    public class MovedEventArgs : EventArgs
    {
        public int X { get; private set; }
        public int Y { get; private set; }
        public bool MouseDown { get; private set; }

        internal MovedEventArgs(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }
    }
}
