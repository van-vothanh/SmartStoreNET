// Compatibility stub for System.Web.UI.HtmlTextWriter
// TODO: Replace with proper HTML generation

using System.IO;

namespace System.Web.UI
{
    public class HtmlTextWriter : TextWriter
    {
        private readonly TextWriter _writer;
        
        public HtmlTextWriter(TextWriter writer)
        {
            _writer = writer;
        }
        
        public override System.Text.Encoding Encoding => _writer.Encoding;
        
        public override void Write(char value) => _writer.Write(value);
        public override void Write(string value) => _writer.Write(value);
        
        public void WriteAttribute(string name, string value)
        {
            _writer.Write($" {name}=\"{value}\"");
        }
        
        public void WriteBeginTag(string tagName)
        {
            _writer.Write($"<{tagName}");
        }
        
        public void WriteEndTag(string tagName)
        {
            _writer.Write($"</{tagName}>");
        }
        
        public void WriteFullBeginTag(string tagName)
        {
            _writer.Write($"<{tagName}>");
        }
    }
}
