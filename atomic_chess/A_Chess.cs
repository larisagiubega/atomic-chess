using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace atomic_chess
{
    public partial class atomicChess : Form
    {
        private PiecePanel panelWhite;
        private PiecePanel panelBlack;

        private string lastPieceTouched;

        private bool firstClick = true;
        private bool secondClick = false;

        static public Dictionary<string, PiecePanel> panelDict = new Dictionary<string, PiecePanel>();

        private System.Drawing.Image currentPieceImage;
        private Piece currentPiece;

        private string currentCoordinates;
        private string lastCoordinates;

        private bool foundPiece = false;

        public int chosenPlayer;

        Board board;
        public atomicChess()
        {
            InitializeComponent();
        }

        private void InitializeForWhiteTurn()
        {
            ////Initializeaza piesele albe

            panelDict["70"].BackgroundImage = Properties.Resources.w_rook_resize;
            panelDict["70"].imageName = "whiteRook";
            panelDict["71"].BackgroundImage = Properties.Resources.w_knight_resize;
            panelDict["71"].imageName = "whiteKnight";
            panelDict["72"].BackgroundImage = Properties.Resources.w_bishop_resize;
            panelDict["72"].imageName = "whiteBishop";
            panelDict["73"].BackgroundImage = Properties.Resources.w_queen_resize;
            panelDict["73"].imageName = "whiteQueen";
            panelDict["74"].BackgroundImage = Properties.Resources.w_king_resize;
            panelDict["74"].imageName = "whiteKing";
            panelDict["75"].BackgroundImage = Properties.Resources.w_bishop_resize;
            panelDict["75"].imageName = "whiteBishop";
            panelDict["76"].BackgroundImage = Properties.Resources.w_knight_resize;
            panelDict["76"].imageName = "whiteKnight";
            panelDict["77"].BackgroundImage = Properties.Resources.w_rook_resize;
            panelDict["77"].imageName = "whiteRook";


            panelDict["60"].BackgroundImage = Properties.Resources.w_pawn_resize;
            panelDict["60"].imageName = "whitePawn";
            panelDict["61"].BackgroundImage = Properties.Resources.w_pawn_resize;
            panelDict["61"].imageName = "whitePawn";
            panelDict["62"].BackgroundImage = Properties.Resources.w_pawn_resize;
            panelDict["62"].imageName = "whitePawn";
            panelDict["63"].BackgroundImage = Properties.Resources.w_pawn_resize;
            panelDict["63"].imageName = "whitePawn";
            panelDict["64"].BackgroundImage = Properties.Resources.w_pawn_resize;
            panelDict["64"].imageName = "whitePawn";
            panelDict["65"].BackgroundImage = Properties.Resources.w_pawn_resize;
            panelDict["65"].imageName = "whitePawn";
            panelDict["66"].BackgroundImage = Properties.Resources.w_pawn_resize;
            panelDict["66"].imageName = "whitePawn";
            panelDict["67"].BackgroundImage = Properties.Resources.w_pawn_resize;
            panelDict["67"].imageName = "whitePawn";


            ////------------------------------------------------------------------------------------------------------------------------------

            ////Initializeaza piesele negre
            panelDict["00"].BackgroundImage = Properties.Resources.b_rook_resize;
            panelDict["00"].imageName = "blackRook";
            panelDict["01"].BackgroundImage = Properties.Resources.b_knight_resize;
            panelDict["01"].imageName = "blackKnight";
            panelDict["02"].BackgroundImage = Properties.Resources.b_bishop_resize;
            panelDict["02"].imageName = "blackBishop";
            panelDict["03"].BackgroundImage = Properties.Resources.b_queen_resize;
            panelDict["03"].imageName = "blackQueen";
            panelDict["04"].BackgroundImage = Properties.Resources.b_king_resize;
            panelDict["04"].imageName = "blackKing";
            panelDict["05"].BackgroundImage = Properties.Resources.b_bishop_resize;
            panelDict["05"].imageName = "blackBishop";
            panelDict["06"].BackgroundImage = Properties.Resources.b_knight_resize;
            panelDict["06"].imageName = "blackKnight";
            panelDict["07"].BackgroundImage = Properties.Resources.b_rook_resize;
            panelDict["07"].imageName = "blackRook";

            panelDict["10"].BackgroundImage = Properties.Resources.b_pawn_resize;
            panelDict["10"].imageName = "blackPawn";
            panelDict["11"].BackgroundImage = Properties.Resources.b_pawn_resize;
            panelDict["11"].imageName = "blackPawn";
            panelDict["12"].BackgroundImage = Properties.Resources.b_pawn_resize;
            panelDict["12"].imageName = "blackPawn";
            panelDict["13"].BackgroundImage = Properties.Resources.b_pawn_resize;
            panelDict["13"].imageName = "blackPawn";
            panelDict["14"].BackgroundImage = Properties.Resources.b_pawn_resize;
            panelDict["14"].imageName = "blackPawn";
            panelDict["15"].BackgroundImage = Properties.Resources.b_pawn_resize;
            panelDict["15"].imageName = "blackPawn";
            panelDict["16"].BackgroundImage = Properties.Resources.b_pawn_resize;
            panelDict["16"].imageName = "blackPawn";
            panelDict["17"].BackgroundImage = Properties.Resources.b_pawn_resize;
            panelDict["17"].imageName = "blackPawn";

            SetPanelsNotFree();

        }
        private void InitializeForBlackTurn()
        {
            ////Initializeaza piesele negre

            panelDict["70"].BackgroundImage = Properties.Resources.b_rook_resize;
            panelDict["70"].imageName = "blackRook";
            panelDict["71"].BackgroundImage = Properties.Resources.b_knight_resize;
            panelDict["71"].imageName = "blackKnight";
            panelDict["72"].BackgroundImage = Properties.Resources.b_bishop_resize;
            panelDict["72"].imageName = "blackBishop";
            panelDict["73"].BackgroundImage = Properties.Resources.b_king_resize;
            panelDict["73"].imageName = "blackKing";
            panelDict["74"].BackgroundImage = Properties.Resources.b_queen_resize;
            panelDict["74"].imageName = "blackQueen";
            panelDict["75"].BackgroundImage = Properties.Resources.b_bishop_resize;
            panelDict["75"].imageName = "blackBishop";
            panelDict["76"].BackgroundImage = Properties.Resources.b_knight_resize;
            panelDict["76"].imageName = "blackKnight";
            panelDict["77"].BackgroundImage = Properties.Resources.b_rook_resize;
            panelDict["77"].imageName = "blackRook";

            panelDict["60"].BackgroundImage = Properties.Resources.b_pawn_resize;
            panelDict["60"].imageName = "blackPawn";
            panelDict["61"].BackgroundImage = Properties.Resources.b_pawn_resize;
            panelDict["61"].imageName = "blackPawn";
            panelDict["62"].BackgroundImage = Properties.Resources.b_pawn_resize;
            panelDict["62"].imageName = "blackPawn";
            panelDict["63"].BackgroundImage = Properties.Resources.b_pawn_resize;
            panelDict["63"].imageName = "blackPawn";
            panelDict["64"].BackgroundImage = Properties.Resources.b_pawn_resize;
            panelDict["64"].imageName = "blackPawn";
            panelDict["65"].BackgroundImage = Properties.Resources.b_pawn_resize;
            panelDict["65"].imageName = "blackPawn";
            panelDict["66"].BackgroundImage = Properties.Resources.b_pawn_resize;
            panelDict["66"].imageName = "blackPawn";
            panelDict["67"].BackgroundImage = Properties.Resources.b_pawn_resize;
            panelDict["67"].imageName = "blackPawn";


            ////------------------------------------------------------------------------------------------------------------------------------

            ////Initializeaza piesele albe
            panelDict["00"].BackgroundImage = Properties.Resources.w_rook_resize;
            panelDict["00"].imageName = "whiteRook";
            panelDict["01"].BackgroundImage = Properties.Resources.w_knight_resize;
            panelDict["01"].imageName = "whiteKnight";
            panelDict["02"].BackgroundImage = Properties.Resources.w_bishop_resize;
            panelDict["02"].imageName = "whiteBishop";
            panelDict["03"].BackgroundImage = Properties.Resources.w_king_resize;
            panelDict["03"].imageName = "whiteKing";
            panelDict["04"].BackgroundImage = Properties.Resources.w_queen_resize;
            panelDict["04"].imageName = "whiteQueen";
            panelDict["05"].BackgroundImage = Properties.Resources.w_bishop_resize;
            panelDict["05"].imageName = "whiteBishop";
            panelDict["06"].BackgroundImage = Properties.Resources.w_knight_resize;
            panelDict["06"].imageName = "whiteKnight";
            panelDict["07"].BackgroundImage = Properties.Resources.w_rook_resize;
            panelDict["07"].imageName = "whiteRook";

            panelDict["10"].BackgroundImage = Properties.Resources.w_pawn_resize;
            panelDict["10"].imageName = "whitePawn";
            panelDict["11"].BackgroundImage = Properties.Resources.w_pawn_resize;
            panelDict["11"].imageName = "whitePawn";
            panelDict["12"].BackgroundImage = Properties.Resources.w_pawn_resize;
            panelDict["12"].imageName = "whitePawn";
            panelDict["13"].BackgroundImage = Properties.Resources.w_pawn_resize;
            panelDict["13"].imageName = "whitePawn";
            panelDict["14"].BackgroundImage = Properties.Resources.w_pawn_resize;
            panelDict["14"].imageName = "whitePawn";
            panelDict["15"].BackgroundImage = Properties.Resources.w_pawn_resize;
            panelDict["15"].imageName = "whitePawn";
            panelDict["16"].BackgroundImage = Properties.Resources.w_pawn_resize;
            panelDict["16"].imageName = "whitePawn";
            panelDict["17"].BackgroundImage = Properties.Resources.w_pawn_resize;
            panelDict["17"].imageName = "whitePawn";

            SetPanelsNotFree();

        }

        private void SetPanelsNotFree() //panelurile pe care se afla piese albe/negre se seteaza ca ocupate INDIFERENT de randul cui este
        {
            panelDict["70"].isFree = false;
            panelDict["71"].isFree = false;
            panelDict["72"].isFree = false;
            panelDict["73"].isFree = false;
            panelDict["74"].isFree = false;
            panelDict["75"].isFree = false;
            panelDict["76"].isFree = false;
            panelDict["77"].isFree = false;

            panelDict["60"].isFree = false;
            panelDict["61"].isFree = false;
            panelDict["62"].isFree = false;
            panelDict["63"].isFree = false;
            panelDict["64"].isFree = false;
            panelDict["65"].isFree = false;
            panelDict["66"].isFree = false;
            panelDict["67"].isFree = false;

            panelDict["00"].isFree = false;
            panelDict["01"].isFree = false;
            panelDict["02"].isFree = false;
            panelDict["03"].isFree = false;
            panelDict["04"].isFree = false;
            panelDict["05"].isFree = false;
            panelDict["06"].isFree = false;
            panelDict["07"].isFree = false;

            panelDict["10"].isFree = false;
            panelDict["11"].isFree = false;
            panelDict["12"].isFree = false;
            panelDict["13"].isFree = false;
            panelDict["14"].isFree = false;
            panelDict["15"].isFree = false;
            panelDict["16"].isFree = false;
            panelDict["17"].isFree = false;
        }

        private void onFormLoad()
        {
            chooseLbl.Hide();
            choseBlackBtn.Hide();
            choseRandomBtn.Hide();
            choseWhiteBtn.Hide();

            board = new Board(chosenPlayer);//discutie dupa player ales de catre user
            Minimax.calculateScoreForColor(Piece.Color.white);
            Minimax.calculateScoreForColor(Piece.Color.black);

            Console.WriteLine("WHITE SCORE = " + Minimax.scoreForWhite);
            Console.WriteLine("BLACK SCORE = " + Minimax.scoreForBlack);

            //MessageBox.Show(Convert.ToString(chosenPlayer));
            this.ClientSize = new System.Drawing.Size(800, 800);
            FillBoardMatrix();
        }

        private void FillBoardMatrix()
        {
            int index = 100;
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (i % 2 == 0)
                    {
                        if (j % 2 == 0)
                        {
                            panelWhite = new PiecePanel(Piece.Color.white, "");
                            panelWhite.Location = new System.Drawing.Point(i * index, j * index);
                            panelWhite.Name = "white" + j + i;
                            this.Controls.Add(panelWhite);
                            panelWhite.Click += new System.EventHandler(this.Panel_Click);
                            panelWhite.movePiece = false;
                            panelWhite.BackColor = Color.White;
                            panelWhite.isFree = true;
                            panelDict.Add(string.Format("{0}{1}", j, i), panelWhite);
                        }
                        else
                        {
                            panelBlack = new PiecePanel(Piece.Color.black, "");
                            panelBlack.Location = new System.Drawing.Point(i * index, j * index);
                            panelBlack.Name = "black" + j + i;
                            this.Controls.Add(panelBlack);
                            panelBlack.Click += new System.EventHandler(this.Panel_Click);
                            panelBlack.movePiece = false;
                            panelBlack.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(98)))), ((int)(((byte)(52)))), ((int)(((byte)(18)))));
                            panelBlack.isFree = true;
                            panelDict.Add(string.Format("{0}{1}", j, i), panelBlack);
                        }
                    }
                    else
                    {

                        if (j % 2 != 0)
                        {
                            panelWhite = new PiecePanel(Piece.Color.white, "");
                            panelWhite.Location = new System.Drawing.Point(i * index, j * index);
                            panelWhite.Name = "white" + j + i;
                            this.Controls.Add(panelWhite);
                            panelWhite.Click += new System.EventHandler(this.Panel_Click);
                            panelWhite.movePiece = false;
                            panelWhite.isFree = true;
                            panelWhite.BackColor = Color.White;
                            panelDict.Add(string.Format("{0}{1}", j, i), panelWhite);
                        }
                        else
                        {
                            panelBlack = new PiecePanel(Piece.Color.black, "");
                            panelBlack.Location = new System.Drawing.Point(i * index, j * index);
                            panelBlack.Name = "black" + j + i;
                            this.Controls.Add(panelBlack);
                            panelBlack.Click += new System.EventHandler(this.Panel_Click);
                            panelBlack.movePiece = false;
                            panelBlack.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(98)))), ((int)(((byte)(52)))), ((int)(((byte)(18)))));
                            panelBlack.isFree = true;
                            panelDict.Add(string.Format("{0}{1}", j, i), panelBlack);
                        }
                    }
                }
            }

            if (Board.beginningTurn == Piece.Color.white)
                InitializeForWhiteTurn();

            if (Board.beginningTurn == Piece.Color.black)
                InitializeForBlackTurn();
        }

        private void Panel_Click(object sender, EventArgs e)
        {
            if (!Board.gameOver)
            {
                currentCoordinates = ((Control)sender).Name.Substring(5, 2);//se preiau noile coordonate
                PiecePanel panelNo = (PiecePanel)panelDict[currentCoordinates];//numarul panelului se ia din dictionarul de la coordonatele respective

                //MessageBox.Show(Convert.ToString(panelNo.imageName));

                if (firstClick && !panelNo.imageName.ToLower().Contains(Convert.ToString(Board.turn).ToLower()) && !String.IsNullOrEmpty(panelNo.imageName))
                {
                    MessageBox.Show("It's " + Convert.ToString(Board.turn) + "'s turn!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    secondClickEvent(sender, panelNo);//daca este al doilea click 
                    firstClickEvent(sender, panelNo);//daca este primul click

                    if (secondClick == false) firstClick = true;//se seteza/reseteaza primul click in functie de al doilea
                    else
                        firstClick = false;

                    lastCoordinates = currentCoordinates;//ultimele coordonate devin coordonatele curente

                    //foreach(KeyValuePair<string, PiecePanel> panel in panelDict)
                    //{
                    //    Console.WriteLine(panel.Key + "->" + panel.Value);
                    //}
                    //PrintMatrix();//test. printam matricea
                    //Console.WriteLine();
                }
            }
            else
            {
                if (Board.gameOverForBlack)
                    MessageBox.Show("White won!", "Game over", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                else
                    MessageBox.Show("Black won!", "Game over", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void firstClickEvent(object sender, PiecePanel panelNo)
        {
            currentPiece = Board.board[currentCoordinates[0] - 48, currentCoordinates[1] - 48];

            if (firstClick)//daca e primul click
            {
                if(currentPiece!=null)
                    showPossibleMoves(currentCoordinates[0] - 48, currentCoordinates[1] - 48);

                Board.findKing(Piece.Color.black).setColorInCheck(Piece.Color.black);
                Board.findKing(Piece.Color.white).setColorInCheck(Piece.Color.white);

                if (Board.isWhiteInCheck)
                {
                    MessageBox.Show("White is in check!", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                if (Board.isBlackInCheck)
                {
                    MessageBox.Show("Black is in check!", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                if (currentPiece != null)
                {
                    currentPieceImage = panelNo.BackgroundImage;//piesa curenta va fi reprezentata de imaginea de pe panel

                    panelNo.movePiece = true;//se seteaza flagul conform caruia se poate muta piesa

                    secondClick = true;//se pregateste pentru al doilea click

                    lastPieceTouched = ((Control)sender).Name;
                }
            }
        }

        private void secondClickEvent(object sender, PiecePanel panelNo)
        {
            if (secondClick)//daca e al doilea click
            {
                restoreBoardColors(lastCoordinates[0] - 48, lastCoordinates[1] - 48);

                if (!(((Control)sender).Name).Equals(lastPieceTouched) && currentPieceImage != base.BackgroundImage)//daca piesa actuala e diferita de ultima piesa atinsa
                                                                                                                    //si daca panelul are poza unei piese
                {
                    foreach (KeyValuePair<string, PiecePanel> panel in panelDict)//pentru fiecare valoare din dictionar
                    {
                        if (((PiecePanel)panel.Value).movePiece)//daca valoarea lui movePiece panelul din dictionar este true
                                                                //(daca piesa de pe un panel a fost clickuita si se poate muta)
                        {
                            foundPiece = false;

                            if (Board.board[currentCoordinates[0] - 48, currentCoordinates[1] - 48] != null)
                            {
                                foundPiece = true;
                            }

                            if (currentPiece.canMove(currentCoordinates[0] - 48, currentCoordinates[1] - 48, currentPiece.color))//daca se poate muta piesa
                            {

                                if (!King.castlingPerformed)//daca nu a avut loc o rocada ultima mutare (care a mutat automat piesele)
                                {
                                    currentPiece.x = currentCoordinates[0] - 48;
                                    currentPiece.y = currentCoordinates[1] - 48;

                                    //mutam piesa la nivel de dictionar; fosta locatie devine free, iar actuala devine ocupata
                                    panelDict[lastCoordinates].isFree = true;
                                    panelDict[currentCoordinates].isFree = false;

                                    panelNo.BackgroundImage = currentPieceImage;//culoarea panelului devine piesa curenta

                                    ((PiecePanel)panel.Value).BackgroundImage = base.BackgroundImage;//se reseteaza imaginea
                                    ((PiecePanel)panel.Value).movePiece = false;//se reseteaza capacitatea piesei de a se muta

                                    //MessageBox.Show("Poate muta");

                                    Board.board[currentCoordinates[0] - 48, currentCoordinates[1] - 48] = Board.board[lastCoordinates[0] - 48, lastCoordinates[1] - 48];
                                    Board.board[lastCoordinates[0] - 48, lastCoordinates[1] - 48] = null;

                                    panelNo.imageName = panel.Value.imageName;
                                    panel.Value.imageName = "";
                                }

                                if (currentPiece.name.ToLower().Contains("king") && King.castlingPerformed)//daca piesa curenta este rege si 
                                                                                                           //a avut loc o rocada
                                {
                                    //Board.PrintBoard();
                                    King.castlingPerformed = false; //se reseteaza flagul de rocada
                                }
                                else
                                if (foundPiece)
                                {
                                    currentPiece.explodePieces(currentCoordinates[0] - 48, currentCoordinates[1] - 48);
                                }

                                if (!Board.checkIfGameOver())
                                {
                                    //if (currentPiece.canBeCaptured(currentPiece.x, currentPiece.y, currentPiece.color))
                                    //{
                                    //    MessageBox.Show("POATE FI CAPTURATA");
                                    //}

                                    if (Board.turn == Piece.Color.white)
                                    {
                                        Board.turn = Piece.Color.black;
                                    }
                                    else
                                    {
                                        Board.turn = Piece.Color.white;
                                    }
                                }
                            }
                            else
                            {
                                panelDict.FirstOrDefault(k => k.Value.movePiece && k.Value != panelNo).Value.movePiece = false;//se reseteaza prima/singura mutare a piesei
                                MessageBox.Show("You cannot move there", ":(", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                            }
                            break;
                        }
                    }
                }
                else
                {
                    panelNo.movePiece = false;
                }

                secondClick = false;//se reseteaza al doilea click
            }
        }
        private void showPossibleMoves(int x, int y) //schimbam culoarea de fundal a panelurilor pentru locurile unde poate muta piesa curenta
        {
            List<string> possibleMoves = Minimax.returnPossibleMovesForPiece(x, y);

            if (possibleMoves.Count != 0)
            {
                foreach (string possibleMove in possibleMoves)
                {
                    Color panelColor = panelDict[possibleMove].BackColor;

                    if (panelColor == Color.White)
                    {
                        panelDict[possibleMove].BackColor = Color.LightGray;
                    }
                    else
                    {
                        panelDict[possibleMove].BackColor = Color.Gray;
                    }
                }
            }
        }

        private void restoreBoardColors(int x, int y) //restauram culorile dupa mutare
        {
            List<string> possibleMoves = Minimax.returnPossibleMovesForPiece(x, y);

            if (possibleMoves.Count != 0)
            {
                foreach (string possibleMove in possibleMoves)
                {
                    Color panelColor = panelDict[possibleMove].BackColor;

                    if (panelColor == Color.LightGray)
                    {
                        panelDict[possibleMove].BackColor = Color.White;
                    }
                    else
                    {
                        panelDict[possibleMove].BackColor = Color.FromArgb(((int)(((byte)(98)))), ((int)(((byte)(52)))), ((int)(((byte)(18)))));
                    }
                }
            }
        }
        private void choseRandomBtn_Click(object sender, EventArgs e)
        {
            chosenPlayer = 2;
            onFormLoad();
        }

        private void choseWhiteBtn_Click(object sender, EventArgs e)
        {
            chosenPlayer = (int)Piece.Color.white;
            onFormLoad();
        }

        private void choseBlackBtn_Click(object sender, EventArgs e)
        {
            chosenPlayer = (int)Piece.Color.black;
            onFormLoad();
        }
    }
}
