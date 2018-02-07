﻿using System;
using System.Linq;

namespace Roro.Activities.Element
{
    public sealed class Click : ProcessNodeActivity
    {
        public Input<ElementQuery> Element { get; set; }

        public override void Execute(ActivityContext context)
        {
            var query = ElementQuery.Get(this.Element);
            if (query == null)
                return;

            var elements = WinContext.Shared.GetElementsFromQuery(query);
            if (elements.Count() == 0)
                throw new Exception("Element not found.");
            if (elements.Count() > 1)
                throw new Exception("Too many elements found.");

            using (var input = new InputDriver())
            {
                var p = elements.First().Bounds.Center();
                input.MouseMove(p.X, p.Y);
                input.Click(MouseButton.Left);
            }
        }
    }
}