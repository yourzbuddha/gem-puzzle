using System;
using System.Collections.Generic;
using FifteenPuzzle.Game.Models;
using Spectre.Console;

namespace FifteenPuzzle.Ui
{
    public class ConsoleRenderer : IUiRenderer
    {
        public void Render(GameData data)
        {
            Console.Clear();

            var table = new Table()
                .Caption("Let's WIX it!")
                .Border(TableBorder.None)
                .HideHeaders()
                .Centered();

            // Init columns
            for (var i = 0; i < data.Board.GetLongLength(0); i++)
            {
                table.AddColumn(new TableColumn("").Alignment(Justify.Center).NoWrap().Width(6));
            }

            // Init rows
            for (var i = 0; i < 4; i++)
            {
                var items = new List<string>();
                for (var j = 0; j < 4; j++)
                {
                    items.Add(data.Board[i, j].ToString());
                }

                var index = items.FindIndex(_ => _ == "0");
                if (index != -1)
                    items[index] = "_";

                table.AddRow(items.ToArray());
            }

            table.AddRow($"Moves: {data.Moves}");

            AnsiConsole.Render(table);
        }
    }
}