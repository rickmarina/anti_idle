internal class PowerHelper
{
    public static void ForceSystemAwake()
    {
        WindowsNativeMethods.SetThreadExecutionState(WindowsNativeMethods.EXECUTION_STATE.ES_CONTINUOUS |
                                              WindowsNativeMethods.EXECUTION_STATE.ES_DISPLAY_REQUIRED |
                                              WindowsNativeMethods.EXECUTION_STATE.ES_SYSTEM_REQUIRED |
                                              WindowsNativeMethods.EXECUTION_STATE.ES_AWAYMODE_REQUIRED);
    }

    public static void ResetSystemDefault()
    {
        WindowsNativeMethods.SetThreadExecutionState(WindowsNativeMethods.EXECUTION_STATE.ES_CONTINUOUS);
    }
}