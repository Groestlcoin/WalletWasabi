using Avalonia.Diagnostics.ViewModels;
using System;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Collections.Generic;
using System.Text;
using WalletWasabi.Gui.ViewModels;
using System.IO;
using ReactiveUI;
using System.Reactive;
using WalletWasabi.Helpers;
using WalletWasabi.Logging;
using System.Reactive.Linq;
using WalletWasabi.WebClients.Wasabi;
using System.Reactive.Disposables;
using Splat;
using WalletWasabi.Models;
using WalletWasabi.Services;

namespace WalletWasabi.Gui.Tabs
{
	internal class AboutViewModel : WasabiDocumentTabViewModel
	{
		private string _currentBackendMajorVersion;

		public AboutViewModel() : base("About")
		{
			OpenBrowserCommand = ReactiveCommand.CreateFromTask<string>(IoHelpers.OpenBrowserAsync);

			OpenBrowserCommand.ThrownExceptions
				.ObserveOn(RxApp.TaskpoolScheduler)
				.Subscribe(ex => Logger.LogError(ex));
		}

		public ReactiveCommand<string, Unit> OpenBrowserCommand { get; }
		public Version ClientVersion => Constants.ClientVersion;
		public string BackendCompatibleVersions => Constants.ClientSupportBackendVersionText;

		public string CurrentBackendMajorVersion
		{
			get => _currentBackendMajorVersion;
			set => this.RaiseAndSetIfChanged(ref _currentBackendMajorVersion, value);
		}

		public Version BitcoinCoreVersion => Constants.BitcoinCoreVersion;
		public Version HwiVersion => Constants.HwiVersion;

		public string ClearnetLink => "https://groestlcoin.org/";

		public string TorLink => "http://6brsrbiinpc32tfc.onion";

		public string SourceCodeLink => "https://github.com/Groestlcoin/WalletWasabi/";

		public string CustomerSupportLink => "https://www.reddit.com/r/Groestlcoin/";

		public string UserSupportLink => "https://www.reddit.com/r/Groestlcoin/";

		public string FAQLink => "https://groestlcoin.org/forum/";

		public string DocsLink => "https://groestlcoin.org/forum/";

		public override void OnOpen(CompositeDisposable disposables)
		{
			base.OnOpen(disposables);

			CurrentBackendMajorVersion = WasabiClient.ApiVersion.ToString();
		}
	}
}
