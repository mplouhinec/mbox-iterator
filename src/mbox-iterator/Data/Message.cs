using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mbox_iterator.Data
{
    /// <summary>
    /// Message Class
    /// </summary>
    public class Message : IEnumerable<Message>
    {
        #region Constants

        /// <summary>
        /// End character of a line
        /// <see cref="https://tools.ietf.org/html/rfc2822#section-2.1"/>
        /// </summary>
        public const string END_LINE_CHARACTER = "\r\n";

        /// <summary>
        /// Numbers of characters of a line, including CRLF
        /// <see cref="https://tools.ietf.org/html/rfc2822#section-2.1.1"/> 
        /// </summary>
        public const int MAX_LINE_CHARACTERS_LENGTH = 1000;

        /// <summary>
        /// String representing the beginning of a message inside a mbox file
        /// </summary>
        public const string START_MESSAGE_STRING = "From ";

        #endregion

        #region Properties

        /// <summary>
        /// Header of the message
        /// </summary>
        public MessageHeader Header { get; set; }

        /// <summary>
        /// Body of the message
        /// </summary>
        public string Body { get; set; }
        
        
        #endregion

        #region Méthods

        /// <summary>
        /// Read an mbox file and return all messages in the mbox file
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static IList<Message> GetMessages(string filePath)
        {

            string line;

            if (string.IsNullOrEmpty(filePath))
                throw new ArgumentNullException("filePath");

            if (!File.Exists(filePath))
                throw new FileNotFoundException("The file doesn't exist.", filePath);

            StringBuilder stringBuilderMessageBuffered = new StringBuilder();
            IList<Message> messages = new List<Message>();

            using (FileStream fileStream = File.Open(filePath, FileMode.Open, FileAccess.Read))
            {
                using (BufferedStream bufferedStream = new BufferedStream(fileStream))
                {
                    using (StreamReader streamReader = new StreamReader(bufferedStream))
                    {
                        while ((line = streamReader.ReadLine()) != null)
                        {
                            if (line.StartsWith(START_MESSAGE_STRING))
                            {
                                // Manage message in string builder.
                                var newMessage = FromString(stringBuilderMessageBuffered.ToString());
                                if(newMessage != null)
                                    messages.Add(newMessage);

                                // Start new message
                                stringBuilderMessageBuffered.Clear();
                                stringBuilderMessageBuffered.AppendLine(line);
                            }
                            else
                            {
                                stringBuilderMessageBuffered.AppendLine(line);
                            }
                        }

                        var lastMessage = FromString(stringBuilderMessageBuffered.ToString());
                        messages.Add(lastMessage);
                    }
                }
            }

            return messages;
        }

        /// <summary>
        /// Build a message from a block string
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static Message FromString(string data)
        {
            if (string.IsNullOrEmpty(data))
                return null;

            Message result;

            try
            {
                var index = data.IndexOf(END_LINE_CHARACTER + END_LINE_CHARACTER);
                var stringHeader = data.Substring(0, index);
                var body = data.Substring(index + 4, data.Length - index - 4);

                var header = MessageHeader.FromString(stringHeader);

                result = new Message();
                result.Header = header;
                result.Body = body;

                return result;
            }
            catch(Exception ex)
            {
                return null;
            }

            
        }

        public IEnumerator<Message> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }


        #endregion


    }
}
