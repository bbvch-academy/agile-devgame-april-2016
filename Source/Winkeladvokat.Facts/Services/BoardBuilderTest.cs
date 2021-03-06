﻿namespace Winkeladvokat.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows.Media;
    using FluentAssertions;
    using Models;
    using Xunit;

    public class BoardBuilderTest
    {
        [Fact]
        public void CreateFields_ThenReturnsFieldsWithValues()
        {
            BoardBuilder testee = new BoardBuilder();
            var expectedValues = testee.BoardFieldValues;
            var fourPlayers = this.CreateFourPlayers();

            var board = testee.CreateBoard(fourPlayers);
            var result = this.To2DArray(board.Fields);

            result.Should().BeEquivalentTo(expectedValues);
        }

        [Theory]
        [InlineData(0, 0, 0)]
        [InlineData(1, 7, 0)]
        [InlineData(2, 7, 7)]
        [InlineData(3, 0, 7)]
        public void CreateFields_ThenColorizePlayerFields(int playerIndex, int row, int column)
        {
            BoardBuilder testee = new BoardBuilder();
            Player[] players = this.CreateFourPlayers();
            Color expectedColor = players[playerIndex].Color;

            var board = testee.CreateBoard(players);
            var result = board.Fields[row][column].FieldColor.ToString();

            result.Should().Be(expectedColor.ToString());
        }

        [Theory]
        [InlineData(0, 0, 0)]
        [InlineData(1, 7, 0)]
        [InlineData(2, 7, 7)]
        [InlineData(3, 0, 7)]
        public void CreateFields_ThenAddAdvocateTokensForEveryPlayer(int playerIndex, int row, int column)
        {
            BoardBuilder testee = new BoardBuilder();
            Player[] players = this.CreateFourPlayers();
            Player expectedPlayer = players[playerIndex];

            var board = testee.CreateBoard(players);
            var result = board.Fields[row][column].Token;

            result.Type.Should().Be(TokenType.Advocate);
            result.Player.Should().Be(expectedPlayer);
        }

        public int[,] To2DArray(List<List<BoardField>> source)
        {
            int max = source.Select(l => l).Max(l => l.Count);
            var result = new int[source.Count, max];

            foreach (var field in source.SelectMany(field => field))
            {
                result[field.Position.X, field.Position.Y] = field.Value;
            }

            return result;
        }

        private Player[] CreateFourPlayers()
        {
            return new[]
                       {
                            new Player(Colors.Cyan),
                            new Player(Colors.Blue),
                            new Player(Colors.Yellow),
                            new Player(Colors.Magenta)
                       };
        }
    }
}