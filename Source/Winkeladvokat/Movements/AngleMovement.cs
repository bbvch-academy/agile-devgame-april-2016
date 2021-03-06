﻿namespace Winkeladvokat.Movements
{
    using Models;

    public class AngleMovement : IMovement
    {
        private readonly BoardField startField;
        private BoardField firstSelectedField;

        public AngleMovement(BoardField startField)
        {
            this.startField = startField;
        }

        public bool SelectField(BoardField field)
        {
            if (this.firstSelectedField == null)
            {
                this.firstSelectedField = field;
                this.firstSelectedField.Token = new Token(TokenType.Paragraph, this.startField.Token.Player);
                return false;
            }

            this.MoveToken(field);
            return true;
        }

        private void MoveToken(BoardField field)
        {
            field.Token = this.startField.Token;
            this.startField.Token = null;
        }
    }
}