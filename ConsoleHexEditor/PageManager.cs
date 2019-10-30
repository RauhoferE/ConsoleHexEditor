//-----------------------------------------------------------------------
// <copyright file="PageManager.cs" company="FH Wiener Neustadt">
//     Copyright (c) Emre Rauhofer. All rights reserved.
// </copyright>
// <author>Emre Rauhofer</author>
// <summary>
// This program is a hex editor. 
// </summary>
//-----------------------------------------------------------------------
namespace ConsoleHexEditor
{
    using System;
    using System.Linq;
    using System.Runtime.Remoting.Channels;

    /// <summary>
    /// The <see cref="PageManager"/> class.
    /// </summary>
    public class PageManager
    {
        /// <summary>
        /// The page element.
        /// </summary>
        private PageElement page;

        /// <summary>
        /// The shown page.
        /// </summary>
        private PageElement shownPage;

        /// <summary>
        /// The random object.
        /// </summary>
        private Random rnd;

        /// <summary>
        /// The current view.
        /// </summary>
        private ICurrentView currentView;

        /// <summary>
        /// Initializes a new instance of the <see cref="PageManager"/> class.
        /// </summary>
        public PageManager()
        {
            this.shownPage = new PageElement();
            this.page = new PageElement();
            this.HexViewIndex = 0;
            this.TextViewIndex = 0;
            this.SplitViewIndex = 0;
            this.CurrentView = new NullView();
            this.rnd = new Random();
        }

        /// <summary>
        /// This event fires when the hex page has been created.
        /// </summary>
        public event EventHandler<PageEventArgs> OnHexPageCreated;

        /// <summary>
        /// This event fires when the text page has been created.
        /// </summary>
        public event EventHandler<PageEventArgs> OnTextPageCreated;

        /// <summary>
        /// This event fires when the split page has been created.
        /// </summary>
        public event EventHandler<PageEventArgs> OnTSplitViewPageCreated;

        /// <summary>
        /// This event fires when the end of the page has been reached.
        /// </summary>
        public event EventHandler OnEndReached;

        /// <summary>
        /// This event fires when the begin of the page has been reached.
        /// </summary>
        public event EventHandler OnBeginReached;

        /// <summary>
        /// This event fires when a message should be printed.
        /// </summary>
        public event EventHandler<StringEventArgs> OnMessagePrint;

        /// <summary>
        /// This event fires when a error message should be printed.
        /// </summary>
        public event EventHandler<StringEventArgs> OnErrorMessagePrint;

        /// <summary>
        /// This event fires when a position has been found.
        /// </summary>
        public event EventHandler<PageAndPositionEventArgs> OnPositionFound;

        /// <summary>
        /// This event fires when the reader should jump to the begin of the file..
        /// </summary>
        public event EventHandler OnJumpToBegin;

        /// <summary>
        /// This event fires when a page should be saved.
        /// </summary>
        public event EventHandler<PageEventArgs> OnPageSave;

        /// <summary>
        /// Gets the current view.
        /// </summary>
        /// <value> A normal <see cref="ICurrentView"/> object. </value>
        public ICurrentView CurrentView
        {
            get
            {
                return this.currentView;
            }

            private set
            {
                if (value == null)
                {
                    throw new ArgumentException("Error the currentview is null.");
                }

                this.currentView = value;
            }
        }

        /// <summary>
        /// Gets the current page.
        /// </summary>
        /// <value> A normal <see cref="PageElement"/> object. </value>
        public PageElement Page
        {
            get
            {
                return this.page;
            }

            private set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("Error page cant be null.");
                }

                this.page = value;
            }
        }

        /// <summary>
        /// Gets the shown page.
        /// </summary>
        /// <value> A normal <see cref="PageElement"/> object. </value>
        public PageElement ShownPage
        {
            get
            {
                return this.shownPage;
            }

            private set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("Error page cant be null.");
                }

                this.shownPage = value;
            }
        }

        /// <summary>
        /// Gets the index of the hex page.
        /// </summary>
        /// <value> A normal <see cref="int"/> object. </value>
        public int HexViewIndex
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the index of the text page.
        /// </summary>
        /// <value> A normal <see cref="int"/> object. </value>
        public int TextViewIndex
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the index of the split view page.
        /// </summary>
        /// <value> A normal <see cref="int"/> object. </value>
        public int SplitViewIndex
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the first index of the hex page.
        /// </summary>
        /// <value> A normal <see cref="int"/> object. </value>
        public int LastViewSplitIndex
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the first index of the text page.
        /// </summary>
        /// <value> A normal <see cref="int"/> object. </value>
        public int LastViewHexIndex
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the first index of the split view page.
        /// </summary>
        /// <value> A normal <see cref="int"/> object. </value>
        public int LastViewTextIndex
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the number of the page in the split view.
        /// </summary>
        /// <value> A normal <see cref="int"/> object. </value>
        public int LocalPage
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the number of the page in the text view.
        /// </summary>
        /// <value> A normal <see cref="int"/> object. </value>
        public int LocalPageText
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the number of the page in the hex view.
        /// </summary>
        /// <value> A normal <see cref="int"/> object. </value>
        public int LocalPageHex
        {
            get;
            private set;
        }

        /// <summary>
        /// This method gets the next page.
        /// </summary>
        /// <param name="sender"> The object sender. </param>
        /// <param name="e"> The <see cref="EventArgs"/>. </param>
        public void GetNextPage(object sender, EventArgs e)
        {
            switch (this.CurrentView.Name)
            {
                case "HexView":
                    this.GetNextHexPageView();
                    break;
                case "SplitView":
                    this.GetNextSplitView();
                    break;
                case "TextView":
                    this.GetNextTextView();
                    break;
                case "NullView":
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// This method gets the previous page.
        /// </summary>
        /// <param name="sender"> The object sender. </param>
        /// <param name="e"> The <see cref="EventArgs"/>. </param>
        public void GetPreviousPage(object sender, EventArgs e)
        {
            this.FireOnMessagePrint(new StringEventArgs("Please wait..."));

            switch (this.CurrentView.Name)
            {
                case "HexView":
                    this.GetPreviousHexPageView();
                    break;
                case "SplitView":
                    this.GetPreviousSplitView();
                    break;
                case "TextView":
                    this.GetPreviousTextView();
                    break;
                case "NullView":
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// This method gets the a new page.
        /// </summary>
        /// <param name="sender"> The object sender. </param>
        /// <param name="e"> The <see cref="PageEventArgs"/>. </param>
        public void GetNewPage(object sender, PageEventArgs e)
        {
            this.Page = e.Page;
            this.SplitViewIndex = 0;
            this.TextViewIndex = 0;
            this.HexViewIndex = 0;
            this.LastViewSplitIndex = 0;
            this.LastViewHexIndex = 0;
            this.LastViewTextIndex = 0;
            this.LocalPage = 0;

            switch (this.CurrentView.Name)
            {
                case "HexView":
                    switch (this.CurrentView.Modifier)
                    {
                        case "Begin":
                            this.CountDownToLastHexPage();
                            break;
                        default:
                            this.GetCurrentHexPageView(this, EventArgs.Empty);
                            break;
                    }

                    break;
                case "SplitView":
                    switch (this.CurrentView.Modifier)
                    {
                        case "Begin":
                            this.CountToLastSplitPage();
                            break;
                        default:
                            this.GetCurrentSplitView(this, EventArgs.Empty);
                            break;
                    }

                    break;
                case "TextView":
                    switch (this.CurrentView.Modifier)
                    {
                        case "Begin":
                            this.CountDownToLastTextpage();
                            break;
                        default:
                            this.GetCurrentTextView(this, EventArgs.Empty);
                            break;
                    }

                    break;
                case "NullView":
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// This method jumps to the given position.
        /// </summary>
        /// <param name="sender"> The object sender. </param>
        /// <param name="e"> The <see cref="StringEventArgs"/>. </param>
        public void JumpToPosition(object sender, StringEventArgs e)
        {
            long position;
            
            try
            {
                position = Convert.ToInt64(e.Text, 16);
            }
            catch (Exception)
            {
                throw new ArgumentException("Error couldnt convert the position.");
            }

            this.CurrentView = new NullView();
            this.FireOnJumpToBegin();
            this.GetCurrentSplitView(this, EventArgs.Empty);
            while (true)
            {
                if (this.page.HexElements.LastOrDefault().HexSegment.LastOrDefault().Position >= position)
                {
                    if (this.shownPage.HexElements.LastOrDefault().HexSegment.LastOrDefault().Position >= position)
                    {
                        foreach (var element in this.shownPage.HexElements)
                        {
                            foreach (var hex in element.HexSegment)
                            {
                                if (hex.Position == position)
                                {
                                    this.CreateCurrentSplitViewShownPage();
                                    this.FireOnPositionFound(new PageAndPositionEventArgs(this.shownPage, position));
                                    return;
                                }
                            }
                        }
                    }

                    this.GetNextSplitView();
                }
                else
                {
                    this.SplitViewIndex = this.page.HexElements.Count;
                    this.GetNextSplitView();
                }

                string s = string.Empty;

                for (int i = 0; i < this.rnd.Next(0, 4); i++)
                {
                    s = s + ".";
                }

                this.FireOnMessagePrint(new StringEventArgs("Searching for: " + e.Text + s));
            }
        }

        /// <summary>
        /// This method saves the current page.
        /// </summary>
        /// <param name="sender"> The object sender. </param>
        /// <param name="e"> The <see cref="PageEventArgs"/>. </param>
        public void SavePage(object sender, PageEventArgs e)
        {
            for (int i = 0; i < this.page.HexElements.Count; i++)
            {
                for (int j = 0; j < e.Page.HexElements.Count; j++)
                {
                    if (this.page.HexElements[i].HexSegment.FirstOrDefault().Position == e.Page.HexElements[j].HexSegment.FirstOrDefault().Position)
                    {
                        this.page.HexElements[i] = e.Page.HexElements[j];
                    }
                }
            }

            this.FireOnPageSave(new PageEventArgs(this.page));
            this.GetCurrentHexPageView(this, EventArgs.Empty);
        }

        /// <summary>
        /// This method gets the current split view page.
        /// </summary>
        /// <param name="sender"> The object sender. </param>
        /// <param name="e"> The <see cref="EventArgs"/>. </param>
        public void GetCurrentSplitView(object sender, EventArgs e)
        {
            this.CreateCurrentSplitViewShownPage();

            this.CurrentView = new SplitView();
            this.FireOnTSplitViewPageCreated(new PageEventArgs(this.shownPage));
        }

        /// <summary>
        /// This method gets the current hex view page.
        /// </summary>
        /// <param name="sender"> The object sender. </param>
        /// <param name="e"> The <see cref="EventArgs"/>. </param>
        public void GetCurrentHexPageView(object sender, EventArgs e)
        {
            this.CreateCurrentHexViewShownPage();

            this.CurrentView = new HexView();
            this.FireOnHexPageCreated(new PageEventArgs(this.shownPage));
        }

        /// <summary>
        /// This method gets the current text view page.
        /// </summary>
        /// <param name="sender"> The object sender. </param>
        /// <param name="e"> The <see cref="EventArgs"/>. </param>
        public void GetCurrentTextView(object sender, EventArgs e)
        {
            this.CreateCurrentTextViewShownPage();

            this.CurrentView = new TextView();
            this.FireOnTextPageCreated(new PageEventArgs(this.shownPage));
        }

        /// <summary>
        /// This method fires the <see cref="OnHexPageCreated"/> event.
        /// </summary>
        /// <param name="e">A <see cref="PageEventArgs"/>. </param>> 
        protected virtual void FireOnHexPageCreated(PageEventArgs e)
        {
            this.OnHexPageCreated?.Invoke(this, e);
        }

        /// <summary>
        /// This method fires the <see cref="OnTextPageCreated"/> event.
        /// </summary>
        /// <param name="e">A <see cref="PageEventArgs"/>. </param>> 
        protected virtual void FireOnTextPageCreated(PageEventArgs e)
        {
            this.OnTextPageCreated?.Invoke(this, e);
        }

        /// <summary>
        /// This method fires the <see cref="OnTSplitViewPageCreated"/> event.
        /// </summary>
        /// <param name="e">A <see cref="PageEventArgs"/>. </param>> 
        protected virtual void FireOnTSplitViewPageCreated(PageEventArgs e)
        {
            this.OnTSplitViewPageCreated?.Invoke(this, e);
        }

        /// <summary>
        /// This method fires the <see cref="OnEndReached"/> event.
        /// </summary>
        protected virtual void FireOnEndReached()
        {
            this.OnEndReached?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// This method fires the <see cref="OnBeginReached"/> event.
        /// </summary>
        protected virtual void FireOnBeginReached()
        {
            this.OnBeginReached?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// This method fires the <see cref="OnPositionFound"/> event.
        /// </summary>
        /// <param name="e">A <see cref="PageAndPositionEventArgs"/>. </param>> 
        protected virtual void FireOnPositionFound(PageAndPositionEventArgs e)
        {
            this.OnPositionFound?.Invoke(this, e);
        }

        /// <summary>
        /// This method fires the <see cref="OnMessagePrint"/> event.
        /// </summary>
        /// <param name="e">A <see cref="StringEventArgs"/>. </param>> 
        protected virtual void FireOnMessagePrint(StringEventArgs e)
        {
            this.OnMessagePrint?.Invoke(this, e);
        }

        /// <summary>
        /// This method fires the <see cref="OnPageSave"/> event.
        /// </summary>
        /// <param name="e">A <see cref="PageEventArgs"/>. </param>> 
        protected virtual void FireOnPageSave(PageEventArgs e)
        {
            this.OnPageSave?.Invoke(this, e);
        }

        /// <summary>
        /// This method fires the <see cref="OnJumpToBegin"/> event.
        /// </summary>
        protected virtual void FireOnJumpToBegin()
        {
            this.OnJumpToBegin?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// This method counts to the last split view page.
        /// </summary>
        private void CountToLastSplitPage()
        {
            this.shownPage = new PageElement();

            while (this.SplitViewIndex < this.page.HexElements.Count)
            {
                this.LastViewSplitIndex = this.SplitViewIndex;
                this.CreateNextSplitViewShownPage();

                if (this.LastViewSplitIndex != 0)
                {
                    this.LocalPage++;
                }
            }

            this.CurrentView = new SplitView();
            this.FireOnTSplitViewPageCreated(new PageEventArgs(this.shownPage));
        }

        /// <summary>
        /// This method gets the next split view page.
        /// </summary>
        private void GetNextSplitView()
        {
            if (this.SplitViewIndex == this.page.HexElements.Count)
            {
                this.FireOnEndReached();
                return;
            }

            if (this.LastViewSplitIndex == 0)
            {
                this.LastViewSplitIndex = this.shownPage.HexElements.Count;
                this.SplitViewIndex = this.shownPage.HexElements.Count;
            }
            else
            {
                this.shownPage = new PageElement();
                this.LastViewSplitIndex = this.SplitViewIndex;
            }

            this.CreateNextSplitViewShownPage();
            this.LocalPage++;
            this.CurrentView = new SplitView();
            this.FireOnTSplitViewPageCreated(new PageEventArgs(this.shownPage));
        }

        /// <summary>
        /// This method creates the next split view page.
        /// </summary>
        private void CreateNextSplitViewShownPage()
        {
            int temp = 0;
            this.shownPage = new PageElement();

            foreach (var element in this.page.HexElements.Skip(this.LastViewSplitIndex))
            {
                if (temp + element.HexSegment.Count <= 464)
                {
                    this.shownPage.HexElements.Add(element);
                    temp = temp + element.HexSegment.Count;

                    this.SplitViewIndex++;
                }
                else
                {
                    break;
                }
            }
        }

        /// <summary>
        /// This method creates current split view page.
        /// </summary>
        private void CreateCurrentSplitViewShownPage()
        {
            int temp = 0;
            this.shownPage = new PageElement();

            foreach (var element in this.page.HexElements.Skip(this.LastViewSplitIndex))
            {
                if (temp + element.HexSegment.Count <= 464)
                {
                    this.shownPage.HexElements.Add(element);
                    temp = temp + element.HexSegment.Count;
                }
                else
                {
                    break;
                }
            }
        }

        /// <summary>
        /// This method gets the previous split view page.
        /// </summary>
        private void GetPreviousSplitView()
        {
            this.shownPage = new PageElement();

            if (this.LocalPage < 1)
            {
                this.CurrentView = new SplitView() { Modifier = "Begin" };
                this.FireOnBeginReached();
                return;
            }
            else
            {
                var tempPage = this.LocalPage - 1;
                this.LastViewSplitIndex = 0;
                this.SplitViewIndex = 0;
                this.LocalPage = 0;

                for (int i = 0; i <= tempPage; i++)
                {
                    this.LocalPage = i;
                    this.LastViewSplitIndex = this.SplitViewIndex;
                    this.CreateNextSplitViewShownPage();
                }

                this.CurrentView = new SplitView();
                this.FireOnTSplitViewPageCreated(new PageEventArgs(this.shownPage));
            }
        }

        /// <summary>
        /// This method creates the next text view page.
        /// </summary>
        private void CreateNextTextViewShownPage()
        {
            this.shownPage = new PageElement();

            int temp = 0;

            foreach (var element in this.page.HexElements.Skip(this.LastViewTextIndex))
            {
                if (temp <= 2900)
                {
                    this.shownPage.HexElements.Add(element);

                    if (element.TextSegment == '\u000D')
                    {
                        temp = temp + 100;
                    }
                    else
                    {
                        temp++;
                    }

                    this.TextViewIndex++;
                }
                else
                {
                    break;
                }
            }
        }

        /// <summary>
        /// This method creates current text view page.
        /// </summary>
        private void CreateCurrentTextViewShownPage()
        {
            int temp = 0;
            this.shownPage = new PageElement();

            foreach (var element in this.page.HexElements.Skip(this.LastViewTextIndex))
            {
                if (temp <= 2900)
                {
                    this.shownPage.HexElements.Add(element);

                    if (element.TextSegment == '\u000D')
                    {
                        temp = temp + 100;
                    }
                    else
                    {
                        temp++;
                    }
                }
                else
                {
                    break;
                }
            }
        }

        /// <summary>
        /// This method creates the next text view page.
        /// </summary>
        private void GetNextTextView()
        {
            if (this.TextViewIndex == this.page.HexElements.Count)
            {
                this.FireOnEndReached();
                return;
            }

            if (this.LastViewTextIndex == 0)
            {
                this.LastViewTextIndex = this.shownPage.HexElements.Count;
                this.TextViewIndex = this.shownPage.HexElements.Count;
            }
            else
            {
                this.LastViewTextIndex = this.TextViewIndex;
            }

            this.CreateNextTextViewShownPage();
            this.LocalPageText++;
            this.CurrentView = new TextView();
            this.FireOnTextPageCreated(new PageEventArgs(this.shownPage));
        }

        /// <summary>
        /// This method gets the previous text view page.
        /// </summary>
        private void GetPreviousTextView()
        {
            this.shownPage = new PageElement();

            if (this.LocalPageText < 1)
            {
                this.CurrentView = new TextView() { Modifier = "Begin" };
                this.FireOnBeginReached();
                return;
            }
            else
            {
                var tempPage = this.LocalPageText - 1;
                this.LastViewTextIndex = 0;
                this.TextViewIndex = 0;
                this.LocalPageText = 0;

                for (int i = 0; i <= tempPage; i++)
                {
                    this.LocalPageText = i;
                    this.LastViewTextIndex = this.TextViewIndex;

                    this.CreateNextTextViewShownPage();
                }

                this.CurrentView = new TextView();
                this.FireOnTextPageCreated(new PageEventArgs(this.shownPage));
            }
        }

        /// <summary>
        /// This method counts to the last text view page.
        /// </summary>
        private void CountDownToLastTextpage()
        {
            this.shownPage = new PageElement();

            while (this.TextViewIndex < this.page.HexElements.Count)
            {
                this.LastViewTextIndex = this.TextViewIndex;
                this.CreateNextTextViewShownPage();

                if (this.LastViewTextIndex != 0)
                {
                    this.LocalPageText++;
                }
            }

            this.CurrentView = new TextView();
            this.FireOnTextPageCreated(new PageEventArgs(this.shownPage));
        }

        /// <summary>
        /// This method creates the next hex view page.
        /// </summary>
        private void CreateNextHexViewShownPage()
        {
            int temp = 0;
            this.shownPage = new PageElement();

            foreach (var element in this.page.HexElements.Skip(this.LastViewHexIndex))
            {
                if (temp + element.HexSegment.Count <= 928)
                {
                    this.shownPage.HexElements.Add(element);
                    temp = temp + element.HexSegment.Count;
                    this.HexViewIndex++;
                }
                else
                {
                    break;
                }
            }
        }

        /// <summary>
        /// This method creates current hex view page.
        /// </summary>
        private void CreateCurrentHexViewShownPage()
        {
            int temp = 0;
            this.shownPage = new PageElement();

            foreach (var element in this.page.HexElements.Skip(this.LastViewHexIndex))
            {
                if (temp + element.HexSegment.Count <= 928)
                {
                    this.shownPage.HexElements.Add(element);
                    temp = temp + element.HexSegment.Count;
                }
                else
                {
                    break;
                }
            }
        }

        /// <summary>
        /// This method creates the next hex view page.
        /// </summary>
        private void GetNextHexPageView()
        {
            if (this.HexViewIndex == this.page.HexElements.Count)
            {
                this.FireOnEndReached();
                return;
            }

            if (this.LastViewHexIndex == 0)
            {
                this.LastViewHexIndex = this.shownPage.HexElements.Count;
                this.HexViewIndex = this.shownPage.HexElements.Count;
            }
            else
            {
                this.shownPage = new PageElement();
                this.LastViewHexIndex = this.HexViewIndex;
            }

            this.CreateNextHexViewShownPage();

            this.LocalPageHex++;
            this.CurrentView = new HexView();
            this.FireOnHexPageCreated(new PageEventArgs(this.shownPage));
        }

        /// <summary>
        /// This method gets the previous hex view page.
        /// </summary>
        private void GetPreviousHexPageView()
        {
            this.shownPage = new PageElement();

            if (this.LocalPageHex < 1)
            {
                this.CurrentView = new HexView() { Modifier = "Begin" };
                this.FireOnBeginReached();
                return;
            }
            else
            {
                var tempPage = this.LocalPageHex - 1;
                this.LastViewHexIndex = 0;
                this.HexViewIndex = 0;
                this.LocalPageHex = 0;

                for (int i = 0; i <= tempPage; i++)
                {
                    this.LocalPageHex = i;
                    this.LastViewHexIndex = this.HexViewIndex;

                    this.CreateNextHexViewShownPage();
                }

                this.CurrentView = new HexView();
                this.FireOnHexPageCreated(new PageEventArgs(this.shownPage));
            }
        }

        /// <summary>
        /// This method counts to the last hex view page.
        /// </summary>
        private void CountDownToLastHexPage()
        {
            this.shownPage = new PageElement();

            while (this.HexViewIndex < this.page.HexElements.Count)
            {
                this.LastViewHexIndex = this.HexViewIndex;

                this.CreateNextHexViewShownPage();

                if (this.LastViewHexIndex != 0)
                {
                    this.LocalPageHex++;
                }
            }

            this.CurrentView = new HexView();
            this.FireOnHexPageCreated(new PageEventArgs(this.shownPage));
        }
    }
}