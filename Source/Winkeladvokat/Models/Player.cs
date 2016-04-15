﻿using System.Windows;
using System.Windows.Media;
using PropertyChanged;

namespace Winkeladvokat.Models
{
    [ImplementPropertyChanged]
    public class Player
    {
        public Player(Position position, Color color)
        {
            this.Position = position;
            this.Color = color; 
        }

        public Color Color { get; private set; }

        public Brush FillBrush { get { return new SolidColorBrush(this.Color); } }

        public Position Position { get; set; }
    }
}
