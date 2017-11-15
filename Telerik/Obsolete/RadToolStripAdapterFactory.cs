using System;
using Microsoft.Practices.CompositeUI.UIElements;
using Telerik.WinControls.UI;

namespace Telerik.WinControls.CompositeUI
{
    [Obsolete("This type is obsolete.")]
    public class RadToolStripAdapterFactory : IUIElementAdapterFactory
    {
        #region IUIElementAdapterFactory Members

        public IUIElementAdapter GetAdapter(object uiElement)
        {
            if (uiElement is CommandBarRowElement)
            {
                return new RadToolStripElementAdapter((CommandBarRowElement)uiElement);
            }

            if (uiElement is CommandBarStripElement)
            {
                return new RadToolStripItemAdapter((CommandBarStripElement)uiElement);
            }

            throw new ArgumentException("uiElement");
        }

        public bool Supports(object uiElement)
        {
            return (uiElement is CommandBarRowElement) || (uiElement is CommandBarStripElement);
        }

        #endregion
    }
}
