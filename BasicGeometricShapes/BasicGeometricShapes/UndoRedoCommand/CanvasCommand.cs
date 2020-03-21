using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace BasicGeometricShapes.UndoRedoCommand
{
    public class CanvasCommand : Command
    {
        public static Stack<List<UIElement>> UndoStack;
        public static Stack<List<UIElement>> RedoStack;
        private Canvas activeCanvas;
        public Canvas ActiveCanvas { get => activeCanvas; set => activeCanvas = value; }

        public CanvasCommand(Canvas drawTable)
        {
            UndoStack = new Stack<List<UIElement>>();
            RedoStack = new Stack<List<UIElement>>();
            activeCanvas = drawTable;
        }
        public override void Execute()
        {
            Undo();
        }

        public override void UnExecute()
        {
            Redo();
        }

        private void Undo()
        {
            if (UndoStack.Count > 0)
            {
                List<UIElement> currentElements = UndoStack.Pop();//izbaci trenutnu listu sa steka
                RedoStack.Push(currentElements);//prebaci je na Redo stek
                ActiveCanvas.Children.Clear();
                if (UndoStack.Count > 0)//posle popa opet provera
                {
                    AddToCanvas_Rearrange(UndoStack.Peek());//peekuje sledecu listu na steku i preslozi da bi redosled na kanvasu bio dobar
                }
            }
        }

        private void Redo()
        {
            if (RedoStack.Count > 0)
            {
                List<UIElement> currentElements = RedoStack.Pop();
                UndoStack.Push(currentElements);
                ActiveCanvas.Children.Clear();
                AddToCanvas_Rearrange(currentElements);
            }
        }

        private void AddToCanvas_Rearrange(List<UIElement> list)
        {
            Stack<UIElement> helpStack = new Stack<UIElement>();

            foreach (var item in list)
            {
                helpStack.Push(item);

            }
            var presolozi = helpStack.Count;
            for (int i = 0; i < presolozi; i++)
            {
                ActiveCanvas.Children.Add(helpStack.Pop());
            }

        }
    }
}
