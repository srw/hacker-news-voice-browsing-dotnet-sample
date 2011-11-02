using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Speech.Recognition;
using System.Speech.Synthesis;
using System.Speech.AudioFormat;
using System.Net;
using System.IO;
using System.Xml;



namespace VoiceWebBrowsing
{
    public partial class MainForm : Form
    {
        #region Constants
        //const string _bogusRSSFeed = "<items><item><title>First title</title><link>http://</link></item><item><title>Second title</title><link>http://</link></item></items>";
        const string _bogusRSSFeed = null;
        const string downloadPath = @"..\..\..\..\Download";
        #endregion

        #region Private Variables
        SpeechRecognizer _speechRecognizer = new SpeechRecognizer();
        SpeechSynthesizer _ttsVoice = new SpeechSynthesizer();
        SpeechAudioFormatInfo formatInfo = new SpeechAudioFormatInfo(8000, AudioBitsPerSample.Sixteen, AudioChannel.Mono);
        Queue<string> _queue = new Queue<string>();
        List<string> articleList = new List<string>();
        HashSet<SpeechSynthesizer> tts2FileTasks = new HashSet<SpeechSynthesizer>();
        #endregion

        #region Private Methods
        private void InitGrammar()
        {
            GrammarBuilder readGrammar = new Choices(new string[] { "read article" });
            Choices articleChoice = new Choices();
            for (int i = 1; i <= 30; i++)
            {
                articleChoice.Add(i.ToString());
            }
            readGrammar.Append(articleChoice);

            GrammarBuilder saveGrammar = new Choices(new string[] { "save article" });
            saveGrammar.Append(articleChoice);

            GrammarBuilder otherGrammar = new Choices(new string[] { "receive hacker news", "stop", "test" });

            //GrammarBuilder commands = new Choices(new string[] { "receive hacker news", "stop", "test" });
            Choices commands = new Choices();
            commands.Add(new Choices(new GrammarBuilder[] { readGrammar, saveGrammar, otherGrammar }));

            var grammar = new Grammar(commands);
            this._speechRecognizer.LoadGrammar(grammar);
        }

        private void Say(string text)
        {
            this._ttsVoice.SpeakAsync(text);
        }

        private void ReadHackerNewsFeed()
        {
            string hackerNewsRSSUrl = "http://news.ycombinator.com/rss";

            using (WebClient client = new WebClient()) // WebClient class inherits IDisposable
            {
                //
                // an app.config is added to surpress: The server committed a protocol violation. Section=ResponseStatusLine
                //
                string rssXmlStr = null;
                if (_bogusRSSFeed == null)
                    rssXmlStr = client.DownloadString(hackerNewsRSSUrl);
                else
                    rssXmlStr = _bogusRSSFeed;
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(rssXmlStr);

                XmlNodeList items = xmlDoc.SelectNodes("//item");

                int counter = 1;
                articleList.Clear();
                foreach (XmlNode item in items)
                {
                    string title = item.SelectSingleNode("title").InnerText;
                    string link = item.SelectSingleNode("link").InnerText;
                    articleList.Add(link);
                    Say("article " + counter.ToString() + " " + title);
                    System.Diagnostics.Debug.WriteLine(title);

                    counter++;
                }
            }
        }

        private void ReceiveHackerNewsButton_Click(object sender, EventArgs e)
        {
            ReceiveHackerNewsCommand();
        }

        private void SaveArticle(string link, string article)
        {
            SpeechSynthesizer tts2File = new SpeechSynthesizer();
            tts2File.SpeakStarted += new EventHandler<SpeakStartedEventArgs>(tts2File_SpeakStarted);
            tts2File.SpeakCompleted += new EventHandler<SpeakCompletedEventArgs>(tts2File_SpeakCompleted);
            System.Security.Cryptography.SHA1Managed hashAlgorithm = new System.Security.Cryptography.SHA1Managed();
            hashAlgorithm.Initialize();
            byte[] buffer = Encoding.UTF8.GetBytes(link);
            byte[] hash = hashAlgorithm.ComputeHash(buffer);
            string fileName = BitConverter.ToString(hash).Replace("-", string.Empty) + ".wav";
            string executionPath = System.Reflection.Assembly.GetExecutingAssembly().Location;
            string fullPath = Path.Combine(executionPath, downloadPath, fileName);
            hashAlgorithm.Clear(); 
            tts2File.SetOutputToWaveFile(fullPath, formatInfo);
            tts2File.SpeakAsync(article);
            this.tts2FileTasks.Add(tts2File);
        }

        #endregion

        #region Constructor
        public MainForm()
        {
            InitializeComponent();
            _speechRecognizer.Enabled = true;
            _speechRecognizer.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(_speechRecognizer_SpeechRecognized);
        }
        #endregion

        #region Events
        void _ttsVoice_SpeakCompleted(object sender, SpeakCompletedEventArgs e)
        {
            this._ttsVoice.SetOutputToNull(); // Needed for flushing file buffers.
        }

        void _speechRecognizer_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            string command = e.Result.Text;
            CommandTextBox.Text = command;

            if (command == "test")
            {
                return;
            }

            if (command == "stop")
            {
                StopCommand();

                return;
            }

            if (command == "receive hacker news")
            {
                ReceiveHackerNewsCommand();

                return;
            }

            if (command.Contains("read article"))
            {
                string[] words = command.Split(' ');

                ReadArticleCommand(Decimal.Parse(words[2]));

                return;
            }

            if (command.Contains("save article"))
            {
                string[] words = command.Split(' ');

                SaveArticleCommand(Decimal.Parse(words[2]));

                return;
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            InitGrammar();
            BackgroundWorker.RunWorkerAsync();
        }
        
        void tts2File_SpeakStarted(object sender, SpeakStartedEventArgs e)
        {
        }

        void tts2File_SpeakCompleted(object sender, SpeakCompletedEventArgs e)
        {
            SpeechSynthesizer tts2File = (SpeechSynthesizer)sender;

            tts2File.SetOutputToNull();
            this.tts2FileTasks.Remove(tts2File);
        }

        private void StopButton_Click(object sender, EventArgs e)
        {
            StopCommand();
        }
        private void ReadArticleButton_Click(object sender, EventArgs e)
        {
            ReadArticleCommand(ArticleNumberUpDown.Value);
        }

        private void SaveArticleButton_Click(object sender, EventArgs e)
        {
            SaveArticleCommand(ArticleNumberUpDown.Value);            
        }

        private void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            while (true)
            {
                System.Threading.Thread.Sleep(125);

                lock(this)
                {
                    if(this._queue.Count > 0)
                    {
                        string cmd = this._queue.Dequeue();

                        if (cmd != null)
                        {
                            System.Uri uri = new Uri(cmd);
                            if (uri.Scheme == "voicewebbrowsing")
                            {
                                if (uri.Host == "receivehackernews")
                                {
                                    ReadHackerNewsFeed();
                                }
                                else if (uri.Host == "stop")
                                {
                                    this._ttsVoice.SpeakAsyncCancelAll();
                                }
                                else if (uri.Host == "readarticle" || uri.Host == "savearticle")
                                {
                                    string articleNumberStr = System.IO.Path.GetFileName(uri.AbsolutePath);
                                    int articleNumber = int.Parse(articleNumberStr);

                                    if (articleNumber > articleList.Count)
                                    {
                                        Say("please retrieve hacker news articles first");
                                    }
                                    else
                                    {
                                        articleNumber--; // 0-based index
                                        string link = articleList[articleNumber];

                                        java.net.URL url = new java.net.URL(link);
                                        string article = de.l3s.boilerpipe.extractors.ArticleExtractor.INSTANCE.getText(url);
                                        if (uri.Host == "readarticle")
                                        {
                                            Say(article);
                                        }
                                        else if (uri.Host == "savearticle")
                                        {
                                            SaveArticle(link, article);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        #endregion

        #region Commands
        private void ReceiveHackerNewsCommand()
        {
            StopCommand();
            lock (this)
            {
                this._queue.Enqueue("voicewebbrowsing://receivehackernews");
            }
        }
        private void StopCommand()
        {
            lock (this)
            {
                this._queue.Enqueue("voicewebbrowsing://stop");
            }
        }

        private void ReadArticleCommand(Decimal article)
        {
            StopCommand();
            lock (this)
            {
                this._queue.Enqueue(String.Format("voicewebbrowsing://readarticle/{0}", article.ToString()));
            }            
        }

        private void SaveArticleCommand(decimal article)
        {
            lock (this)
            {
                this._queue.Enqueue(String.Format("voicewebbrowsing://savearticle/{0}", article.ToString()));
            }
        }
        #endregion
    }
}
