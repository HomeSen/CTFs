using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace BMPHIDE
{
    public class A
    {
        [StructLayout(LayoutKind.Sequential, Size = 24)]
        private struct CORINFO_EH_CLAUSE
        {
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        private struct CORINFO_METHOD_INFO
        {
            public IntPtr ftn;

            public IntPtr scope;

            public unsafe byte* ILCode;

            public uint ILCodeSize;
        }

        private struct CORINFO_SIG_INFO_x64
        {
            public uint callConv;

            private uint pad1;

            public IntPtr retTypeClass;

            public IntPtr retTypeSigClass;

            public byte retType;

            public byte flags;

            public ushort numArgs;

            private uint pad2;

            public CORINFO_SIG_INST_x64 sigInst;

            public IntPtr args;

            public IntPtr sig;

            public IntPtr scope;

            public uint token;

            private uint pad3;
        }

        private struct CORINFO_SIG_INFO_x86
        {
            public uint callConv;

            public IntPtr retTypeClass;

            public IntPtr retTypeSigClass;

            public byte retType;

            public byte flags;

            public ushort numArgs;

            public CORINFO_SIG_INST_x86 sigInst;

            public IntPtr args;

            public IntPtr sig;

            public IntPtr scope;

            public uint token;
        }

        [StructLayout(LayoutKind.Sequential, Size = 32)]
        private struct CORINFO_SIG_INST_x64
        {
        }

        [StructLayout(LayoutKind.Sequential, Size = 16)]
        private struct CORINFO_SIG_INST_x86
        {
        }

        private struct ICorClassInfo
        {
            public unsafe readonly IntPtr* vfptr;
        }

        private struct ICorDynamicInfo
        {
            public unsafe IntPtr* vfptr;

            public unsafe int* vbptr;

            public unsafe static ICorStaticInfo* ICorStaticInfo(ICorDynamicInfo* ptr)
            {
                return (ICorStaticInfo*)((byte*)(&ptr->vbptr) + ptr->vbptr[hasLinkInfo ? 9 : 8]);
            }
        }

        private struct ICorJitInfo
        {
            public unsafe IntPtr* vfptr;

            public unsafe int* vbptr;

            public unsafe static ICorDynamicInfo* ICorDynamicInfo(ICorJitInfo* ptr)
            {
                hasLinkInfo = (ptr->vbptr[10] > 0 && ptr->vbptr[10] >> 16 == 0);
                return (ICorDynamicInfo*)((byte*)(&ptr->vbptr) + ptr->vbptr[hasLinkInfo ? 10 : 9]);
            }
        }

        private struct ICorMethodInfo
        {
            public unsafe IntPtr* vfptr;
        }

        private struct ICorModuleInfo
        {
            public unsafe IntPtr* vfptr;
        }

        private struct ICorStaticInfo
        {
            public unsafe IntPtr* vfptr;

            public unsafe int* vbptr;

            public unsafe static ICorMethodInfo* ICorMethodInfo(ICorStaticInfo* ptr)
            {
                return (ICorMethodInfo*)((byte*)(&ptr->vbptr) + ptr->vbptr[1]);
            }

            public unsafe static ICorModuleInfo* ICorModuleInfo(ICorStaticInfo* ptr)
            {
                return (ICorModuleInfo*)((byte*)(&ptr->vbptr) + ptr->vbptr[2]);
            }

            public unsafe static ICorClassInfo* ICorClassInfo(ICorStaticInfo* ptr)
            {
                return (ICorClassInfo*)((byte*)(&ptr->vbptr) + ptr->vbptr[3]);
            }
        }

        private class CorMethodInfoHook
        {
            private static int ehNum = -1;

            public unsafe CORINFO_EH_CLAUSE* clauses;

            public IntPtr ftn;

            public unsafe ICorMethodInfo* info;

            public getEHinfo n_getEHinfo;

            public unsafe IntPtr* newVfTbl;

            public getEHinfo o_getEHinfo;

            public unsafe IntPtr* oldVfTbl;

            private unsafe void hookEHInfo(IntPtr self, IntPtr ftn, uint EHnumber, CORINFO_EH_CLAUSE* clause)
            {
                if (ftn == this.ftn)
                {
                    *clause = clauses[EHnumber];
                }
                else
                {
                    o_getEHinfo(self, ftn, EHnumber, clause);
                }
            }

            public unsafe void Dispose()
            {
                Marshal.FreeHGlobal((IntPtr)(void*)newVfTbl);
                info->vfptr = oldVfTbl;
            }

            public unsafe static CorMethodInfoHook Hook(ICorJitInfo* comp, IntPtr ftn, CORINFO_EH_CLAUSE* clauses)
            {
                ICorMethodInfo* ptr = ICorStaticInfo.ICorMethodInfo(ICorDynamicInfo.ICorStaticInfo(ICorJitInfo.ICorDynamicInfo(comp)));
                IntPtr* vfptr = ptr->vfptr;
                IntPtr* ptr2 = (IntPtr*)(void*)Marshal.AllocHGlobal(27 * IntPtr.Size);
                for (int i = 0; i < 27; i++)
                {
                    ptr2[i] = vfptr[i];
                }
                if (ehNum == -1)
                {
                    for (int j = 0; j < 27; j++)
                    {
                        bool flag = true;
                        byte* ptr3 = (byte*)(void*)vfptr[j];
                        while (*ptr3 != 233)
                        {
                            if (!((IntPtr.Size == 8) ? (*ptr3 == 72 && ptr3[1] == 129 && ptr3[2] == 233) : (*ptr3 == 131 && ptr3[1] == 233)))
                            {
                                ptr3++;
                                continue;
                            }
                            flag = false;
                            break;
                        }
                        if (flag)
                        {
                            ehNum = j;
                            break;
                        }
                    }
                }
                CorMethodInfoHook corMethodInfoHook = new CorMethodInfoHook
                {
                    ftn = ftn,
                    info = ptr,
                    clauses = clauses,
                    newVfTbl = ptr2,
                    oldVfTbl = vfptr
                };
                corMethodInfoHook.n_getEHinfo = corMethodInfoHook.hookEHInfo;
                corMethodInfoHook.o_getEHinfo = (getEHinfo)Marshal.GetDelegateForFunctionPointer(vfptr[ehNum], typeof(getEHinfo));
                ptr2[ehNum] = Marshal.GetFunctionPointerForDelegate(corMethodInfoHook.n_getEHinfo);
                ptr->vfptr = ptr2;
                return corMethodInfoHook;
            }
        }

        private class CorJitInfoHook
        {
            public unsafe CORINFO_EH_CLAUSE* clauses;

            public IntPtr ftn;

            public unsafe ICorJitInfo* info;

            public getEHinfo n_getEHinfo;

            public unsafe IntPtr* newVfTbl;

            public getEHinfo o_getEHinfo;

            public unsafe IntPtr* oldVfTbl;

            private unsafe void hookEHInfo(IntPtr self, IntPtr ftn, uint EHnumber, CORINFO_EH_CLAUSE* clause)
            {
                if (ftn == this.ftn)
                {
                    *clause = clauses[EHnumber];
                }
                else
                {
                    o_getEHinfo(self, ftn, EHnumber, clause);
                }
            }

            public unsafe void Dispose()
            {
                Marshal.FreeHGlobal((IntPtr)(void*)newVfTbl);
                info->vfptr = oldVfTbl;
            }

            public unsafe static CorJitInfoHook Hook(ICorJitInfo* comp, IntPtr ftn, CORINFO_EH_CLAUSE* clauses)
            {
                IntPtr* vfptr = comp->vfptr;
                IntPtr* ptr = (IntPtr*)(void*)Marshal.AllocHGlobal(158 * IntPtr.Size);
                for (int i = 0; i < 158; i++)
                {
                    ptr[i] = vfptr[i];
                }
                CorJitInfoHook corJitInfoHook = new CorJitInfoHook
                {
                    ftn = ftn,
                    info = comp,
                    clauses = clauses,
                    newVfTbl = ptr,
                    oldVfTbl = vfptr
                };
                corJitInfoHook.n_getEHinfo = corJitInfoHook.hookEHInfo;
                corJitInfoHook.o_getEHinfo = (getEHinfo)Marshal.GetDelegateForFunctionPointer(vfptr[8], typeof(getEHinfo));
                ptr[8] = Marshal.GetFunctionPointerForDelegate(corJitInfoHook.n_getEHinfo);
                comp->vfptr = ptr;
                return corJitInfoHook;
            }
        }

        private struct MethodData
        {
            public readonly uint ILCodeSize;

            public readonly uint MaxStack;

            public readonly uint EHCount;

            public readonly uint LocalVars;

            public readonly uint Options;

            public readonly uint MulSeed;
        }

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private unsafe delegate uint locateNativeCallingConvention(IntPtr self, ICorJitInfo* comp, CORINFO_METHOD_INFO* info, uint flags, byte** nativeEntry, uint* nativeSizeOfCode);

        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        private unsafe delegate void getEHinfo(IntPtr self, IntPtr ftn, uint EHnumber, CORINFO_EH_CLAUSE* clause);

        private unsafe delegate IntPtr* getJit();

        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        private delegate uint getMethodDefFromMethod(IntPtr self, IntPtr ftn);

        private static locateNativeCallingConvention originalDelegate;

        private static bool ver4;

        private static bool ver5;

        private static locateNativeCallingConvention handler;

        private static bool hasLinkInfo;

        public static void CalculateStack()
        {
            Module module = typeof(A).Module;
            ModuleHandle moduleHandle = module.ModuleHandle;
            ver4 = (Environment.Version.Major == 4);
            if (ver4)
            {
                ver5 = (Environment.Version.Revision > 17020);
            }
            IdentifyLocals();
        }

        [DllImport("kernel32.dll")]
        private static extern IntPtr LoadLibrary(string lib);

        [DllImport("kernel32.dll")]
        private static extern IntPtr GetProcAddress(IntPtr lib, string proc);

        [DllImport("kernel32.dll")]
        private static extern bool VirtualProtect(IntPtr lpAddress, uint dwSize, uint flNewProtect, out uint lpflOldProtect);

        private unsafe static void IdentifyLocals()
        {
            ulong* ptr = stackalloc ulong[2];
            if (ver4)
            {
                *ptr = 7218835248827755619uL;
                ptr[1] = 27756uL;
            }
            else
            {
                *ptr = 8388352820681864045uL;
                ptr[1] = 1819042862uL;
            }
            IntPtr lib = LoadLibrary(new string((sbyte*)ptr));
            *ptr = 127995569530215uL;
            getJit getJit = (getJit)Marshal.GetDelegateForFunctionPointer(GetProcAddress(lib, new string((sbyte*)ptr)), typeof(getJit));
            IntPtr intPtr = *getJit();
            IntPtr val = *(IntPtr*)(void*)intPtr;
            IntPtr intPtr2;
            uint flNewProtect;
            if (IntPtr.Size == 8)
            {
                intPtr2 = Marshal.AllocHGlobal(16);
                ulong* ptr2 = (ulong*)(void*)intPtr2;
                *ptr2 = 18446744073709533256uL;
                ptr2[1] = 10416984890032521215uL;
                VirtualProtect(intPtr2, 12u, 64u, out flNewProtect);
                Marshal.WriteIntPtr(intPtr2, 2, val);
            }
            else
            {
                intPtr2 = Marshal.AllocHGlobal(8);
                ulong* ptr3 = (ulong*)(void*)intPtr2;
                *ptr3 = 10439625411221520312uL;
                VirtualProtect(intPtr2, 7u, 64u, out flNewProtect);
                Marshal.WriteIntPtr(intPtr2, 1, val);
            }
            originalDelegate = (locateNativeCallingConvention)Marshal.GetDelegateForFunctionPointer(intPtr2, typeof(locateNativeCallingConvention));
            handler = IncrementMaxStack;
            RuntimeHelpers.PrepareDelegate(originalDelegate);
            RuntimeHelpers.PrepareDelegate(handler);
            VirtualProtect(intPtr, (uint)IntPtr.Size, 64u, out flNewProtect);
            Marshal.WriteIntPtr(intPtr, Marshal.GetFunctionPointerForDelegate(handler));
            VirtualProtect(intPtr, (uint)IntPtr.Size, flNewProtect, out flNewProtect);
        }

        public static MethodBase c(IntPtr MethodHandleValue)
        {
            Type[] types = Assembly.GetExecutingAssembly().GetTypes();
            foreach (Type type in types)
            {
                MethodInfo[] methods = type.GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
                MethodInfo[] array = methods;
                RuntimeMethodHandle methodHandle;
                foreach (MethodInfo methodInfo in array)
                {
                    methodHandle = methodInfo.MethodHandle;
                    if (methodHandle.Value == MethodHandleValue)
                    {
                        return methodInfo;
                    }
                }
                ConstructorInfo[] constructors = type.GetConstructors(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
                ConstructorInfo[] array2 = constructors;
                foreach (ConstructorInfo constructorInfo in array2)
                {
                    methodHandle = constructorInfo.MethodHandle;
                    if (methodHandle.Value == MethodHandleValue)
                    {
                        return constructorInfo;
                    }
                }
            }
            return null;
        }

        private unsafe static uint IncrementMaxStack(IntPtr self, ICorJitInfo* comp, CORINFO_METHOD_INFO* info, uint flags, byte** nativeEntry, uint* nativeSizeOfCode)
        {
            if (info != null)
            {
                MethodBase methodBase = c(info->ftn);
                if (methodBase != (MethodBase)null)
                {
                    if (methodBase.MetadataToken == 100663317)
                    {
                        VirtualProtect((IntPtr)(void*)info->ILCode, info->ILCodeSize, 4u, out uint flNewProtect);
                        Marshal.WriteByte((IntPtr)(void*)info->ILCode, 23, 20);
                        Marshal.WriteByte((IntPtr)(void*)info->ILCode, 62, 20);
                        VirtualProtect((IntPtr)(void*)info->ILCode, info->ILCodeSize, flNewProtect, out flNewProtect);
                    }
                    else if (methodBase.MetadataToken == 100663316)
                    {
                        VirtualProtect((IntPtr)(void*)info->ILCode, info->ILCodeSize, 4u, out uint flNewProtect2);
                        Marshal.WriteInt32((IntPtr)(void*)info->ILCode, 6, 309030853);
                        Marshal.WriteInt32((IntPtr)(void*)info->ILCode, 18, 209897853);
                        VirtualProtect((IntPtr)(void*)info->ILCode, info->ILCodeSize, flNewProtect2, out flNewProtect2);
                    }
                }
            }
            return originalDelegate(self, comp, info, flags, nativeEntry, nativeSizeOfCode);
        }

        public unsafe static void VerifySignature(MethodInfo m1, MethodInfo m2)
        {
            RuntimeHelpers.PrepareMethod(m1.MethodHandle);
            RuntimeHelpers.PrepareMethod(m2.MethodHandle);
            RuntimeMethodHandle methodHandle = m1.MethodHandle;
            IntPtr value = methodHandle.Value;
            int* ptr = (int*)((byte*)value.ToPointer() + 2L * 4L);
            methodHandle = m2.MethodHandle;
            value = methodHandle.Value;
            int* ptr2 = (int*)((byte*)value.ToPointer() + 2L * 4L);
            *ptr = *ptr2;
        }
    }
}
