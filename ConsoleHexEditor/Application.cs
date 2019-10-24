//-----------------------------------------------------------------------
// <copyright file="Application.cs" company="FH Wiener Neustadt">
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
    using System.Threading.Tasks;

    /// <summary>
    /// The <see cref="Application"/> class.
    /// </summary>
    public class Application
    {
        /// <summary>
        /// Represents the input watcher.
        /// </summary>
        private readonly IInputWatcher inputWatcher;

        /// <summary>
        /// Represents the renderer.
        /// </summary>
        private readonly IRenderer renderer;

        /// <summary>
        /// Represents the page manager.
        /// </summary>
        private readonly PageManager pageManager;

        /// <summary>
        /// Represents the text editor.
        /// </summary>
        private readonly EditorForHexaElements editor;

        /// <summary>
        /// Represents the validator for the code page input.
        /// </summary>
        private readonly InputValidatorForChangingCodePage codePageValidator;

        /// <summary>
        /// Represents the validator for the exit menu input.
        /// </summary>
        private readonly InputValidatorForExitMenu exitMenuValidator;

        /// <summary>
        /// Represents the validator for the go to input.
        /// </summary>
        private readonly InputValidatorForGoTo gotoValidator;

        /// <summary>
        /// Represents the validator for the normal input.
        /// </summary>
        private readonly InputValidatorForNormalView normalValidator;

        /// <summary>
        /// Represents the validator for the partial loading input.
        /// </summary>
        private readonly InputValidatorForPartialLoading partialValidator;

        /// <summary>
        /// Represents the validator for the when no file is found input.
        /// </summary>
        private readonly InputValidatorForShortcuts shortcutValidator;

        /// <summary>
        /// Represents the validator for the text mode input.
        /// </summary>
        private readonly InputValidatorForTextMode textValidator;

        /// <summary>
        /// Represents the validator for the create file name input.
        /// </summary>
        private readonly UserInputCreator userInputCreator;

        /// <summary>
        /// Represents the validator for the file searching input.
        /// </summary>
        private readonly InputvalidatorForFileSearching fileSearchingValidator;

        /// <summary>
        /// Represents the validator for the go to input.
        /// </summary>
        private readonly GoToInputCreator gotoInput;

        /// <summary>
        /// Represents the validator for the partial loading input.
        /// </summary>
        private readonly PartialInputCreator partialInputCreator;

        /// <summary>
        /// Represents the text element creator.
        /// </summary>
        private TextElementCreator textElementCreator;

        /// <summary>
        /// Represents the file watcher.
        /// </summary>
        private FileWatcher fileWatcher;

        /// <summary>
        /// Represents the window watcher.
        /// </summary>
        private WindowWatcher windowWatcher;

        /// <summary>
        /// Represents the validator for the encoding chooser input.
        /// </summary>
        private EncodingChooser encodingChooser;

        /// <summary>
        /// Initializes a new instance of the <see cref="Application"/> class.
        /// </summary>
        /// <param name="appParser"> The command line parser. </param>
        /// <param name="renderer"> The renderer. </param>
        /// <param name="input"> The input watcher. </param>
        public Application(ApplicationParamsParser appParser, IRenderer renderer, IInputWatcher input)
        {
            Console.TreatControlCAsInput = true;

            this.renderer = renderer;
            this.inputWatcher = input;
            this.pageManager = new PageManager();
            this.editor = new EditorForHexaElements();
            this.codePageValidator = new InputValidatorForChangingCodePage();
            this.exitMenuValidator = new InputValidatorForExitMenu();
            this.gotoValidator = new InputValidatorForGoTo();
            this.normalValidator = new InputValidatorForNormalView();
            this.partialValidator = new InputValidatorForPartialLoading();
            this.shortcutValidator = new InputValidatorForShortcuts();
            this.textValidator = new InputValidatorForTextMode();
            this.encodingChooser = new EncodingChooser();
            this.userInputCreator = new UserInputCreator();
            this.fileSearchingValidator = new InputvalidatorForFileSearching();
            this.gotoInput = new GoToInputCreator();
            this.partialInputCreator = new PartialInputCreator();
            this.windowWatcher = new WindowWatcher(renderer.WindowHeight, renderer.WindowWith);
            this.windowWatcher.StartWindowWatcher();

            // No File Found
            this.shortcutValidator.OnEscapeButtonPressed += this.Exit;
            this.shortcutValidator.OnOpenNewFileButtonPressed += this.SubscribeToUserInputGetNewFile;

            // File searching
            this.fileSearchingValidator.OnDeletePressed += this.userInputCreator.DeleteLastCharacter;
            this.fileSearchingValidator.OnEnterPressed += this.userInputCreator.SendInput;
            this.fileSearchingValidator.OnEscapeKeyPressed += this.Exit;
            this.fileSearchingValidator.OnStringInput += this.userInputCreator.EnterCharacter;

            // The input from file searching
            this.userInputCreator.OnErrorMessagePrint += this.renderer.PrintErrorMessage;
            this.userInputCreator.OnUserInputDelete += this.renderer.DeleteUserInput;
            this.userInputCreator.OnUserInputPrint += this.renderer.PrintUserInput;
            this.userInputCreator.OnSendInput += this.CreateNewTextCreator;

            // The encoding input
            this.codePageValidator.OnEscapePressed += this.Exit;
            this.codePageValidator.OnKeyForAsciiPressed += this.encodingChooser.SetConsoleEncodingToASCII;
            this.codePageValidator.OnKeyForUTF8Pressed += this.encodingChooser.SetConsoleEncodingToUTF8;
            this.codePageValidator.OnKeyForWindowsPressed += this.encodingChooser.SetConsoleEncodingToWindows;
            this.codePageValidator.OnHexViewButtonPressed += this.ActivateHexView;
            this.codePageValidator.OnSplitViewButtonPressed += this.ActivateSplitView;
            this.codePageValidator.OnTextViewButtonPressed += this.ActivateTextView;
            this.codePageValidator.OnPartialLoadingButtonpressed += this.ActivateInputPartialLoading;
            this.codePageValidator.OnOpenNewFileButtonPressed += this.SubscribeToUserInputGetNewFile;
            this.codePageValidator.OnGoToButtonPressed += this.ActivateInputGoTo;
            this.encodingChooser.OnEncodingChoosen += this.renderer.ChangeEncoding;
            this.encodingChooser.OnEncodingChoosen += this.SubscribeFromEncodingToNormalInput;
            this.encodingChooser.OnMessagePrint += this.renderer.PrintMessage;
            this.encodingChooser.OnEncodingChoosen += this.renderer.ChangeEncoding;

            // The editor
            this.textValidator.OnEscapeButtonPressed += this.ActivateExitMenu;
            this.textValidator.OnEscapeButtonPressed += this.exitMenuValidator.PrintChoice;
            this.textValidator.OnHexKeyReceived += this.editor.ChangeHexAtCurrentPositionChanges;
            this.textValidator.OnKeyForNextPositionPressed += this.editor.JumpToNextPosition;
            this.textValidator.OnKeyForPreviousPositionPressed += this.editor.JumpToPreviousPosition;
            this.editor.OnPositionChanged += this.renderer.PrintPageToEdit;
            this.exitMenuValidator.OnMessagePrint += this.renderer.PrintMessage;
            this.exitMenuValidator.OnSaveButtonPressed += this.DeactivateFileWatcher;
            this.exitMenuValidator.OnSaveButtonPressed += this.editor.SaveChanges;
            this.exitMenuValidator.OnDontSaveButtonPressed += this.DontSavePage;
            this.editor.OnSaveChanges += this.SavePageFromEditor;
            this.editor.OnSaveChanges += this.pageManager.SavePage;

            // Go To
            this.gotoValidator.OnEscapePressed += this.Exit;
            this.gotoValidator.OnGoToPressed += this.gotoInput.SendInput;
            this.gotoValidator.OnDeletePressed += this.gotoInput.DeleteLastCharacter;
            this.gotoValidator.OnHexKeyPressed += this.gotoInput.EnterCharacter;
            this.gotoValidator.OnHexViewButtonPressed += this.ActivateHexView;
            this.gotoValidator.OnSplitViewButtonPressed += this.ActivateSplitView;
            this.gotoValidator.OnTextViewButtonPressed += this.ActivateTextView;
            this.gotoValidator.OnChangeCodePageButtonpressed += this.SubscribeToEncodingChooser;
            this.gotoValidator.OnOpenNewFileButtonPressed += this.SubscribeToUserInputGetNewFile;
            this.gotoValidator.OnPartialLoadingButtonpressed += this.ActivateInputPartialLoading;
            this.gotoInput.OnErrorMessagePrint += this.renderer.PrintErrorMessage;
            this.gotoInput.OnSendInput += this.UnsubscribePageManagerFromRenderer;
            this.gotoInput.OnSendInput += this.pageManager.JumpToPosition;
            this.gotoInput.OnUserInputDelete += this.renderer.DeleteUserInput;
            this.gotoInput.OnUserInputPrint += this.renderer.PrintUserInput;

            // PageManager
            this.pageManager.OnErrorMessagePrint += this.renderer.PrintErrorMessage;
            this.pageManager.OnHexPageCreated += this.renderer.RenderHexView;
            this.pageManager.OnMessagePrint += this.renderer.PrintMessage;
            this.pageManager.OnTSplitViewPageCreated += this.renderer.RenderSplitView;
            this.pageManager.OnTextPageCreated += this.renderer.RenderTextView;
            this.pageManager.OnPositionFound += this.renderer.PrintSplitViewAndColorPosition;
            this.pageManager.OnPositionFound += this.SubscribePageManagerFromRenderer;

            // Partial Validator
            this.partialValidator.OnEscapePressed += this.Exit;
            this.partialValidator.OnChangeCodePageButtonpressed += this.SubscribeToEncodingChooser;
            this.partialValidator.OnDeletePressed += this.partialInputCreator.DeleteLastCharacter;
            this.partialValidator.OnEnterPressed += this.partialInputCreator.SendInput;
            this.partialValidator.OnGoToButtonPressed += this.ActivateInputGoTo;
            this.partialValidator.OnNumberPressed += this.partialInputCreator.EnterCharacter;
            this.partialValidator.OnDeletePressed += this.partialInputCreator.DeleteLastCharacter;
            this.partialValidator.OnOpenNewFileButtonPressed += this.SubscribeToUserInputGetNewFile;
            this.partialValidator.OnHexViewButtonPressed += this.ActivateHexView;
            this.partialValidator.OnSplitViewButtonPressed += this.ActivateSplitView;
            this.partialValidator.OnTextViewButtonPressed += this.ActivateTextView;
            this.partialInputCreator.OnUserInputDelete += this.renderer.DeleteUserInput;
            this.partialInputCreator.OnUserInputPrint += this.renderer.PrintUserInput;
            this.partialInputCreator.OnErrorMessagePrint += this.renderer.PrintErrorMessage;
            this.partialInputCreator.OnMessagePrint += this.renderer.PrintMessage;

            // Normal validator
            this.normalValidator.OnEscapeButtonPressed += this.Exit;
            this.normalValidator.OnOpenNewFileButtonPressed += this.SubscribeToUserInputGetNewFile;
            this.normalValidator.OnChangeCodePageButtonpressed += this.SubscribeToEncodingChooser;
            this.normalValidator.OnHexViewButtonPressed += this.pageManager.GetCurrentHexPageView;
            this.normalValidator.OnSplitViewButtonPressed += this.pageManager.GetCurrentSplitView;
            this.normalValidator.OnTextViewButtonPressed += this.pageManager.GetCurrentTextView;
            this.normalValidator.OnPartialLoadingButtonpressed += this.ActivateInputPartialLoading;
            this.normalValidator.OnGoToButtonPressed += this.ActivateInputGoTo;
            this.normalValidator.OnEditButtonPressed += this.ActivateEditMode;

            if (appParser.IsFileNameSet == false)
            {
                this.ActivateNoFileInput();
            }
            else
            {
                this.renderer.PrintMessage(this, new StringEventArgs("Please wait while Page is loading."));

                try
                {
                    string correctedPath = FileHelper.ReturnAbsolutePath(appParser.FileName);
                    this.textElementCreator = new TextElementCreator(correctedPath);
                    this.fileWatcher = new FileWatcher(correctedPath);
                    this.fileWatcher.OnFileChanged += this.PrintFileErrorAndDeleteFile;
                    this.fileWatcher.Run();

                    this.ActivatePageManagerAndCreatorElements();
                }
                catch (Exception e)
                {
                    this.renderer.PrintErrorMessage(this, new StringEventArgs("Error couldnt open file!"));
                    this.ActivateNoFileInput();
                }
            }

            this.inputWatcher.Start();
        }

        /// <summary>
        /// This method starts the app.
        /// </summary>
        public void Start()
        {
            while (true)
            {
            }
        }

        /// <summary>
        /// This method activates the events for when a valid file has been read in.
        /// </summary>
        private void ActivatePageManagerAndCreatorElements()
        {
            // Textelement creator
            this.textElementCreator.OnErrorMessageCreated += this.renderer.PrintErrorMessage;
            this.textElementCreator.OnMaxLineCounted += this.renderer.ChangeLengthOfPositionValue;
            this.textElementCreator.OnMaxLineCounted += this.gotoInput.SetMaxBytes;
            this.textElementCreator.OnPageCreated += this.pageManager.GetNewPage;
            this.textElementCreator.OnMessageCreated += this.renderer.PrintMessage;
            this.pageManager.OnPageSave += this.textElementCreator.SaveChangesToBuffer;
            this.pageManager.OnJumpToBegin += this.textElementCreator.JumpToFirstPage;
            this.pageManager.OnBeginReached += this.textElementCreator.GetPreviousPagesOutOfBuffer;
            this.pageManager.OnEndReached += this.textElementCreator.GetNextPagesOutOfBuffer;
            this.normalValidator.OnGetNextPageOutOfBufferKey += this.pageManager.GetNextPage;
            this.normalValidator.OnGetPreviousPageOutOfBufferKey += this.pageManager.GetPreviousPage;
            this.partialInputCreator.OnSendInput += this.textElementCreator.ChangeBufferSizeOfReader;

            this.textElementCreator.CalculateMaxHexNumberLength();
            this.textElementCreator.CreateNextPageElement(this, EventArgs.Empty);
            this.pageManager.GetCurrentSplitView(this, EventArgs.Empty);
            this.ActivateNormalInput();
        }

        /// <summary>
        /// This method activates the input validator for the hex view.
        /// </summary>
        /// <param name="sender"> The object sender. </param>
        /// <param name="e"> The <see cref="EventArgs"/>. </param>
        private void ActivateHexView(object sender, EventArgs e)
        {
            this.inputWatcher.OnInputReceived -= this.codePageValidator.GetInput;
            this.inputWatcher.OnInputReceived -= this.gotoValidator.GetInput;
            this.inputWatcher.OnInputReceived -= this.partialValidator.GetInput;
            this.ActivateNormalInput();
            this.pageManager.GetCurrentHexPageView(this, EventArgs.Empty);
        }

        /// <summary>
        /// This method activates the input validator for the split view.
        /// </summary>
        /// <param name="sender"> The object sender. </param>
        /// <param name="e"> The <see cref="EventArgs"/>. </param>
        private void ActivateSplitView(object sender, EventArgs e)
        {
            this.inputWatcher.OnInputReceived -= this.codePageValidator.GetInput;
            this.inputWatcher.OnInputReceived -= this.gotoValidator.GetInput;
            this.inputWatcher.OnInputReceived -= this.partialValidator.GetInput;
            this.ActivateNormalInput();
            this.pageManager.GetCurrentSplitView(this, EventArgs.Empty);
        }

        /// <summary>
        /// This method activates the input validator for the text view.
        /// </summary>
        /// <param name="sender"> The object sender. </param>
        /// <param name="e"> The <see cref="EventArgs"/>. </param>
        private void ActivateTextView(object sender, EventArgs e)
        {
            this.inputWatcher.OnInputReceived -= this.codePageValidator.GetInput;
            this.inputWatcher.OnInputReceived -= this.gotoValidator.GetInput;
            this.inputWatcher.OnInputReceived -= this.partialValidator.GetInput;
            this.ActivateNormalInput();
            this.pageManager.GetCurrentTextView(this, EventArgs.Empty);
        }

        /// <summary>
        /// This method activates the input validator when the edited page has been saved.
        /// </summary>
        /// <param name="sender"> The object sender. </param>
        /// <param name="e"> The <see cref="EventArgs"/>. </param>
        private void SavePageFromEditor(object sender, PageEventArgs e)
        {
            this.inputWatcher.OnInputReceived -= this.exitMenuValidator.GetInput;
            this.ActivateNormalInput();
        }

        /// <summary>
        /// This method unsubscribes the <see cref="pageManager"/> from the renderer.
        /// </summary>
        /// <param name="sender"> The object sender. </param>
        /// <param name="e"> The <see cref="EventArgs"/>. </param>
        private void UnsubscribePageManagerFromRenderer(object sender, StringEventArgs e)
        {
            this.renderer.PrintMessage(this, new StringEventArgs("Please wait...."));
            this.pageManager.OnTSplitViewPageCreated -= this.renderer.RenderSplitView;
            this.inputWatcher.OnInputReceived -= this.gotoValidator.GetInput;
        }

        /// <summary>
        /// This method subscribes the <see cref="pageManager"/> to the renderer.
        /// </summary>
        /// <param name="sender"> The object sender. </param>
        /// <param name="e"> The <see cref="EventArgs"/>. </param>
        private void SubscribePageManagerFromRenderer(object sender, PageAndPositionEventArgs e)
        {
            this.pageManager.OnTSplitViewPageCreated += this.renderer.RenderSplitView;
            this.inputWatcher.OnInputReceived += this.normalValidator.GetInput;
        }

        /// <summary>
        /// This method is activated when the page should not be saved.
        /// </summary>
        /// <param name="sender"> The object sender. </param>
        /// <param name="e"> The <see cref="EventArgs"/>. </param>
        private void DontSavePage(object sender, EventArgs e)
        {
            this.inputWatcher.OnInputReceived -= this.exitMenuValidator.GetInput;
            this.ActivateNormalInput();
            this.pageManager.GetCurrentHexPageView(this, EventArgs.Empty);
        }

        /// <summary>
        /// This method activates the file manager.
        /// </summary>
        /// <param name="sender"> The object sender. </param>
        /// <param name="e"> The <see cref="EventArgs"/>. </param>
        private void ActivateFileManager(object sender, PageEventArgs e)
        {
            this.fileWatcher.OnFileChanged += this.PrintFileErrorAndDeleteFile;
            this.pageManager.OnHexPageCreated -= this.ActivateFileManager;
        }

        /// <summary>
        /// This method deactivates the file manager.
        /// </summary>
        /// <param name="sender"> The object sender. </param>
        /// <param name="e"> The <see cref="EventArgs"/>. </param>
        private void DeactivateFileWatcher(object sender, EventArgs e)
        {
            this.fileWatcher.OnFileChanged -= this.PrintFileErrorAndDeleteFile;
            this.pageManager.OnHexPageCreated += this.ActivateFileManager;
        }

        /// <summary>
        /// This method activates when the file has been changed or deleted.
        /// </summary>
        /// <param name="sender"> The object sender. </param>
        /// <param name="e"> The <see cref="EventArgs"/>. </param>
        private void PrintFileErrorAndDeleteFile(object sender, EventArgs e)
        {
            this.inputWatcher.OnInputReceived -= this.normalValidator.GetInput;
            this.inputWatcher.OnInputReceived -= this.codePageValidator.GetInput;
            this.inputWatcher.OnInputReceived -= this.gotoValidator.GetInput;
            this.inputWatcher.OnInputReceived -= this.partialValidator.GetInput;
            this.inputWatcher.OnInputReceived -= this.fileSearchingValidator.GetInput;
            this.inputWatcher.OnInputReceived -= this.exitMenuValidator.GetInput;
            this.inputWatcher.OnInputReceived -= this.textValidator.GetInput;
            this.renderer.PrintErrorMessage(this, new StringEventArgs("Error file has been moved or deleted. Please read the file again."));
            this.textElementCreator.DeleteBufferAndFile(this, EventArgs.Empty);
            this.inputWatcher.OnInputReceived += this.shortcutValidator.InterpretInput;
        }

        /// <summary>
        /// This method activates when a normal file has been loaded.
        /// </summary>
        private void ActivateNormalInput()
        {
            // When file is found 
            this.inputWatcher.OnInputReceived += this.normalValidator.GetInput;
        }

        /// <summary>
        /// This method activates the go to input.
        /// </summary>
        /// <param name="sender"> The object sender. </param>
        /// <param name="e"> The <see cref="EventArgs"/>. </param>
        private void ActivateInputGoTo(object sender, EventArgs e)
        {
            this.renderer.PrintMessage(this, new StringEventArgs("Please put in the offset in hexadecimal: "));
            this.renderer.PrintUserInput(this, new StringEventArgs(this.gotoInput.UserInput));
            this.inputWatcher.OnInputReceived -= this.normalValidator.GetInput;
            this.inputWatcher.OnInputReceived -= this.partialValidator.GetInput;
            this.inputWatcher.OnInputReceived -= this.codePageValidator.GetInput;

            this.inputWatcher.OnInputReceived += this.gotoValidator.GetInput;
        }

        /// <summary>
        /// This method activates the partial loading input.
        /// </summary>
        /// <param name="sender"> The object sender. </param>
        /// <param name="e"> The <see cref="EventArgs"/>. </param>
        private void ActivateInputPartialLoading(object sender, EventArgs e)
        {
            this.renderer.PrintMessage(this, new StringEventArgs("Please put in how many mb you want to read in(Allowed 1- 50): "));
            this.renderer.PrintUserInput(this, new StringEventArgs(this.partialInputCreator.UserInput));
            this.inputWatcher.OnInputReceived -= this.normalValidator.GetInput;
            this.inputWatcher.OnInputReceived -= this.codePageValidator.GetInput;
            this.inputWatcher.OnInputReceived -= this.gotoValidator.GetInput;

            this.inputWatcher.OnInputReceived += this.partialValidator.GetInput;
        }

        /// <summary>
        /// This method activates the edit mode.
        /// </summary>
        /// <param name="sender"> The object sender. </param>
        /// <param name="e"> The <see cref="EventArgs"/>. </param>
        private void ActivateEditMode(object sender, EventArgs e)
        {
            this.inputWatcher.OnInputReceived -= this.normalValidator.GetInput;
            var currentPage = this.pageManager.ShownPage;
            this.editor.SetPage(this, new PageEventArgs(currentPage));
            this.inputWatcher.OnInputReceived += this.textValidator.GetInput;
        }

        /// <summary>
        /// This method toggles the exit menu.
        /// </summary>
        /// <param name="sender"> The object sender. </param>
        /// <param name="e"> The <see cref="EventArgs"/>. </param>
        private void ActivateExitMenu(object sender, EventArgs e)
        {
            this.inputWatcher.OnInputReceived -= this.textValidator.GetInput;
            this.inputWatcher.OnInputReceived += this.exitMenuValidator.GetInput;
        }

        /// <summary>
        /// This method activates if no file has been put in.
        /// </summary>
        private void ActivateNoFileInput()
        {
            // When no file is found
            this.renderer.PrintMessage(this, new StringEventArgs("Please press ctrl+o to input a file."));
            this.inputWatcher.OnInputReceived += this.shortcutValidator.InterpretInput;
        }

        /// <summary>
        /// This method activates when the user added a new file.
        /// </summary>
        /// <param name="sender"> The object sender. </param>
        /// <param name="e"> The <see cref="EventArgs"/>. </param>
        private void SubscribeToUserInputGetNewFile(object sender, EventArgs e)
        {
            this.renderer.PrintMessage(this, new StringEventArgs("Please put in the file path: "));
            this.renderer.PrintUserInput(this, new StringEventArgs(this.userInputCreator.UserInput));

            // Unsubscribe from keyboard
            this.inputWatcher.OnInputReceived -= this.shortcutValidator.InterpretInput;
            this.inputWatcher.OnInputReceived -= this.normalValidator.GetInput;
            this.inputWatcher.OnInputReceived -= this.partialValidator.GetInput;
            this.inputWatcher.OnInputReceived -= this.gotoValidator.GetInput;
            this.inputWatcher.OnInputReceived -= this.codePageValidator.GetInput;

            // Subscribe to user input
            this.inputWatcher.OnInputReceived += this.fileSearchingValidator.GetInput;
        }

        /// <summary>
        /// This method creates a new <see cref="textElementCreator"/>. 
        /// </summary>
        /// <param name="sender"> The object sender. </param>
        /// <param name="e"> The <see cref="EventArgs"/>. </param>
        private void CreateNewTextCreator(object sender, StringEventArgs e)
        {
            this.inputWatcher.OnInputReceived -= this.fileSearchingValidator.GetInput;
            this.inputWatcher.OnInputReceived -= this.shortcutValidator.InterpretInput;
            this.renderer.PrintMessage(this, new StringEventArgs("Please wait while Page is loading."));

            try
            {
                string correctedPath = FileHelper.ReturnAbsolutePath(e.Text);
                this.textElementCreator = new TextElementCreator(correctedPath);
                this.fileWatcher = new FileWatcher(correctedPath);
                this.fileWatcher.OnFileChanged += this.PrintFileErrorAndDeleteFile;
                this.fileWatcher.Run();
                this.ActivatePageManagerAndCreatorElements();
            }
            catch (Exception exception)
            {
                this.renderer.PrintErrorMessage(this, new StringEventArgs("Error file couldnt be opened."));
                this.ActivateNoFileInput();
            }
        }

        /// <summary>
        /// This method activates the encoding chooser.
        /// </summary>
        /// <param name="sender"> The object sender. </param>
        /// <param name="e"> The <see cref="EventArgs"/>. </param>
        private void SubscribeToEncodingChooser(object sender, EventArgs e)
        {
            this.renderer.PrintMessage(this, new StringEventArgs("Press the number to choose an encoding\n1. UTF8\n2. ASCII\n3. Windows-1250"));

            // Unsubscribe from keyboard
            this.inputWatcher.OnInputReceived -= this.normalValidator.GetInput;
            this.inputWatcher.OnInputReceived -= this.partialValidator.GetInput;
            this.inputWatcher.OnInputReceived -= this.gotoValidator.GetInput;

            // New Subscribe to keyboard
            this.inputWatcher.OnInputReceived += this.codePageValidator.GetInput;
        }

        /// <summary>
        /// This method deactivates the encoding chooser.
        /// </summary>
        /// <param name="sender"> The object sender. </param>
        /// <param name="e"> The <see cref="EventArgs"/>. </param>
        private void SubscribeFromEncodingToNormalInput(object sender, CustomEncodingEventArgs e)
        {
            this.inputWatcher.OnInputReceived -= this.codePageValidator.GetInput;
            this.inputWatcher.OnInputReceived += this.normalValidator.GetInput;
        }

        /// <summary>
        /// This method is called when the user wants to exit the app.
        /// </summary>
        /// <param name="sender"> The object sender. </param>
        /// <param name="e"> The <see cref="EventArgs"/>. </param>
        private void Exit(object sender, EventArgs e)
        {
            TaskFactory ts = new TaskFactory();
            ts.StartNew(() => this.inputWatcher.Stop());
            ts.StartNew(() => this.windowWatcher.StopWindowWatcher());
            Environment.Exit(0);
        }
    }
}