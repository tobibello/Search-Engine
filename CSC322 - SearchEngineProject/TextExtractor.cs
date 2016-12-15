using ICSharpCode.SharpZipLib.Zip;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Text;
using System.Xml;
using Toxy;

namespace CSC322_SearchEngineProject
{
    internal static class TextExtractor
    {
        private static readonly Dictionary<string, bool> TextParserExtensions = new Dictionary<string, bool>
        {
            {".pdf", true},
            {".doc", true},
            {".docx", true},
            {".xlsx", true},
            {".xls", true},
            {".pptx", true},
            {".txt", true},
        };

        internal static string ReadAllText(FileInfo file)
        {
            Contract.Requires(file != null);
            Contract.Ensures(Contract.Result<string>() != null);

            StringBuilder builder = new StringBuilder();
            try
            {
                string extension = file.Extension.ToLower();
                string filePath = file.FullName;
                ParserContext context = new ParserContext(filePath);
                if (TextParserExtensions.ContainsKey(extension))
                {
                    ITextParser parser = ParserFactory.CreateText(context);
                    builder.AppendLine(parser.Parse());
                } //Bug 1: Toxy Can't Extract from .ppt
                else if (".html" == extension)
                {
                    IDomParser parser = ParserFactory.CreateDom(context);
                    ToxyDom dom = parser.Parse();
                    builder.Append(dom.Root.InnerText);
                }
                else if (".xml" == extension)
                {
                    Stream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
                    XmlTextReader reader = new XmlTextReader(stream);
                    while (reader.Read())
                    {
                        switch (reader.NodeType)
                        {
                            case XmlNodeType.Text:
                                builder.Append(reader.ReadString() + " ");
                                break;
                        }
                    }
                }
            }
            catch (IOException ioe)
            {
                File.AppendAllLines("logFile.log",
                    new[] { $"[{DateTime.Now}]:IOException; Source:{ioe.Source};Message: {ioe.Message}" });
            }
            catch (ZipException ze)
            {
                File.AppendAllLines("logFile.log",
                    new[] { $"[{DateTime.Now}]: ZipException; Source:{ze.Source};Message: {ze.Message}" });
            }
            catch (InvalidOperationException ioe)
            {
                File.AppendAllLines("logFile.log",
                    new[] { $"[{DateTime.Now}]:IOException; Source:{ioe.Source};Message: {ioe.Message}" });
            }
            return builder.ToString();
        }
    }
}