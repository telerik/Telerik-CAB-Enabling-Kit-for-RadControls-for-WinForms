using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.CompositeUI.WinForms;
using System.Drawing;
using System.ComponentModel;

namespace Telerik.CAB.WinForms
{
	public class RadTabSmartPartInfo : TabSmartPartInfo
	{     
		/// <summary>
		/// Gets or sets the background color of the tab page's client region.
		/// </summary>
		private Color backColor;

		public Color BackColor
		{
			get { return backColor; }
			set { backColor = value; }
		}
     
		/// <summary>
		/// Gets or sets whether a tab page can be selected.
		/// </summary>
		private bool enabled;

		public bool Enabled
		{
			get { return enabled; }
			set { enabled = value; }
		}
     
		/// <summary>
		/// Gets or sets the foreground color of the tab page's client region.
		/// </summary>
		private Color foreColor;

		public Color ForeColor
		{
			get { return foreColor; }
			set { foreColor = value; }
		}
     
		/// <summary>
		/// Gets or sets the image displayed within the tab page's header.
		/// </summary>
		private Image image;

		public Image Image
		{
			get { return image; }
			set { image = value; }
		}
     
		/// <summary>
		/// Gets or sets the index of the image displayed within the tab page's header.
		/// </summary>
		[DefaultValue(-1)]
		[Category("Appearance")]
		[Description("Gets or sets the index of the image displayed within the tab page's header.")]
		private int imageIndex = -1;

		public int ImageIndex
		{
			get { return imageIndex; }
			set { imageIndex = value; }
		}
     
		/// <summary>
		/// Gets or sets whether a tab page can be selected.
		/// </summary>
		private bool pageEnabled = true;

		public bool PageEnabled
		{
			get { return pageEnabled; }
			set { pageEnabled = value; }
		}
     
		/// <summary>
		/// Gets or sets whether the tab page is visible.
		/// </summary>
		[DefaultValue(true)]
		[Description("Gets or sets whether the tab page is visible.")]
		[Category("Behavior")]
		private bool pageVisible = true;

		public bool PageVisible
		{
			get { return pageVisible; }
			set { pageVisible = value; }
		}
     
		/// <summary>
		/// Gets or sets the control's height and width.
		/// </summary>
		private Size size;

		public Size Size
		{
			get { return size; }
			set { size = value; }
		}
     
		/// <summary>
		/// Gets or sets the tab order of the control within its container.
		/// </summary>
		private int tabIndex;

		public int TabIndex
		{
			get { return tabIndex; }
			set { tabIndex = value; }
		}
     
		/// <summary>
		/// Gets or sets a value indicating whether an end user can focus this page using
		/// the TAB key.
		/// </summary>
		private bool tabStop;

		public bool TabStop
		{
			get { return tabStop; }
			set { tabStop = value; }
		}
     
		/// <summary>
		/// Gets or sets the tab page's caption.
		/// </summary>
		private string text;

		public string Text
		{
			get { return text; }
			set { text = value; }
		}
     
		/// <summary>
		/// Gets or sets a tooltip for the tab page.
		/// </summary>
		[DefaultValue("")]
		[Description("Gets or sets a tooltip for the tab page.")]
		[Category("Appearance")]
		[Localizable(true)]
		public string tooltip = String.Empty;

		public string Tooltip
		{
			get { return tooltip; }
			set { tooltip = value; }
		}
     
		/// <summary>
		/// Gets or sets whether the tab page is visible.
		/// </summary>
		[DesignerSerializationVisibility(0)]
		[Browsable(false)]
		private bool visible;

		public bool Visible
		{
			get { return visible; }
			set { visible = value; }
		}
	}
}
