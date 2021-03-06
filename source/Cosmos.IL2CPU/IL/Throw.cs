using System;
using CPU = XSharp.Assembler.x86;
using CPUx86 = XSharp.Assembler.x86;
using XSharp.Assembler;

using XSharp;

namespace Cosmos.IL2CPU.X86.IL
{
    [Cosmos.IL2CPU.OpCode( ILOpCode.Code.Throw )]
    public class Throw : ILOp
    {
        public Throw( XSharp.Assembler.Assembler aAsmblr )
            : base( aAsmblr )
        {
        }

        public override void Execute(_MethodInfo aMethod, ILOpCode aOpCode )
        {
#warning TODO: Implement exception
            DoNullReferenceCheck(Assembler, DebugEnabled, 4);
            XS.Add(XSRegisters.ESP, 4);
            XS.Pop(XSRegisters.EAX);
            new CPUx86.Mov { DestinationRef = XSharp.Assembler.ElementReference.New( DataMember.GetStaticFieldName( ExceptionHelperRefs.CurrentExceptionRef ) ), DestinationIsIndirect = true, SourceReg = CPUx86.RegistersEnum.EAX };
            XS.Call("SystemExceptionOccurred");
            XS.Set(XSRegisters.ECX, 3);
            Call.EmitExceptionLogic( Assembler,aMethod, aOpCode, false, null );

        }

        // namespace Cosmos.IL2CPU.IL.X86 {
        // 	[XSharp.Assembler.OpCode(OpCodeEnum.Throw)]
        // 	public class Throw: Op {
        // 		private MethodInformation mMethodInfo;
        // 		private int mCurrentILOffset;
        //         public Throw(ILReader aReader, MethodInformation aMethodInfo)
        // 			: base(aReader, aMethodInfo) {
        // 			mMethodInfo = aMethodInfo;
        // 			mCurrentILOffset = (int)aReader.Position;
        // 		}
        //
        // 		public static void Assemble(Assembler.Assembler aAssembler, MethodInformation aMethodInfo, int aCurrentILOffset, string aExceptionOccurredLabel) {
        //             XS.Pop(XSRegisters.EAX);
        //             new CPUx86.Move { DestinationRef = CPU.ElementReference.New(CPU.DataMember.GetStaticFieldName(CPU.Assembler.CurrentExceptionRef)), DestinationIsIndirect = true, SourceReg = CPUx86.Registers.EAX };
        //             XS.Call(aExceptionOccurredLabel);
        //             XS.Mov(XSRegisters.ECX, 3);
        // 			Call.EmitExceptionLogic(aAssembler, (uint)aCurrentILOffset, aMethodInfo, null, false, null);
        // 			aAssembler.Stack.Pop();
        // 		}
        //
        // 		public override void DoAssemble() {
        // 		    var xMethodInfo = GetService<IMetaDataInfoService>().GetMethodInfo(CPU.Assembler.CurrentExceptionOccurredRef,
        // 		                                                                       false);
        // 			Assemble(Assembler, mMethodInfo, mCurrentILOffset, xMethodInfo.LabelName);
        // 		}
        // 	}
        // }
    }
}
