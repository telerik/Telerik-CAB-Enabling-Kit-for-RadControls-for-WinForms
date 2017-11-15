﻿//----------------------------------------------------------------------------------------
// patterns & practices - Smart Client Software Factory - Guidance Package
//
// This file was generated by this guidance package as part of the solution template
//
// The ShellApplication class is the entry point for your application. ShellApplication 
// contains the Main method and derives from FormShellApplication base class which is
// provided by the Composite UI Application Block (CAB).
// 
// Note that the RootWorkItem is the default WorkItem provided by CAB.
// 
// It also implements basic exception handling using Enterprise Library Exception
// Handling Application Block.
//
// The shell in this Guidance Package (ShellForm) has a DeckWorkspace called LayoutWorkspace
// The default layout is defined in a separate module called Infrastructure.Layout. This module
// has a usercontorl ShellLayoutView which has a left and right workspace.
//
// For more information see: 
// ms-help://MS.VSCC.v80/MS.VSIPCC.v80/ms.practices.scsf.2007may/SCSF/html/03-01-010-How_to_Create_Smart_Client_Solutions.htm
//
// Latest version of this Guidance Package: http://go.microsoft.com/fwlink/?LinkId=62182
//----------------------------------------------------------------------------------------

using System;
using System.Windows.Forms;
using Microsoft.Practices.CompositeUI;

using Telerik.WinControls.CompositeUI;

namespace FinanceApplicationCAB.Infrastructure.Shell
{
	/// <summary>
	/// Main application entry point class.
	/// Note that the class derives from CAB supplied base class FormShellApplication, and the 
	/// main form will be ShellForm, also created by default by this solution template
	/// </summary>
    class ShellApplication : RadFormShellApplication<WorkItem, ShellForm>
	{
		/// <summary>
		/// Application entry point.
		/// </summary>
		[STAThread]
		static void Main()
		{
#if (DEBUG)
			RunInDebugMode();
#else
			RunInReleaseMode();
#endif
		}

		private static void RunInDebugMode()
		{
			Application.SetCompatibleTextRenderingDefault(false);
			new ShellApplication().Run();
		}

		private static void RunInReleaseMode()
		{
			AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(AppDomainUnhandledException);
			Application.SetCompatibleTextRenderingDefault(false);
			try
			{
				new ShellApplication().Run();
			}
			catch (Exception ex)
			{
				HandleException(ex);
			}
		}

		private static void AppDomainUnhandledException(object sender, UnhandledExceptionEventArgs e)
		{
			HandleException(e.ExceptionObject as Exception);
		}

		private static void HandleException(Exception ex)
		{
			if (ex == null)
				return;

			MessageBox.Show("An unhandled exception occurred, and the application is terminating. For more information, see your Application event log.");
			Application.Exit();
		}

		protected override void AfterShellCreated()
		{
			base.AfterShellCreated();
		}
	}
}
