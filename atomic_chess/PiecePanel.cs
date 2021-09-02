using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace atomic_chess
{
    public class PiecePanel : Panel
    {
        public Piece.Color color { set; get; }
        public bool isFree { set; get; }
        public bool movePiece { set; get; }
        public string imageName { set; get; }

        public PiecePanel(Piece.Color color, string imageName)
        {
            this.color = color;
            this.Size = new Size(100, 100);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.imageName = imageName;
        }
    }
}
