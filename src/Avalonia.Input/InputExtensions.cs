using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia.VisualTree;

namespace Avalonia.Input
{
    /// <summary>
    /// Defines extensions for the <see cref="IInputElement"/> interface.
    /// </summary>
    public static class InputExtensions
    {
        private static readonly Func<IVisual, bool> s_hitTestDelegate = IsHitTestVisible;

        /// <summary>
        /// Returns the active input elements at a point on an <see cref="IInputElement"/>.
        /// </summary>
        /// <param name="element">The element to test.</param>
        /// <param name="p">The point on <paramref name="element"/>.</param>
        /// <returns>
        /// The active input elements found at the point, ordered topmost first.
        /// </returns>
        public static IEnumerable<IInputElement> GetInputElementsAt(this IInputElement element, Point p)
        {
            Contract.Requires<ArgumentNullException>(element != null);

            return element.GetVisualsAt(p, s_hitTestDelegate).Cast<IInputElement>();
        }

        /// <summary>
        /// Returns the topmost active input element at a point on an <see cref="IInputElement"/>.
        /// </summary>
        /// <param name="element">The element to test.</param>
        /// <param name="p">The point on <paramref name="element"/>.</param>
        /// <returns>The topmost <see cref="IInputElement"/> at the specified position.</returns>
        public static IInputElement InputHitTest(this IInputElement element, Point p)
        {
            Contract.Requires<ArgumentNullException>(element != null);

            return element.GetVisualAt(p, s_hitTestDelegate) as IInputElement;
        }

        private static bool IsHitTestVisible(IVisual visual)
        {
            var element = visual as IInputElement;
            return element != null &&
                   element.IsVisible &&
                   element.IsHitTestVisible &&
                   element.IsEffectivelyEnabled &&
                   element.IsAttachedToVisualTree;
        }
    }
}
