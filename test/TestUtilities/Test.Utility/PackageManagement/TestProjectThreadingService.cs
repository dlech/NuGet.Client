using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Threading;
using NuGet.PackageManagement.VisualStudio;

namespace Test.Utility
{
    public class TestProjectThreadingService : IVsProjectThreadingService
    {
        public TestProjectThreadingService(JoinableTaskFactory jtf)
        {
            JoinableTaskFactory = jtf;
        }

        public JoinableTaskFactory JoinableTaskFactory { get; }

        public void ExecuteSynchronously(Func<System.Threading.Tasks.Task> asyncAction)
        {
            JoinableTaskFactory.Run(asyncAction);
        }

        public T ExecuteSynchronously<T>(Func<Task<T>> asyncAction)
        {
            return JoinableTaskFactory.Run(asyncAction);
        }

        public void ThrowIfNotOnUIThread(string callerMemberName)
        {
            ThreadHelper.ThrowIfNotOnUIThread(callerMemberName);
        }
    }
}
