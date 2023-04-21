//-----------------------------------------------------------------------
// File name: ScriptPostProcessor.cs
// Author: Dayron Suárez del Toro
// Email: dsuarezt92@gmail.com
// Created on: April 21, 2023
//-----------------------------------------------------------------------

using System;
using System.Globalization;
using System.IO;
using UnityEditor;

namespace LitLab.CyberTitans.Editor
{
    public class ScriptPostProcessor : AssetPostprocessor
    {
        #region Methods
        void OnPreprocessAsset()
        {
            string assetName = assetPath;

            if (assetName.EndsWith(".cs") && !assetName.Contains(nameof(ScriptPostProcessor)))
            {
                StreamReader reader = new StreamReader(assetName);
                string file = reader.ReadToEnd();
                reader.Close();

                if (!file.Contains("#NEW#")) return;

                StreamWriter writer = new StreamWriter(assetName, false);
                file = file.Replace("#NEW#", string.Empty);
                file = file.Replace("#DATE#", DateTime.Today.ToString("MMMM dd, yyyy", new CultureInfo("en-US")));
                file = file.Replace("#AUTHOR#", "Dayron Suárez del Toro");
                file = file.Replace("#EMAIL#", "dsuarezt92@gmail.com");
                file = file.Replace("#NAMESPACE#", "LitLab.CyberTitans");

                writer.Write(file);
                writer.Flush();
                writer.Close();
            }
        }

        #endregion
    }
}
