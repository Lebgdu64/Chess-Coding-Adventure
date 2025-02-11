﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using ChessLogic;

namespace ChessInterface
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly Image[,] pieceImages = new Image[8, 8];
        private readonly Rectangle[,] highlights = new Rectangle[8, 8];
        private readonly Dictionary<Position, Move> moveCache = new Dictionary<Position, Move>();
        

        private ChessLogic.GameState gameState;
        private Position selectedPos = null;
        private EvolutionarySystem evo;
        private FenSave fenSave = new FenSave();
        public MainWindow()
        {
            InitializeComponent();
            InitializeBoard();
            evo = new(20, 5);
            HumanVSHuman();
        }


        private void InitializeBoard()
        {
            for (int r = 0; r < 8; r++)
            {
                for(int c = 0; c < 8; c++)
                {
                    Image image = new Image();
                    pieceImages[r, c] = image;
                    PieceGrid.Children.Add(image);

                    Rectangle highlight = new Rectangle();
                    highlights[r, c] = highlight;
                    BoardGrid.Children.OfType<UniformGrid>().FirstOrDefault(ug => ug.Name == "HighlightGrid").Children.Add(highlight);
                }
            }
        }

        private void DrawBoard(Board board)
        {
            for (int r = 0; r < 8; r++)
            {
                for (int c = 0; c < 8; c++)
                {
                    Piece piece = board[r, c];
                    if (piece != null)
                    {
                        pieceImages[r, c].Source = Images.GetImage(piece);
                    }
                    else
                    {
                        pieceImages[r, c].Source = null;
                    }
                }
            }
        }
        private void CachMoves(IEnumerable<Move> moves)
        {
            moveCache.Clear();

            foreach (Move move in moves)
            {
                moveCache[move.ToPos] = move;
            }
        }

        private void ShowHighlights()
        {
            Color color = Color.FromArgb(150, 137, 207, 240);

            foreach (Position to in moveCache.Keys)
            {
                highlights[to.Row, to.Column].Fill = new SolidColorBrush(color);
            }
        }

        private void HideHighlights()
        {
            foreach (Position to in moveCache.Keys)
            {
                highlights[to.Row, to.Column].Fill = Brushes.Transparent;
            }
        }
        private Position ToSquarePosition(Point point)
        {
            double squareSize = BoardGrid.ActualWidth / 8;
            int row = (int)(point.Y / squareSize);
            int col = (int)(point.X / squareSize);
            return new Position(row, col);
        }

        private void OnFromPositionSelected(Position pos)
        {
            IEnumerable<Move> moves = gameState.LegalMoveForPiece(pos);

            if (moves.Any())
            {
                selectedPos = pos;
                CachMoves(moves);
                ShowHighlights();
            }
        }
        private void OnToPositionSelected(Position pos)
        {
            selectedPos = null;
            HideHighlights();

            if (moveCache.TryGetValue(pos, out Move move))
            {
                if (move.Type == MoveType.PawnPromotion)
                {
                    HandlePromotion(move.FromPos, move.ToPos);
                }
                else
                {
                    HandleMove(move);
                }

            }
        }

        private void HandlePromotion(Position from, Position to)
        {
            pieceImages[to.Row, to.Column].Source = Images.GetImage(gameState.CurrentPlayer, PieceType.Pawn);
            pieceImages[from.Row, from.Column].Source = null;

            PromotionMenu proMenu = new PromotionMenu(gameState.CurrentPlayer);
            MenuContainer.Content = proMenu;

            proMenu.PieceSelected += type =>
            {
                MenuContainer.Content = null;
                Move promMove = new PawnPromotion(from, to, type);
                HandleMove(promMove);

            };
        }
        private void HandleMove(Move move)
        {
            gameState.MakeMove(move);
            DrawBoard(gameState.Board);

            if (gameState.IsGameOver())
            {
                Restart();
            }
        }



        public void BoardGrid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Point point = e.GetPosition(BoardGrid);
            Position pos = ToSquarePosition(point);

            if (selectedPos == null)
            {
                OnFromPositionSelected(pos);
            }
            else
            {
                OnToPositionSelected(pos);
            }
        }

        public bool Restart()
        {
            HideHighlights();
            moveCache.Clear();
            MenuContainer.Content = null;
            // try getting new contenders
            Individual[] i = evo.GetContenders();
            if (i == null)
            {
                if (evo.EndGeneration())
                {
                    
                    HumanVSHuman();
                    return false;
                }
                else
                {
                    BotVsBot();
                    return false;
                }
            }

            gameState = new ChessLogic.GameState(Player.White, Board.Initial(), i[0], i[1]);
            DrawBoard(gameState.Board);
            return true;
        }
        public void RestartHuman()
        {
            HideHighlights();
            moveCache.Clear();
            gameState = new ChessLogic.GameState(Player.White, Board.Initial());
            DrawBoard(gameState.Board);

        }


        private bool IsMenuOnScreen()
        {
            return MenuContainer.Content != null;
        }


        private void o(object sender, KeyEventArgs e)
        {
            GameModeSelectScreen gameModeMenu = new GameModeSelectScreen(this);
            MenuContainer.Content = gameModeMenu;
        }

        public void HumanVSHuman()
        {
            RestartHuman();
            MenuContainer.Content = null;
        }
        

        public void BotVsBot()
        {
            if (!Restart()) return;

            var isWhitePlaying = true;

            while (!gameState.IsGameOver())
            {
                gameState.MakeBotMove(isWhitePlaying ? Player.White : Player.Black);
                DrawBoard(gameState.Board);
                isWhitePlaying = !isWhitePlaying;
            }

            fenSave.AddFen(gameState.stateString);
            gameState.AssignFitness();

            BotVsBot();
        }

        public void HumanVsBot()
        {

        }
    }

    [Serializable]
    public class FenSave
    {
        public List<string> Fens { get; set; } = new();

        public FenSave()
        {
            try
            {
                Fens.AddRange(LoadFromFile<List<string>>("D:/Chess/ChessBot/ChessLogic/Json/Fen.json"));
            }
            catch
            {
                Trace.WriteLine("Cannot load fen from file");
                // ignored
            }
        }

        public void AddFen(string fen)
        {
            Fens.Add(fen);
            Trace.WriteLine(fen);
            SaveToFile();
        }

        private void SaveToFile()
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };
            var json = JsonSerializer.Serialize(this, options);
            File.WriteAllText("D:/Chess/ChessBot/ChessLogic/Json/Fen.json", json);
        }

        private T LoadFromFile<T>(string path)
        {
            var json = File.ReadAllText(path);
            return JsonSerializer.Deserialize<T>(json);
        }
    }
}