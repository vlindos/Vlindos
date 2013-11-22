using System.Diagnostics;

namespace Vlindos.Logging
{
    public interface ICallingStackFrameGetter
    {
        StackFrame GetCallingStackFrame(uint framesToSkip);
    }

    public class CallingStackFrameGetter
    {
        public StackFrame GetCallingStackFrame(uint framesToSkip)
        {
            //var stackFrameHelperType = typeof(object).Assembly.GetType("System.Diagnostics.StackFrameHelper");
            //var type = Type.GetType("System.Diagnostics.StackTrace, mscorlib");
            //if (type == null) return null;
            //var stackFramesInternal = type.GetMethod("GetStackFramesInternal", BindingFlags.Static | BindingFlags.NonPublic);
            //var method = new DynamicMethod("GetStackTraceFast", typeof (object), new Type[0], typeof (StackTrace), true);
            //var generator = method.GetILGenerator();
            //generator.DeclareLocal(stackFrameHelperType);
            //generator.Emit(OpCodes.Ldc_I4_0);
            //generator.Emit(OpCodes.Ldnull);
            //var constructor = stackFrameHelperType.GetConstructor(new[] {typeof (bool), typeof (Thread)});
            //if (constructor == null) return null;
            //generator.Emit(OpCodes.Newobj, constructor);
            //generator.Emit(OpCodes.Stloc_0);
            //generator.Emit(OpCodes.Ldloc_0);
            //generator.Emit(OpCodes.Ldc_I4_0);
            //generator.Emit(OpCodes.Ldnull);
            //generator.Emit(OpCodes.Call, stackFramesInternal);
            //generator.Emit(OpCodes.Ldloc_0);
            //generator.Emit(OpCodes.Ret);
            //var getTheStackTrace = (Func<object>) method.CreateDelegate(typeof (Func<object>));
            //return getTheStackTrace.Invoke() as StackFrame;

            return new StackFrame((int)(framesToSkip + 1), true);
        }
    }
}
