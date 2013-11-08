using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Vlindos.Logging.PerformanceTests
{
    class Program
    {
        static int generateFrame =0;

        static void Main(string[] args)
        {
            Stopwatch sw;

            // warm up
            for (int i = 0; i < 100000; i++)
            {
                CallA();
            }

            // call 100K times; no stackframes
            sw = Stopwatch.StartNew();
            for (int i = 0; i < 100000; i++)
            {
                CallA();
            }
            sw.Stop();
            Console.WriteLine("Don't generate 100K frames: {0}ms"
                                 , sw.ElapsedMilliseconds);

            // call 100K times; generate stackframes
            generateFrame = 1;
            sw = Stopwatch.StartNew();
            for (int i = 0; i < 100000; i++)
            {
                CallA();
            }
            Console.WriteLine("Generate 100K frames: {0}ms"
                           , sw.ElapsedMilliseconds);
            Getter = StackTraceGetterGet();
            // call 100K times; generate stackframes using Il code
            generateFrame = 2;
            sw = Stopwatch.StartNew();
            for (int i = 0; i < 100000; i++)
            {
                CallA();
            }
            Console.WriteLine("Generate 100K frames using Il code: {0}ms"
                           , sw.ElapsedMilliseconds);

            Console.ReadKey();
        }

        public static Func<object> Getter { get; set; }

        private static void CallA()
        {
            CallB();
        }

        private static void CallB()
        {
            CallC();
        }

        private static void CallC()
        {
            if (generateFrame == 1)
            {
                StackFrame stackFrame = new StackFrame(1);
            } 
            else  if (generateFrame == 2)
            {
                StackFrame stackFrame = getStackFrame(Getter);
            }
        }

        private static Func<object> StackTraceGetterGet()
        {
            var stackFrameHelperType = typeof(object).Assembly.GetType("System.Diagnostics.StackFrameHelper");
            var GetStackFramesInternal = Type.GetType("System.Diagnostics.StackTrace, mscorlib").GetMethod("GetStackFramesInternal", BindingFlags.Static | BindingFlags.NonPublic);

            var method = new DynamicMethod("GetStackTraceFast", typeof(object), new Type[0], typeof(StackTrace), true);

            var generator = method.GetILGenerator();
            generator.DeclareLocal(stackFrameHelperType);
            generator.Emit(OpCodes.Ldc_I4_0);
            generator.Emit(OpCodes.Ldnull);
            generator.Emit(OpCodes.Newobj, stackFrameHelperType.GetConstructor(new[] { typeof(bool), typeof(Thread) }));
            generator.Emit(OpCodes.Stloc_0);
            generator.Emit(OpCodes.Ldloc_0);
            generator.Emit(OpCodes.Ldc_I4_0);
            generator.Emit(OpCodes.Ldnull);
            generator.Emit(OpCodes.Call, GetStackFramesInternal);
            generator.Emit(OpCodes.Ldloc_0);
            generator.Emit(OpCodes.Ret);
            var getTheStackTrace = (Func<object>)method.CreateDelegate(typeof(Func<object>));
            return getTheStackTrace;
        }
        private static StackFrame getStackFrame(Func<object> getter)
        {
            //return null;
            var ret = getter.Invoke();
            // return ret as StackFrame;
            return null;
        }
    }

    //class TestProgram
    //{
    //    static void Main(string[] args)
    //    {
    //        OneTimeSetup();

    //        int i = GetCallStackDepth();   // i = 10 on my test machine
    //        i = AddOneToNesting();         // Now i = 11
    //    }


    //    private delegate object DGetStackFrameHelper();

    //    private static DGetStackFrameHelper _getStackFrameHelper;

    //    private static FieldInfo _frameCount;

    //    private static StackFrame getStackFrame()
    //    {
    //        var stackFrameHelperType = typeof(object).Assembly.GetType("System.Diagnostics.StackFrameHelper");
    //        var GetStackFramesInternal = Type.GetType("System.Diagnostics.StackTrace, mscorlib").GetMethod("GetStackFramesInternal", BindingFlags.Static | BindingFlags.NonPublic);

    //        var method = new DynamicMethod("GetStackTraceFast", typeof(object), new Type[0], typeof(StackTrace), true);

    //        var generator = method.GetILGenerator();
    //        generator.DeclareLocal(stackFrameHelperType);
    //        generator.Emit(OpCodes.Ldc_I4_0);
    //        generator.Emit(OpCodes.Ldnull);
    //        generator.Emit(OpCodes.Newobj, stackFrameHelperType.GetConstructor(new[] { typeof(bool), typeof(Thread) }));
    //        generator.Emit(OpCodes.Stloc_0);
    //        generator.Emit(OpCodes.Ldloc_0);
    //        generator.Emit(OpCodes.Ldc_I4_0);
    //        generator.Emit(OpCodes.Ldnull);
    //        generator.Emit(OpCodes.Call, GetStackFramesInternal);
    //        generator.Emit(OpCodes.Ldloc_0);
    //        generator.Emit(OpCodes.Ret);
    //        var getTheStackTrace = (Func<object>)method.CreateDelegate(typeof(Func<object>));
    //        return getTheStackTrace.Invoke() as StackFrame;
    //    }
    //    private static void OneTimeSetup()
    //    {
    //        Type stackFrameHelperType =
    //           typeof(object).Assembly.GetType("System.Diagnostics.StackFrameHelper");


    //        MethodInfo getStackFramesInternal =
    //           Type.GetType("System.Diagnostics.StackTrace, mscorlib").GetMethod(
    //                           "GetStackFramesInternal", BindingFlags.Static | BindingFlags.NonPublic);


    //        DynamicMethod dynamicMethod = new DynamicMethod(
    //                     "GetStackFrameHelper", typeof(object), new Type[0], typeof(StackTrace), true);

    //        ILGenerator generator = dynamicMethod.GetILGenerator();
    //        generator.DeclareLocal(stackFrameHelperType);
    //        generator.Emit(OpCodes.Ldc_I4_0);
    //        generator.Emit(OpCodes.Ldnull);
    //        generator.Emit(OpCodes.Newobj,
    //                 stackFrameHelperType.GetConstructor(new Type[] { typeof(bool), typeof(Thread) }));
    //        generator.Emit(OpCodes.Stloc_0);
    //        generator.Emit(OpCodes.Ldloc_0);
    //        generator.Emit(OpCodes.Ldc_I4_0);
    //        generator.Emit(OpCodes.Ldnull);
    //        generator.Emit(OpCodes.Call, getStackFramesInternal);
    //        generator.Emit(OpCodes.Ldloc_0);
    //        generator.Emit(OpCodes.Ret);


    //        _getStackFrameHelper =
    //                  (DGetStackFrameHelper)dynamicMethod.CreateDelegate(typeof(DGetStackFrameHelper));


    //        _frameCount = stackFrameHelperType.GetField(
    //                                    "iFrameCount", BindingFlags.NonPublic | BindingFlags.Instance);
    //    }


    //    private static int GetCallStackDepth()
    //    {
    //        return (int)_frameCount.GetValue(_getStackFrameHelper());
    //    }


    //    private static int AddOneToNesting()
    //    {
    //        return GetCallStackDepth();
    //    }
    //}
}
