using MasterDevs.ChromeDevTools;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace MasterDevs.ChromeDevTools.Protocol.Chrome.Debugger
{
	/// <summary>
	/// Enables or disables async call stacks tracking.
	/// </summary>
	[Command(ProtocolName.Debugger.SetAsyncCallStackDepth)]
	[SupportedBy("Chrome")]
	public class SetAsyncCallStackDepthCommand: ICommand<SetAsyncCallStackDepthCommandResponse>
	{
		/// <summary>
		/// Gets or sets Maximum depth of async call stacks. Setting to `0` will effectively disable collecting asynccall stacks (default).
		/// </summary>
		public long MaxDepth { get; set; }
	}
}