namespace Wox.LastPass
{
    using System.Diagnostics;
    using System.Windows.Forms;
    using System.Collections.Generic;
    using Wox.Plugin;
    using Newtonsoft.Json;

    public class LastPassPlugin : IPlugin, IContextMenu
    {
        public void Init(PluginInitContext context)
        {
        }

        public List<Result> LoadContextMenus(Result selectedResult)
        {
            var results = new List<Result>();
            LastPassEntry entry = (LastPassEntry)selectedResult.ContextData;

            results.Add(this.MakeContextResult("Username", entry.UserName, entry.UserName));
            results.Add(this.MakeContextResult("Password", "[Hidden]", entry.Password));
            results.Add(this.MakeContextResult("URL", entry.Url, entry.Url));
            results.Add(this.MakeContextResult("Note", entry.Note, entry.Note));

            return results;
        }

        private Result MakeContextResult(string fieldName, string valueToShow, string valueForClipboard)
        {
            return new Result
            {
                Title = $"Copy {fieldName}",
                SubTitle = valueToShow,
                Action = e => this.ResultToClipboardAction(e, valueForClipboard, true)
            };
        }

        public List<Result> Query(Query query)
        {
            if (string.IsNullOrEmpty(query.Search))
            {
                return new List<Result>();
            }

            var entries = this.QueryLastPass(query.Search);

            if (entries == null)
            {

                return new List<Result>() {
                    new Result
                    {
                        Title = "No Results",
                        SubTitle = "No Results. Perhaps you need to login through a WSL Terminal",
                        Action = e =>
                        {
                            Process.Start("wt.exe", "-p \"Ubuntu-20.04\""); // Big assumption the user has Windows Terminal AND this profile present.
                            return false;
                        }
                    }
                };
            }

            var results = new List<Result>();

            foreach (var entry in entries)
            {
                var res = new Result
                {
                    Title = entry.Name,
                    SubTitle = entry.UserName,
                    ContextData = entry,
                    Action = e => this.ResultToClipboardAction(e, string.IsNullOrEmpty(entry.Password) ? entry.Note : entry.Password, false)
                };

                results.Add(res);
            }

            return results;
        }

        public LastPassEntry[] QueryLastPass(string query)
        {
            using (var proc = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "bash.exe",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardInput = true,
                    CreateNoWindow = true,
                    Arguments = $@"-c ""lpass show -x --json -G {query};"""
                }
            })
            { 
                proc.Start();
                System.Threading.Thread.Sleep(500);
                proc.WaitForExit(1000);
                var output = proc.StandardOutput.ReadToEnd();

                if (string.IsNullOrEmpty(output))
                {
                    return null;
                }

                LastPassEntry[] entries = JsonConvert.DeserializeObject<LastPassEntry[]>(output);
                return entries;
            }
        }

        private bool ResultToClipboardAction(ActionContext e, string valueToClipboard, bool shouldHide = true)
        {

            Clipboard.SetText(valueToClipboard);
            return shouldHide;
        }
    }
}
