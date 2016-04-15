﻿using System.Windows.Media;
using Winkeladvokat.Models;

namespace Winkeladvokat.Movements
{
    using FluentAssertions;
    using Xunit;

    public class AngleMovementTest
    {
        [Fact]
        public void SelectField_WhenFirstFieldHasBeenSelected_ThenMovementIsNotFinished()
        {
            BoardField startField = BoardField.Empty;
            Player player = new Player(Colors.Red);
            startField.Token = new AdvocateToken(player);
            BoardField selectedField = BoardField.Empty;
            AngleMovement testee = new AngleMovement(startField);

            var result = testee.SelectField(selectedField);

            result.Should().BeFalse();
        }

        [Fact]
        public void SelectField_WhenFirstFieldHasBeenSelected_ThenDoNotMoveToken()
        {
            AdvocateToken token = new AdvocateToken();
            BoardField startField = BoardField.Empty;
            startField.Token = token;

            BoardField selectedField = BoardField.Empty;
            AngleMovement testee = new AngleMovement(startField);

            testee.SelectField(selectedField);

            startField.Token.Should().Be(token);
            selectedField.Token.Should().BeOfType<ParagraphToken>();
        }

        [Fact]
        public void SelectField_WhenFirstFieldHasBeenSelected_ThenParagraphTokenShoulBePlaced()
        {
            AdvocateToken token = new AdvocateToken();
            BoardField startField = BoardField.Empty;
            startField.Token = token;

            BoardField selectedField = BoardField.Empty;
            AngleMovement testee = new AngleMovement(startField);

            testee.SelectField(selectedField);

            selectedField.Token.Should().BeOfType<ParagraphToken>();
        }

        [Fact]
        public void SelectField_WhenFirstFieldHasBeenSelected_ThenParagraphTokenShoulBeFromCorrectPlayer()
        {
            Player player = new Player(Colors.Red);
            AdvocateToken token = new AdvocateToken(player);
            BoardField startField = BoardField.Empty;
            startField.Token = token;

            BoardField selectedField = BoardField.Empty;
            AngleMovement testee = new AngleMovement(startField);

            testee.SelectField(selectedField);

            selectedField.Token.Player.Should().Be(player);
        }

        [Fact]
        public void SelectField_WhenSecondFieldHasBeenSelected_ThenMoveTokenToSecondField()
        {
            AdvocateToken token = new AdvocateToken();
            BoardField startField = BoardField.Empty;
            startField.Token = token;

            BoardField firstSelectedField = BoardField.Empty;
            BoardField secondSelectedField = BoardField.Empty;
            AngleMovement testee = new AngleMovement(startField);

            testee.SelectField(firstSelectedField);
            testee.SelectField(secondSelectedField);

            startField.Token.Should().BeNull("start field should not have a token after angle move");
            firstSelectedField.Token.Should().BeOfType<ParagraphToken>();
            secondSelectedField.Token.Should().Be(token);
        }

        [Fact]
        public void SelectField_WhenSecondFieldHasBeenSelected_ThenMovementIsFinished()
        {
            BoardField startField = BoardField.Empty;
            Player player = new Player(Colors.Red);
            startField.Token = new AdvocateToken(player);
            BoardField firstSelectedField = BoardField.Empty;
            BoardField secondSelectedField = BoardField.Empty;
            AngleMovement testee = new AngleMovement(startField);

            testee.SelectField(firstSelectedField);
            var result = testee.SelectField(secondSelectedField);

            result.Should().BeTrue();
        }
    }
}