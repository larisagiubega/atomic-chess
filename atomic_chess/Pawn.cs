using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace atomic_chess
{
    class Pawn : Piece, IPiece
    {
        //declaram o matrice care contine toate mutarile legale ale pionului; este necesara o singura verificare a intregii matrici pentru o mutare
        //matricea urmatoare se ocupa DOAR de pionii din partea de jos a tablei; ulterior se va inmulti cu -1 pentru a obtine matricea care se ocupa
        //de pionii din partea de sus a tablei
        public int[,] legalMoves = new int[,] {
                                            { -1, 0 }, //o pozitie sus
                                            { -2, 0 }, //doua pozitii sus
                                            { -1, 1 }, //dreapta
                                            { -1, -1 } //stanga
                                            };

        public Pawn(int x, int y, Piece.Color color, string name) : base(x, y, color, name)
        {
            points = 1;

        }

        public override bool canMove(int newX, int newY, Piece.Color color)
        {
            string key = Convert.ToInt32(newX) + Convert.ToString(newY);

            //Console.WriteLine("pawn= " + this.x + "->" + newX + ", " + this.y + "->" + newY + "--- points= " + points);
            //fa verificare cu matrice newX newY
            if (Board.beginningTurn == Piece.Color.white)
            {
                if (color == Piece.Color.white)
                {
                    if (atomicChess.panelDict[key].isFree == true || atomicChess.panelDict[key].imageName.Contains("black")) //daca panelul e liber sau daca e ocupat de adversar se poate muta
                    {
                        if (newX == this.x + legalMoves[0, 0] && newY == this.y + legalMoves[0, 1] &&
                       newX > 0 && atomicChess.panelDict[key].isFree) //daca se muta o pozitie in sus, daca nu iese din tabla si daca noua pozitie e libera
                        {
                            return true;
                        }

                        if (newX == this.x + legalMoves[1, 0] && newY == this.y + legalMoves[1, 1] &&
                       newX > 3 && atomicChess.panelDict[key].isFree) //daca se muta 2 pozitii in sus, daca nu iese din tabla si daca noua pozitie e libera
                                                                      //(mutarea poate avea loc doar daca nu s-a trecut de jumatatea tablei)
                        {
                            return true;
                        }

                        if (newX == this.x + legalMoves[2, 0] && newY == this.y + legalMoves[2, 1]
                            && atomicChess.panelDict[key].imageName.Contains("black"))//se poate muta in dreapta DOAR daca este piesa adversarului acolo
                        {
                            return true;
                        }

                        if (newX == this.x + legalMoves[3, 0] && newY == this.y + legalMoves[3, 1]
                            && atomicChess.panelDict[key].imageName.Contains("black")) //se poate muta in stanga DOAR daca este piesa adversarului acolo
                        {
                            return true;
                        }
                    }

                }
                if (color == Piece.Color.black)
                {
                    if (atomicChess.panelDict[key].isFree == true || atomicChess.panelDict[key].imageName.Contains("white")) //daca panelul e liber sau daca e ocupat de adversar se poate muta
                    {
                        if (newX == this.x + (-1) * legalMoves[0, 0] && newY == this.y + (-1) * legalMoves[0, 1] &&
                       newX < 7 && atomicChess.panelDict[key].isFree) //daca se muta o pozitie in jos, daca nu iese din tabla si daca noua pozitie e libera
                        {
                            return true;
                        }

                        if (newX == this.x + (-1) * legalMoves[1, 0] && newY == this.y + (-1) * legalMoves[1, 1] &&
                           newX < 4 && atomicChess.panelDict[key].isFree) //daca se muta 2 pozitii in jos, daca nu iese din tabla si daca noua pozitie e libera
                                                                          //(mutarea poate avea loc doar daca nu s-a trecut de jumatatea tablei)
                        {
                            return true;
                        }

                        if (newX == this.x + (-1) * legalMoves[2, 0] && newY == this.y + (-1) * legalMoves[2, 1]
                            && atomicChess.panelDict[key].imageName.Contains("white"))//se poate muta in dreapta DOAR daca este piesa adversarului acolo
                        {
                            return true;
                        }

                        if (newX == this.x + (-1) * legalMoves[3, 0] && newY == this.y + (-1) * legalMoves[3, 1]
                            && atomicChess.panelDict[key].imageName.Contains("white")) //se poate muta in stanga DOAR daca este piesa adversarului acolo
                        {
                            return true;
                        }
                    }
                }
            }
            else
            {
                if (color == Piece.Color.black)
                {
                    if (atomicChess.panelDict[key].isFree == true || atomicChess.panelDict[key].imageName.Contains("white")) //daca panelul e liber sau daca e ocupat de adversar se poate muta
                    {
                        //MessageBox.Show(Convert.ToString(this.x) + "->" + Convert.ToString(newX) + " " +
                        //    Convert.ToString(this.y) + "->" + Convert.ToString(newY));
                        if (newX == this.x + legalMoves[0, 0] && newY == this.y + legalMoves[0, 1] &&
                       newX > 0 && atomicChess.panelDict[key].isFree) //daca se muta o pozitie in sus, daca nu iese din tabla si daca noua pozitie e libera
                        {
                            return true;
                        }

                        if (newX == this.x + legalMoves[1, 0] && newY == this.y + legalMoves[1, 1] &&
                       newX > 3 && atomicChess.panelDict[key].isFree) //daca se muta 2 pozitii in sus, daca nu iese din tabla si daca noua pozitie e libera
                                                                      //(mutarea poate avea loc doar daca nu s-a trecut de jumatatea tablei)
                        {
                            return true;
                        }

                        if (newX == this.x + legalMoves[2, 0] && newY == this.y + legalMoves[2, 1]
                            && atomicChess.panelDict[key].imageName.Contains("white"))//se poate muta in dreapta DOAR daca este piesa adversarului acolo
                        {
                            return true;
                        }

                        if (newX == this.x + legalMoves[3, 0] && newY == this.y + legalMoves[3, 1]
                            && atomicChess.panelDict[key].imageName.Contains("white")) //se poate muta in stanga DOAR daca este piesa adversarului acolo
                        {
                            return true;
                        }
                    }

                }

                if (color == Piece.Color.white)
                {
                    if (atomicChess.panelDict[key].isFree == true || atomicChess.panelDict[key].imageName.Contains("black")) //daca panelul e liber sau daca e ocupat de adversar se poate muta
                    {
                        if (newX == this.x + (-1) * legalMoves[0, 0] && newY == this.y + (-1) * legalMoves[0, 1] &&
                       newX < 7 && atomicChess.panelDict[key].isFree) //daca se muta o pozitie in jos,daca nu iese din tabla  si daca noua pozitie e libera
                        {
                            return true;
                        }


                        if (newX == this.x + (-1) * legalMoves[1, 0] && newY == this.y + (-1) * legalMoves[1, 1] &&
                           newX < 4 && atomicChess.panelDict[key].isFree) //daca se muta 2 pozitii in jos, daca nu iese din tabla si daca noua pozitie e libera
                                                                          //(mutarea poate avea loc doar daca nu s-a trecut de jumatatea tablei)
                        {
                            return true;
                        }

                        if (newX == this.x + (-1) * legalMoves[2, 0] && newY == this.y + (-1) * legalMoves[2, 1]
                            && atomicChess.panelDict[key].imageName.Contains("black"))//se poate muta in dreapta DOAR daca este piesa adversarului acolo
                        {
                            return true;
                        }

                        if (newX == this.x + (-1) * legalMoves[3, 0] && newY == this.y + (-1) * legalMoves[3, 1]
                            && atomicChess.panelDict[key].imageName.Contains("black")) //se poate muta in stanga DOAR daca este piesa adversarului acolo
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }
    }
}
