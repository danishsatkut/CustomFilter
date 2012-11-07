using System;
using System.Windows.Input;

namespace CustomFilteringForRadGridView.ViewModel
{
    public class ViewModelCommand : ICommand
    {
        private readonly Predicate<object> _canExecute;
        private readonly Action<object> _executeAction;

        /// <summary>
        /// Creates a ViewModelCommand instance with specified execute action
        /// and canexecute predicate.
        /// </summary>
        /// <param name="executeAction">Action that must be executed for the command.</param>
        /// <param name="canExecute">
        /// Optional predicate that must be checked for the command. If not specified action is always available.
        /// </param>
        public ViewModelCommand(Action<object> executeAction, Predicate<object> canExecute = null)
        {
            if (executeAction == null)
                throw new NullReferenceException("Action for executing command cannot be null");

            _executeAction = executeAction;
            _canExecute = canExecute;
        }

        /// <summary>
        /// Forces the raising of the <see cref="CanExecuteChanged"/> event.
        /// In the ViewModel, any code that the <see cref="CanExecute"/> method must call this method to raise the event.
        /// </summary>
        public void OnCanExecuteChanged()
        {
            if (CanExecuteChanged != null)
                CanExecuteChanged(this, EventArgs.Empty);
        }

        #region Implementation of ICommand

        /// <summary>
        /// Defines the method that determines whether the command can execute in its current state.
        /// </summary>
        /// <returns>
        /// true if this command can be executed; otherwise, false.
        /// </returns>
        /// <param name="parameter">Data used by the command. If the command does not require data to be passed, this object can be set to null. </param>
        public bool CanExecute(object parameter)
        {
            if (_canExecute == null)
                return true;

            return _canExecute(parameter);
        }

        /// <summary>
        /// Defines the method to be called when the command is invoked.
        /// </summary>
        /// <param name="parameter">Data used by the command. If the command does not require data to be passed, this object can be set to null. </param>
        public void Execute(object parameter)
        {
            _executeAction(parameter);
        }

        /// <summary>
        /// Event to be raised when CanExecute changes in the client.
        /// </summary>
        public event EventHandler CanExecuteChanged;

        #endregion
    }
}
