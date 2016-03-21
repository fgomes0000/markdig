﻿// Copyright (c) Alexandre Mutel. All rights reserved.
// This file is licensed under the BSD-Clause 2 license. 
// See the license.txt file in the project root for more information.

using System;

namespace Textamina.Markdig.Extensions
{
    /// <summary>
    /// Options for enabling support for extra emphasis.
    /// </summary>
    [Flags]
    public enum EmphasisExtraOptions
    {
        /// <summary>
        /// Allows all extra emphasis (default).
        /// </summary>
        Default = Strikeout | Subscript | Superscript | Inserted | Marked,

        /// <summary>
        /// A text that can be strikeout using the double character ~~
        /// </summary>
        Strikeout = 1,

        /// <summary>
        /// A text that can be rendered as a subscript using the character ~
        /// </summary>
        Subscript = 2,

        /// <summary>
        /// A text that can be rendered as a superscript using the character ^
        /// </summary>
        Superscript = 4,

        /// <summary>
        /// A text that can be rendered as a inserted using the character ++
        /// </summary>
        Inserted = 8,

        /// <summary>
        /// A text that can be rendered as a inserted using the character ==
        /// </summary>
        Marked = 16,
    }
}