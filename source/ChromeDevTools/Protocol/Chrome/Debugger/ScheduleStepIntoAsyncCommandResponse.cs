using MasterDevs.ChromeDevTools;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace MasterDevs.ChromeDevTools.Protocol.Chrome.Debugger
{
	/// <summary>
	/// This method is deprecated - use Debugger.stepInto with breakOnAsyncCall and
	/// Debugger.pauseOnAsyncTask instead. Steps into next scheduled async task if any is scheduled
	/// before next pause. Returns success when async task is actually scheduled, returns error if no
	/// task were scheduled or another scheduleStepIntoAsync was called.
	/// </summary>
	[CommandResponse(ProtocolName.Debugger.ScheduleStepIntoAsync)]
	[SupportedBy("Chrome")]
	public class ScheduleStepIntoAsyncCommandResponse
	{
	}
}
