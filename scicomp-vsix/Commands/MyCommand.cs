using Microsoft.VisualStudio.Text;

namespace intinc_vsix.Commands
{
    [Command(PackageIds.MyCommand)]
    internal sealed class MyCommand : BaseCommand<MyCommand>
    {
        protected override async Task ExecuteAsync(OleMenuCmdEventArgs e)
        {
            await Package.JoinableTaskFactory.SwitchToMainThreadAsync();
            DocumentView docView = await VS.Documents.GetActiveDocumentViewAsync();
            if (docView?.TextView == null) return;

            SnapshotPoint position = docView.TextView.Caret.Position.BufferPosition;
            var currentLineNumber = position.GetContainingLineNumber();
            if (currentLineNumber < 1) return;

            var currentLine = docView.TextBuffer.CurrentSnapshot.GetLineFromLineNumber(currentLineNumber);
            var previousLine = docView.TextBuffer.CurrentSnapshot.GetLineFromLineNumber(currentLineNumber-1);
            var column = currentLine.Start.Difference(position);

            var currentText = currentLine.GetText();
            var previousText = previousLine.GetText();  
            var previousNumberRange = Utility.GetDigits(previousText, column);
            var currentNumberRange = Utility.GetDigits(currentText, column);

            if (previousNumberRange == null || currentNumberRange == null) return;

            var previousNumber = Convert.ToInt64(previousText.Substring(previousNumberRange.start, previousNumberRange.length));
            var currentNumber = previousNumber + 1;
            docView.TextBuffer.Replace(new Span(currentLine.Start.Position + currentNumberRange.start,currentNumberRange.length),
                currentNumber.ToString());

        }

        
    }
}
