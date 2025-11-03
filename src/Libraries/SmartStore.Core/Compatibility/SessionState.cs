// Compatibility stub for System.Web.SessionState
// TODO: Replace with ASP.NET Core Session

using System.Collections.Generic;

namespace System.Web.SessionState
{
    public class HttpSessionState : Dictionary<string, object>
    {
        public object this[string key]
        {
            get => TryGetValue(key, out var value) ? value : null;
            set => base[key] = value;
        }
        
        public string SessionID { get; set; } = Guid.NewGuid().ToString();
        public int Timeout { get; set; } = 20;
        public bool IsNewSession { get; set; } = true;
        
        public void Abandon()
        {
            Clear();
        }
    }
}
