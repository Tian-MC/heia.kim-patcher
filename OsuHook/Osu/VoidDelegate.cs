using System;
using OsuHook.Osu.Stubs;

namespace OsuHook.Osu
{
    /// <summary>
    ///     Original: <c>osu_common.Helpers.VoidDelegate</c>
    /// </summary>
    internal class VoidDelegate
    {
        /// <summary>
        ///     Transforms an <see cref="Action" /> into a VoidDelegate from osu!.
        /// </summary>
        /// <param name="action">A parameter-less void function</param>
        /// <returns>A void delegate instance that wraps <paramref name="action" /></returns>
        public static object MakeInstance(Action action)
        {
            // 4th param of NotificationManager:ShowMessage is a VoidDelegate
            var voidDelegateType = NotificationManager.ShowMessage.Reference.GetParameters()[3].ParameterType;
            var voidDelegateInstance = Delegate.CreateDelegate(voidDelegateType, action, "Invoke");

            return voidDelegateInstance;
        }
    }
}