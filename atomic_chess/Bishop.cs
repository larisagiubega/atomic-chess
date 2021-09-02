using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace atomic_chess
{
    class Bishop : Piece, IPiece
    {
        public static int[,] legalMoves = new int[,] {
                                            { -1, 1 }, //dreapta sus
                                            { -1, -1 }, //stanga sus
                                            { 1, -1 }, //stanga jos
                                            { 1, 1 } //dreapta jos
                                            };
        public Bishop(int x, int y, Piece.Color color, string name) : base(x, y, color, name)
        {
            points = 3;
        }

        public override bool canMove(int newX, int newY, Piece.Color color)
        {
            string key;//pentru calcularea cheii fiecarei pozitii
            int x, y;//x si y local, variabile in care calculam pozitiile
            bool movedPiece = false;//boolean cu ajutorul caruia facem break din for si care tine cont daca s-a reusit mutarea
            for (int i = 0; i < 4; i++)//parcurgem matricea de mutari legale
            {
                x = this.x;//la fiecare iteratie x ia x initial al obiectului
                y = this.y;//la fiecare iteratie y ia y initial al obiectului

                while (this.inBounds(x + legalMoves[i, 0], y + legalMoves[i, 1]))//cat timp nu s-a iesit din tabla cu x si cu y
                {
                    x = x + legalMoves[i, 0];//x ia vechiul x+valoarea de pe coloana 0 din matricea de mutari legale
                    y = y + legalMoves[i, 1];//y ia vechiul y+valoarea de pe coloana 1 din matricea de mutari legale
                    //practic se simuleaza mutarea piesei pentru a verifica daca este o mutare corecta

                    key = Convert.ToInt32(x) + Convert.ToString(y);//se calculeaza cheia formata din noii x si y

                    if (!atomicChess.panelDict[key].isFree)//daca panelul corespunzator lui x si y nu e liber
                    {
                        if (!atomicChess.panelDict[key].imageName.Contains(Convert.ToString(color))) //daca e o culoare diferita 
                        {   //se ia piesa de culoare diferita
                            if (x == newX && y == newY)//daca x si y curente corespund x-ului si y-ului pe care am dat click
                            {
                                //se face mutarea
                                movedPiece = true;
                                break; //se iese din while
                            }
                            else break;//daca x si y nu corespund x-ului si y-ului pe care am dat click se iese din while pentru ca inseamna ca nu e directia potrivita
                        }
                        else break; //daca nu e culoare diferita break
                    }
                    if (x == newX && y == newY)//daca panelul e liber se face mutarea
                    {
                        movedPiece = true;
                        break;
                    }
                }
                if (movedPiece == true) break;//daca s-a reusit mutarea se iese din for (nu se mai verifica alte pozitii)
            }
            return movedPiece;//se returneaza true sau false
        }

    }
}
