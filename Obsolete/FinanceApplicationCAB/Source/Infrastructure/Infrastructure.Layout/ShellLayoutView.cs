using System;
using System.Windows.Forms;
using Microsoft.Practices.ObjectBuilder;
using FinanceApplicationCAB.Infrastructure.Interface.Constants;
using Telerik.WinControls.UI;
using Telerik.WinControls;
using Telerik.WinControls.Themes;
using Telerik.WinControls.UI.Docking;

namespace FinanceApplicationCAB.Infrastructure.Layout
{
	public partial class ShellLayoutView : UserControl
	{
		private ShellLayoutViewPresenter _presenter;

		/// <summary>
		/// Initializes a new instance of the <see cref="T:ShellLayoutView"/> class.
		/// </summary>
		public ShellLayoutView()
		{
			InitializeComponent();
			//_leftWorkspace.Name = WorkspaceNames.LeftWorkspace;
			//_rightWorkspace.Name = WorkspaceNames.RightWorkspace;

			DesertTheme desertTheme = new DesertTheme();

			RadThemeManager themeManager = new RadThemeManager();
			ThemeSource dockPresenterThemeSource = new ThemeSource();
			dockPresenterThemeSource.StorageType = ThemeStorageType.Resource;
			dockPresenterThemeSource.ThemeLocation = "FinanceApplicationCAB.Infrastructure.Layout.Resources.FinanceApplication_DockPresenter.xml";
			themeManager.LoadedThemes.Add(dockPresenterThemeSource);

			this.dockingManager.ThemeName = "Desert"; //"FinanceApplication";
			this._mainMenuStrip.ThemeName = "Desert"; // "Office2007Silver";
		}
		
		internal RadDock DockingManager
		{
			get
			{
				return this.dockingManager;
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
				_presenter = value;
				_presenter.View = this;
			}
		}

		/// <summary>
		/// Gets the main menu strip.
		/// </summary>
		/// <value>The main menu strip.</value>
		internal RadMenu MainMenuStrip
		{
			get { return _mainMenuStrip; }
		}

		/// <summary>
		/// Gets the main status strip.
		/// </summary>
		/// <value>The main status strip.</value>
		internal StatusStrip MainStatusStrip
		{
			get { return _mainStatusStrip; }
		}

		/// <summary>
		/// Close the application.
		/// </summary>
		private void OnFileExit(object sender, EventArgs e)
		{
			_presenter.OnFileExit();
		}

		/// <summary>
		/// Sets the status label.
		/// </summary>
		/// <param name="text">The text.</param>
		public void SetStatusLabel(string text)
		{
			_statusLabel.Text = text;
		}

	}
}
