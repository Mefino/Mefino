using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mefino.Core
{
    /// <summary>
    /// Used to track the status of a Mefino package.
    /// </summary>
    public enum InstallState
    {
        NotInstalled,
        NotWorking,
        Outdated,
        MissingDependency,
        HasConflict,
        OptionalUpdate,
        Installed
    }
}
