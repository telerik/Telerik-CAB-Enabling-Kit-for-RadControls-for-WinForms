using System;
using System.Windows.Forms;
using Microsoft.Practices.ObjectBuilder;
using Telerik.WinControls.UI;

namespace FinanceApplicationCAB.Infrastructure.Layout
{
	public partial class ShellLayoutView : UserControl
	{
		private ShellLayoutViewPresenter shellPresenter;

		/// <summary>
		/// Initializes a new instance of the <see cref="T:ShellLayoutView"/> class.
		/// </summary>
		public ShellLayoutView()
		{
			InitializeComponent();
		}

        public Telerik.WinControls.CompositeUI.RadDockWorkspace DockWorkspace
		{
			get
			{
				return this.radDockWorkspace;
			}
		}

		/// <summary>
		/// Sets the presenter.
		/// </summary>
		/// <value>The presenter.</value>
		[CreateNew]
		public ShellLayoutViewPresenter Presenter
		{
			set
			{
				shellPresenter = value;
				shellPresenter.View = this;
			}
		}

		/// <summary>
		/// Gets the main menu strip.
		/// </summary>
		/// <value>The main menu strip.</value>
		public RadMenu MainMenuStrip
		{
			get { return mainMenuStrip; }
		}

		/// <summary>
		/// Gets the main status strip.
		/// </summary>
		/// <value>The main status strip.</value>
		public RadStatusStrip MainStatusStrip
		{
			get { return radStatusStrip; }
		}

		/// <summary>
		/// Close the application.
		/// </summary>
		private void OnFileExit(object sender, EventArgs e)
		{
			shellPresenter.OnFileExit();
		}

		/// <summary>
		/// Sets the status label.
		/// </summary>
		/// <param name="text">The text.</param>
		public void SetStatusLabel(string text)
		{
			statusLabel.Text = text;
		}
	}
}
