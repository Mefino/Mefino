using Mefino.Core.Web;
using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace Mefino.Core
{
    public static class SelfUpdater
    {
        // Github URLs
        private const string MEFINO_RELEASE_API_QUERY = @"https://api.github.com/repos/Mefino/Mefino/releases/latest";
        private const string MEFINO_RELEASE_URL = @"https://github.com/Mefino/Mefino/releases/latest";

        /// <summary>
        /// Check if an update to Mefino is available, if so prompt the user to close this version and view the update page.
        /// </summary>
        /// <returns><see langword="true" /> if there is an update and user wants to close and view page, otherwise <see langword="false" /></returns>
        internal static bool CheckUpdatedWanted()
        {
            var fetchedVersion = GithubHelper.GetLatestReleaseVersion(MEFINO_RELEASE_API_QUERY);

            if (fetchedVersion == null)
                return false;

            if (new Version(fetchedVersion) > new Version(MefinoApp.VERSION))
            {
                var result = MessageBox.Show(
                    $"A new version of Mefino is available for download: {fetchedVersion}" +
                    $"\n\n" +
                    $"Close this version and open the download page?",
                    "Update Available",
                    MessageBoxButtons.YesNo);

                if (result == DialogResult.Yes)
                {
                    Process.Start(MEFINO_RELEASE_URL);
                    return true;
                }
            }

            return false;
        }
    }
}
