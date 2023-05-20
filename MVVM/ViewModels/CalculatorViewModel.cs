using Calculator.MVVM.Logic;
using System.Windows.Input;
using PropertyChanged;
namespace Calculator.MVVM.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class CalculatorViewModel
    {
        public string Expression { get; set; } = "";
        public double Result { get; set; }
        public ICommand ClickCommand => new Command((entry) =>
        {
            Expression += entry ?? string.Empty;
        });
        public ICommand ClickOperatorCommand => new Command((entry) =>
        {
            if (entry == null) return;

            _ = char.TryParse(entry?.ToString(), out char Operator);

            if (!CalculatorLogic.IsOperator(Operator)) return;

            var lastChar = Expression.Length == 0 ? char.MinValue : Expression[^1];

            if (lastChar is char.MinValue && !CalculatorLogic.IsOperatorValidInStart(Operator)) return;

            if (CalculatorLogic.IsOperator(lastChar) && !CalculatorLogic.IsOperatorValidInEnd(lastChar))
            {
                Expression = Expression[..^1] + Operator;
                return;
            }

            Expression += Operator;
        });
        public ICommand ResultClickCommand => new Command(() =>
        {
            try
            {
                Result = CalculatorLogic.Calculate(Expression);
            }
            catch (OverflowException)
            {
                App.Current.MainPage.DisplayAlert("Error", "Valor demasiado grande", "Cerrar");
            }
            catch (Exception)
            {
                App.Current.MainPage.DisplayAlert("Error", "Expresión inválida", "Cerrar");
            }
        });
        public ICommand CleanClickCommand => new Command(() =>
        {
            Expression = string.Empty;
            Result = 0f;
        });
        public ICommand ClickUndoCommand => new Command(() => Expression = Expression[..^1]);
    }
}
