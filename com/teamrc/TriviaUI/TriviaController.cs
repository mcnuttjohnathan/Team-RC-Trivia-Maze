﻿using DatabaseSystem;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriviaMaze.com.teamrc.TriviaUI {
    public partial class TriviaController : Component {
        public enum Location { TOP, BOTTOM };

        private Location location;

        public TriviaController(Point playerPosition) {
            InitializeComponent();

            this.init(playerPosition);
        }

        public TriviaController(Point playerPosition, IContainer container) {
            container.Add(this);

            InitializeComponent();

            this.init(playerPosition);
        }

        private void init(Point playerPosition) {
            if (playerPosition.Y <= 224) {
                location = Location.BOTTOM;
            }
            else
                location = Location.TOP;
        }
    }
}