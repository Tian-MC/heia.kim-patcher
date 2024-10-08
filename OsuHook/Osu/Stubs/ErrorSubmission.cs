using OsuHook.OpcodeUtil;
using static System.Reflection.Emit.OpCodes;

namespace OsuHook.Osu.Stubs
{
    /// <summary>
    ///     Original: <c>osu.Helpers.ErrorSubmission</c>
    ///     v20230326: <c>#=zhC91LB1xsJMwYkF0UQ==</c>
    /// </summary>
    internal static class ErrorSubmission
    {
        /// <summary>
        ///     Original: osu.Helpers.ErrorSubmission:Submit(OsuError)
        ///     v20230326: #=zhC91LB1xsJMwYkF0UQ==:#=zPqLxZPA=
        /// </summary>
        public static readonly LazySignature Submit = new LazySignature(
            "ErrorSubmission#Submit",
            new[]
            {
                Ldsfld,
                Ldc_I4_0,
                Ble_S,
                Ldsfld,
                Ldsfld,
                Sub,
                Ldc_I4,
                Bge_S,
                Ret,
                Ldsfld,
                Stsfld,
                Ldarg_0,
                Ldfld,
                Ldarg_0,
            }
        );
    }
}