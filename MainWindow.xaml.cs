﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using SlickProcess;

namespace SlickProcess
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		#region Constructors

		public MainWindow()
		{
			InitializeComponent();
			this.Title += " " + Application.ResourceAssembly.GetName().Version + 
				" © " + DateTime.UtcNow.Year + " TeamRalon";
			this.Tag = this.Title;

			// Handle passed in arguments
			string[] args = Environment.GetCommandLineArgs();
			for (int i = 0; i < args.Length; i++)
			{
				Console.WriteLine("Argument #" + (i + 1) + ":  " + args[i]);
			}

			if (args.Length > 1 && args[1] != "")
			{
				StateManager.Instance.Open(args[1]);
			}
			else
			{
				StateManager.Instance.Demo();
			}

			DataContext = StateManager.Instance.State;
		}

		#endregion Constructors

		#region Public Methods

		#endregion Public Methods

		#region Private Methods

		private void btnBack_Click(object sender, RoutedEventArgs e)
		{
			StateManager.Instance.Back();
		}

		private void btnNext_Click(object sender, RoutedEventArgs e)
		{
			StateManager.Instance.Next();
		}

		private void btnCancel_Click(object sender, RoutedEventArgs e)
		{
			if (StateManager.Instance.Close())
			{
				this.Close();
			}
		}

		private void chkEdit_Checked(object sender, RoutedEventArgs e)
		{
			StateManager.Instance.ToggleEditMode((bool)chkEdit.IsChecked);
		}

		private void chkEdit_Unchecked(object sender, RoutedEventArgs e)
		{
			StateManager.Instance.ToggleEditMode((bool)chkEdit.IsChecked);
		}
		
		private void btnDelete_Click(object sender, RoutedEventArgs e)
		{
			StateManager.Instance.DeleteCurrentStep();
		}

		private void picPicture_Drop(object sender, DragEventArgs e)
		{
			if ((bool)chkEdit.IsChecked)
			{
				if (e.Data.GetDataPresent(DataFormats.FileDrop))
				{
					// Get the paths of all files dragged in
					string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

					// Insert the first picture
					StateManager.Instance.InsertPicture(files[0]);

					// Reset the data context for the window
					DataContext = StateManager.Instance.State;
				}
			}
		}

		private void btnMoveBack_Click(object sender, RoutedEventArgs e)
		{
			StateManager.Instance.MoveCurrentStepBack();
		}

		private void btnMoveNext_Click(object sender, RoutedEventArgs e)
		{
			StateManager.Instance.MoveCurrentStepNext();
		}

		private void Window_Drop(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent(DataFormats.FileDrop))
			{
				// Get the paths of all files dragged in
				string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

				// Open the first file
				StateManager.Instance.Open(files[0]);

				// Reset the data context for the window
				DataContext = StateManager.Instance.State;
			}
		}

		#endregion Private Methods
	}
}
