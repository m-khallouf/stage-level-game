// <copyright file="Skin.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Project1.Entities
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.Xna.Framework.Graphics;

    /// <summary>
    /// Represents a skin that can be applied to a player.
    /// </summary>
    public class Skin
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Skin"/> class.
        /// </summary>
        /// <param name="name">Name of the skin.</param>
        /// <param name="icon">Image of the skin.</param>
        public Skin(string name, Texture2D icon)
        {
            {
                this.Name = name;
                this.Icon = icon;

                // Price = price;
                // TexturePath = texturePath;
            }
        }

        /// <summary>
        /// Gets or sets the name of the skin.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the icon of the skin.
        /// </summary>
        public Texture2D Icon { get; set; }
    }
}
