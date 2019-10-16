using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DesignPattern.DesignPatterns
{
    public class Memento : Pattern
    {

        #region Originator
        public interface IEditor
        {
            IMemento Save();
        }

        public class Editor : IEditor
        {
            public string DisplayText { get; private set; }
            public IMemento Save()
            {
                return new EditorMemento(this) { DisplayText = this.DisplayText, PatchedDate = DateTime.Now };
            }
            public void SetState(IMemento state)
            {
                var recentVersion = state as EditorMemento;
                DisplayText = recentVersion.DisplayText;
            }

            public void InputText(string input)
            {
                DisplayText = $"{DisplayText}{input}";
            }
        }
        #endregion

        #region Memento
        public interface IMemento
        {
            DateTime CreatedDate { get; }
            void Restore();
        }

        public class EditorMemento : IMemento
        {
            private Editor originator;
            public string DisplayText { get; set; }
            public DateTime PatchedDate { get; set; }
            public EditorMemento(Editor originator) => this.originator = originator;
            public DateTime CreatedDate { get { return PatchedDate; } }
            public void Restore()
            {
                originator.SetState(this);
            }
        }
        #endregion

        #region Caretaker
        public class EditorCaretaker
        {
            public IList<IMemento> histories { get; set; }
            public EditorCaretaker()
            {
                histories = new List<IMemento>();
            }
            public void Undo()
            {
                var lastHistory = histories.LastOrDefault();
                if (lastHistory == null) return;
                lastHistory.Restore();
                histories.Remove(lastHistory);
            }
        }
        #endregion

        /// <summary>
        /// Problem: When we have to do something that can undo the object and get the recent state back
        /// Solved: Use Memento pattern to keep track of object history
        /// </summary>
        public override void Demo()
        {
            var caretaker = new EditorCaretaker();
            var editor = new Editor();
            Console.WriteLine("Input step1 text into editor ");
            editor.InputText("Step 1");
            Console.WriteLine("Input step2 text into editor ");
            editor.InputText("Step 2");

            DisplayEditorText(editor);
            Console.WriteLine();
            Console.WriteLine($"Now Save data");
            caretaker.histories.Add(editor.Save());

            Console.WriteLine("Input step3 text into editor ");
            editor.InputText("Step 3");
            DisplayEditorText(editor);

            Console.WriteLine();
            Console.WriteLine("Undo state in editor to previous state");
            caretaker.Undo();
            DisplayEditorText(editor);
        }

        private void DisplayEditorText(Editor editor)
        {
            Console.WriteLine($"Editor text is: {editor.DisplayText}");
        }

    }

}

