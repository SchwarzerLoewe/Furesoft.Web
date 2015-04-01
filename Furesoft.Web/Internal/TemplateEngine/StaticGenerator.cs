﻿using System;
using System.IO;
using Furesoft.Web.Internal.TemplateEngine;

namespace Furesoft.Web.Internal.TemplateEngine
{
    /// <summary>
    /// Generates a static block of text.
    /// </summary>
    internal sealed class StaticGenerator : IGenerator
    {
        private readonly string value;

        /// <summary>
        /// Initializes a new instance of a StaticGenerator.
        /// </summary>
        public StaticGenerator(string value, bool removeNewLines)
        {
            if (removeNewLines)
            {
                this.value = value.Replace(Environment.NewLine, String.Empty);
            }
            else
            {
                this.value = value;
            }
        }

        /// <summary>
        /// Gets or sets the static text.
        /// </summary>
        public string Value
        {
            get { return value; }
        }

        void IGenerator.GetText(Scope scope, TextWriter writer, Scope context)
        {
            writer.Write(Value);
        }
    }
}
