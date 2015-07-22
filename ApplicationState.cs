﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace SlickProcess
{
	public class ApplicationState : INotifyPropertyChanged
	{
		#region Fields

		public event PropertyChangedEventHandler PropertyChanged;

		private ImageSource picture;

		private string instruction;

		private string picturePath;
		
		private string stepNumber;

		private string backText;

		private string nextText;

		private string cancelText;

		private bool backEnabled;

		private bool nextEnabled;

		#endregion Fields

		#region Properties

		public string Instruction
		{
			get { return instruction; }
			set
			{
				instruction = value;
				NotifyPropertyChanged("Instruction");
			}
		}

		public string PicturePath
		{
			get { return picturePath; }
			set
			{
				picturePath = value;
				NotifyPropertyChanged("PicturePath");
				NotifyPropertyChanged("Picture"); // Update the picture too!
			}
		}

		public ImageSource Picture
		{
			get { return Convert(picturePath); }
			set
			{
				picture = value;
				NotifyPropertyChanged("Picture");
			}
		}

		public string StepNumber
		{
			get { return stepNumber; }
			set
			{
				stepNumber = value;
				NotifyPropertyChanged("StepNumber");
			}
		}

		public string BackText
		{
			get { return backText; }
			set
			{
				backText = value;
				NotifyPropertyChanged("BackText");
			}
		}

		public string NextText
		{
			get { return nextText; }
			set
			{
				nextText = value;
				NotifyPropertyChanged("NextText");
			}
		}

		public string CancelText
		{
			get { return cancelText; }
			set
			{
				cancelText = value;
				NotifyPropertyChanged("CancelText");
			}
		}

		public bool BackEnabled
		{
			get { return backEnabled; }
			set
			{
				backEnabled = value;
				NotifyPropertyChanged("BackEnabled");
			}
		}

		public bool NextEnabled
		{
			get { return nextEnabled; }
			set
			{
				nextEnabled = value;
				NotifyPropertyChanged("NextEnabled");
			}
		}

		#endregion Properties

		#region Constructors

		#endregion Constructors

		#region Public Methods

		#endregion Public Methods

		#region Private Methods

		private void NotifyPropertyChanged(string propertyName)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		private static BitmapImage Convert(string bitmapPath)
		{
			Bitmap bitmap = new Bitmap(bitmapPath);
			MemoryStream ms = new MemoryStream();
			bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);

			BitmapImage image = new BitmapImage();
			image.BeginInit();
			ms.Seek(0, SeekOrigin.Begin);
			image.StreamSource = ms;
			image.EndInit();

			return image;
		}

		#endregion Private Methods
	}
}