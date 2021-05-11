using MailKit;
using MailKit.Net.Imap;
using MailKit.Search;
using MimeKit;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Text;

namespace Lowcode.Framework.Utils
{
    public class MailKitUtils
    {
        public static Dictionary<string, byte[]> GetAttachments(string server, int port, bool ssl, string user, string password, int mailId)
        {
            using (var client = new ImapClient())
            {
                // For demo-purposes, accept all SSL certificates
                client.ServerCertificateValidationCallback = (s, c, h, e) => true;

                client.Connect(server, port, ssl);

                client.Authenticate(user, password);

                // The Inbox folder is always available on all IMAP servers...
                var inbox = client.Inbox;
                inbox.Open(FolderAccess.ReadOnly);
                UniqueId uid = new UniqueId(Convert.ToUInt32(mailId));
                var message = inbox.GetMessage(uid);
                Dictionary<string, byte[]> fileList = new Dictionary<string, byte[]>();
                foreach (var attachment in message.Attachments)
                {
                    if (attachment is MimePart)
                    {

                        var part = (MimePart)attachment;
                        using (var stream = new MemoryStream())
                        {
                            part.Content.DecodeTo(stream);
                          
                            
                            fileList.Add(part.FileName,stream.ToArray());
                        }
                    }
                }

             


                client.Disconnect(true);
                return fileList;
            }
        }

        public static void CompressToZip(string fileName, Dictionary<string, byte[]> fileList)
        {
            using (var memoryStream = new MemoryStream())
            {
                using (var archive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true))
                {
                    foreach (var file in fileList)
                    {
                        var demoFile = archive.CreateEntry(file.Key);

                        using (var entryStream = demoFile.Open())
                        using (var b = new BinaryWriter(entryStream))
                        {
                            b.Write(file.Value);
                        }
                    }
                }

                using (var fileStream = new FileStream(fileName, FileMode.Create))
                {
                    memoryStream.Seek(0, SeekOrigin.Begin);
                    memoryStream.CopyTo(fileStream);
                   
                }
            }
        }
    }
}
