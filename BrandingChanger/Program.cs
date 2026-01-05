using System;
using System.IO;
using System.Diagnostics;
using System.Windows.Forms;

namespace BrandingChanger
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                ShowHelp();
                return;
            }

            string input = args[0].ToLower().Replace("/", "").Replace("branding:", "");

            string edition = "";
            switch (input)
            {
                case "ultimate": edition = "Ultimate"; break;
                case "homepremium": edition = "HomePremium"; break;
                case "professional": edition = "Professional"; break;
                case "enterprise": edition = "Enterprise"; break;
                default:
                    return;
            }

            ApplyBranding(edition);
        }

        static void ApplyBranding(string edition)
        {
            string sourcePath = $@"C:\Relive\SetupFiles\Branding\{edition}";
            string sourceLicense = Path.Combine(sourcePath, "license.rtf");

            string targetPath = @"C:\Windows\Branding";
            string targetLicense = @"C:\Windows\System32\license.rtf";

            try
            {
                if (!Directory.Exists(sourcePath)) return;
                ExecuteCommand($"takeown /f \"{targetPath}\" /r /d y");
                ExecuteCommand($"icacls \"{targetPath}\" /grant *S-1-5-32-544:F /t /q");
                CopyFolder(sourcePath, targetPath);

                if (File.Exists(sourceLicense))
                {
                    ExecuteCommand($"takeown /f \"{targetLicense}\" /a");
                    ExecuteCommand($"icacls \"{targetLicense}\" /grant *S-1-5-32-544:F /q");
                    try
                    {
                        if (File.Exists(targetLicense))
                        {
                            File.SetAttributes(targetLicense, FileAttributes.Normal);
                        }
                        File.Copy(sourceLicense, targetLicense, true);
                    }
                    catch
                    {
                        ExecuteCommand($"del /f /q \"{targetLicense}\"");
                        File.Copy(sourceLicense, targetLicense, true);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Błąd krytyczny podczas nakładania brandingu:\n" + ex.Message, "Relive7 Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        static void ExecuteCommand(string command)
        {
            ProcessStartInfo psi = new ProcessStartInfo("cmd.exe", "/c " + command)
            {
                CreateNoWindow = true,
                UseShellExecute = false,
                WindowStyle = ProcessWindowStyle.Hidden
            };

            using (Process p = Process.Start(psi))
            {
                p?.WaitForExit();
            }
        }

        static void CopyFolder(string source, string target)
        {
            foreach (string dir in Directory.GetDirectories(source, "*", SearchOption.AllDirectories))
            {
                Directory.CreateDirectory(dir.Replace(source, target));
            }

            foreach (string file in Directory.GetFiles(source, "*.*", SearchOption.AllDirectories))
            {
                string fileName = Path.GetFileName(file);
                if (fileName.ToLower() == "license.rtf") continue;

                string destFile = file.Replace(source, target);

                if (File.Exists(destFile))
                {
                    File.SetAttributes(destFile, FileAttributes.Normal);
                }

                File.Copy(file, destFile, true);
            }
        }

        static void ShowHelp()
        {
            MessageBox.Show(
                "Simple Branding Changer\n\n" +
                "Dostępne argumenty:\n" +
                "/ultimate, /homepremium, /professional, /enterprise",
                "A simple Branding Changer");
        }
    }
}