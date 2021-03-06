﻿namespace Winkeladvokat
{
    using FluentAssertions;
    using Models;
    using ViewModels;
    using Xbehave;

    public class ParagraphMove
    {
        [Scenario]
        public void RegularMove(BoardViewModel boardViewModel)
        {
            "establish board"._(() =>
            {
                boardViewModel = new BoardViewModel();
            });

            "establish two paragraph tokens of different players"._(() =>
            {
                boardViewModel.Fields[1][1].Token = new Token(TokenType.Paragraph, boardViewModel.Players[0]);
                boardViewModel.Fields[1][2].Token = new Token(TokenType.Paragraph, boardViewModel.Players[1]);
            });

            "when first player does a paragraph move"._(() =>
            {
                boardViewModel.SelectField.Execute(boardViewModel.Fields[1][1]);
                boardViewModel.SelectField.Execute(boardViewModel.Fields[1][3]);
            });

            "it should have moved the paragraph token to the destination field"._(() =>
            {
                boardViewModel.Fields[1][3].Token.Type.Should().Be(TokenType.Paragraph);
                boardViewModel.Fields[1][3].Token.Player.Should().Be(boardViewModel.Players[0]);
            });

            "it should clear token on paragraph token's previous field"._(() =>
            {
                boardViewModel.Fields[1][1].Token.Should().BeNull();
            });

            "it should have removed the paragraph token of the second player"._(() =>
            {
                boardViewModel.Fields[1][2].Token.Should().BeNull();
            });
        }
    }
}