using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace Portfolio.Website.Extensions
{
    public static class JsRuntimeExtension
    {
        /// <summary>
        /// Injects the specified js object reference as a Task.
        /// </summary>
        /// <param name="jSRuntime"></param>
        /// <param name="identifier">An identifier of the function to invoke.</param>
        /// <param name="args">Arguments provided to the identifier if any.</param>
        /// <returns><see cref="IJSObjectReference"/> ValueTask as a <see cref="Task"/> see <see cref="ValueTask.AsTask"/>.</returns>
        public static Task<IJSObjectReference> InjectJsObjectReference(this IJSRuntime jSRuntime, string identifier, params object[] args)
        {
            return jSRuntime.InvokeAsync<IJSObjectReference>(identifier, args).AsTask();
        }
    }
}
