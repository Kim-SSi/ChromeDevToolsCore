using MasterDevs.ChromeDevTools;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace MasterDevs.ChromeDevTools.Protocol.Chrome.Debugger
{
	/// <summary>
	/// Steps into the function call.
	/// </summary>
	[Command(ProtocolName.Debugger.StepInto)]
	[SupportedBy("Chrome")]
	public class StepIntoCommand: ICommand<StepIntoCommandResponse>
	{
		/// <summary>
		/// Gets or sets Debugger will issue additional Debugger.paused notification if any async task is scheduledbefore next pause.
		/// </summary>
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public bool? BreakOnAsyncCall { get; set; }
	}
}
