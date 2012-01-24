﻿/*
 * Greenshot - a free and open source screenshot tool
 * Copyright (C) 2007-2011  Thomas Braun, Jens Klingen, Robin Krom
 * 
 * For more information see: http://getgreenshot.org/
 * The Greenshot project is hosted on Sourceforge: http://sourceforge.net/projects/greenshot/
 * 
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 1 of the License, or
 * (at your option) any later version.
 * 
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with this program.  If not, see <http://www.gnu.org/licenses/>.
 */
using System;
using System.Windows.Forms;
using GreenshotPlugin.Core;

namespace GreenshotOCR {
	/// <summary>
	/// Description of SettingsForm.
	/// </summary>
	public partial class SettingsForm : Form {
		private ILanguage language = Language.GetInstance();
		private OCRConfiguration config;
		        
		public SettingsForm(string [] languages, OCRConfiguration config) {
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			this.config = config;
			InitializeComponent();
			language.SynchronizeLanguageToCulture();
			initializeComponentText();
			
			comboBox_languages.Items.Clear();
			int index=0;

			// Preventing Tracker #3234560, although this should not happen...
			string languageFromConfig = "ENGLISH";
			if (config.Language != null) {
				languageFromConfig  = config.Language;
			}
			foreach(string availableLanguage in languages) {
				string displayLanguage = availableLanguage.Substring(0, 1).ToUpper() + availableLanguage.Substring(1).ToLower();
				comboBox_languages.Items.Add(displayLanguage);
				if (availableLanguage.Equals(languageFromConfig, StringComparison.CurrentCultureIgnoreCase)) {
					comboBox_languages.SelectedIndex = index;
				}
				index++;
			}
			checkBox_orientImage.Checked = config.Orientimage;
			checkBox_straightenImage.Checked = config.StraightenImage;
			ocr_hotkeyControl.SetHotkey(config.HotKey);
		}

		private void initializeComponentText() {
			this.label_language.Text = language.GetString(LangKey.language);
			this.checkBox_orientImage.Text = language.GetString(LangKey.orient_image);
		}
		
		void ButtonCancelClick(object sender, EventArgs e) {
			DialogResult = DialogResult.Cancel;
		}
		
		void ButtonOKClick(object sender, EventArgs e) {
			string selectedString = (string) comboBox_languages.SelectedItem;
			if (selectedString != null) {
				config.Language = selectedString.ToUpper();
			}
			config.Orientimage = checkBox_orientImage.Checked;
			config.StraightenImage = checkBox_straightenImage.Checked;

			DialogResult = DialogResult.OK;
		}
	}
}